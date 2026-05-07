using System;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class InfoItemReward : MonoBehaviour
{
	public static InfoItemReward current
	{
		get
		{
			if (InfoItemReward._ins == null)
			{
				InfoItemReward._ins = UnityEngine.Object.FindObjectOfType<InfoItemReward>();
			}
			return InfoItemReward._ins;
		}
	}

	public void ShowPopupInfoItem(PrizeManager.RewardType rewardType, int numberItem)
	{
		this.isOpenPopupCardPlaneDrone = false;
		this.textNumberItem.text = "x" + numberItem;
		this.indexItem = (int)rewardType;
		switch (rewardType)
		{
		case PrizeManager.RewardType.Card_All_Plane:
			this.textNameItem.text = ScriptLocalization.ultra_starship_card;
			this.textDescriptionItem.text = ScriptLocalization.ultra_starship_card_can_use_for_any;
			this.stock.text = ScriptLocalization.your_stock + " " + GameContext.totalUltraStarshipCard;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Card_All_Drone:
			this.textNameItem.text = ScriptLocalization.ultra_drone_card;
			this.textDescriptionItem.text = ScriptLocalization.ultra_drone_card_can_use_for;
			this.stock.text = ScriptLocalization.your_stock + " " + GameContext.totalUltraDroneCard;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Card_Random_Plane:
			this.textNameItem.text = ScriptLocalization.random_starship_card;
			this.textDescriptionItem.text = ScriptLocalization.contain_random_starship_cards;
			this.stock.gameObject.SetActive(false);
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Card_Random_Drone:
			this.textNameItem.text = ScriptLocalization.random_drone_card;
			this.textDescriptionItem.text = ScriptLocalization.contain_random_drone_cards;
			this.stock.gameObject.SetActive(false);
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Gem:
			this.textNameItem.text = ScriptLocalization.gem;
			this.textDescriptionItem.text = ScriptLocalization.the_primary_currency_in_game;
			this.stock.text = ScriptLocalization.your_stock + " " + GameUtil.FormatNumber(ShopContext.currentGem);
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Coin:
			this.textNameItem.text = ScriptLocalization.coin;
			this.textDescriptionItem.text = ScriptLocalization.the_primary_currency_in_game;
			this.stock.text = ScriptLocalization.your_stock + " " + GameUtil.FormatNumber(ShopContext.currentCoin);
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Energy:
			this.textNameItem.text = ScriptLocalization.energy;
			this.textDescriptionItem.text = ScriptLocalization.energy_charge;
			this.stock.text = ScriptLocalization.your_stock + " " + GameContext.totalEnergy;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Key_Super_Prize:
			this.textNameItem.text = ScriptLocalization.super_prize_key;
			this.textDescriptionItem.text = ScriptLocalization.collect_key_to_get_super_bonus;
			this.stock.text = ScriptLocalization.your_stock + " " + GameContext.totalKeySuperPrize;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Item_Heart:
			this.textNameItem.text = ScriptLocalization.heart;
			this.textDescriptionItem.text = "+1 " + ScriptLocalization.life;
			this.stock.text = ScriptLocalization.your_stock + " " + GameContext.totalItemPowerUpHeart;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Item_Bullet_10:
			this.textNameItem.text = ScriptLocalization.power_10;
			this.textDescriptionItem.text = ScriptLocalization.upgrade_power_to_10;
			this.stock.text = ScriptLocalization.your_stock + " " + GameContext.totalItemPowerUpBullet10;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Item_Bullet_5:
			this.textNameItem.text = ScriptLocalization.power_5;
			this.textDescriptionItem.text = ScriptLocalization.upgrade_power_to_5;
			this.stock.text = ScriptLocalization.your_stock + " " + GameContext.totalItemPowerUpBullet5;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Item_Shield:
			this.textNameItem.text = ScriptLocalization.shield;
			this.textDescriptionItem.text = ScriptLocalization.a_force_shield_protect_the_starship;
			this.stock.text = ScriptLocalization.your_stock + " " + GameContext.totalItemPowerShield;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Special_Skill:
			this.textNameItem.text = ScriptLocalization.special_skill;
			this.textDescriptionItem.text = ScriptLocalization.use_to_active_special_skill;
			this.stock.text = ScriptLocalization.your_stock + " " + GameContext.totalSkillPlane;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Box:
			this.textNameItem.text = ScriptLocalization.reward_box;
			this.textDescriptionItem.text = ScriptLocalization.contain_random_item_reward_or_money;
			this.stock.gameObject.SetActive(false);
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Card_Plane:
			this.objCard.SetActive(true);
			this.isOpenPopupCardPlaneDrone = true;
			this.ShowPopupInfoCardPlane(this._planeID, numberItem);
			break;
		case PrizeManager.RewardType.Card_Drone:
			this.objCard.SetActive(true);
			this.isOpenPopupCardPlaneDrone = true;
			this.ShowPopupInfoCardDrone(this._droneID, numberItem);
			break;
		case PrizeManager.RewardType.Unlock_Plane:
			this.isOpenPopupCardPlaneDrone = true;
			this.objCard.SetActive(false);
			this.ShowPopupInfoCardPlane(this._planeID, 0);
			break;
		case PrizeManager.RewardType.Blue_Star:
			this.textNameItem.text = ScriptLocalization.blue_star;
			this.textDescriptionItem.text = ScriptLocalization.des_blue_star;
			this.stock.text = ScriptLocalization.your_stock + " " + EndlessModeManager.endlessModeData.numberBlueStar;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Ticket_Galaxy_1:
			this.textNameItem.text = string.Format(ScriptLocalization.ticket, "Milki Way");
			this.textDescriptionItem.text = string.Format(ScriptLocalization.des_ticket, "Milki Way");
			this.stock.text = ScriptLocalization.your_stock + " " + EndlessModeManager.endlessModeData.numberTicketUniverse2;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Ticket_Galaxy_2:
			this.textNameItem.text = string.Format(ScriptLocalization.ticket, "DH880");
			this.textDescriptionItem.text = string.Format(ScriptLocalization.des_ticket, "DH880");
			this.stock.text = ScriptLocalization.your_stock + " " + EndlessModeManager.endlessModeData.numberTicketUniverse3;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Ticket_Galaxy_3:
			this.textNameItem.text = string.Format(ScriptLocalization.ticket, "Brokenage");
			this.textDescriptionItem.text = string.Format(ScriptLocalization.des_ticket, "Brokenage");
			this.stock.text = ScriptLocalization.your_stock + " " + EndlessModeManager.endlessModeData.numberTicketUniverse4;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		case PrizeManager.RewardType.Ticket_Galaxy_4:
			this.textNameItem.text = string.Format(ScriptLocalization.ticket, "NGC1200");
			this.textDescriptionItem.text = string.Format(ScriptLocalization.des_ticket, "NGC1200");
			this.stock.text = ScriptLocalization.your_stock + " " + EndlessModeManager.endlessModeData.numberTicketUniverse5;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			break;
		}
		if (!this.isOpenPopupCardPlaneDrone)
		{
			this.arrObjReward[this.indexItem].SetActive(true);
			this.popupInfoItem.SetActive(true);
			DOTween.Restart("INFO_ITEM_REWARD_INGAME", true, -1f);
			DOTween.Play("INFO_ITEM_REWARD_INGAME");
		}
	}

	public void ShowPopupInfoItem(DailyGiftManager.Gift gift, int numberItem)
	{
		this.isOpenPopupCardPlaneDrone = false;
		this.textNumberItem.text = "x" + numberItem;
		switch (gift)
		{
		case DailyGiftManager.Gift.Gem:
			this.textNameItem.text = ScriptLocalization.gem;
			this.textDescriptionItem.text = ScriptLocalization.the_primary_currency_in_game;
			this.stock.text = string.Empty + ShopContext.currentGem;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			this.indexItem = 4;
			break;
		case DailyGiftManager.Gift.Coin:
			this.textNameItem.text = ScriptLocalization.coin;
			this.textDescriptionItem.text = ScriptLocalization.the_primary_currency_in_game;
			this.stock.text = GameUtil.FormatNumber(ShopContext.currentCoin);
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			this.indexItem = 5;
			break;
		case DailyGiftManager.Gift.Card_All_Plane:
			this.textNameItem.text = ScriptLocalization.ultra_starship_card;
			this.textDescriptionItem.text = ScriptLocalization.ultra_starship_card_can_use_for_any;
			this.stock.text = string.Empty + GameContext.totalUltraStarshipCard;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			this.indexItem = 0;
			break;
		case DailyGiftManager.Gift.Card_All_Drone:
			this.textNameItem.text = ScriptLocalization.ultra_drone_card;
			this.textDescriptionItem.text = ScriptLocalization.ultra_drone_card_can_use_for;
			this.stock.text = string.Empty + GameContext.totalUltraDroneCard;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			this.indexItem = 1;
			break;
		case DailyGiftManager.Gift.Card_Random_Plane:
			this.objCard.SetActive(true);
			this.isOpenPopupCardPlaneDrone = true;
			this.ShowPopupInfoCardPlane(this._planeID, numberItem);
			this.indexItem = 2;
			break;
		case DailyGiftManager.Gift.Card_Random_Drone:
			this.objCard.SetActive(true);
			this.isOpenPopupCardPlaneDrone = true;
			this.ShowPopupInfoCardDrone(this._droneID, numberItem);
			this.indexItem = 3;
			break;
		case DailyGiftManager.Gift.Item_Heart:
			this.textNameItem.text = ScriptLocalization.heart;
			this.textDescriptionItem.text = ScriptLocalization._1_life;
			this.stock.text = string.Empty + GameContext.totalItemPowerUpHeart;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			this.indexItem = 8;
			break;
		case DailyGiftManager.Gift.Item_Bullet_5:
			this.textNameItem.text = ScriptLocalization.power_5;
			this.textDescriptionItem.text = ScriptLocalization.upgrade_power_to_5;
			this.stock.text = string.Empty + GameContext.totalItemPowerUpBullet5;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			this.indexItem = 10;
			break;
		case DailyGiftManager.Gift.Item_Bullet_10:
			this.textNameItem.text = ScriptLocalization.power_10;
			this.textDescriptionItem.text = ScriptLocalization.upgrade_power_to_10;
			this.stock.text = string.Empty + GameContext.totalItemPowerUpBullet10;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			this.indexItem = 9;
			break;
		case DailyGiftManager.Gift.Item_Shield:
			this.textNameItem.text = ScriptLocalization.shield;
			this.textDescriptionItem.text = ScriptLocalization.a_force_shield_protect_the_starship;
			this.stock.text = string.Empty + GameContext.totalItemPowerShield;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			this.indexItem = 11;
			break;
		case DailyGiftManager.Gift.Special_Skill:
			this.textNameItem.text = ScriptLocalization.special_skill;
			this.textDescriptionItem.text = ScriptLocalization.use_to_active_special_skill;
			this.stock.text = string.Empty + GameContext.totalSkillPlane;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			this.indexItem = 12;
			break;
		case DailyGiftManager.Gift.Key_Super_Prize:
			this.textNameItem.text = ScriptLocalization.super_prize_key;
			this.textDescriptionItem.text = ScriptLocalization.collect_key_to_get_super_bonus;
			this.stock.text = string.Empty + CacheGame.KeySuperRize;
			EscapeManager.Current.AddAction(new Action(this.HidePopupInfoItem));
			this.indexItem = 7;
			break;
		}
		if (!this.isOpenPopupCardPlaneDrone)
		{
			this.arrObjReward[this.indexItem].SetActive(true);
			this.popupInfoItem.SetActive(true);
			DOTween.Restart("INFO_ITEM_REWARD_INGAME", true, -1f);
			DOTween.Play("INFO_ITEM_REWARD_INGAME");
		}
	}

	public void HidePopupInfoItem()
	{
		DOTween.PlayBackwards("INFO_ITEM_REWARD_INGAME");
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.arrObjReward[this.indexItem].SetActive(false);
			this.popupInfoItem.SetActive(false);
			this.stock.gameObject.SetActive(true);
		}));
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupInfoItem));
	}

	public void ShowPopupInfoCardPlane(GameContext.Plane planeID, int numberCard)
	{
		this.textNumberCard.text = "x" + numberCard;
		DataGame dataGame = DataGame.Current;
		dataGame.GetInfoPlane(planeID);
		int currentNumberCardToEvolution = dataGame.arrPlanes[(int)planeID].CurrentNumberCardToEvolution;
		int numberCardToEvolution = dataGame.arrPlanes[(int)planeID].NumberCardToEvolution;
		this.sliderCurrentCard.maxValue = (float)numberCardToEvolution;
		this.sliderCurrentCard.value = (float)(currentNumberCardToEvolution + GameContext.totalUltraStarshipCard);
		this.textNamePlaneDrone.text = dataGame.arrPlanes[(int)planeID].PlaneName;
		dataGame.SetSpriteImagePlane(this.imgPlaneDrone, planeID);
		dataGame.SetImageRank2(this.imgRank, DataGame.InfoPlane.CurrentRank);
		this.textCurrentCard.text = currentNumberCardToEvolution + "/" + numberCardToEvolution;
		this.textNameSkill.text = DataGame.InfoPlane.NameSkill;
		this.textDescriptionSkill.text = DataGame.InfoPlane.DescriptionSkill;
		this.popupInfoCardPlaneDrone.SetActive(true);
		this.imgSkill.sprite = this.sprSkillPlane;
		this.imgSkill.SetNativeSize();
		DOTween.Restart("INFO_CARD_PLANE_DRONE", true, -1f);
		DOTween.Play("INFO_CARD_PLANE_DRONE");
		EscapeManager.Current.AddAction(new Action(this.HidePopupInfoCardPlaneDrone));
	}

	public void ShowPopupInfoCardDrone(GameContext.Drone droneID, int numberCard)
	{
		this.textNumberCard.text = "x" + numberCard;
		DataGame dataGame = DataGame.Current;
		dataGame.GetInfoDrone(droneID);
		int currentNumberCard = DataGame.InfoDrone.CurrentNumberCard;
		int numberCardToEvolve = DataGame.InfoDrone.NumberCardToEvolve;
		this.sliderCurrentCard.maxValue = (float)numberCardToEvolve;
		this.sliderCurrentCard.value = (float)currentNumberCard;
		this.textNamePlaneDrone.text = DataGame.InfoDrone.DroneName;
		dataGame.SetSpriteImageDrone(this.imgPlaneDrone, droneID);
		dataGame.SetImageRank2(this.imgRank, DataGame.InfoDrone.CurrentRank);
		dataGame.SetSpriteImageSkillDrone(this.imgSkill, droneID);
		this.textCurrentCard.text = currentNumberCard + "/" + numberCardToEvolve;
		this.textNameSkill.text = DataGame.InfoDrone.NameSkill;
		this.textDescriptionSkill.text = DataGame.InfoDrone.DescriptionSkill;
		this.popupInfoCardPlaneDrone.SetActive(true);
		DOTween.Restart("INFO_CARD_PLANE_DRONE", true, -1f);
		DOTween.Play("INFO_CARD_PLANE_DRONE");
		EscapeManager.Current.AddAction(new Action(this.HidePopupInfoCardPlaneDrone));
	}

	public void HidePopupInfoCardPlaneDrone()
	{
		DOTween.PlayBackwards("INFO_CARD_PLANE_DRONE");
		base.StartCoroutine(GameContext.Delay(0.2f, delegate
		{
			this.popupInfoCardPlaneDrone.SetActive(false);
		}));
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupInfoCardPlaneDrone));
	}

	public static InfoItemReward _ins;

	[Header("Item")]
	public GameObject popupInfoItem;

	public Text textNumberItem;

	public Text stock;

	public Text textNameItem;

	public Text textDescriptionItem;

	public GameObject[] arrObjReward;

	[Header("Card Plane, Drone")]
	public GameObject popupInfoCardPlaneDrone;

	public Image imgPlaneDrone;

	public Image imgRank;

	public Text textNumberCard;

	public Slider sliderCurrentCard;

	public Text textCurrentCard;

	public Text textNamePlaneDrone;

	public Text textNameSkill;

	public Text textDescriptionSkill;

	public Image imgSkill;

	public Sprite sprSkillPlane;

	public GameObject objCard;

	private int indexItem;

	private bool isOpenPopupCardPlaneDrone;

	[HideInInspector]
	public GameContext.Plane _planeID;

	[HideInInspector]
	public GameContext.Drone _droneID;
}
