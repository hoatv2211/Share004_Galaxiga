using System;
using DG.Tweening;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemPowerUpEndlessMode : MonoBehaviour
{
	private void Awake()
	{
		ShopItemPowerUpEndlessMode.curent = this;
		this.numberPlayEndlessMode = CacheGame.NumberPlayEndlessModeInDay;
		this.SetText();
		this.CheckAllItemBooster();
	}

	private void CheckAllItemBooster()
	{
		for (int i = 0; i < this.arrItems.Length; i++)
		{
			this.arrItems[i].CheckItem();
		}
	}

	private void SetText()
	{
		this.numberItemShield = GameContext.totalItemPowerShield;
		this.numberItemPowerBullet5 = GameContext.totalItemPowerUpBullet5;
		this.numberItemPowerBullet10 = GameContext.totalItemPowerUpBullet10;
		this.numberHeart = GameContext.totalItemPowerUpHeart;
		this.textPriceSkillPlane.text = string.Empty + ShopContext.PRICE_ITEM_POWERUP_SKILL_PLANE;
		this.SetTextNumberSkillPlaneInGame();
		this.textNumberSkill.text = "x" + GameContext.totalSkillPlane;
		this.textNumberItemShield.text = "x" + this.numberItemShield;
		this.textNumberBullet5.text = "x" + this.numberItemPowerBullet5;
		this.textNumberBullet10.text = "x" + this.numberItemPowerBullet10;
		this.textNumberHeart.text = "x" + this.numberHeart;
		this.textNumberPlayEndlessMode.text = this.maxNumberPlayEndlessMode - this.numberPlayEndlessMode + "/" + this.maxNumberPlayEndlessMode;
	}

	public void UpdateText()
	{
		this.numberItemShield = GameContext.totalItemPowerShield;
		this.numberItemPowerBullet5 = GameContext.totalItemPowerUpBullet5;
		this.numberItemPowerBullet10 = GameContext.totalItemPowerUpBullet10;
		this.numberHeart = GameContext.totalItemPowerUpHeart;
		this.SetTextNumberSkillPlaneInGame();
		this.textNumberSkill.text = "x" + GameContext.totalSkillPlane;
		this.textNumberItemShield.text = "x" + this.numberItemShield;
		this.textNumberBullet5.text = "x" + this.numberItemPowerBullet5;
		this.textNumberBullet10.text = "x" + this.numberItemPowerBullet10;
		this.textNumberHeart.text = "x" + this.numberHeart;
	}

	public void UpdateTextSkillPlane()
	{
		this.SetTextNumberSkillPlaneInGame();
		this.textNumberSkill.text = "x" + GameContext.totalSkillPlane;
	}

	public void SelectItemShield()
	{
		if (GameContext.totalItemPowerShield <= 0)
		{
			this.shopBooster.ShowPanelShopBooster(ShopBooster.Booster.Shield);
			return;
		}
		if (GameContext.isBonusShield)
		{
			GameContext.isBonusShield = false;
			this.arrItems[0].DeselectItem();
			GameContext.isBonusShield = false;
			this.numberItemShield++;
		}
		else
		{
			GameContext.isBonusShield = true;
			this.arrItems[0].SelectItem();
			if (this.numberItemShield > 0)
			{
				this.numberItemShield--;
			}
		}
		this.textNumberItemShield.text = "x" + this.numberItemShield;
	}

	public void SelectItemHeart()
	{
		if (GameContext.totalItemPowerUpHeart <= 0)
		{
			this.shopBooster.ShowPanelShopBooster(ShopBooster.Booster.Heart);
			return;
		}
		if (GameContext.isBonusLife)
		{
			GameContext.isBonusLife = false;
			this.arrItems[1].DeselectItem();
			GameContext.isBonusLife = false;
			this.numberHeart++;
		}
		else
		{
			GameContext.isBonusLife = true;
			this.arrItems[1].SelectItem();
			if (this.numberHeart > 0)
			{
				this.numberHeart--;
			}
		}
		this.textNumberHeart.text = "x" + this.numberHeart;
	}

	public void SelectItemPowerBullet5()
	{
		if (GameContext.totalItemPowerUpBullet5 <= 0)
		{
			this.shopBooster.ShowPanelShopBooster(ShopBooster.Booster.Power5);
			return;
		}
		if (GameContext.power == 10)
		{
			this.arrItems[3].DeselectItem();
			this.numberItemPowerBullet10++;
			this.textNumberBullet10.text = "x" + this.numberItemPowerBullet10;
			this.arrItems[2].SelectItem();
			GameContext.isBonusPower = true;
			GameContext.power = 5;
			this.numberItemPowerBullet5--;
		}
		else if (GameContext.power == 5)
		{
			this.arrItems[2].DeselectItem();
			this.numberItemPowerBullet5++;
			GameContext.isBonusPower = false;
			GameContext.power = 0;
		}
		else
		{
			this.arrItems[2].SelectItem();
			GameContext.isBonusPower = true;
			GameContext.power = 5;
			this.numberItemPowerBullet5--;
		}
		this.textNumberBullet5.text = "x" + this.numberItemPowerBullet5;
	}

	public void SelectItemPowerBullet10()
	{
		if (GameContext.totalItemPowerUpBullet10 <= 0)
		{
			this.shopBooster.ShowPanelShopBooster(ShopBooster.Booster.Power10);
			return;
		}
		if (GameContext.power == 10)
		{
			this.arrItems[3].DeselectItem();
			this.numberItemPowerBullet10++;
			GameContext.isBonusPower = false;
			GameContext.power = 0;
		}
		else if (GameContext.power == 5)
		{
			this.arrItems[2].DeselectItem();
			this.numberItemPowerBullet5++;
			this.textNumberBullet5.text = "x" + this.numberItemPowerBullet5;
			this.arrItems[3].SelectItem();
			GameContext.isBonusPower = true;
			GameContext.power = 10;
			this.numberItemPowerBullet10--;
		}
		else
		{
			this.arrItems[3].SelectItem();
			GameContext.isBonusPower = true;
			GameContext.power = 10;
			this.numberItemPowerBullet10--;
		}
		this.textNumberBullet10.text = "x" + this.numberItemPowerBullet10;
	}

	public void SelectItemSkillPlane()
	{
		InfoItemReward.current.ShowPopupInfoItem(PrizeManager.RewardType.Special_Skill, GameContext.totalSkillPlane);
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

	public void UpdateNumberShield()
	{
		this.numberItemShield = GameContext.totalItemPowerShield;
		this.textNumberItemShield.text = "x" + this.numberItemShield;
		this.arrItems[0].CheckItem();
	}

	public void UpdateNumberHeart()
	{
		this.numberHeart = GameContext.totalItemPowerUpHeart;
		this.textNumberHeart.text = "x" + this.numberHeart;
		this.arrItems[1].CheckItem();
	}

	public void UpdateNumberPower5()
	{
		this.numberItemPowerBullet5 = GameContext.totalItemPowerUpBullet5;
		this.textNumberBullet5.text = "x" + this.numberItemPowerBullet5;
		this.arrItems[2].CheckItem();
	}

	public void UpdateNumberPower10()
	{
		this.numberItemPowerBullet10 = GameContext.totalItemPowerUpBullet10;
		this.textNumberBullet10.text = "x" + this.numberItemPowerBullet10;
		this.arrItems[3].CheckItem();
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
		this.arrItems[1].DeselectItem();
		this.numberHeart++;
		this.textNumberHeart.text = "x" + this.numberHeart;
	}

	private void DeselectItemPower5()
	{
		this.arrItems[2].DeselectItem();
		GameContext.power = 0;
		this.numberItemPowerBullet5++;
		this.textNumberBullet5.text = "x" + this.numberItemPowerBullet5;
	}

	private void DeselectItemPower10()
	{
		this.arrItems[3].DeselectItem();
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
			DOTween.Restart("Special_Skill_Endless", true, -1f);
			this.shopItemPowerUp.UpdateTextSkillPlane();
		}
		else
		{
			ShopManager.Instance.ShowPopupNotEnoughCoin();
		}
	}

	public void PlayEndlessMode()
	{
		if (this.numberPlayEndlessMode < this.maxNumberPlayEndlessMode)
		{
			this.numberPlayEndlessMode++;
			this.textNumberPlayEndlessMode.text = this.maxNumberPlayEndlessMode - this.numberPlayEndlessMode + "/" + this.maxNumberPlayEndlessMode;
			CacheGame.NumberPlayEndlessModeInDay = this.numberPlayEndlessMode;
			if (GameContext.isBonusLife)
			{
				GameContext.currentLive++;
				CacheGame.NumberItemPowerHeart = this.numberHeart;
				GameContext.totalItemPowerUpHeart = this.numberHeart;
			}
			if (GameContext.isBonusShield)
			{
				CacheGame.NumberItemPowerShield = this.numberItemShield;
				GameContext.totalItemPowerShield = this.numberItemShield;
			}
			if (GameContext.isBonusPower)
			{
				int power = GameContext.power;
				if (power != 5)
				{
					if (power == 10)
					{
						CacheGame.NumberItemPowerUpBullet10 = this.numberItemPowerBullet10;
						GameContext.totalItemPowerUpBullet10 = this.numberItemPowerBullet10;
					}
				}
				else
				{
					CacheGame.NumberItemPowerUpBullet5 = this.numberItemPowerBullet5;
					GameContext.totalItemPowerUpBullet5 = this.numberItemPowerBullet5;
				}
			}
			SpaceForceFirebaseLogger.PlayEndlessMode();
			try
			{
				if (GameContext.audioMainMenu != null)
				{
					GameContext.audioMainMenu.Pause();
				}
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log(ex.Message);
			}
			SceneContext.sceneName = "LevelEndLess";
			SelectLevelManager.current.NextToLoadingScene();
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_come_back_tomorrow_to_play);
		}
	}

	public static ShopItemPowerUpEndlessMode curent;

	public ShopBooster shopBooster;

	public ShopItemPowerUp shopItemPowerUp;

	public TextMeshProUGUI textNumberPlayEndlessMode;

	public TextMeshProUGUI textNumberItemShield;

	public TextMeshProUGUI textNumberBullet5;

	public TextMeshProUGUI textNumberBullet10;

	public TextMeshProUGUI textNumberHeart;

	public Text textNumberSkill;

	public Text textNumberSkillPlaneInGame;

	public Text textPriceSkillPlane;

	public ShopItemPowerUp.ItemPowerUp[] arrItems;

	private int numberItemShield;

	private int numberItemPowerBullet5;

	private int numberItemPowerBullet10;

	private int numberHeart;

	private int numberPlayEndlessMode;

	private int maxNumberPlayEndlessMode = 3;

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
