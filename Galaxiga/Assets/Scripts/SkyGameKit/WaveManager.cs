using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SkyGameKit
{
	public abstract class WaveManager : MonoBehaviour
	{
		[ShowInInspector]
		[DisplayAsString]
		public int ID { get; set; }

		[ShowInInspector]
		[DisplayAsString]
		public int TurnRemain
		{
			get
			{
				return this._turnRemain;
			}
			set
			{
				this._turnRemain = value;
				if (this._turnRemain == 0 && this.State == WaveState.Running)
				{
					this.EndWave();
				}
			}
		}

		public WaveState State { get; private set; }

		public List<TurnManager> ActiveTurns { get; protected set; } = new List<TurnManager>();

		public List<TurnManager> InactiveTurns { get; protected set; } = new List<TurnManager>();

		protected virtual void PrepareForStart()
		{
			TurnManager[] componentsInChildren = base.GetComponentsInChildren<TurnManager>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				TurnManager turnManager = componentsInChildren[i];
				turnManager.ID = Const.GetTurnID(i, this.ID);
				if (turnManager.timeToBegin < 0f)
				{
					this.InactiveTurns.Add(turnManager);
				}
				else
				{
					this.ActiveTurns.Add(turnManager);
				}
			}
			this.TurnRemain = this.ActiveTurns.Count;
		}

		public virtual void StartWave()
		{
			this.PrepareForStart();
			if (this.State == WaveState.Stopped || this.State == WaveState.NotStarted)
			{
				this.TurnRemain = this.ActiveTurns.Count;
				if (this.onStartWave != null)
				{
					this.onStartWave();
				}
				base.StartCoroutine(this.StartWaveCoroutine());
			}
		}

		protected virtual IEnumerator StartWaveCoroutine()
		{
			this.State = WaveState.Calculating;
			yield return this.CalculateTurnProperty();
			if (this.warningTime > 0f)
			{
				this.State = WaveState.Warning;
				yield return new WaitForSeconds(this.warningTime);
			}
			if (this.ActiveTurns.Count > 0)
			{
				this.State = WaveState.Running;
				foreach (TurnManager turnTMP in this.ActiveTurns)
				{
					if (this.startTurnOverMultipleFrames)
					{
						yield return null;
					}
					turnTMP.StartWithDelayAndDisplay();
				}
			}
			else
			{
				this.EndWave();
			}
			if (this.ID == 2000000 && GameContext.currentLevel == 1)
			{
				yield return new WaitForSeconds(2f);
				NewTutorial.current.UseSkillPlane_Step3();
			}
			if (this.ID == 1000000)
			{
				if (GameContext.currentLevel == 2 && GameContext.isBonusShield)
				{
					yield return new WaitForSeconds(2f);
					NewTutorial.current.BuyItems_Step10();
				}
				if (GameContext.currentLevel >= 5)
				{
					yield return new WaitForSeconds(2f);
					NewTutorial.current.EquipDrone_Step10();
				}
			}
			yield break;
		}

		public IEnumerator CalculateTurnProperty()
		{
			foreach (TurnManager turnTMP in this.ActiveTurns.Concat(this.InactiveTurns))
			{
				turnTMP.MotherWave = this;
				yield return turnTMP.CalculateReflectionParam(this.decodeOverMultipleFrames);
			}
			yield break;
		}

		public virtual void EndWave()
		{
			this.State = WaveState.Stopped;
			if (this.onEndWave != null)
			{
				this.onEndWave();
			}
		}

		public Action onStartWave;

		public Action onEndWave;

		private int _turnRemain = -1;

		public float warningTime;

		public int difficultyLevel;

		public bool decodeOverMultipleFrames = true;

		public bool startTurnOverMultipleFrames = true;
	}
}
