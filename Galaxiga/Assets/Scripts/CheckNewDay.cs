using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CheckNewDay : MonoBehaviour
{
	public static CheckNewDay Current
	{
		get
		{
			if (CheckNewDay._ins == null)
			{
				CheckNewDay._ins = UnityEngine.Object.FindObjectOfType<CheckNewDay>();
			}
			return CheckNewDay._ins;
		}
	}

	private void Awake()
	{
		this.currentTime = DateTime.Now;
		DateTime dateTime = this.currentTime.AddDays(1.0);
		this.theNextDay = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
		this.InitOldDay();
	}

	private void OnEnable()
	{
		if (!GameContext.firstInstallGame)
		{
			this.CheckDayIsNew();
		}
		else
		{
			UnityEngine.Debug.Log("no set new day");
		}
	}

	public double CurrentSecondDayleft()
	{
		return GameContext.GetTimeSpan(this.theNextDay, DateTime.Now).TotalSeconds;
	}

	public void NewDay()
	{
		GameContext.isNewDay = true;
		if (!GameContext.firstInstallGame)
		{
			GameContext.isShowPack = false;
		}
		else
		{
			GameContext.isShowPack = true;
		}
		CacheGame.NumberPlayEndlessModeInDay = 0;
		CacheGame.IsOpenShopItemCoin = false;
		GameContext.isOpenShopItemCoin = false;
		CacheGame.NumberBuyEnergy1 = 0;
		CacheGame.NumberWatchVideoGetEnergy = 0;
		CacheGame.NumberResetShopCoinItem = 0;
		CacheGame.NumberWatchVideoToGetFreeGiftInDay = 0;
		CacheGame.TotalSecondCountDownFreeGift = 0;
		CacheGame.NumberWatchVideoToBecomeRich = 0;
		CacheGame.SetMaxLevelBoss(1);
		if (!GameContext.firstInstallGame)
		{
			SaveDataQuest.ResetDataQuest();
			SaveDataQuestEndless.ResetDataQuest();
		}
		if (!GameContext.firstInstallGame)
		{
			SaveDataStateItemShopCoin.InitData();
		}
		CacheGame.CanCollectDailyGift = true;
		CacheGame.CanCollectDailyGiftVIP = true;
		GameContext.canCollectDailyGift = true;
		GameContext.canCollectDailyGiftVIP = true;
		CacheGame.NumberWatchAdsCard = 5;
		CacheGame.NumberWatchAdsGemCoin = 5;
		CacheGame.NumberWatchAdsItemBooster = 5;
		int count = DataGame.Current.ListPlaneOwned.Count;
		for (int i = 0; i < count; i++)
		{
			CacheGame.SetNumberBackUpPlane(DataGame.Current.ListPlaneOwned[i], 3);
		}
		if (GameContext.isPurchasePremiumPack)
		{
			GameContext.numberFreeRespawnPlayer = 1;
		}
		GameContext.needShowPanelSale = true;
	}

	private void InitOldDay()
	{
		if (CacheGame.OldDay == string.Empty)
		{
			DateTime now = DateTime.Now;
			DateTime dateTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
			CacheGame.OldDay = dateTime.ToBinary().ToString();
		}
	}

	private void CheckDayIsNew()
	{
		if (GameContext.GetTimeSpan(this.currentTime, CacheGame.OldDay).TotalSeconds > 0.0)
		{
			this.NewDay();
			CacheGame.OldDay = this.theNextDay.ToBinary().ToString();
		}
	}

	public void StartCoroutineResetDay(Text textTimeDayleft)
	{
		this.resetDay = base.StartCoroutine(this.CoroutineResetDay(textTimeDayleft));
	}

	public void StopCoroutineResetDay()
	{
		if (this.resetDay != null)
		{
			base.StopCoroutine(this.resetDay);
		}
	}

	private IEnumerator CoroutineResetDay(Text textTimeDayleft)
	{
		double timerDayleft;
		for (timerDayleft = this.CurrentSecondDayleft(); timerDayleft > 0.0; timerDayleft -= 1.0)
		{
			string textFormat = TimeSpan.FromSeconds(timerDayleft).ToString("hh\\:mm\\:ss");
			textTimeDayleft.text = textFormat;
			yield return new WaitForSecondsRealtime(1f);
		}
		timerDayleft = 86400.0;
		this.NewDay();
		base.StartCoroutine("CoroutineResetDay");
		yield break;
	}

	private static CheckNewDay _ins;

	private DateTime currentTime;

	private DateTime lastTimeQuitGame;

	private DateTime theNextDay;

	private Coroutine resetDay;
}
