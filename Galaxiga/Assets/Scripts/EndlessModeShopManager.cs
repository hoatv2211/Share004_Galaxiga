using System;
using UnityEngine;
using UnityEngine.UI;

public class EndlessModeShopManager : MonoBehaviour
{
	private void Awake()
	{
		EndlessModeShopManager.current = this;
		this.endlessModeManager = base.GetComponent<EndlessModeManager>();
	}

	private void Start()
	{
	}

	public void CheckPackOneTime()
	{
		if (EndlessModeManager.endlessModeData.isPurchasePackOneTime)
		{
			this.objPackOneTime.SetActive(false);
			this.viewPort.anchoredPosition = Vector2.zero;
			this.tabTicket.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -280f);
		}
		else
		{
			this.textPricePackOneTime.text = IAPGameManager.Current.GetLocalizePrice("Card All Plane Sale 60");
			this.textPriceOneTimeDiscount.text = IAPGameManager.Current.GetLocalizePrice("Endless Mode Pack One Time");
		}
	}

	public void PurchasePackOneTime()
	{
		IAPGameManager.Current.PurchasePackOneTimeEndlessMode();
	}

	public void PurchasePackOneTimeSuccess()
	{
		this.objPackOneTime.SetActive(false);
		this.viewPort.anchoredPosition = Vector2.zero;
		this.tabTicket.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -280f);
		EndlessModeManager.endlessModeData.isPurchasePackOneTime = true;
		CacheGame.AddGems(400);
		CacheGame.AddGems(40000);
		this.endlessModeManager.AddBlueStar(4000);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Gem, 400);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Coin, 40000);
		GetRewardSuccess.current.AddItemToView(PrizeManager.RewardType.Blue_Star, 4000);
		GetRewardSuccess.current.ShowPanelGetRewardSuccess();
	}

	public void SelectTabShop()
	{
		if (this.curretnTab != EndlessModeShopManager.TabShop.shop)
		{
			this.curretnTab = EndlessModeShopManager.TabShop.shop;
			this.tabShop.SetActive(true);
			this.tabTicket.SetActive(false);
			this.imgBtnTabShop.sprite = this.sprBtnActive;
			this.imgBtnTabTicket.sprite = this.sprBtnDeactive;
			this.textShop.color = Color.white;
			this.textTicket.color = Color.gray;
		}
	}

	public void SelectTabTicket()
	{
		if (this.curretnTab != EndlessModeShopManager.TabShop.ticket)
		{
			this.curretnTab = EndlessModeShopManager.TabShop.ticket;
			this.tabShop.SetActive(false);
			this.tabTicket.SetActive(true);
			this.imgBtnTabShop.sprite = this.sprBtnDeactive;
			this.imgBtnTabTicket.sprite = this.sprBtnActive;
			this.textShop.color = Color.gray;
			this.textTicket.color = Color.white;
		}
	}

	public void BuyBlueStar1()
	{
		if (ShopContext.currentGem >= 10)
		{
			this.endlessModeManager.AddBlueStar(100);
			CacheGame.MinusGems(10);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void BuyBlueStar2()
	{
		if (ShopContext.currentGem >= 200)
		{
			this.endlessModeManager.AddBlueStar(2000);
			CacheGame.MinusGems(200);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void BuyBlueStar3()
	{
		if (ShopContext.currentGem >= 1000)
		{
			this.endlessModeManager.AddBlueStar(10000);
			CacheGame.MinusGems(1000);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public static EndlessModeShopManager current;

	public GameObject objPackOneTime;

	public RectTransform viewPort;

	public GameObject tabShop;

	public GameObject tabTicket;

	public Image imgBtnTabShop;

	public Image imgBtnTabTicket;

	public Sprite sprBtnActive;

	public Sprite sprBtnDeactive;

	public Text textShop;

	public Text textTicket;

	public Text textPricePackOneTime;

	public Text textPriceOneTimeDiscount;

	private EndlessModeManager endlessModeManager;

	private EndlessModeShopManager.TabShop curretnTab;

	private enum TabShop
	{
		shop,
		ticket
	}
}
