using System;
using System.Collections;
using System.Collections.Generic;

namespace SkyGameKit
{
	public class FreeWave : SgkSingleton<FreeWave>
	{
		protected override void Awake()
		{
			base.Awake();
			this.turns = base.GetComponentsInChildren<TurnManager>();
			for (int i = 0; i < this.turns.Length; i++)
			{
				TurnManager turnManager = this.turns[i];
				turnManager.ID = Const.GetTurnID(i, 0);
				if (!this.turnDic.ContainsKey(turnManager.name))
				{
					this.turnDic.Add(turnManager.name, new List<TurnManager>());
				}
				this.turnDic[turnManager.name].Add(turnManager);
			}
		}

		public static TurnManager GetTurn(string name)
		{
			if (SgkSingleton<FreeWave>.Instance == null)
			{
				return null;
			}
			foreach (TurnManager turnManager in SgkSingleton<FreeWave>.Instance.turnDic[name])
			{
				if (!turnManager.IsRunning)
				{
					return turnManager;
				}
			}
			return null;
		}

		public static TurnManager StartTurn(string name)
		{
			if (SgkSingleton<FreeWave>.Instance == null)
			{
				SgkLog.LogError("Không có FreeWave");
				return null;
			}
			if (SgkSingleton<FreeWave>.Instance.turnDic.ContainsKey(name))
			{
				foreach (TurnManager turnManager in SgkSingleton<FreeWave>.Instance.turnDic[name])
				{
					if (!turnManager.IsRunning)
					{
						return FreeWave.StartTurn(turnManager);
					}
				}
				SgkLog.LogError("Hết turn " + name + " cần tạo thêm turn mới");
				return null;
			}
			SgkLog.LogError("Không có turn " + name);
			return null;
		}

		public static TurnManager StartTurn(TurnManager turn)
		{
			if (SgkSingleton<FreeWave>.Instance == null)
			{
				SgkLog.LogError("Không có FreeWave");
				return null;
			}
			if (!turn.IsRunning)
			{
				SgkSingleton<FreeWave>.Instance.StartCoroutine(SgkSingleton<FreeWave>.Instance.CalculateTurnProperty(turn));
				return turn;
			}
			SgkLog.LogError(turn.name + " đang chạy rồi, không thể bắt đầu");
			return null;
		}

		public static void StopAll()
		{
			foreach (TurnManager turnManager in SgkSingleton<FreeWave>.Instance.turns)
			{
				if (turnManager.IsRunning)
				{
					turnManager.ForceStopAndKillAllEnemies();
				}
			}
		}

		public IEnumerator CalculateTurnProperty(TurnManager turn)
		{
			yield return turn.CalculateReflectionParam(true);
			turn.StartWithDelayAndDisplay();
			yield break;
		}

		protected Dictionary<string, List<TurnManager>> turnDic = new Dictionary<string, List<TurnManager>>();

		public TurnManager[] turns;
	}
}
