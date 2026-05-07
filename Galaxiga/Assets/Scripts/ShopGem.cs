using System;
using EasyMobile;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

public class ShopGem : MonoBehaviour
{
	private void Awake()
	{
		this.CheckX3Pack();
		this.SetViewAllPack();
	}

	private void Start()
	{
	}

	public void SetViewPack(int packID)
	{
		this.arrPackGem[packID].SetViewPack();
	}

	private void SetViewAllPack()
	{
		int num = this.arrPackGem.Length;
		for (int i = 0; i < num; i++)
		{
			this.arrPackGem[i].SetViewPack();
		}
	}

	public void SetNextTimeX3Pack()
	{
		DateTime now = DateTime.Now;
		float num = UnityEngine.Random.Range(1f, 3f);
		now.AddDays((double)num);
		CacheGame.NextTimeX3Pack = now.ToBinary().ToString();
		PersistentSingleton<NotificationsEasyMobileManager>.Instance.PushNotificationX3PackGem((int)(num * 24f));
		CacheGame.IDPackGemX3 = -1;
		GameContext.currentIDPackGemX3 = -1;
		this.objOffer.SetActive(false);
	}

	private void CheckX3Pack()
	{
		if (GameContext.currentIDPackGemX3 > 0)
		{
			this.objOffer.SetActive(true);
		}
		string nextTimeX3Pack = CacheGame.NextTimeX3Pack;
		if (nextTimeX3Pack == string.Empty)
		{
			return;
		}
		DateTime now = DateTime.Now;
		if (GameContext.GetTimeSpan(now, nextTimeX3Pack).TotalSeconds <= 0.0)
		{
			CacheGame.IDPackGemX3 = UnityEngine.Random.Range(0, 3);
		}
	}

	public void DisableOneTimeOfferPack(int packID)
	{
		SaveDataStateOneTimeOfferPackGem.SetData(packID - 1);
		this.arrPackGem[packID - 1].SetViewPack();
	}

	public void SetTextLocalPrice()
	{
		//if (!this.isSetText)
		//{
		//	this.isSetText = true;
		//	this.arrTextPrice[0].text = InAppPurchasing.GetPrice("Pack 1");
		//	this.arrTextPrice[1].text = InAppPurchasing.GetPrice("Pack 2");
		//	this.arrTextPrice[2].text = InAppPurchasing.GetPrice("Pack 3");
		//	this.arrTextPrice[3].text = InAppPurchasing.GetPrice("Pack 4");
		//	this.arrTextPrice[4].text = InAppPurchasing.GetPrice("Pack 5");
		//	this.arrTextPrice[5].text = InAppPurchasing.GetPrice("Pack 6");
		//}
	}

	public Image imgBGGem;

	public GameObject objOffer;

	public ShopGem.Pack[] arrPackGem;

	public Text[] arrTextPrice;

	private bool isSetText;

	[Serializable]
	public class Pack
	{
		public void SetViewPack()
		{
			if (this.packID == 6)
			{
				if (GameContext.currentIDPackGemX3 == 0)
				{
					this.numberGem = ShopGemDataSheet.Get(this.packID).numberGem * 3;
					this.SetX3PackOffer();
				}
				else
				{
					this.isOneTimeOffer = SaveDataStateOneTimeOfferPackGem.IsOfferPack(this.packID - 1);
					if (this.isOneTimeOffer)
					{
						this.numberGem = ShopGemDataSheet.Get(this.packID).numberGemOneTime;
						this.objOneTimeOffer.SetActive(true);
					}
					else
					{
						this.numberGem = ShopGemDataSheet.Get(this.packID).numberGem;
						this.objNamePack.SetActive(true);
						this.objOneTimeOffer.SetActive(false);
					}
				}
			}
			else if (GameContext.currentIDPackGemX3 == this.packID)
			{
				this.numberGem = ShopGemDataSheet.Get(this.packID).numberGem * 3;
				this.SetX3PackOffer();
			}
			else
			{
				this.isOneTimeOffer = SaveDataStateOneTimeOfferPackGem.IsOfferPack(this.packID - 1);
				if (this.isOneTimeOffer)
				{
					this.numberGem = ShopGemDataSheet.Get(this.packID).numberGemOneTime;
					this.objOneTimeOffer.SetActive(true);
				}
				else
				{
					this.numberGem = ShopGemDataSheet.Get(this.packID).numberGem;
					this.objNamePack.SetActive(true);
					this.objOneTimeOffer.SetActive(false);
				}
			}
			this.textGemNoOffer.text = string.Empty + ShopGemDataSheet.Get(this.packID).numberGem;
			this.textGem.text = string.Empty + this.numberGem;
		}

		private void SetX3PackOffer()
		{
			this.imgPack.sprite = this.sprPackX3;
			this.objTextX3PackOffer.SetActive(true);
			this.objTextOneTimeOffer.SetActive(false);
		}

		public void DisableX3Pack()
		{
			this.imgPack.sprite = this.sprNormal;
			this.objTextX3PackOffer.SetActive(false);
			this.objTextOneTimeOffer.SetActive(true);
		}

		public int packID;

		public GameObject objOneTimeOffer;

		public GameObject objNamePack;

		public GameObject objTextX3PackOffer;

		public GameObject objTextOneTimeOffer;

		public Text textGemNoOffer;

		public Text textGem;

		public Image imgPack;

		public int numberGem;

		public Sprite sprPackX3;

		public Sprite sprNormal;

		private bool isOneTimeOffer;
	}
}
