using System;
using System.Collections;
using DG.Tweening;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaleManager : MonoBehaviour
{
	public static SaleManager Currrent
	{
		get
		{
			if (SaleManager._ins == null)
			{
				SaleManager._ins = UnityEngine.Object.FindObjectOfType<SaleManager>();
			}
			return SaleManager._ins;
		}
	}

	private void Start()
	{
	}

	public void CheckSale()
	{
		string packSale = CacheGame.PackSale;
		if (packSale == string.Empty)
		{
			this.StartSale();
		}
		else
		{
			this.PackSale = JsonUtility.FromJson<SaleManager.Sale>(packSale);
			if (this.PackSale.isPurchased)
			{
				DateTime dateTime = this.GetDateTime(CacheGame.NextTimeSale);
				if (GameContext.GetTimeSpan(dateTime, DateTime.Now).TotalSeconds <= 0.0)
				{
					this.StartSale();
				}
			}
			else
			{
				DateTime dateTime2 = this.GetDateTime(this.PackSale.endTimeSale);
				this.timeSale = (float)GameContext.GetTimeSpan(dateTime2, DateTime.Now).TotalSeconds;
				if (this.timeSale <= 0f)
				{
					this.isSale = false;
					DateTime dateTime3 = this.GetDateTime(CacheGame.NextTimeSale);
					if (GameContext.GetTimeSpan(dateTime3, dateTime2).TotalSeconds <= 0.0)
					{
						this.StartSale();
					}
				}
				else
				{
					this.isSale = true;
					this.ShowPack(this.PackSale);
				}
			}
		}
	}

	private void ShowPack(SaleManager.Sale packSale)
	{
		this.arrObjPack[packSale.packID].SetActive(true);
		this.textPercenDiscount.text = "-" + packSale.percentDiscount + "%";
		this.SetPricePack((SaleManager.PackID)packSale.packID);
		this.textPriceNoSale.text = this.priceNoSale;
		this.textPriceSale.text = this.priceSale;
		base.StartCoroutine(this.CountDownSale());
		if (GameContext.needShowPanelSale)
		{
			this.ShowPanelSale();
		}
	}

	public void StartSale()
	{
		SaleManager.Sale sale = new SaleManager.Sale();
		sale.packID = this.GetPackToSale();
		sale.percentDiscount = (float)this.SetPercentDiscount((SaleManager.PackID)sale.packID);
		DateTime dateTime = DateTime.Now.AddDays(2.0);
		sale.endTimeSale = dateTime.ToBinary().ToString();
		sale.isPurchased = false;
		int num = UnityEngine.Random.Range(24, 72);
		CacheGame.NextTimeSale = dateTime.AddHours((double)num).ToBinary().ToString();
		PersistentSingleton<NotificationsEasyMobileManager>.Instance.PushNotificationSale(sale.percentDiscount, 2);
		string packSale = JsonUtility.ToJson(sale, true);
		CacheGame.PackSale = packSale;
		this.isSale = true;
		this.PackSale = sale;
		this.timeSale = 172800f;
		this.ShowPack(sale);
	}

	public void StopSale()
	{
		this.isSale = false;
		this.PackSale.isPurchased = true;
		string text = JsonUtility.ToJson(this.PackSale, true);
		UnityEngine.Debug.Log("pack to sale " + text);
		CacheGame.PackSale = text;
		if (HomeManager.current != null)
		{
			HomeManager.current.HideBtnSale();
		}
		CacheGame.NextTimeSale = DateTime.Now.AddDays((double)UnityEngine.Random.Range(2, 5)).ToBinary().ToString();
		this.HidePanelSale();
	}

	private int GetPackToSale()
	{
		bool flag = CacheGame.IsOwnedPlane(GameContext.Plane.SSLightning);
		bool flag2 = CacheGame.IsOwnedPlane(GameContext.Plane.Warlock);
		if (flag && !flag2)
		{
			this.arrPackID = new int[]
			{
				1,
				4,
				5,
				6,
				7
			};
		}
		else if (!flag && flag2)
		{
			this.arrPackID = new int[]
			{
				2,
				4,
				5,
				6,
				7
			};
		}
		else
		{
			this.arrPackID = new int[]
			{
				1,
				2,
				4,
				5,
				6,
				7
			};
		}
		return this.arrPackID[UnityEngine.Random.Range(0, this.arrPackID.Length)];
	}

	private int SetPercentDiscount(SaleManager.PackID packID)
	{
		int result;
		switch (packID)
		{
		case SaleManager.PackID.Super_Starship_Pack:
			result = 50;
			break;
		case SaleManager.PackID.Warlock_Pack:
			result = 60;
			break;
		case SaleManager.PackID.SS_Lightning_Pack:
			result = 60;
			break;
		case SaleManager.PackID.Starship_Pack:
			result = 60;
			break;
		case SaleManager.PackID.Elite_Pack:
			result = 60;
			break;
		case SaleManager.PackID.Super_Booster_Pack:
			result = 60;
			break;
		case SaleManager.PackID.Booster_Pack:
			result = 70;
			break;
		case SaleManager.PackID.Offer_Pack:
			result = 70;
			break;
		default:
			result = 0;
			break;
		}
		return result;
	}

	private void SetPricePack(SaleManager.PackID packID)
	{
		switch (packID)
		{
		case SaleManager.PackID.Super_Starship_Pack:
			IAPGameManager.Current.SetTextLocalizePricePackOffer(this.textPriceNoSale, this.textPriceSale, IAPGameManager.Pack.Super_Spaceship_Pack);
			break;
		case SaleManager.PackID.Warlock_Pack:
			IAPGameManager.Current.SetTextLocalizePricePackOffer(this.textPriceNoSale, this.textPriceSale, IAPGameManager.Pack.Warlock_Pack);
			break;
		case SaleManager.PackID.SS_Lightning_Pack:
			IAPGameManager.Current.SetTextLocalizePricePackOffer(this.textPriceNoSale, this.textPriceSale, IAPGameManager.Pack.SS_Lightning_Pack);
			break;
		case SaleManager.PackID.Starship_Pack:
			IAPGameManager.Current.SetTextLocalizePricePackOffer(this.textPriceNoSale, this.textPriceSale, IAPGameManager.Pack.Spaceship_Pack);
			break;
		case SaleManager.PackID.Elite_Pack:
			IAPGameManager.Current.SetTextLocalizePricePackOffer(this.textPriceNoSale, this.textPriceSale, IAPGameManager.Pack.Elite_Pack);
			break;
		case SaleManager.PackID.Super_Booster_Pack:
			IAPGameManager.Current.SetTextLocalizePricePackOffer(this.textPriceNoSale, this.textPriceSale, IAPGameManager.Pack.Super_Booster_Pack);
			break;
		case SaleManager.PackID.Booster_Pack:
			IAPGameManager.Current.SetTextLocalizePricePackOffer(this.textPriceNoSale, this.textPriceSale, IAPGameManager.Pack.Booster_Pack);
			break;
		case SaleManager.PackID.Offer_Pack:
			IAPGameManager.Current.SetTextLocalizePricePackOffer(this.textPriceNoSale, this.textPriceSale, IAPGameManager.Pack.Offer_Pack);
			break;
		}
	}

	public void PurchasePack()
	{
		switch (this.PackSale.packID)
		{
		case 0:
			IAPGameManager.Current.Purchase("Super Spaceship Pack Sale 50");
			break;
		case 1:
			IAPGameManager.Current.Purchase("Warlock Pack Sale 60");
			break;
		case 2:
			IAPGameManager.Current.Purchase("SS Lightning Pack Sale 60");
			break;
		case 3:
			IAPGameManager.Current.Purchase("Starship Pack Sale 60");
			break;
		case 4:
			IAPGameManager.Current.Purchase("Elite Pack Sale 60");
			break;
		case 5:
			IAPGameManager.Current.Purchase("Super Booster Pack Sale 60");
			break;
		case 6:
			IAPGameManager.Current.Purchase("Booster Pack Sale 70");
			break;
		case 7:
			IAPGameManager.Current.Purchase("Offer Pack Sale 70");
			break;
		}
	}

	private void PurchaseSuccess()
	{
		switch (this.PackSale.packID)
		{
		case 0:
			IAPGameManager.Current.PurchasePackSuccess(0);
			break;
		case 1:
			IAPGameManager.Current.PurchasePackSuccess(1);
			break;
		case 2:
			IAPGameManager.Current.PurchasePackSuccess(2);
			break;
		case 3:
			IAPGameManager.Current.PurchasePackSuccess(3);
			break;
		case 4:
			IAPGameManager.Current.PurchasePackSuccess(4);
			break;
		case 5:
			IAPGameManager.Current.PurchasePackSuccess(5);
			break;
		case 6:
			IAPGameManager.Current.PurchasePackSuccess(6);
			break;
		case 7:
			IAPGameManager.Current.PurchasePackSuccess(7);
			break;
		}
		this.StopSale();
	}

	private IEnumerator CountDownSale()
	{
		while (this.timeSale > 0f)
		{
			string textFormat = TimeSpan.FromSeconds((double)this.timeSale).ToString("hh\\:mm\\:ss");
			this.textEndTimeSale.text = textFormat;
			yield return new WaitForSecondsRealtime(1f);
			this.timeSale -= 1f;
		}
		yield break;
	}

	private DateTime GetDateTime(string timeSaved)
	{
		long dateData = Convert.ToInt64(timeSaved);
		return DateTime.FromBinary(dateData);
	}

	public void ShowPanelSale()
	{
		if (EscapeManager.Current != null)
		{
			EscapeManager.Current.AddAction(new Action(this.HidePanelSale));
		}
		if (GameContext.needShowPanelSale)
		{
			GameContext.needShowPanelSale = false;
		}
		this.panelSale.SetActive(true);
		DOTween.Restart("PANEL_SALE", true, -1f);
	}

	public void HidePanelSale()
	{
		if (EscapeManager.Current != null)
		{
			EscapeManager.Current.RemoveAction(new Action(this.HidePanelSale));
		}
		DOTween.PlayBackwards("PANEL_SALE");
		base.StartCoroutine(GameContext.Delay(0.15f, delegate
		{
			this.panelSale.SetActive(false);
		}));
	}

	private void SaveDataPackSale()
	{
		string text = JsonUtility.ToJson(this.PackSale, true);
		UnityEngine.Debug.Log("pack to sale " + text);
		CacheGame.PackSale = text;
	}

	public void SetTextPricePackSale()
	{
		if (this.isSale)
		{
			this.SetPricePack((SaleManager.PackID)this.PackSale.packID);
		}
	}

	public void SetTextTimeEndInHomeScreen(TextMeshProUGUI textEndTime, string _time)
	{
		textEndTime.text = _time;
	}

	public static SaleManager _ins;

	public GameObject panelSale;

	public Text textEndTimeSale;

	public Text textPriceNoSale;

	public Text textPriceSale;

	public Text textPercenDiscount;

	[HideInInspector]
	public bool isSale;

	public GameObject[] arrObjPack;

	public SaleManager.Sale PackSale;

	private string priceSale;

	private string priceNoSale;

	[HideInInspector]
	public float timeSale;

	private int[] arrPackID;

	[Serializable]
	public class Sale
	{
		public int packID;

		public string endTimeSale;

		public float percentDiscount;

		public bool isPurchased;
	}

	public enum PackID
	{
		Super_Starship_Pack,
		Warlock_Pack,
		SS_Lightning_Pack,
		Starship_Pack,
		Elite_Pack,
		Super_Booster_Pack,
		Booster_Pack,
		Offer_Pack
	}
}
