using System;
using DG.Tweening;
using I2.Loc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItemPowerUp : MonoBehaviour
{
	private void Awake()
	{
		if (this.btnInfoItemBooster != null)
		{
			this.btnInfoItemBooster.onClick.AddListener(new UnityAction(this.ShowPopupInfoItemBooster));
		}
		this.CheckAllItemBooster();
	}

	private void Start()
	{
		this.textPriceSkillPlane.text = string.Empty + ShopContext.PRICE_ITEM_POWERUP_SKILL_PLANE;
	}

	private void OnEnable()
	{
		this.UpdateText();
	}

	private void CheckAllItemBooster()
	{
		for (int i = 0; i < this.arrItems.Length; i++)
		{
			this.arrItems[i].CheckItem();
		}
	}

	public void UpdateText()
	{
		this.UpdateTextSkillPlane();
		this.UpdateNumberHeart();
		this.UpdateNumberPower5();
		this.UpdateNumberPower10();
		this.UpdateNumberShield();
	}

	private void SetTextNumberSkillPlaneInGame()
	{
		if (GameContext.totalSkillPlane >= GameContext.MAX_NUMBER_SKILL_IN_GAME)
		{
			GameContext.currentNumberSkillPlane = GameContext.MAX_NUMBER_SKILL_IN_GAME;
			this.textNumberSkillPlaneInGame.text = string.Concat(new object[]
			{
				ScriptLocalization.special_skill,
				" ",
				GameContext.MAX_NUMBER_SKILL_IN_GAME,
				"/",
				GameContext.MAX_NUMBER_SKILL_IN_GAME
			});
		}
		else
		{
			GameContext.currentNumberSkillPlane = GameContext.totalSkillPlane;
			this.textNumberSkillPlaneInGame.text = string.Concat(new object[]
			{
				ScriptLocalization.special_skill,
				" ",
				GameContext.currentNumberSkillPlane,
				"/",
				GameContext.MAX_NUMBER_SKILL_IN_GAME
			});
		}
	}

	[EnumAction(typeof(ShopBooster.Booster))]
	public void SelectItem(int itemID)
	{
		if (itemID == 2)
		{
			if (GameContext.totalItemPowerUpBullet5 <= 0)
			{
				ShopBooster.current.ShowPanelShopBooster(ShopBooster.Booster.Power5);
				return;
			}
		}
		else if (GameContext.totalItemPowerUpBullet10 <= 0)
		{
			ShopBooster.current.ShowPanelShopBooster(ShopBooster.Booster.Power10);
			return;
		}
		this.DeselectItem();
		if (this.currentItem == itemID)
		{
			GameContext.isBonusPower = false;
			GameContext.power = 1;
			this.DeselectItem2(this.currentItem);
			this.currentItem = -1;
		}
		else
		{
			if (itemID != 2)
			{
				if (itemID == 3)
				{
					if (GameContext.isBonusPower)
					{
						this.DeselectItem2(this.currentItem);
					}
					GameContext.power = 10;
					GameContext.isBonusPower = true;
					this.arrItems[2].SelectItem();
					if (this.numberItemPowerBullet10 > 0)
					{
						this.numberItemPowerBullet10--;
						this.textNumberBullet10.text = "x" + this.numberItemPowerBullet10;
					}
				}
			}
			else
			{
				if (GameContext.isBonusPower)
				{
					this.DeselectItem2(this.currentItem);
				}
				GameContext.power = 5;
				GameContext.isBonusPower = true;
				this.arrItems[1].SelectItem();
				if (this.numberItemPowerBullet5 > 0)
				{
					this.numberItemPowerBullet5--;
					this.textNumberBullet5.text = "x" + this.numberItemPowerBullet5;
				}
			}
			this.currentItem = itemID;
		}
	}

	public void SelectItemHeart()
	{
		if (GameContext.totalItemPowerUpHeart <= 0)
		{
			ShopBooster.current.ShowPanelShopBooster(ShopBooster.Booster.Heart);
		}
		else if (GameContext.isBonusLife)
		{
			GameContext.isBonusLife = false;
			this.arrItems[3].DeselectItem();
			this.numberHeart++;
		}
		else
		{
			GameContext.isBonusLife = true;
			this.arrItems[3].SelectItem();
			if (this.numberHeart > 0)
			{
				this.numberHeart--;
			}
		}
		this.textNumberHeart.text = "x" + this.numberHeart;
	}

	public void SelectItemShield()
	{
		if (GameContext.totalItemPowerShield <= 0)
		{
			ShopBooster.current.ShowPanelShopBooster(ShopBooster.Booster.Shield);
		}
		else if (GameContext.isBonusShield)
		{
			GameContext.isBonusShield = false;
			this.arrItems[0].DeselectItem();
			this.numberItemShield++;
		}
		else
		{
			GameContext.isBonusShield = true;
			this.arrItems[0].SelectItem();
			if (this.numberItemShield > 0)
			{
				NewTutorial.current.BuyItems_Step7();
				this.numberItemShield--;
			}
		}
		this.textNumberItemShield.text = "x" + this.numberItemShield;
	}

	private void DeselectItem()
	{
		for (int i = 1; i < 3; i++)
		{
			this.arrItems[i].DeselectItem();
		}
	}

	private void DeselectItem2(int itemID)
	{
		if (itemID != 2)
		{
			if (itemID == 3)
			{
				this.numberItemPowerBullet10++;
				this.textNumberBullet10.text = "x" + this.numberItemPowerBullet10;
			}
		}
		else
		{
			this.numberItemPowerBullet5++;
			this.textNumberBullet5.text = "x" + this.numberItemPowerBullet5;
		}
	}

	private void DeselectItemShield()
	{
		GameContext.isBonusShield = false;
		this.arrItems[0].DeselectItem();
		this.numberItemShield++;
		this.textNumberItemShield.text = "x" + this.numberItemShield;
	}

	private void DeselectItemHeart()
	{
		GameContext.isBonusLife = false;
		this.arrItems[3].DeselectItem();
		this.numberHeart++;
		this.textNumberHeart.text = "x" + this.numberHeart;
	}

	private void DeselectItemPower5()
	{
		this.arrItems[1].DeselectItem();
		GameContext.power = 0;
		this.numberItemPowerBullet5++;
		this.textNumberBullet5.text = "x" + this.numberItemPowerBullet5;
	}

	private void DeselectItemPower10()
	{
		this.arrItems[2].DeselectItem();
		GameContext.power = 0;
		this.numberItemPowerBullet10++;
		this.textNumberBullet10.text = "x" + this.numberItemPowerBullet10;
	}

	public void DeselectAllItem()
	{
		if (GameContext.isBonusLife)
		{
			this.DeselectItemHeart();
		}
		if (GameContext.isBonusShield)
		{
			this.DeselectItemShield();
		}
		if (GameContext.power == 5)
		{
			this.DeselectItemPower5();
		}
		if (GameContext.power == 10)
		{
			this.DeselectItemPower10();
		}
	}

	public void BuySkillPlane()
	{
		if (ShopContext.currentCoin >= ShopContext.PRICE_ITEM_POWERUP_SKILL_PLANE)
		{
			CacheGame.MinusCoins(ShopContext.PRICE_ITEM_POWERUP_SKILL_PLANE);
			CurrencyLog.LogGoldOut(ShopContext.PRICE_ITEM_POWERUP_SKILL_PLANE, CurrencyLog.Out.BuySkillPlane, "Buy_Skill_Plane");
			CacheGame.AddSkillPlane(1);
			this.textNumberSkill.text = "x" + GameContext.totalSkillPlane;
			this.SetTextNumberSkillPlaneInGame();
			DOTween.Restart("Special_Skill", true, -1f);
			DOTween.Play("Special_Skill");
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughCoin();
		}
	}

	public void UpdateTextSkillPlane()
	{
		if (this.numberSkillPlane != GameContext.totalSkillPlane)
		{
			this.numberSkillPlane = GameContext.totalSkillPlane;
			this.textNumberSkill.text = "x" + GameContext.totalSkillPlane;
			this.SetTextNumberSkillPlaneInGame();
		}
	}

	public void UpdateNumberShield()
	{
		if (this.numberItemShield != GameContext.totalItemPowerShield)
		{
			this.numberItemShield = GameContext.totalItemPowerShield;
			this.textNumberItemShield.text = "x" + this.numberItemShield;
			this.arrItems[0].CheckItem();
		}
	}

	public void UpdateNumberPower5()
	{
		if (this.numberItemPowerBullet5 != GameContext.totalItemPowerUpBullet5)
		{
			this.numberItemPowerBullet5 = GameContext.totalItemPowerUpBullet5;
			this.textNumberBullet5.text = "x" + this.numberItemPowerBullet5;
			this.arrItems[1].CheckItem();
		}
	}

	public void UpdateNumberPower10()
	{
		if (this.numberItemPowerBullet10 != GameContext.totalItemPowerUpBullet10)
		{
			this.numberItemPowerBullet10 = GameContext.totalItemPowerUpBullet10;
			this.textNumberBullet10.text = "x" + this.numberItemPowerBullet10;
			this.arrItems[2].CheckItem();
		}
	}

	public void UpdateNumberHeart()
	{
		if (this.numberHeart != GameContext.totalItemPowerUpHeart)
		{
			this.numberHeart = GameContext.totalItemPowerUpHeart;
			this.textNumberHeart.text = "x" + this.numberHeart;
			this.arrItems[3].CheckItem();
		}
	}

	private void ShowPopupInfoItemBooster()
	{
		RewardStageManager.current.ShowPanelInfoItemPowerUp();
	}

	public Text textNumberSkillPlaneInGame;

	public Text textNumberSkill;

	public Text textNumberItemShield;

	public Text textNumberBullet5;

	public Text textNumberBullet10;

	public Text textNumberHeart;

	public Text textPriceSkillPlane;

	public Button btnInfoItemBooster;

	public ShopItemPowerUp.ItemPowerUp[] arrItems;

	private int totalCoin;

	private int totalGem;

	private int numberItemShield;

	private int numberItemPowerBullet5;

	private int numberItemPowerBullet10;

	private int numberHeart;

	private int numberSkillPlane;

	private int currentItem = -1;

	[Serializable]
	public class ItemPowerUp
	{
		public void SelectItem()
		{
			this.objActive.SetActive(true);
			this.objInactive.SetActive(false);
			this.imgBG.sprite = this.sprActive;
		}

		public void DeselectItem()
		{
			this.objActive.SetActive(false);
			this.objInactive.SetActive(true);
			this.imgBG.sprite = this.sprInactive;
		}

		public void CheckItem()
		{
			int num = 0;
			switch (this.itemType)
			{
			case ShopBooster.Booster.Shield:
				num = GameContext.totalItemPowerShield;
				break;
			case ShopBooster.Booster.Heart:
				num = GameContext.totalItemPowerUpHeart;
				break;
			case ShopBooster.Booster.Power5:
				num = GameContext.totalItemPowerUpBullet5;
				break;
			case ShopBooster.Booster.Power10:
				num = GameContext.totalItemPowerUpBullet10;
				break;
			}
			if (num > 0)
			{
				this.iconAdd.SetActive(false);
			}
			else
			{
				this.iconAdd.SetActive(true);
			}
		}

		public ShopBooster.Booster itemType;

		public GameObject objActive;

		public GameObject objInactive;

		public GameObject iconAdd;

		public Image imgBG;

		public Sprite sprActive;

		public Sprite sprInactive;
	}
}
