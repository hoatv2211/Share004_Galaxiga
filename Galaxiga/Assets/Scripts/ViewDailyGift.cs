using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ViewDailyGift : MonoBehaviour
{
	private void Awake()
	{
		this.btnShowInfoGift.onClick.AddListener(new UnityAction(this.ShowInfoGift));
	}

	private void Start()
	{
		if (!this.dailyGiftManager.isNewMonth)
		{
			this.idGift = DailyGiftFirstMonthSheet.Get(this.day).id;
			this.valueGift = DailyGiftFirstMonthSheet.Get(this.day).value;
			this.vipX2 = DailyGiftFirstMonthSheet.Get(this.day).vip;
		}
		else
		{
			this.idGift = DailyGiftMonthAfterSheet.Get(this.day).id;
			this.valueGift = DailyGiftMonthAfterSheet.Get(this.day).value;
			this.vipX2 = DailyGiftMonthAfterSheet.Get(this.day).vip;
		}
		this.gift = (DailyGiftManager.Gift)this.idGift;
		if (this.vipX2 > 1)
		{
			this.valueGift *= 2;
		}
		this.SetView();
	}

	public void SetCanCollectGift()
	{
		this.canCollect = true;
		this.dailyGiftManager.SetPositionFXDailyGift(base.gameObject);
		this.dailyGiftManager.SetActionButtonCollect(new UnityAction(this.CollectGift));
	}

	public void SetActionGiftCollected()
	{
		this.dailyGiftManager.SetActionButtonCollect(new UnityAction(this.CollectGift));
	}

	public void SetViewGiftCollected()
	{
		this.isCollected = true;
		this.objGiftCollected.SetActive(true);
	}

	private void SetText()
	{
		this.textDay.text = "Day " + this.day;
		if (this.vipX2 > 1)
		{
			this.objVipX2.SetActive(true);
			this.textVipX2.text = "VIP" + this.vipX2 + " X2";
			this.textValueGift.text = "x" + this.valueGift / 2;
		}
		else
		{
			this.textValueGift.text = "x" + this.valueGift;
		}
		if (this.idGift == 7)
		{
			this.textPower510.gameObject.SetActive(true);
			this.textPower510.text = string.Empty + 5;
		}
		else if (this.idGift == 8)
		{
			this.textPower510.gameObject.SetActive(true);
			this.textPower510.text = string.Empty + 10;
		}
	}

	private void SetView()
	{
		this.SetText();
		switch (this.gift)
		{
		case DailyGiftManager.Gift.Gem:
			this.imgGift.sprite = this.dailyGiftManager.sprIconGem;
			break;
		case DailyGiftManager.Gift.Coin:
			this.imgGift.sprite = this.dailyGiftManager.sprIconCoin;
			break;
		case DailyGiftManager.Gift.Card_All_Plane:
			this.iconCard.SetActive(true);
			this.imgGift.sprite = this.dailyGiftManager.sprIconCardAllPlane;
			break;
		case DailyGiftManager.Gift.Card_All_Drone:
			this.iconCard.SetActive(true);
			this.imgGift.sprite = this.dailyGiftManager.sprIconCardAllDrone;
			break;
		case DailyGiftManager.Gift.Card_Random_Plane:
			this.iconCard.SetActive(true);
			this.imgGift.gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
			this.currentIdPlane = PlayerPrefs.GetInt("CurrentIdPlaneInDay" + this.day, -1);
			if (this.dailyGiftManager.isNewMonth || this.currentIdPlane == -1)
			{
				this.currentIdPlane = (int)DataGame.Current.RandomPlaneID;
				PlayerPrefs.SetInt("CurrentIdPlaneInDay" + this.day, this.currentIdPlane);
			}
			DataGame.Current.SetSpriteImagePlane(this.imgGift, (GameContext.Plane)this.currentIdPlane);
			break;
		case DailyGiftManager.Gift.Card_Random_Drone:
			this.imgGift.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
			this.iconCard.SetActive(true);
			this.currentIdDrone = PlayerPrefs.GetInt("CurrentIdDroneInDay" + this.day, -1);
			if (this.dailyGiftManager.isNewMonth || this.currentIdDrone == -1)
			{
				this.currentIdDrone = (int)DataGame.Current.RandomDroneID();
				PlayerPrefs.SetInt("CurrentIdDroneInDay" + this.day, this.currentIdDrone);
			}
			DataGame.Current.SetSpriteImageDrone(this.imgGift, (GameContext.Drone)this.currentIdDrone);
			break;
		case DailyGiftManager.Gift.Item_Heart:
			this.imgGift.sprite = this.dailyGiftManager.sprIconHeart;
			break;
		case DailyGiftManager.Gift.Item_Bullet_5:
		case DailyGiftManager.Gift.Item_Bullet_10:
			this.imgGift.sprite = this.dailyGiftManager.sprIconPower;
			break;
		case DailyGiftManager.Gift.Item_Shield:
			this.imgGift.sprite = this.dailyGiftManager.sprIconShield;
			break;
		case DailyGiftManager.Gift.Special_Skill:
			this.imgGift.sprite = this.dailyGiftManager.sprIconSkillPlane;
			break;
		case DailyGiftManager.Gift.Key_Super_Prize:
			this.imgGift.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
			this.imgGift.sprite = this.dailyGiftManager.sprKeySuperPrize;
			break;
		case DailyGiftManager.Gift.Plane:
			this.imgGift.gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
			DataGame.Current.SetSpriteImagePlane(this.imgGift, GameContext.Plane.FuryOfAres);
			this.objTextValue.SetActive(false);
			PlaneManager.current.CheckPlane(GameContext.Plane.FuryOfAres);
			PlaneManager.current.ShowPopupOwnedPlane(GameContext.Plane.FuryOfAres);
			break;
		case DailyGiftManager.Gift.Drone:
			this.imgGift.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
			DataGame.Current.SetSpriteImageDrone(this.imgGift, GameContext.Drone.AutoGatlingGun);
			DroneManager.current.CheckDrone(GameContext.Drone.AutoGatlingGun);
			this.objTextValue.SetActive(false);
			break;
		}
		if (!this.imgGift.gameObject.activeInHierarchy)
		{
			this.imgGift.gameObject.SetActive(true);
		}
		this.imgGift.SetNativeSize();
	}

	public void CollectGift()
	{
		if (!this.isCollected && this.canCollect)
		{
			this.Collect();
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_you_have_already_collected_the_reward);
		}
	}

	private void Collect()
	{
		this.isCollected = true;
		GameContext.canCollectDailyGift = false;
		this.objGiftCollected.SetActive(true);
		this.dailyGiftManager.HideFxDailyGift();
		this.dailyGiftManager.SetImageBtnCollectDeactive();
		SaveDataQuest.CollectReward(DailyQuestManager.Quest.daily_reward);
		DailyQuestManager.Current.SetBottomDailyReward();
		DailyQuestManager.Current.CheckShowNotification();
		CacheGame.CanCollectDailyGift = false;
		this.dailyGiftManager.AddDayCollectGift();
		switch (this.idGift)
		{
		case 0:
			CacheGame.AddGems(this.valueGift);
			CurrencyLog.LogGemIn(this.valueGift, CurrencyLog.In.DailyGift, "Day" + this.day);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, this.valueGift);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 1:
			CacheGame.AddCoins(this.valueGift);
			CurrencyLog.LogGoldIn(this.valueGift, CurrencyLog.In.DailyGift, "Day" + this.day);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, this.valueGift);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 2:
			CacheGame.AddCardAllPlane(this.valueGift);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_All_Plane, this.valueGift);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 3:
			CacheGame.AddCardAllDrone(this.valueGift);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_All_Drone, this.valueGift);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 4:
		{
			GameContext.Plane plane = (GameContext.Plane)this.currentIdPlane;
			DataGame.Current.AddCardPlane(plane, this.valueGift);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_Plane, this.valueGift, plane);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		}
		case 5:
		{
			GameContext.Drone drone = (GameContext.Drone)this.currentIdDrone;
			DataGame.Current.AddCardDrone(drone, this.valueGift);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_Plane, this.valueGift, drone);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		}
		case 6:
			GameContext.totalItemPowerUpHeart += this.valueGift;
			CacheGame.NumberItemPowerHeart = GameContext.totalItemPowerUpHeart;
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Heart, this.valueGift);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 7:
			GameContext.totalItemPowerUpBullet5 += this.valueGift;
			CacheGame.NumberItemPowerUpBullet5 = GameContext.totalItemPowerUpBullet5;
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Bullet_5, this.valueGift);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 8:
			GameContext.totalItemPowerUpBullet10 += this.valueGift;
			CacheGame.NumberItemPowerUpBullet10 = GameContext.totalItemPowerUpBullet10;
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Bullet_10, this.valueGift);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 9:
			GameContext.totalItemPowerShield += this.valueGift;
			CacheGame.NumberItemPowerShield = GameContext.totalItemPowerShield;
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Item_Shield, this.valueGift);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 10:
			CacheGame.AddSkillPlane(this.valueGift);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Special_Skill, this.valueGift);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		case 11:
		{
			int num = CacheGame.KeySuperRize;
			num += this.valueGift;
			CacheGame.KeySuperRize = num;
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Key_Super_Prize, this.valueGift);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			break;
		}
		case 12:
			CacheGame.GetPlane(GameContext.Plane.FuryOfAres);
			PlaneManager.current.ShowPopupOwnedPlane(GameContext.Plane.FuryOfAres);
			PlaneManager.current.CheckPlane(GameContext.Plane.FuryOfAres);
			break;
		case 13:
			CacheGame.SetOwnedDrone(GameContext.Drone.AutoGatlingGun);
			Notification.Current.ShowNotification(ScriptLocalization.notify_you_just_got_drone + " " + GameContext.Drone.AutoGatlingGun.ToString());
			ShowInfoPlaneDrone.Current.ShowInfoDrone(GameContext.Drone.AutoGatlingGun);
			DroneManager.current.CheckDrone(GameContext.Drone.AutoGatlingGun);
			break;
		}
		if (NewTutorial.current.currentStepTutorial_RewardDailyGift == 7)
		{
			NewTutorial.current.RewardDailyGift_Step7();
		}
	}

	private void ShowInfoGift()
	{
		switch (this.gift)
		{
		case DailyGiftManager.Gift.Gem:
		case DailyGiftManager.Gift.Coin:
		case DailyGiftManager.Gift.Card_All_Plane:
		case DailyGiftManager.Gift.Card_All_Drone:
		case DailyGiftManager.Gift.Item_Heart:
		case DailyGiftManager.Gift.Item_Bullet_5:
		case DailyGiftManager.Gift.Item_Bullet_10:
		case DailyGiftManager.Gift.Item_Shield:
		case DailyGiftManager.Gift.Special_Skill:
		case DailyGiftManager.Gift.Key_Super_Prize:
			InfoItemReward.current.ShowPopupInfoItem(this.gift, this.valueGift);
			break;
		case DailyGiftManager.Gift.Card_Random_Plane:
			InfoItemReward.current._planeID = (GameContext.Plane)this.currentIdPlane;
			InfoItemReward.current.ShowPopupInfoItem(this.gift, this.valueGift);
			break;
		case DailyGiftManager.Gift.Card_Random_Drone:
			InfoItemReward.current._droneID = (GameContext.Drone)this.currentIdDrone;
			InfoItemReward.current.ShowPopupInfoItem(this.gift, this.valueGift);
			break;
		case DailyGiftManager.Gift.Plane:
			ShowInfoPlaneDrone.Current.ShowInfoPlane(GameContext.Plane.FuryOfAres);
			break;
		case DailyGiftManager.Gift.Drone:
			ShowInfoPlaneDrone.Current.ShowInfoDrone(GameContext.Drone.AutoGatlingGun);
			break;
		}
	}

	public int day;

	public int idGift;

	public DailyGiftManager.Gift gift;

	public Text textDay;

	public Text textValueGift;

	public Text textPower510;

	public Text textVipX2;

	public Image imgGift;

	public Image imgPlaneDroneGift;

	public Image imgBG;

	public Image imgBgGift;

	public GameObject iconCard;

	public GameObject objGiftCollected;

	public GameObject objTextValue;

	public GameObject objVipX2;

	public DailyGiftManager dailyGiftManager;

	public Button btnShowInfoGift;

	private int valueGift;

	public bool isCollected;

	private bool canCollect;

	private int currentIdPlane;

	private int currentIdDrone;

	private int vipX2 = 1;
}
