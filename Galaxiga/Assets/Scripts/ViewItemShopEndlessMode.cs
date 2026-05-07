using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ViewItemShopEndlessMode : MonoBehaviour
{
	private void Start()
	{
		this.numberItem = ShopEndlessModeSheet.Get(this.idItem).number;
		this.priceItem = ShopEndlessModeSheet.Get(this.idItem).price;
		this.itemType = (PrizeManager.RewardType)ShopEndlessModeSheet.Get(this.idItem).itemID;
		this.SetView();
		this.btnBuy.onClick.AddListener(new UnityAction(this.BuyItem));
	}

	private void SetView()
	{
		this.item.numberReward = this.numberItem;
		this.textPrice.text = string.Empty + this.priceItem;
		if (this.itemType == PrizeManager.RewardType.Card_Drone)
		{
			this.item._droneID = GameContext.Drone.GodOfThunder;
		}
		this.item.SetView();
	}

	private void BuyItem()
	{
		if (EndlessModeManager.current.BlueStar >= this.priceItem)
		{
			EndlessModeManager.current.BlueStar -= this.priceItem;
			UnityEngine.Debug.Log("BuyItem: " + this.priceItem);
			SaveDataQuestEndless.SetProcessQuest(EndlessDailyQuestManager.Quest.purchase_stars_in_mystic_shop0, this.priceItem);
			PrizeManager.RewardType rewardType = this.itemType;
			switch (rewardType)
			{
			case PrizeManager.RewardType.Card_Drone:
				DataGame.Current.AddCardDrone(GameContext.Drone.GodOfThunder, this.numberItem);
				Notification.Current.ShowNotification(string.Format(string.Format(ScriptLocalization.card_drone, "God Of Thunder") + " +" + this.numberItem, Array.Empty<object>()));
				break;
			default:
				if (rewardType != PrizeManager.RewardType.Card_All_Plane)
				{
					if (rewardType != PrizeManager.RewardType.Card_All_Drone)
					{
						if (rewardType == PrizeManager.RewardType.Key_Super_Prize)
						{
							Notification.Current.ShowNotification(ScriptLocalization.super_prize_key + " +" + this.numberItem);
						}
					}
					else
					{
						CacheGame.AddCardAllDrone(this.numberItem);
						Notification.Current.ShowNotification(ScriptLocalization.ultra_drone_card + " +" + this.numberItem);
					}
				}
				else
				{
					CacheGame.AddCardAllPlane(this.numberItem);
					Notification.Current.ShowNotification(ScriptLocalization.ultra_starship_card + " +" + this.numberItem);
				}
				break;
			case PrizeManager.RewardType.Ticket_Galaxy_1:
				EndlessModeManager.current.TicketGalaxy1 += this.numberItem;
				Notification.Current.ShowNotification(string.Format(ScriptLocalization.ticket, "Milki Way") + " +" + this.numberItem);
				break;
			case PrizeManager.RewardType.Ticket_Galaxy_2:
				EndlessModeManager.current.TicketGalaxy2 += this.numberItem;
				Notification.Current.ShowNotification(string.Format(ScriptLocalization.ticket, "DH880") + " +" + this.numberItem);
				break;
			case PrizeManager.RewardType.Ticket_Galaxy_3:
				EndlessModeManager.current.TicketGalaxy3 += this.numberItem;
				Notification.Current.ShowNotification(string.Format(ScriptLocalization.ticket, "Brokenage") + " +" + this.numberItem);
				break;
			case PrizeManager.RewardType.Ticket_Galaxy_4:
				EndlessModeManager.current.TicketGalaxy4 += this.numberItem;
				Notification.Current.ShowNotification(string.Format(ScriptLocalization.ticket, "NGC1200") + " +" + this.numberItem);
				break;
			}
		}
		else
		{
			Notification.Current.ShowNotification(string.Format(ScriptLocalization.not_enough, ScriptLocalization.blue_star));
		}
	}

	public int idItem;

	public PrizeManager.RewardType itemType;

	public ItemRewardInGame item;

	public Text textPrice;

	public Button btnBuy;

	private int numberItem;

	private int priceItem;
}
