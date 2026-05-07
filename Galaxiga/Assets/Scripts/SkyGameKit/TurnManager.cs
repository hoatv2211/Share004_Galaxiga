using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SkyGameKit
{
	public abstract class TurnManager : MonoBehaviour
	{
		[ShowInInspector]
		[DisplayAsString]
		public int ID { get; set; }

		public WaveManager MotherWave { get; set; }

		public int TotalEnemy { get; private set; }

		[ShowInInspector]
		[DisplayAsString]
		public int EnemyRemain
		{
			get
			{
				return this._enemyRemain;
			}
			set
			{
				this._enemyRemain = value;
				if (value == 0 && this.IsRunning)
				{
					this.EndTurn();
				}
			}
		}

		public bool IsRunning { get; protected set; }

		public List<BaseEnemy> EnemyList { get; protected set; } = new List<BaseEnemy>();

		public virtual void StartWithDelayAndDisplay()
		{
			if (base.enabled)
			{
				if (this.timeToBegin > 0f)
				{
					this.Delay(this.timeToBegin, new Action(this.StartTurn), false);
				}
				else
				{
					this.StartTurn();
				}
			}
		}

		protected virtual void StartTurn()
		{
			if (this.IsRunning)
			{
				SgkLog.LogError("Turn: " + base.name + " đang chạy rồi, không gọi thêm nữa");
				return;
			}
			this.IsRunning = true;
			this.enemyIndex = 0;
			this.checkEndTurnByTime = this.Delay((this.timeToEnd <= 0f) ? -1f : this.timeToEnd, delegate
			{
				this.EndTurn();
			}, false);
			int num = this.Spawn();
			this.EnemyRemain = num;
			this.TotalEnemy = num;
			if (this.onStartTurn != null)
			{
				this.onStartTurn();
			}
			SgkSingleton<LevelManager>.Instance.OnStartTurn(this);
		}

		protected virtual void EndTurn()
		{
			if (this.IsRunning)
			{
				this.IsRunning = false;
				if (this.checkEndTurnByTime != null)
				{
					this.checkEndTurnByTime.Dispose();
				}
				if (this.EnemyRemain > 0)
				{
					for (int i = this.EnemyList.Count - 1; i >= 0; i--)
					{
						if (PoolManager.Pools["EnemyPool"].IsSpawned(this.EnemyList[i].transform) && this.EnemyList[i].State != EnemyState.Removed)
						{
							this.EnemyList[i].ForceRemove();
						}
					}
				}
				this.EnemyList.Clear();
				if (this.MotherWave != null && this.timeToBegin >= 0f)
				{
					this.MotherWave.TurnRemain--;
				}
				if (this.onEndTurn != null)
				{
					this.onEndTurn();
				}
				SgkSingleton<LevelManager>.Instance.OnEndTurn(this);
			}
		}

		protected virtual void InitEnemy(BaseEnemy enemy)
		{
			this.InitEnemy(enemy, this.finalFields, this.finalEvents);
		}

		protected virtual void InitEnemy(BaseEnemy enemy, List<EnemyFinalField> finalFields, Dictionary<FieldInfo, List<EnemyFinalEvent>> finalEvents)
		{
			enemy.Index = this.enemyIndex;
			enemy.LevelUID = Const.GetEnemyID(enemy.Index, this.ID);
			enemy.MotherTurn = this;
			if (this.BeforeSetFieldAndAction(enemy))
			{
				enemy.SetField(finalFields);
				enemy.SetAction(finalEvents);
			}
			this.EnemyList.Remove(enemy);
			this.EnemyList.Add(enemy);
			this.enemyIndex++;
			enemy.Restart();
		}

		protected virtual bool BeforeSetFieldAndAction(BaseEnemy enemy)
		{
			return true;
		}

		protected virtual BaseEnemy SpawnEnemy(BaseEnemy enemyPrefab)
		{
			Transform transform = PoolManager.Pools["EnemyPool"].Spawn(enemyPrefab.transform, base.transform.position, base.transform.rotation, base.transform);
			BaseEnemy component = transform.GetComponent<BaseEnemy>();
			this.InitEnemy(component);
			return component;
		}

		protected virtual BaseEnemy SpawnEnemy()
		{
			return this.SpawnEnemy(this.enemy);
		}

		protected abstract int Spawn();

		public List<BaseEnemy> GetAliveEnemy()
		{
			List<BaseEnemy> list = new List<BaseEnemy>();
			foreach (BaseEnemy baseEnemy in this.EnemyList)
			{
				if (baseEnemy.State != EnemyState.InPool)
				{
					list.Add(baseEnemy);
				}
			}
			return list;
		}

		public List<BaseEnemy> GetRandomAliveEnemy(int number)
		{
			return (from x in this.GetAliveEnemy()
			orderby Fu.RandomInt
			select x).Take(number).ToList<BaseEnemy>();
		}

		public virtual void KillAllEnemies(EnemyKilledBy reason)
		{
			if (this.EnemyRemain > 0)
			{
				for (int i = this.EnemyList.Count - 1; i >= 0; i--)
				{
					if (PoolManager.Pools["EnemyPool"].IsSpawned(this.EnemyList[i].transform) && this.EnemyList[i].State != EnemyState.Removed)
					{
						this.EnemyList[i].Die(reason);
					}
				}
			}
		}

		public virtual void ForceStopAndKillAllEnemies()
		{
			this.KillAllEnemies(EnemyKilledBy.Player);
			if (this.spawnStream != null)
			{
				this.spawnStream.Dispose();
			}
			this.EndTurn();
		}

		public bool IsCalculatedReflectionParam
		{
			[CompilerGenerated]
			get
			{
				return this.finalFields != null || this.finalFields != null;
			}
		}

		public virtual IEnumerator CalculateReflectionParam(bool runOverMultipleFrames = true)
		{
			if (this.IsCalculatedReflectionParam)
			{
				yield break;
			}
			this.finalFields = new List<EnemyFinalField>();
			this.finalEvents = new Dictionary<FieldInfo, List<EnemyFinalEvent>>();
			foreach (EnemyFieldInfo enemyFieldInfo in this.enemyFields)
			{
				if (!string.IsNullOrWhiteSpace(enemyFieldInfo.name))
				{
					FieldInfo field = this.enemy.GetType().GetField(enemyFieldInfo.name, BindingFlags.Instance | BindingFlags.Public);
					if (field == null)
					{
						SgkLog.ReflectionLogError("Không có Field: " + enemyFieldInfo.name + " Turn: " + base.name);
					}
					else
					{
						object obj = null;
						if (enemyFieldInfo.isUnityObject)
						{
							UnityEngine.Object objectValue = enemyFieldInfo.objectValue;
							if (((objectValue != null) ? new bool?(objectValue.Equals(null)) : null) == false)
							{
								obj = enemyFieldInfo.objectValue;
							}
						}
						else
						{
							obj = ReflectionUtils.Decode(field.FieldType, enemyFieldInfo.stringValue);
						}
						if (obj != null)
						{
							this.finalFields.Add(new EnemyFinalField
							{
								field = field,
								value = obj
							});
						}
						else
						{
							SgkLog.ReflectionLogError(field.Name + " đang để trống Turn: " + base.name);
						}
					}
				}
			}
			this.enemyFields.Clear();
			if (runOverMultipleFrames)
			{
				yield return null;
			}
			foreach (EnemyEventInfo enemyEvent in this.enemyEvents)
			{
				if (!string.IsNullOrWhiteSpace(enemyEvent.name))
				{
					FieldInfo foundEvent = this.enemy.GetType().GetField(enemyEvent.name, BindingFlags.Instance | BindingFlags.Public);
					if (foundEvent == null)
					{
						SgkLog.ReflectionLogError("Không có Event: " + enemyEvent.name + " Turn: " + base.name);
					}
					else if (enemyEvent.listAction.Count == 0)
					{
						SgkLog.ReflectionLogError("Event " + enemyEvent.name + " không có action nào cả");
					}
					else
					{
						EnemyFinalEvent finalEvent = default(EnemyFinalEvent);
						if (enemyEvent.hasParameter)
						{
							if (enemyEvent.parameter.isUnityObject)
							{
								finalEvent.param = enemyEvent.parameter.objectValue;
							}
							else if (foundEvent.FieldType.IsGenericType)
							{
								finalEvent.param = ReflectionUtils.Decode(foundEvent.FieldType.GetGenericArguments()[0], enemyEvent.parameter.stringValue);
							}
							if (finalEvent.param == null)
							{
								SgkLog.ReflectionLogError("Có lỗi khi decode tham số của Event: " + enemyEvent.name + " Turn: " + base.name);
								continue;
							}
						}
						foreach (EnemyActionInfo enemyActionInfo in enemyEvent.listAction)
						{
							if (!string.IsNullOrWhiteSpace(enemyActionInfo.name))
							{
								MethodInfo methodInfo = null;
								try
								{
									methodInfo = this.enemy.GetType().GetMethod(enemyActionInfo.name, BindingFlags.Instance | BindingFlags.Public);
								}
								catch (AmbiguousMatchException)
								{
									SgkLog.ReflectionLogError("Có nhiều hàm " + enemyActionInfo.name + " Turn: " + base.name);
									continue;
								}
								if (methodInfo == null)
								{
									SgkLog.ReflectionLogError("Không có Hàm " + enemyActionInfo.name + " Turn: " + base.name);
								}
								else
								{
									ParameterInfo[] parameters = methodInfo.GetParameters();
									if (parameters.Length > 0)
									{
										this.paramFinalValues = new object[parameters.Length];
										for (int i = 0; i < parameters.Length; i++)
										{
											int num = -1;
											for (int j = 0; j < enemyActionInfo.parameters.Count; j++)
											{
												if (parameters[i].Name == enemyActionInfo.parameters[j].name)
												{
													num = j;
													break;
												}
											}
											if (num < 0)
											{
												if (parameters[i].HasDefaultValue)
												{
													this.paramFinalValues[i] = parameters[i].DefaultValue;
												}
												else
												{
													this.paramFinalValues[i] = ReflectionUtils.GetDefault(parameters[i].ParameterType);
												}
											}
											else
											{
												EnemyFieldInfo enemyFieldInfo2 = enemyActionInfo.parameters[num];
												if (parameters[i].ParameterType.IsUnityObject())
												{
													this.paramFinalValues[i] = enemyFieldInfo2.objectValue;
												}
												else
												{
													this.paramFinalValues[i] = ReflectionUtils.Decode(parameters[i].ParameterType, enemyFieldInfo2.stringValue);
												}
												enemyActionInfo.parameters.RemoveAt(num);
											}
										}
									}
									else
									{
										this.paramFinalValues = null;
									}
									finalEvent.AddFinalActions(methodInfo, this.paramFinalValues);
								}
							}
						}
						if (this.finalEvents.ContainsKey(foundEvent))
						{
							this.finalEvents[foundEvent].Add(finalEvent);
						}
						else
						{
							this.finalEvents.Add(foundEvent, new List<EnemyFinalEvent>
							{
								finalEvent
							});
						}
						if (runOverMultipleFrames)
						{
							yield return null;
						}
					}
				}
			}
			this.enemyEvents.Clear();
			yield break;
		}

		[Tooltip("Thời gian bắt đầu tính từ khi bắt đầu wave")]
		public float timeToBegin;

		[Tooltip("Thời gian kết thúc tính từ khi bắt đầu turn")]
		public float timeToEnd;

		public BaseEnemy enemy;

		public List<EnemyFieldInfo> enemyFields = new List<EnemyFieldInfo>();

		public List<EnemyEventInfo> enemyEvents = new List<EnemyEventInfo>();

		private int _enemyRemain = -1;

		public Action onStartTurn;

		public Action onEndTurn;

		protected IDisposable checkEndTurnByTime;

		protected IDisposable spawnStream;

		protected int enemyIndex;

		public List<EnemyFinalField> finalFields;

		public Dictionary<FieldInfo, List<EnemyFinalEvent>> finalEvents;

		private object[] paramFinalValues;
	}
}
