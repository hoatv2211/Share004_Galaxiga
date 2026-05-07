using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SkyGameKit
{
	public class LevelManager : SgkSingleton<LevelManager>
	{
		public int Score
		{
			get
			{
				return this._score;
			}
			set
			{
				int obj = value - this._score;
				this._score = value;
				if (this.OnScoreChange != null)
				{
					this.OnScoreChange(obj);
				}
			}
		}

		public LevelState State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
				if (this.OnStateChange != null)
				{
					this.OnStateChange();
				}
			}
		}

		public int CurrentSequenceWaveIndex { get; protected set; } = -1;

		public SequenceWave CurrentSequenceWave
		{
			get
			{
				if (this.CurrentSequenceWaveIndex >= 0 && this.CurrentSequenceWaveIndex < this.listSequenceWave.Count)
				{
					return this.listSequenceWave[this.CurrentSequenceWaveIndex];
				}
				return this.listSequenceWave[0];
			}
		}

		public Dictionary<string, int> EnemyKilled { get; protected set; } = new Dictionary<string, int>();

		public Dictionary<string, int> EnemySpawned { get; protected set; } = new Dictionary<string, int>();

		public float LastTimeEnemyDie { get; protected set; }

		public List<BaseEnemy> AliveEnemy { get; protected set; } = new List<BaseEnemy>();

		public List<SequenceWave> listSequenceWave { get; protected set; } = new List<SequenceWave>();

		public void AddSequenceWave(SequenceWave wave)
		{
			wave.ID = Const.GetSequenceWaveID(this.listSequenceWave.Count);
			this.listSequenceWave.Add(wave);
			this.passAllSequenceWave = false;
		}

		public List<PointWave> listPointWave { get; protected set; } = new List<PointWave>();

		public void AddPointWave(PointWave wave)
		{
			wave.ID = Const.GetPointWaveID(this.listPointWave.Count);
			this.listPointWave.Add(wave);
			this.listPointWave.Sort();
			this.passAllPointWave = false;
		}

		protected virtual void OnEnable()
		{
			if (SgkSingleton<LevelManager>._instance == null)
			{
				SgkSingleton<LevelManager>._instance = this;
			}
		}

		protected virtual void OnDisable()
		{
			if (SgkSingleton<LevelManager>._instance == this)
			{
				SgkSingleton<LevelManager>._instance = null;
			}
		}

		protected virtual IEnumerator Start()
		{
			PointWave.topCamera = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane)).y;
			this.listSequenceWave = base.GetComponentsInChildren<SequenceWave>().ToList<SequenceWave>();
			this.passAllSequenceWave = (this.listSequenceWave.Count == 0);
			for (int i = 0; i < this.listSequenceWave.Count; i++)
			{
				this.listSequenceWave[i].ID = Const.GetSequenceWaveID(i);
			}
			this.listPointWave = base.GetComponentsInChildren<PointWave>().ToList<PointWave>();
			this.passAllPointWave = (this.listPointWave.Count == 0);
			for (int j = 0; j < this.listPointWave.Count; j++)
			{
				this.listPointWave[j].ID = Const.GetPointWaveID(j);
			}
			this.listPointWave.Sort();
			yield return null;
			this.startLevelStream = this.Delay(this.startLevelDelay, new Action(this.StartLevel), false);
			yield break;
		}

		protected virtual void StartLevel()
		{
			if (this.startLevelStream != null)
			{
				this.startLevelStream.Dispose();
			}
			this.State = LevelState.Started;
			if (this.listSequenceWave.Count > 0)
			{
				this.NextSequenceWave(1);
			}
		}

		protected virtual void Update()
		{
			if (this.State == LevelState.PassAllWave)
			{
				if (this.AliveEnemy.Count == 0)
				{
					this.EndLevel(true);
				}
			}
			else if (this.State == LevelState.Started && this.passAllSequenceWave && this.passAllPointWave)
			{
				this.State = LevelState.PassAllWave;
			}
		}

		public virtual void NextSequenceWave(int n = 1)
		{
			SequenceWave currentSequenceWave = this.CurrentSequenceWave;
			this.CurrentSequenceWaveIndex += n;
			if (this.OnNextSequenceWave != null)
			{
				this.OnNextSequenceWave(currentSequenceWave);
			}
			if (this.CurrentSequenceWaveIndex < this.listSequenceWave.Count)
			{
				this.listSequenceWave[this.CurrentSequenceWaveIndex].StartWave();
			}
			else
			{
				this.passAllSequenceWave = true;
				if (!this.passAllPointWave)
				{
					string text = string.Empty;
					foreach (PointWave pointWave in this.listPointWave)
					{
						if (pointWave.State != WaveState.Stopped)
						{
							text = text + pointWave.name + ", ";
						}
					}
					SgkLog.Log("Point Wave Remain: " + text);
				}
			}
		}

		public virtual void GoToSequenceWave(int waveIndex)
		{
			SequenceWave currentSequenceWave = this.CurrentSequenceWave;
			this.CurrentSequenceWaveIndex = waveIndex % this.listSequenceWave.Count;
			if (this.OnNextSequenceWave != null)
			{
				this.OnNextSequenceWave(currentSequenceWave);
			}
			this.listSequenceWave[this.CurrentSequenceWaveIndex].StartWave();
		}

		public virtual void EndPointWave(PointWave pointWave)
		{
			foreach (PointWave pointWave2 in this.listPointWave)
			{
				if (pointWave2.State != WaveState.Stopped)
				{
					this.passAllPointWave = false;
					return;
				}
			}
			this.passAllPointWave = true;
		}

		public virtual void EndLevel(bool isVictory)
		{
			if (this.State != LevelState.Victory && this.State != LevelState.Defeat)
			{
				this.State = ((!isVictory) ? LevelState.Defeat : LevelState.Victory);
			}
		}

		public virtual void OnEnemySpawned(BaseEnemy enemy)
		{
			string id = enemy.id;
			if (this.EnemySpawned.ContainsKey(id))
			{
				Dictionary<string, int> enemySpawned;
				string key;
				(enemySpawned = this.EnemySpawned)[key = id] = enemySpawned[key] + 1;
			}
			else
			{
				this.EnemySpawned.Add(id, 1);
			}
			this.AliveEnemy.Add(enemy);
		}

		public virtual void OnEnemyDie(BaseEnemy enemy, EnemyKilledBy type)
		{
			string id = enemy.id;
			if (type == EnemyKilledBy.Player)
			{
				if (this.EnemyKilled.ContainsKey(id))
				{
					Dictionary<string, int> enemyKilled;
					string key;
					(enemyKilled = this.EnemyKilled)[key = id] = enemyKilled[key] + 1;
				}
				else
				{
					this.EnemyKilled.Add(id, 1);
				}
				this.LastTimeEnemyDie = Time.time;
				this.Score += enemy.score;
			}
			this.AliveEnemy.Remove(enemy);
		}

		public virtual void OnStartTurn(TurnManager turn)
		{
		}

		public virtual void OnEndTurn(TurnManager turn)
		{
		}

		private int _score;

		public Action<int> OnScoreChange;

		private LevelState _state;

		public Action OnStateChange;

		public Action<SequenceWave> OnNextSequenceWave;

		protected bool passAllSequenceWave;

		protected bool passAllPointWave;

		private IDisposable startLevelStream;

		public float startLevelDelay;
	}
}
