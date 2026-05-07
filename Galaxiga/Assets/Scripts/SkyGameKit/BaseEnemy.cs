using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SkyGameKit
{
	public class BaseEnemy : MonoBehaviour
	{
		public TurnManager MotherTurn { get; set; }

		public virtual EnemyState State { get; protected set; }

		[ShowInInspector]
		[DisplayAsString]
		public int LevelUID { get; set; }

		[ShowInInspector]
		[DisplayAsString]
		public virtual int Index { get; set; }

		[ShowInInspector]
		[DisplayAsString]
		public virtual int CurrentHP
		{
			get
			{
				return this._currentHP;
			}
			set
			{
				this._currentHP = value;
				if (this.State != EnemyState.InPool && this._currentHP <= 0)
				{
					this.Die(EnemyKilledBy.Player);
				}
			}
		}

		public virtual void ForceRemove()
		{
			this.State = EnemyState.Removed;
			if (this.timeout != null)
			{
				this.timeout();
			}
		}

		public virtual void Restart()
		{
			if (this.CurrentHP <= 0)
			{
				this.CurrentHP = ((this.startHP <= 0) ? 5 : this.startHP);
				if (this.startHP < 0)
				{
					SgkLog.LogError("Máu enemy <= 0, tự gán = 5 Turn: " + this.MotherTurn.name);
				}
			}
			this.State = EnemyState.Started;
			this.startTime = Time.time;
			SgkSingleton<LevelManager>.Instance.OnEnemySpawned(this);
			if (this.started != null)
			{
				this.started();
			}
			if (this.afterSecond != null)
			{
				this.afterSecond.ForEach(delegate(EnemyEventUnit<float> x)
				{
					this.Delay(x.param, new Action(x.Invoke), false);
				});
			}
		}

		public virtual void Die(EnemyKilledBy type = EnemyKilledBy.Player)
		{
			this.State = EnemyState.InPool;
			if (PoolManager.Pools["EnemyPool"].IsSpawned(base.transform))
			{
				if (this.MotherTurn != null)
				{
					this.MotherTurn.EnemyRemain--;
				}
				else
				{
					base.transform.parent.GetComponent<TurnManager>().EnemyRemain--;
				}
				if (type == EnemyKilledBy.Player)
				{
					Fu.SpawnExplosion(this.dieExplosion, base.transform.position, Quaternion.identity);
					if (this.killedByPlayer != null)
					{
						this.killedByPlayer();
					}
				}
				this.ClearData();
				PoolManager.Pools["EnemyPool"].Despawn(base.transform, Const.EnemyPoolTransform);
				SgkSingleton<LevelManager>.Instance.OnEnemyDie(this, type);
			}
		}

		protected virtual void ClearData()
		{
			foreach (FieldInfo fieldInfo in this.GetEvents())
			{
				Type fieldType = fieldInfo.FieldType;
				if (fieldType.Equals(typeof(EnemyEvent)))
				{
					fieldInfo.SetValue(this, null);
				}
				else if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(EnemyEvent<>))
				{
					object value = fieldInfo.GetValue(this);
					if (value != null)
					{
						fieldType.GetMethod("ClearEvent").Invoke(value, new object[0]);
					}
				}
			}
			this.CurrentHP = this.startHP;
		}

		public virtual Tweener MoveCurve(Vector3[] points, float duration, PathType pathType, PathMode pathMode, Ease ease)
		{
			return base.transform.DOPath(points, duration, pathType, pathMode, 10, null).SetEase(ease).SetLookAt(0f, null, null);
		}

		public virtual void SetField(List<EnemyFinalField> finalFields)
		{
			foreach (EnemyFinalField enemyFinalField in finalFields)
			{
				enemyFinalField.field.SetValue(this, enemyFinalField.value);
			}
		}

		public virtual void SetAction(Dictionary<FieldInfo, List<EnemyFinalEvent>> finalEvents)
		{
			foreach (KeyValuePair<FieldInfo, List<EnemyFinalEvent>> keyValuePair in finalEvents)
			{
				Type fieldType = keyValuePair.Key.FieldType;
				if (fieldType.Equals(typeof(EnemyEvent)))
				{
					this.AddMethodToEvent(keyValuePair.Value, keyValuePair.Key, this);
				}
				else if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(EnemyEvent<>))
				{
					this.AddMethodToParameterEvent(keyValuePair.Value, keyValuePair.Key);
				}
			}
		}

		private void AddMethodToParameterEvent(List<EnemyFinalEvent> eventInfos, FieldInfo eventTarget)
		{
			if (eventTarget.GetValue(this) == null)
			{
				SgkLog.ReflectionLogError("Event " + eventTarget.Name + " có tham số cần được khởi tạo trước trong code");
			}
			else
			{
				foreach (EnemyFinalEvent enemyFinalEvent in eventInfos)
				{
					eventTarget.FieldType.GetMethod("AddEvent").Invoke(eventTarget.GetValue(this), new object[]
					{
						enemyFinalEvent,
						this
					});
				}
			}
		}

		public void AddMethodToEvent(List<EnemyFinalEvent> enemyEvents, FieldInfo eventField, object eventTarget)
		{
			EnemyEvent enemyEvent2 = null;
			using (List<EnemyFinalEvent>.Enumerator enumerator = enemyEvents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EnemyFinalEvent enemyEvent = enumerator.Current;
					BaseEnemy _0024this = this;
					if (enemyEvent.FinalAction != null)
					{
						enemyEvent2 = (EnemyEvent)Delegate.Combine(enemyEvent2, new EnemyEvent(delegate()
						{
							enemyEvent.FinalAction(_0024this);
						}));
					}
					else
					{
						SgkLog.ReflectionLogError("Event " + eventField.Name + " không có action");
					}
				}
			}
			eventField.SetValue(eventTarget, enemyEvent2);
		}

		public virtual List<FieldInfo> GetFields()
		{
			List<FieldInfo> list = new List<FieldInfo>();
			foreach (FieldInfo fieldInfo in base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				foreach (object obj in fieldInfo.GetCustomAttributes(true))
				{
					if (obj is EnemyField)
					{
						list.Add(fieldInfo);
					}
				}
			}
			return list;
		}

		public virtual List<MethodInfo> GetMethods()
		{
			List<MethodInfo> list = new List<MethodInfo>();
			foreach (MethodInfo methodInfo in base.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
			{
				foreach (object obj in methodInfo.GetCustomAttributes(true))
				{
					if (obj is EnemyAction)
					{
						list.Add(methodInfo);
					}
				}
			}
			return list;
		}

		public virtual List<FieldInfo> GetEvents()
		{
			List<FieldInfo> list = new List<FieldInfo>();
			foreach (FieldInfo fieldInfo in base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				if (fieldInfo.FieldType == typeof(EnemyEvent))
				{
					list.Add(fieldInfo);
				}
				else if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition() == typeof(EnemyEvent<>))
				{
					list.Add(fieldInfo);
				}
			}
			return list;
		}

		[EnemyAction(displayName = "BaseEnemy/Print")]
		public virtual void BasePrint(string message)
		{
			MonoBehaviour.print(message);
		}

		[EnemyAction(displayName = "BaseEnemy/Teleport")]
		public virtual void BaseTeleport(Vector2 point)
		{
			base.transform.position = point;
		}

		[EnemyAction(displayName = "BaseEnemy/MoveStraight")]
		public virtual Tweener BaseMoveStraight(float speed, Ease ease, Vector2 direction)
		{
			return base.transform.DOMove(base.transform.position + (Vector3)direction, speed, false).SetEase(ease).SetSpeedBased<Tweener>();
		}

		[EnemyAction(displayName = "BaseEnemy/MoveCurve")]
		public virtual Tweener BaseMoveCurve(float speed, Ease ease, PathMode pathMode, Vector2[] points)
		{
			Vector3[] points2 = Array.ConvertAll<Vector2, Vector3>(points, (Vector2 x) => x);
			return this.MoveCurve(points2, speed, PathType.CatmullRom, pathMode, ease).SetSpeedBased<Tweener>();
		}

		[EnemyAction(displayName = "TurnManager/StartFreeTurn (Kéo thả)")]
		public virtual void StartFreeTurn(TurnManager turn)
		{
			if (turn.gameObject.activeInHierarchy)
			{
				FreeWave.StartTurn(turn);
			}
			else
			{
				SgkLog.LogError(turn.name + " không có trên Hierarchy");
			}
		}

		[EnemyAction(displayName = "TurnManager/CallFreeTurn (Điền tên)")]
		public virtual void CallFreeTurn(string turnName)
		{
			FreeWave.StartTurn(turnName);
		}

		[EnemyAction(displayName = "TurnManager/CallAllTurn (Điền tên turn trong Wave)")]
		public virtual void CallAllTurn(string turnName)
		{
			foreach (TurnManager turnManager in this.MotherTurn.MotherWave.InactiveTurns.Concat(this.MotherTurn.MotherWave.ActiveTurns))
			{
				if (turnManager.name == turnName)
				{
					turnManager.StartWithDelayAndDisplay();
				}
			}
		}

		[EnemyAction(displayName = "TurnManager/StartTurn (Kéo thả turn bất kỳ)")]
		public virtual void StartTurn(TurnManager turn)
		{
			turn.StartWithDelayAndDisplay();
		}

		public string id;

		public int score;

		public int startHP;

		public GameObject dieExplosion;

		[HideInInspector]
		public float startTime;

		[EnemyEventCustom(displayName = "BaseEnemy/Started")]
		public EnemyEvent started;

		[EnemyEventCustom(displayName = "BaseEnemy/KilledByPlayer")]
		public EnemyEvent killedByPlayer;

		[EnemyEventCustom(displayName = "BaseEnemy/Timeout")]
		public EnemyEvent timeout;

		[EnemyEventCustom(paramName = "Second", displayName = "BaseEnemy/AfterSecond")]
		public EnemyEvent<float> afterSecond = new EnemyEvent<float>();

		protected int _currentHP;
	}
}
