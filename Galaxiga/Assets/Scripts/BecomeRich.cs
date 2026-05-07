using System;
using System.Collections;
using DG.Tweening;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.UI;

public class BecomeRich : MonoBehaviour
{
	private void Awake()
	{
		BecomeRich.current = this;
		this.numberWatchedVideo = CacheGame.NumberWatchVideoToBecomeRich;
	}

	private void Start()
	{
	}

	public void GetRandomReward()
	{
		if (this.numberWatchedVideo < 3)
		{
			this.randomNumberGem = UnityEngine.Random.Range(10, 20);
		}
		else
		{
			this.randomNumberGem = UnityEngine.Random.Range(2, 6);
		}
		this.textNumberGem.text = "x" + this.randomNumberGem;
		this.idReward = UnityEngine.Random.Range(0, this.arrRewards.Length);
		this.arrRewards[this.idReward].SetViewReward(true);
	}

	public void GetRandomReward2()
	{
		if (this.numberWatchedVideo < 3)
		{
			this.randomNumberGem = UnityEngine.Random.Range(10, 20);
		}
		else
		{
			this.randomNumberGem = UnityEngine.Random.Range(2, 6);
		}
		this.idReward = UnityEngine.Random.Range(0, this.arrRewards.Length);
		this.arrRewards[this.idReward].SetViewReward(false);
	}

	public void WatchVideoToBecomeRich()
	{
		GameContext.actionVideo = GameContext.ActionVideoAds.Get_Rich;
	}

	public void WatchVideoToBecomeRichComplete()
	{
		this.numberWatchedVideo++;
		CacheGame.NumberWatchVideoToBecomeRich = this.numberWatchedVideo;
		this.arrRewards[this.idReward].GetReward();
		CacheGame.AddGems(this.randomNumberGem);
		if (this.arrRewards[this.idReward].rewardView.rewardType == PrizeManager.RewardType.Coin)
		{
			CurrencyLog.LogGoldIn(this.arrRewards[this.idReward].numberRewards, CurrencyLog.In.WatchVideo, "Become_Rich");
		}
		CurrencyLog.LogGemIn(this.randomNumberGem, CurrencyLog.In.WatchVideo, "Become_Rich");
		this.HidePanelBecomeRich();
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, this.randomNumberGem);
		GetRewardSuccess.current.AddItemToView(this.arrRewards[this.idReward].rewardView.rewardType, this.arrRewards[this.idReward].numberRewards);
		GetRewardSuccess.current.ShowPanelGetRewardSuccess();
	}

	public void ShowPanelBecomeRich()
	{
		if (this.numberWatchedVideo < 5 && this.CanShowBemcomeRich())
		{
			this.GetRandomReward();
			base.StartCoroutine("CountDown");
			this.panelBecomeRich.SetActive(true);
			EscapeManager.Current.AddAction(new Action(this.HidePanelBecomeRich));
		}
	}

	public void HidePanelBecomeRich()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelBecomeRich));
		base.StopCoroutine("CountDown");
		DOTween.PlayBackwards("BECOME_RICH");
		base.StartCoroutine(GameContext.Delay(0.2f, delegate
		{
			this.panelBecomeRich.SetActive(false);
		}));
	}

	private IEnumerator CountDown()
	{
		while (this.timeLeft > 0)
		{
			yield return new WaitForSeconds(1f);
			this.timeLeft--;
			this.textTimeLeft.text = string.Empty + this.timeLeft;
			EazySoundManager.PlayUISound(AudioCache.UISound.tik_tok);
		}
		this.HidePanelBecomeRich();
		yield break;
	}

	public void GetReward()
	{
		this.arrRewards[this.idReward].GetReward();
		if (this.arrRewards[this.idReward].rewardView.rewardType == PrizeManager.RewardType.Coin)
		{
			CurrencyLog.LogGoldIn(this.arrRewards[this.idReward].numberRewards, CurrencyLog.In.WatchVideo, "Free_Gift");
		}
		CacheGame.AddGems(this.randomNumberGem);
		CurrencyLog.LogGemIn(this.randomNumberGem, CurrencyLog.In.WatchVideo, "Free_Gift");
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, this.randomNumberGem);
		GetRewardSuccess.current.AddItemToView(this.arrRewards[this.idReward].rewardView.rewardType, this.arrRewards[this.idReward].numberRewards);
		GetRewardSuccess.current.ShowPanelGetRewardSuccess();
	}

	public void HideReward()
	{
		this.arrRewards[this.idReward].HideReward();
	}

	private bool CanShowBemcomeRich()
	{
		if (BecomeRich.lastTimeShowBecomeRich.Year < 2019)
		{
			string text = CacheGame.LastTimeShowBecomeRich;
			if (text == string.Empty)
			{
				BecomeRich.lastTimeShowBecomeRich = DateTime.Now;
				CacheGame.LastTimeShowBecomeRich = BecomeRich.lastTimeShowBecomeRich.ToBinary().ToString();
				return true;
			}
			long dateData = Convert.ToInt64(text);
			BecomeRich.lastTimeShowBecomeRich = DateTime.FromBinary(dateData);
		}
		if (GameContext.GetTimeSpan(DateTime.Now, BecomeRich.lastTimeShowBecomeRich).TotalMinutes >= 15.0)
		{
			BecomeRich.lastTimeShowBecomeRich = DateTime.Now;
			CacheGame.LastTimeShowBecomeRich = BecomeRich.lastTimeShowBecomeRich.ToBinary().ToString();
			return true;
		}
		return false;
	}

	public static BecomeRich current;

	public GameObject panelBecomeRich;

	public RewardView rewardGem;

	public Text textNumberGem;

	public Text textTimeLeft;

	public BecomeRich.Reward[] arrRewards;

	public static DateTime lastTimeShowBecomeRich;

	private int timeLeft = 12;

	private int numberWatchedVideo;

	private int idReward;

	private int randomNumberGem;

	[Serializable]
	public class Reward
	{
		public void SetViewReward(bool needActiveObject)
		{
			if (needActiveObject)
			{
				this.rewardView.gameObject.SetActive(true);
			}
			switch (this.rewardView.rewardType)
			{
			case PrizeManager.RewardType.Card_All_Plane:
				this.numberRewards = UnityEngine.Random.Range(2, 4);
				break;
			case PrizeManager.RewardType.Card_All_Drone:
				this.numberRewards = UnityEngine.Random.Range(2, 4);
				break;
			case PrizeManager.RewardType.Coin:
				this.numberRewards = UnityEngine.Random.Range(1000, 3000);
				break;
			case PrizeManager.RewardType.Energy:
				this.numberRewards = UnityEngine.Random.Range(10, 15);
				break;
			}
			this.rewardView.numberReward = this.numberRewards;
			if (needActiveObject)
			{
				this.textNumberRewards.text = "x" + this.numberRewards;
			}
		}

		public void GetReward()
		{
			switch (this.rewardView.rewardType)
			{
			case PrizeManager.RewardType.Card_All_Plane:
				CacheGame.AddCardAllPlane(this.numberRewards);
				break;
			case PrizeManager.RewardType.Card_All_Drone:
				CacheGame.AddCardAllDrone(this.numberRewards);
				break;
			case PrizeManager.RewardType.Coin:
				CacheGame.AddCoins(this.numberRewards);
				break;
			case PrizeManager.RewardType.Energy:
				CacheGame.AddEnergy(this.numberRewards);
				break;
			}
		}

		public void HideReward()
		{
			this.rewardView.gameObject.SetActive(false);
		}

		public RewardView rewardView;

		public Text textNumberRewards;

		public int numberRewards;
	}
}
