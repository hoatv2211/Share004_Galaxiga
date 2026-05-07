using System;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class FreeGiftManager : MonoBehaviour
{
	private void Awake()
	{
		FreeGiftManager.current = this;
	}

	private void Start()
	{
		this.CheckAllCanWatchAds();
	}

	private void CheckAllCanWatchAds()
	{
		this.numberWatchAdsGemCoin = CacheGame.NumberWatchAdsGemCoin;
		this.numberWatchAdsCard = CacheGame.NumberWatchAdsCard;
		this.numberWatchAdsItemBooster = CacheGame.NumberWatchAdsItemBooster;
		this.CheckCanWatchAdsGemCoin();
		this.CheckCanWatchAdsCard();
		this.CheckCanWatchAdsItemBooster();
		this.CheckNotificationBtnFreeGift();
	}

	private void CheckNotificationBtnFreeGift()
	{
		if (this.canWatchAdsCard || this.canWatchAdsGemCoin || this.canWatchAdsItemBooster)
		{
			this.objNotification.SetActive(true);
		}
		else
		{
			this.objNotification.SetActive(false);
		}
	}

	private void CheckCanWatchAdsGemCoin()
	{
		this.textNumberWatchAdsGemCoin.text = this.numberWatchAdsGemCoin + "/" + this.maxNumberWatchVideo;
		this.canWatchAdsGemCoin = (this.numberWatchAdsGemCoin > 0);
		if (this.canWatchAdsGemCoin)
		{
			this.imgBtnWatchAdsGemCoin.sprite = this.sprBtn;
			this.objNotificationAdsGemCoin.SetActive(true);
		}
		else
		{
			this.imgBtnWatchAdsGemCoin.sprite = this.sprtBtnDisable;
			this.objNotificationAdsGemCoin.SetActive(false);
		}
	}

	private void CheckCanWatchAdsCard()
	{
		this.textNumberWatchAdsCard.text = this.numberWatchAdsCard + "/" + this.maxNumberWatchVideo;
		this.canWatchAdsCard = (this.numberWatchAdsCard > 0);
		if (this.canWatchAdsCard)
		{
			this.imgBtnWatchAdsCard.sprite = this.sprBtn;
			this.objNotificationAdsCard.SetActive(true);
		}
		else
		{
			this.imgBtnWatchAdsCard.sprite = this.sprtBtnDisable;
			this.objNotificationAdsCard.SetActive(false);
		}
	}

	private void CheckCanWatchAdsItemBooster()
	{
		this.textNumberWatchAdsItemBooster.text = this.numberWatchAdsItemBooster + "/" + this.maxNumberWatchVideo;
		this.canWatchAdsItemBooster = (this.numberWatchAdsItemBooster > 0);
		if (this.canWatchAdsItemBooster)
		{
			this.imgBtnWatchAdsItemBooster.sprite = this.sprBtn;
			this.objNotificationAdsBooster.SetActive(true);
		}
		else
		{
			this.imgBtnWatchAdsItemBooster.sprite = this.sprtBtnDisable;
			this.objNotificationAdsBooster.SetActive(false);
		}
	}

	public void WatchAdsGemCoin()
	{
		if (this.canWatchAdsGemCoin)
		{
			this.coins = UnityEngine.Random.Range(300, 3001);
			this.gems = UnityEngine.Random.Range(1, 11);
			this.typeVideo = FreeGiftManager.TypeVideo.Gem_Coin;
			GameContext.actionVideo = GameContext.ActionVideoAds.Free_Gift;
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_video_unavailable);
		}
	}

	public void WatchAdsCard()
	{
		if (this.canWatchAdsCard)
		{
			this.numberCardStarship = UnityEngine.Random.Range(1, 6);
			this.numberCardDrone = UnityEngine.Random.Range(1, 4);
			this.typeVideo = FreeGiftManager.TypeVideo.Card;
			GameContext.actionVideo = GameContext.ActionVideoAds.Free_Gift;
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_video_unavailable);
		}
	}

	public void WatchAdsItemBooster()
	{
		if (this.canWatchAdsItemBooster)
		{
			this.typeVideo = FreeGiftManager.TypeVideo.Item_Booster;
			this.numberRewardItemBooster = UnityEngine.Random.Range(1, 4);
			this.numberRewardItemBooster1 = UnityEngine.Random.Range(1, 4);
			this.reward1 = this.rewardTypes[UnityEngine.Random.Range(0, this.rewardTypes.Length)];
			this.reward2 = this.rewardTypes[UnityEngine.Random.Range(0, this.rewardTypes.Length)];
			GameContext.actionVideo = GameContext.ActionVideoAds.Free_Gift;
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_video_unavailable);
		}
	}

	public void WatchAdsFreeGiftComplete()
	{
		UnityEngine.Debug.Log("WatchAdsFreeGiftComplete");
		switch (this.typeVideo)
		{
		case FreeGiftManager.TypeVideo.Gem_Coin:
			CacheGame.AddCoins(this.coins);
			CacheGame.AddGems(this.gems);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, this.coins);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, this.gems);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			if (this.numberWatchAdsGemCoin > 0)
			{
				this.numberWatchAdsGemCoin--;
				CacheGame.NumberWatchAdsGemCoin = this.numberWatchAdsGemCoin;
			}
			this.CheckCanWatchAdsGemCoin();
			this.CheckNotificationBtnFreeGift();
			break;
		case FreeGiftManager.TypeVideo.Card:
		{
			GameContext.Plane randomPlaneID = DataGame.Current.RandomPlaneID;
			GameContext.Drone drone = DataGame.Current.RandomDroneID();
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_Random_Plane, this.numberCardStarship, randomPlaneID);
			GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Card_Random_Drone, this.numberCardDrone, drone);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			DataGame.Current.AddCardPlane(randomPlaneID, this.numberCardStarship);
			DataGame.Current.AddCardDrone(drone, this.numberCardDrone);
			if (this.numberWatchAdsCard > 0)
			{
				this.numberWatchAdsCard--;
				CacheGame.NumberWatchAdsCard = this.numberWatchAdsCard;
			}
			this.CheckCanWatchAdsCard();
			this.CheckNotificationBtnFreeGift();
			break;
		}
		case FreeGiftManager.TypeVideo.Item_Booster:
			this.AddRewardBooster(this.reward1, this.numberRewardItemBooster);
			this.AddRewardBooster(this.reward2, this.numberRewardItemBooster1);
			GetRewardSuccess.current.AddItemToView(this.reward1, this.numberRewardItemBooster);
			GetRewardSuccess.current.AddItemToView(this.reward2, this.numberRewardItemBooster1);
			GetRewardSuccess.current.ShowPanelGetRewardSuccess();
			if (this.numberWatchAdsItemBooster > 0)
			{
				this.numberWatchAdsItemBooster--;
				CacheGame.NumberWatchAdsItemBooster = this.numberWatchAdsItemBooster;
			}
			this.CheckCanWatchAdsItemBooster();
			this.CheckNotificationBtnFreeGift();
			break;
		}
	}

	private void AddRewardBooster(PrizeManager.RewardType rewardType, int numberReward)
	{
		switch (rewardType)
		{
		case PrizeManager.RewardType.Item_Heart:
			GameContext.totalItemPowerUpHeart += numberReward;
			CacheGame.NumberItemPowerHeart = GameContext.totalItemPowerUpHeart;
			break;
		case PrizeManager.RewardType.Item_Bullet_10:
			GameContext.totalItemPowerUpBullet5 += numberReward;
			CacheGame.NumberItemPowerUpBullet10 = GameContext.totalItemPowerUpBullet10;
			break;
		case PrizeManager.RewardType.Item_Bullet_5:
			GameContext.totalItemPowerUpBullet5 += numberReward;
			CacheGame.NumberItemPowerUpBullet5 = GameContext.totalItemPowerUpBullet5;
			break;
		case PrizeManager.RewardType.Item_Shield:
			GameContext.totalItemPowerShield += numberReward;
			CacheGame.NumberItemPowerShield = GameContext.totalItemPowerShield;
			break;
		case PrizeManager.RewardType.Special_Skill:
			CacheGame.AddSkillPlane(numberReward);
			break;
		}
	}

	public void ShowPanelFreeGift()
	{
		this.panelFreeGift.SetActive(true);
		DOTween.Restart("BECOME_RICH", true, -1f);
		DOTween.Play("BECOME_RICH");
		EscapeManager.Current.AddAction(new Action(this.HidePanelFreeGift));
	}

	public void HidePanelFreeGift()
	{
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelFreeGift));
		DOTween.PlayBackwards("BECOME_RICH");
		base.StartCoroutine(GameContext.Delay(0.2f, delegate
		{
			this.panelFreeGift.SetActive(false);
		}));
	}

	public static FreeGiftManager current;

	public GameObject panelFreeGift;

	public Sprite sprBtn;

	public Sprite sprtBtnDisable;

	public GameObject objNotification;

	public Image imgBtnWatchAdsGemCoin;

	public Image imgBtnWatchAdsCard;

	public Image imgBtnWatchAdsItemBooster;

	public Text textNumberWatchAdsGemCoin;

	public Text textNumberWatchAdsCard;

	public Text textNumberWatchAdsItemBooster;

	public GameObject objNotificationAdsGemCoin;

	public GameObject objNotificationAdsCard;

	public GameObject objNotificationAdsBooster;

	private int coins;

	private int gems;

	private bool canWatchAdsGemCoin;

	private bool canWatchAdsCard;

	private bool canWatchAdsItemBooster;

	private int numberWatchAdsGemCoin;

	private int numberWatchAdsCard;

	private int numberWatchAdsItemBooster;

	private FreeGiftManager.TypeVideo typeVideo;

	private int maxNumberWatchVideo = 5;

	private int numberCardStarship;

	private int numberCardDrone;

	private PrizeManager.RewardType[] rewardTypes = new PrizeManager.RewardType[]
	{
		PrizeManager.RewardType.Special_Skill,
		PrizeManager.RewardType.Item_Heart,
		PrizeManager.RewardType.Item_Shield,
		PrizeManager.RewardType.Item_Bullet_5,
		PrizeManager.RewardType.Item_Bullet_10
	};

	private int numberRewardItemBooster;

	private int numberRewardItemBooster1;

	private PrizeManager.RewardType reward1;

	private PrizeManager.RewardType reward2;

	private enum TypeVideo
	{
		Gem_Coin,
		Card,
		Item_Booster
	}
}
