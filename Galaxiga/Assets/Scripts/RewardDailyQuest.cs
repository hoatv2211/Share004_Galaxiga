using System;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class RewardDailyQuest : MonoBehaviour
{
	private void Awake()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.targetQuest = DailyQuestSheet.Get((int)this.quest).targetQuest;
		this.typerReward = DailyQuestSheet.Get((int)this.quest).typeReward;
		this.valueReward = DailyQuestSheet.Get((int)this.quest).valueRewardQuest;
		this.isCollected = SaveDataQuest.IsCollectReward(this.quest);
	}

	private void SetValueRewardFollowVIP()
	{
		if (GameContext.currentVIP >= 11)
		{
			this.valueReward *= 2;
		}
	}

	private void Start()
	{
		this.SetValueRewardFollowVIP();
		this.CheckFreeEnergy();
	}

	public void ShowQuest()
	{
		this.SetView();
		this.CheckCollectReward();
		this.tweening.DORestart(false);
		this.tweening.DOPlay();
	}

	private void HideQuest()
	{
		this.tweeningHide.DOPlay();
	}

	public void SetView()
	{
		this.textNameQuest.text = DailyQuestSheet.Get((int)this.quest).nameQuest;
		if (this.quest != DailyQuestManager.Quest.daily_reward)
		{
			this.textValueRewardQuest.text = string.Empty + this.valueReward;
		}
		this.sliderQuest.maxValue = (float)this.targetQuest;
		string text = string.Empty;
		string text2 = string.Empty;
		switch (this.quest)
		{
		case DailyQuestManager.Quest.tv_trailer:
			text2 = ScriptLocalization.tv_trailer;
			text = ScriptLocalization.watch_a_short_video;
			break;
		case DailyQuestManager.Quest.investor:
			text2 = ScriptLocalization.investor;
			text = string.Format(ScriptLocalization.spend_coins, DailyQuestSheet.Get((int)this.quest).targetQuest);
			break;
		case DailyQuestManager.Quest.mechanic:
			text2 = ScriptLocalization.mechanic;
			text = string.Format(ScriptLocalization.upgrade_your_starship, DailyQuestSheet.Get((int)this.quest).targetQuest);
			break;
		case DailyQuestManager.Quest.upgrade_drone:
			text2 = ScriptLocalization.upgrade_drone;
			text = string.Format(ScriptLocalization.upgrade_your_drone, DailyQuestSheet.Get((int)this.quest).targetQuest);
			break;
		case DailyQuestManager.Quest.hunter_easy:
			text2 = ScriptLocalization.hunter;
			text = string.Format(ScriptLocalization.win_any_stage_in_easy_mode, DailyQuestSheet.Get((int)this.quest).targetQuest);
			break;
		case DailyQuestManager.Quest.hunter_normal:
			text2 = ScriptLocalization.hunter;
			text = string.Format(ScriptLocalization.win_any_stage_in_normal_mode, DailyQuestSheet.Get((int)this.quest).targetQuest);
			break;
		case DailyQuestManager.Quest.hunter_hard:
			text2 = ScriptLocalization.hunter;
			text = string.Format(ScriptLocalization.win_any_stage_in_hard_mode, DailyQuestSheet.Get((int)this.quest).targetQuest);
			break;
		case DailyQuestManager.Quest.coiner:
			text2 = ScriptLocalization.coiner;
			text = string.Format(ScriptLocalization.get_coins, DailyQuestSheet.Get((int)this.quest).targetQuest);
			break;
		case DailyQuestManager.Quest.free_energy1:
			text2 = ScriptLocalization.free_energy;
			text = ScriptLocalization.grab_free_energy_at + "\n12:00-15:00";
			break;
		case DailyQuestManager.Quest.free_energy2:
			text2 = ScriptLocalization.free_energy;
			text = ScriptLocalization.grab_free_energy_at + "\n9:00-22:00";
			break;
		case DailyQuestManager.Quest.im_rick:
			text2 = ScriptLocalization.im_rich;
			text = string.Format(ScriptLocalization.spend_gems, DailyQuestSheet.Get((int)this.quest).targetQuest);
			break;
		case DailyQuestManager.Quest.premium:
			text2 = ScriptLocalization.premium;
			text = string.Format(ScriptLocalization.free_get_gems, 30);
			break;
		case DailyQuestManager.Quest.daily_reward:
			text2 = ScriptLocalization.daily_rewards;
			text = ScriptLocalization.grab_free_rewards_every_day;
			break;
		}
		this.textNameQuest.text = text2;
		this.textDesQuest.text = text;
	}

	public void CollectReward()
	{
		if (this.quest == DailyQuestManager.Quest.daily_reward)
		{
			DailyGiftManager.Current.ShowPanelDailyGift();
		}
		else if (this.isCompleteQuest)
		{
			this.GetReward();
		}
		else
		{
			this.GoToQuest();
		}
		if (this.quest == DailyQuestManager.Quest.mechanic && NewTutorial.current.currentStepTutorial_RewardDailyGift == 3)
		{
			NewTutorial.current.RewardDailyGift_Step3();
		}
		if (this.quest == DailyQuestManager.Quest.daily_reward && NewTutorial.current.currentStepTutorial_RewardDailyGift == 5)
		{
			NewTutorial.current.RewardDailyGift_Step5();
		}
	}

	private void GetReward()
	{
		if (this.quest != DailyQuestManager.Quest.daily_reward)
		{
			this.btnCollect.SetActive(false);
		}
		if (this.typerReward == RewardDailyQuest.Reward.coin.ToString())
		{
			CacheGame.AddCoins(this.valueReward);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, this.valueReward);
			CurrencyLog.LogGoldIn(this.valueReward, CurrencyLog.In.DailyQuest, this.quest.ToString());
		}
		else if (this.typerReward == RewardDailyQuest.Reward.gem.ToString())
		{
			CacheGame.AddGems(this.valueReward);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, this.valueReward);
			CurrencyLog.LogGemIn(this.valueReward, CurrencyLog.In.DailyQuest, this.quest.ToString());
		}
		else
		{
			ShopEnergy.current.AddEnergy(this.valueReward);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Energy, this.valueReward);
		}
		GetRewardSuccess.current.ShowPanelGetRewardSuccess();
		if (this.quest == DailyQuestManager.Quest.premium)
		{
			WarningManager.Current.HideNotificationQuest();
		}
		SaveDataQuest.CollectReward(this.quest);
		DailyQuestManager.Current.GetRewardQuestComplete();
		this.HideQuest();
		base.StartCoroutine(GameContext.Delay(0.5f, delegate
		{
			base.gameObject.SetActive(false);
		}));
	}

	private void GoToQuest()
	{
		switch (this.quest)
		{
		case DailyQuestManager.Quest.tv_trailer:
			GameContext.actionVideo = GameContext.ActionVideoAds.Daily_Quest;
			break;
		case DailyQuestManager.Quest.investor:
		case DailyQuestManager.Quest.mechanic:
		case DailyQuestManager.Quest.im_rick:
			PlaneManager.current.ShowPanelSelectPlane();
			break;
		case DailyQuestManager.Quest.upgrade_drone:
			PlaneManager.current.ShowPanelSelectDrone();
			break;
		case DailyQuestManager.Quest.hunter_easy:
		case DailyQuestManager.Quest.hunter_normal:
		case DailyQuestManager.Quest.hunter_hard:
		case DailyQuestManager.Quest.coiner:
			HomeManager.current.Campaign();
			break;
		case DailyQuestManager.Quest.free_energy1:
		case DailyQuestManager.Quest.free_energy2:
			Notification.Current.ShowNotification(ScriptLocalization.log_in_during);
			break;
		}
	}

	public void SetBottomDailyReward()
	{
		this.rectTransform.SetAsLastSibling();
		DailyQuestManager.Current.SetObjTemp();
	}

	public void CheckCollectReward()
	{
		if (this.isCollected)
		{
			if (this.quest == DailyQuestManager.Quest.daily_reward)
			{
				this.textBtnCollect.text = ScriptLocalization.open;
				this.SetBottomDailyReward();
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			this.CheckCompleteQuest();
		}
	}

	public void CheckCompleteQuest()
	{
		this.currentProgessQuest = SaveDataQuest.dataQuestContainer.dataQuests[(int)this.quest].processQuest;
		if (this.quest == DailyQuestManager.Quest.daily_reward)
		{
			Slider slider = this.sliderQuest;
			float num = 1f;
			this.sliderQuest.maxValue = num;
			slider.value = num;
			this.textBtnCollect.text = ScriptLocalization.go;
		}
		else if (this.quest == DailyQuestManager.Quest.premium)
		{
			if (GameContext.isPurchasePremiumPack)
			{
				Slider slider2 = this.sliderQuest;
				float num = 1f;
				this.sliderQuest.maxValue = num;
				slider2.value = num;
				this.isCompleteQuest = true;
				this.textBtnCollect.text = ScriptLocalization.collect;
				DailyQuestManager.Current.CompleteQuest();
			}
		}
		else
		{
			this.CheckQuestIsComplete(this.quest, this.currentProgessQuest);
		}
	}

	public void CheckQuestIsComplete(DailyQuestManager.Quest quest, int currentProgress)
	{
		if (!this.isCompleteQuest && !this.isCollected)
		{
			this.sliderQuest.value = (float)currentProgress;
			if (currentProgress >= this.targetQuest)
			{
				this.imgBtnCollect.sprite = this.sprBtnComplete;
				this.textSliderQuest.text = ScriptLocalization.complete;
				this.textBtnCollect.text = ScriptLocalization.collect;
				this.isCompleteQuest = true;
				this.rectTransform.SetAsFirstSibling();
				DailyQuestManager.Current.CompleteQuest();
				if (quest == DailyQuestManager.Quest.daily_reward)
				{
					this.SetBottomDailyReward();
				}
				if (quest == DailyQuestManager.Quest.tv_trailer)
				{
					WarningManager.Current.HideNotificationTvTrailer();
				}
			}
			else
			{
				if (quest == DailyQuestManager.Quest.tv_trailer)
				{
					this.textBtnCollect.text = ScriptLocalization.watch;
				}
				else
				{
					this.textBtnCollect.text = ScriptLocalization.go;
				}
				this.textSliderQuest.text = currentProgress + "/" + this.targetQuest;
			}
		}
	}

	private void CheckFreeEnergy()
	{
		if (this.isCollected)
		{
			return;
		}
		if (this.quest == DailyQuestManager.Quest.free_energy1)
		{
			int hour = DateTime.Now.Hour;
			if (hour >= 12 && hour < 15)
			{
				SaveDataQuest.SetProcessQuest(this.quest, 1);
			}
			else if (hour >= 15)
			{
				this.isCollected = true;
			}
		}
		else if (this.quest == DailyQuestManager.Quest.free_energy2)
		{
			int hour2 = DateTime.Now.Hour;
			if (hour2 >= 19 && hour2 < 22)
			{
				SaveDataQuest.SetProcessQuest(this.quest, 1);
			}
			else if (hour2 >= 22)
			{
				this.isCollected = true;
			}
		}
	}

	public DailyQuestManager.Quest quest;

	public Text textNameQuest;

	public Text textDesQuest;

	public Text textValueRewardQuest;

	public Text textBtnCollect;

	public Text textSliderQuest;

	public Slider sliderQuest;

	public DOTweenAnimation tweening;

	public DOTweenAnimation tweeningHide;

	public Sprite sprBtnComplete;

	public Image imgBtnCollect;

	public GameObject btnCollect;

	private int currentProgessQuest;

	private RectTransform rectTransform;

	private int targetQuest;

	private bool isCompleteQuest;

	private string typerReward;

	private int valueReward;

	private bool isCollected;

	private enum Reward
	{
		gem,
		coin,
		energy
	}
}
