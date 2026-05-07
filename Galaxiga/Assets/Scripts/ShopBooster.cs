using System;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class ShopBooster : MonoBehaviour
{
	private void Awake()
	{
		ShopBooster.current = this;
	}

	private void Start()
	{
		this.buy_for = ScriptLocalization.buy_for;
	}

	private void SetTextPrice()
	{
		this.textPriceShield.text = string.Format(this.buy_for, 1, ShopContext.PRICE_ITEM_POWERUP_SHIELD);
		this.textPriceHeart.text = string.Format(this.buy_for, 1, ShopContext.PRICE_ITEM_POWERUP_HEART);
		this.textPricePower5.text = string.Format(this.buy_for, 1, ShopContext.PRICE_ITEM_POWERUP_BULLET_5);
		this.textPricePower10.text = string.Format(this.buy_for, 1, ShopContext.PRICE_ITEM_POWERUP_BULLET_10);
		this.priceShieldX5 = (int)((float)(ShopContext.PRICE_ITEM_POWERUP_SHIELD * 5) * 0.85f);
		this.priceHeartX5 = (int)((float)(ShopContext.PRICE_ITEM_POWERUP_SHIELD * 5) * 0.85f);
		this.pricePower5X5 = (int)((float)(ShopContext.PRICE_ITEM_POWERUP_SHIELD * 5) * 0.85f);
		this.pricePower10X5 = (int)((float)(ShopContext.PRICE_ITEM_POWERUP_SHIELD * 5) * 0.85f);
		this.textPriceShieldX5.text = string.Format(this.buy_for, 5, this.priceShieldX5);
		this.textPriceHeartX5.text = string.Format(this.buy_for, 5, this.priceHeartX5);
		this.textPricePower5X5.text = string.Format(this.buy_for, 5, this.pricePower5X5);
		this.textPricePower10X5.text = string.Format(this.buy_for, 5, this.pricePower10X5);
		this.textTotalItemShield.text = "x" + GameContext.totalItemPowerShield;
		this.textTotalItemHeart.text = "x" + GameContext.totalItemPowerUpHeart;
		this.textTotalItemPower5.text = "x" + GameContext.totalItemPowerUpBullet5;
		this.textTotalItemPower10.text = "x" + GameContext.totalItemPowerUpBullet10;
	}

	public void ShowPanelShopBooster(ShopBooster.Booster booster)
	{
		if (!this.isSetText)
		{
			this.isSetText = true;
			this.SetTextPrice();
		}
		switch (booster)
		{
		case ShopBooster.Booster.Shield:
			NewTutorial.current.BuyItems_Step1();
			this.shopShield.SetActive(true);
			break;
		case ShopBooster.Booster.Heart:
			this.shopHeart.SetActive(true);
			break;
		case ShopBooster.Booster.Power5:
			this.shopPower5.SetActive(true);
			break;
		case ShopBooster.Booster.Power10:
			this.shopPower10.SetActive(true);
			break;
		}
		this.currentBooster = booster;
		EscapeManager.Current.AddAction(new Action(this.HidePanelShopBooster));
		this.panelShopBooster.SetActive(true);
		this.tween.DORestart(false);
	}

	public void HidePanelShopBooster()
	{
		if (NewTutorial.current.currentStepTutorial_BuyItems == 5)
		{
			NewTutorial.current.BuyItems_Step5();
		}
		EscapeManager.Current.RemoveAction(new Action(this.HidePanelShopBooster));
		this.tween.DOPlayBackwards();
		base.StartCoroutine(GameContext.Delay(0.15f, delegate
		{
			switch (this.currentBooster)
			{
			case ShopBooster.Booster.Shield:
				this.shopShield.SetActive(false);
				break;
			case ShopBooster.Booster.Heart:
				this.shopHeart.SetActive(false);
				break;
			case ShopBooster.Booster.Power5:
				this.shopPower5.SetActive(false);
				break;
			case ShopBooster.Booster.Power10:
				this.shopPower10.SetActive(false);
				break;
			}
			this.panelShopBooster.SetActive(false);
		}));
	}

	public void BuyShield()
	{
		if (ShopContext.currentGem >= ShopContext.PRICE_ITEM_POWERUP_SHIELD)
		{
			if (NewTutorial.current.currentStepTutorial_BuyItems == 3)
			{
				NewTutorial.current.BuyItems_Step3();
			}
			CacheGame.MinusGems(ShopContext.PRICE_ITEM_POWERUP_SHIELD);
			CurrencyLog.LogGemOut(ShopContext.PRICE_ITEM_POWERUP_SHIELD, CurrencyLog.Out.BuyItemBooster, "Buy_Shield");
			GameContext.totalItemPowerShield++;
			CacheGame.NumberItemPowerShield = GameContext.totalItemPowerShield;
			this.textTotalItemShield.text = "x" + GameContext.totalItemPowerShield;
			DOTween.Restart("Power_Up_Shield", true, -1f);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void BuyShieldX5()
	{
		if (ShopContext.currentGem >= this.priceShieldX5)
		{
			NewTutorial.current.BuyItems_Step3();
			CacheGame.MinusGems(this.priceShieldX5);
			CurrencyLog.LogGemOut(this.priceShieldX5, CurrencyLog.Out.BuyItemBooster, "Buy_Shield");
			GameContext.totalItemPowerShield += 5;
			CacheGame.NumberItemPowerShield = GameContext.totalItemPowerShield;
			this.textTotalItemShield.text = "x" + GameContext.totalItemPowerShield;
			DOTween.Restart("Power_Up_Shield", true, -1f);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void BuyHeart()
	{
		if (ShopContext.currentGem >= ShopContext.PRICE_ITEM_POWERUP_HEART)
		{
			CacheGame.MinusGems(ShopContext.PRICE_ITEM_POWERUP_HEART);
			CurrencyLog.LogGemOut(ShopContext.PRICE_ITEM_POWERUP_HEART, CurrencyLog.Out.BuyItemBooster, "Buy_Heart");
			GameContext.totalItemPowerUpHeart++;
			CacheGame.NumberItemPowerHeart = GameContext.totalItemPowerUpHeart;
			this.textTotalItemHeart.text = "x" + GameContext.totalItemPowerUpHeart;
			DOTween.Restart("Power_Up_Heart", true, -1f);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void BuyHeartX5()
	{
		if (ShopContext.currentGem >= this.priceHeartX5)
		{
			CacheGame.MinusGems(this.priceHeartX5);
			CurrencyLog.LogGemOut(this.priceHeartX5, CurrencyLog.Out.BuyItemBooster, "Buy_Heart");
			GameContext.totalItemPowerUpHeart += 5;
			CacheGame.NumberItemPowerHeart = GameContext.totalItemPowerUpHeart;
			this.textTotalItemHeart.text = "x" + GameContext.totalItemPowerUpHeart;
			DOTween.Restart("Power_Up_Heart", true, -1f);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void BuyPower5()
	{
		if (ShopContext.currentGem >= ShopContext.PRICE_ITEM_POWERUP_BULLET_5)
		{
			CacheGame.MinusGems(ShopContext.PRICE_ITEM_POWERUP_BULLET_5);
			CurrencyLog.LogGemOut(ShopContext.PRICE_ITEM_POWERUP_BULLET_5, CurrencyLog.Out.BuyItemBooster, "Buy_Power_5");
			GameContext.totalItemPowerUpBullet5++;
			CacheGame.NumberItemPowerUpBullet5 = GameContext.totalItemPowerUpBullet5;
			this.textTotalItemPower5.text = "x" + GameContext.totalItemPowerUpBullet5;
			DOTween.Restart("Power_Up_5", true, -1f);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void BuyPower5X5()
	{
		if (ShopContext.currentGem >= this.pricePower5X5)
		{
			CacheGame.MinusGems(this.pricePower5X5);
			CurrencyLog.LogGemOut(this.pricePower5X5, CurrencyLog.Out.BuyItemBooster, "Buy_Power_5");
			GameContext.totalItemPowerUpBullet5 += 5;
			CacheGame.NumberItemPowerUpBullet5 = GameContext.totalItemPowerUpBullet5;
			this.textTotalItemPower5.text = "x" + GameContext.totalItemPowerUpBullet5;
			DOTween.Restart("Power_Up_5", true, -1f);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void BuyPower10()
	{
		if (ShopContext.currentGem >= ShopContext.PRICE_ITEM_POWERUP_BULLET_10)
		{
			CacheGame.MinusGems(ShopContext.PRICE_ITEM_POWERUP_BULLET_10);
			CurrencyLog.LogGemOut(ShopContext.PRICE_ITEM_POWERUP_BULLET_10, CurrencyLog.Out.BuyItemBooster, "Buy_Power_10");
			GameContext.totalItemPowerUpBullet10++;
			CacheGame.NumberItemPowerUpBullet10 = GameContext.totalItemPowerUpBullet10;
			this.textTotalItemPower10.text = "x" + GameContext.totalItemPowerUpBullet10;
			DOTween.Restart("Power_Up_10", true, -1f);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public void BuyPower10X5()
	{
		if (ShopContext.currentGem >= this.pricePower10X5)
		{
			CacheGame.MinusGems(this.pricePower10X5);
			CurrencyLog.LogGemOut(this.pricePower10X5, CurrencyLog.Out.BuyItemBooster, "Buy_Power_10");
			GameContext.totalItemPowerUpBullet10 += 5;
			CacheGame.NumberItemPowerUpBullet10 = GameContext.totalItemPowerUpBullet10;
			this.textTotalItemPower10.text = "x" + GameContext.totalItemPowerUpBullet10;
			DOTween.Restart("Power_Up_10", true, -1f);
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public static ShopBooster current;

	public GameObject panelShopBooster;

	public Text textPriceShield;

	public Text textPriceHeart;

	public Text textPricePower5;

	public Text textPricePower10;

	public Text textPriceShieldX5;

	public Text textPriceHeartX5;

	public Text textPricePower5X5;

	public Text textPricePower10X5;

	public Text textTotalItemShield;

	public Text textTotalItemHeart;

	public Text textTotalItemPower5;

	public Text textTotalItemPower10;

	public DOTweenAnimation tween;

	public GameObject shopShield;

	public GameObject shopHeart;

	public GameObject shopPower5;

	public GameObject shopPower10;

	private int priceShieldX5;

	private int priceHeartX5;

	private int pricePower5X5;

	private int pricePower10X5;

	private string buy_for = string.Empty;

	private bool isSetText;

	private ShopBooster.Booster currentBooster;

	[Serializable]
	public enum Booster
	{
		Shield,
		Heart,
		Power5,
		Power10
	}
}
