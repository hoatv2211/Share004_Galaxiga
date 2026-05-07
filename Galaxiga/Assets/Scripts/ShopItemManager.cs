using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemManager : MonoBehaviour
{
	private void Awake()
	{
		this.numberResetShop = CacheGame.NumberResetShopCoinItem;
		this.CheckPriceResetShop();
		if (!GameContext.isOpenShopItemCoin)
		{
			WarningManager.Current.ShowNotificationShopCoinItem();
		}
	}

	private void Start()
	{
		this.scrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnValueChange));
	}

	private void OnValueChange(Vector2 arg0)
	{
		if (this.scrollRect.verticalNormalizedPosition <= 0.1f)
		{
			this.fade.SetActive(false);
		}
		else
		{
			this.fade.SetActive(true);
		}
	}

	public void StartCountdownResetDay()
	{
		this.scrollRect.verticalNormalizedPosition = 1f;
		CheckNewDay.Current.StartCoroutineResetDay(this.textTimeResetShop);
	}

	public void StopCountdownResetDay()
	{
		CheckNewDay.Current.StopCoroutineResetDay();
	}

	private void CheckPriceResetShop()
	{
		switch (this.numberResetShop)
		{
		case 0:
			this.priceResetShop = 20;
			break;
		case 1:
			this.priceResetShop = 50;
			break;
		case 2:
			this.priceResetShop = 100;
			break;
		default:
			this.priceResetShop = 150;
			break;
		}
		this.textPriceResetShop.text = string.Empty + this.priceResetShop;
	}

	public void SetActiveButton(Image imgBtn)
	{
		imgBtn.sprite = this.sprBtnActive;
	}

	public void SetDeactiveButton(Image imgBtn)
	{
		imgBtn.sprite = this.sprBtnDeactive;
	}

	public void ResetShop()
	{
		if (ShopContext.currentGem >= this.priceResetShop)
		{
			this.numberResetShop++;
			CacheGame.MinusGems(this.priceResetShop);
			CurrencyLog.LogGemOut(this.priceResetShop, CurrencyLog.Out.ResetShopItemCoin, "ResetShopItemCoin");
			CacheGame.NumberResetShopCoinItem = this.numberResetShop;
			this.CheckPriceResetShop();
			SaveDataStateItemShopCoin.ResetData();
			int num = this.arrItemShopCoins.Length;
			for (int i = 0; i < num; i++)
			{
				this.arrItemShopCoins[i].ResetItem();
			}
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughGem();
		}
	}

	public Text textTimeResetShop;

	public Text textPriceResetShop;

	public Sprite sprBtnActive;

	public Sprite sprBtnDeactive;

	public ScrollRect scrollRect;

	public GameObject fade;

	public ViewItemShopCoin[] arrItemShopCoins;

	private int numberResetShop;

	private int priceResetShop;
}
