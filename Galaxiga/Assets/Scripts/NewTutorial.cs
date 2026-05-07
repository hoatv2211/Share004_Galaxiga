using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class NewTutorial : MonoBehaviour
{
	public static NewTutorial current
	{
		get
		{
			if (NewTutorial._current == null)
			{
				NewTutorial._current = UnityEngine.Object.FindObjectOfType<NewTutorial>();
			}
			return NewTutorial._current;
		}
	}

	public bool IsSceneHome()
	{
		return this.scene == NewTutorial.Scene.Home;
	}

	public bool IsSceneSelectLevel()
	{
		return this.scene == NewTutorial.Scene.SelectLevel;
	}

	public bool IsSceneLevel()
	{
		return this.scene == NewTutorial.Scene.Level;
	}

	private void Awake()
	{
		if (NewTutorial.isOldUser)
		{
			this.LoadDataFirstGame();
		}
		else
		{
			this.LoadData();
		}
	}

	private void Test()
	{
		this.currentStepTutorial_UseSkillPlane = 7;
		this.currentStepTutorial_BuyItems = 12;
		this.currentStepTutorial_UpgradePlane = 14;
		this.currentStepTutorial_RewardDailyGift = 12;
		this.currentStepTutorial_EquipDrone = 12;
		this.currentStepTutorial_OpenBox = 8;
		this.currentStepTutorial_OpenBossMode = 8;
		this.currentStepTutorial_EvovlePlane = 12;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_UseSkillPlane", this.currentStepTutorial_UseSkillPlane);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_BuyItems", this.currentStepTutorial_BuyItems);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_UpgradePlane", this.currentStepTutorial_UpgradePlane);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_RewardDailyGift", this.currentStepTutorial_RewardDailyGift);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EquipDrone", this.currentStepTutorial_EquipDrone);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_OpenBox", this.currentStepTutorial_OpenBox);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_OpenBossMode", this.currentStepTutorial_OpenBossMode);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EvovlePlane", this.currentStepTutorial_EvovlePlane);
	}

	private void Start()
	{
		this.chatBoxController = ChatBoxController.current;
		this.LoadStep();
	}

	private void LoadDataFirstGame()
	{
		if (GameContext.maxLevelUnlocked > 11)
		{
			this.currentStepTutorial_UseSkillPlane = 7;
			this.currentStepTutorial_BuyItems = 12;
			this.currentStepTutorial_UpgradePlane = 14;
			this.currentStepTutorial_RewardDailyGift = 12;
			this.currentStepTutorial_EquipDrone = 12;
			this.currentStepTutorial_OpenBox = 8;
			this.currentStepTutorial_OpenBossMode = 8;
			this.currentStepTutorial_EvovlePlane = 12;
		}
		else if (GameContext.maxLevelUnlocked > 6)
		{
			this.currentStepTutorial_UseSkillPlane = 7;
			this.currentStepTutorial_BuyItems = 12;
			this.currentStepTutorial_UpgradePlane = 14;
			this.currentStepTutorial_RewardDailyGift = 12;
			this.currentStepTutorial_EquipDrone = 12;
			this.currentStepTutorial_OpenBox = 8;
			this.currentStepTutorial_OpenBossMode = 8;
			this.currentStepTutorial_EvovlePlane = 0;
		}
		else if (GameContext.maxLevelUnlocked > 5)
		{
			this.currentStepTutorial_UseSkillPlane = 7;
			this.currentStepTutorial_BuyItems = 12;
			this.currentStepTutorial_UpgradePlane = 14;
			this.currentStepTutorial_RewardDailyGift = 12;
			this.currentStepTutorial_EquipDrone = 12;
			this.currentStepTutorial_OpenBox = 0;
			this.currentStepTutorial_OpenBossMode = 0;
			this.currentStepTutorial_EvovlePlane = 0;
		}
		else if (GameContext.maxLevelUnlocked > 4)
		{
			this.currentStepTutorial_UseSkillPlane = 7;
			this.currentStepTutorial_BuyItems = 12;
			this.currentStepTutorial_UpgradePlane = 14;
			this.currentStepTutorial_RewardDailyGift = 12;
			this.currentStepTutorial_EquipDrone = 0;
			this.currentStepTutorial_OpenBox = 0;
			this.currentStepTutorial_OpenBossMode = 0;
			this.currentStepTutorial_EvovlePlane = 0;
		}
		else if (GameContext.maxLevelUnlocked > 1)
		{
			this.currentStepTutorial_UseSkillPlane = 7;
			this.currentStepTutorial_BuyItems = 0;
			this.currentStepTutorial_UpgradePlane = 0;
			this.currentStepTutorial_RewardDailyGift = 0;
			this.currentStepTutorial_EquipDrone = 0;
			this.currentStepTutorial_OpenBox = 0;
			this.currentStepTutorial_OpenBossMode = 0;
			this.currentStepTutorial_EvovlePlane = 0;
		}
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_UseSkillPlane", this.currentStepTutorial_UseSkillPlane);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_BuyItems", this.currentStepTutorial_BuyItems);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_UpgradePlane", this.currentStepTutorial_UpgradePlane);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_RewardDailyGift", this.currentStepTutorial_RewardDailyGift);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EquipDrone", this.currentStepTutorial_EquipDrone);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_OpenBox", this.currentStepTutorial_OpenBox);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_OpenBossMode", this.currentStepTutorial_OpenBossMode);
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EvovlePlane", this.currentStepTutorial_EvovlePlane);
		NewTutorial.isOldUser = false;
	}

	private void LoadData()
	{
		this.currentStepTutorial_UseSkillPlane = PlayerPrefs.GetInt("NewTutorialcurrentStepTutorial_UseSkillPlane", 0);
		this.currentStepTutorial_BuyItems = PlayerPrefs.GetInt("NewTutorialcurrentStepTutorial_BuyItems", 0);
		this.currentStepTutorial_UpgradePlane = PlayerPrefs.GetInt("NewTutorialcurrentStepTutorial_UpgradePlane", 0);
		this.currentStepTutorial_RewardDailyGift = PlayerPrefs.GetInt("NewTutorialcurrentStepTutorial_RewardDailyGift", 0);
		this.currentStepTutorial_EquipDrone = PlayerPrefs.GetInt("NewTutorialcurrentStepTutorial_EquipDrone", 0);
		this.currentStepTutorial_OpenBox = PlayerPrefs.GetInt("NewTutorialcurrentStepTutorial_OpenBox", 0);
		this.currentStepTutorial_OpenBossMode = PlayerPrefs.GetInt("NewTutorialcurrentStepTutorial_OpenBossMode", 0);
		this.currentStepTutorial_EvovlePlane = PlayerPrefs.GetInt("NewTutorialcurrentStepTutorial_EvovlePlane", 0);
	}

	private void LoadStep()
	{
		if (this.IsSceneSelectLevel())
		{
			switch (GameContext.maxLevelUnlocked)
			{
			case 1:
				this.SetObjForcus(this.listFxForcus[0], this.posLevel1);
				break;
			case 2:
				this.SetObjForcus(this.listFxForcus[0], this.posLevel2);
				break;
			case 3:
				this.SetObjForcus(this.listFxForcus[0], this.posLevel3);
				break;
			case 4:
				this.SetObjForcus(this.listFxForcus[0], this.posLevel4);
				break;
			case 5:
				this.SetObjForcus(this.listFxForcus[0], this.posLevel5);
				break;
			case 6:
				this.SetObjForcus(this.listFxForcus[0], this.posLevel6);
				break;
			case 7:
				this.SetObjForcus(this.listFxForcus[0], this.posLevel7);
				break;
			case 8:
				this.SetObjForcus(this.listFxForcus[0], this.posLevel8);
				break;
			case 9:
				this.SetObjForcus(this.listFxForcus[0], this.posLevel9);
				break;
			case 10:
				this.SetObjForcus(this.listFxForcus[0], this.posLevel10);
				break;
			}
			if (GameContext.maxLevelUnlocked < 4)
			{
				this.barBottom.SetActive(false);
				this.maskBarBottom.enabled = false;
				this.imgBarBottom.enabled = false;
			}
			else
			{
				this.barBottom.SetActive(true);
				this.maskBarBottom.enabled = true;
				this.imgBarBottom.enabled = true;
			}
			if (GameContext.maxLevelUnlocked >= 7)
			{
				int num = this.currentStepTutorial_OpenBossMode;
				if (num != 0)
				{
					if (num == 2)
					{
						this.OpenBossMode_Step0();
					}
				}
				else
				{
					this.OpenBossMode_Step0();
				}
			}
		}
		else if (this.IsSceneHome())
		{
			if (GameContext.maxLevelUnlocked >= 4)
			{
				int num2 = this.currentStepTutorial_UpgradePlane;
				switch (num2)
				{
				case 0:
					this.UpgradePlane_Step0();
					break;
				default:
					switch (num2)
					{
					case 8:
						this.UpgradePlane_Step8();
						break;
					case 10:
						this.currentStepTutorial_UpgradePlane = 14;
						PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_UpgradePlane", this.currentStepTutorial_UpgradePlane);
						break;
					}
					break;
				case 2:
					this.UpgradePlane_Step2();
					break;
				}
			}
			if (this.currentStepTutorial_UpgradePlane >= 2 && GameContext.maxLevelUnlocked >= 4)
			{
				int num3 = this.currentStepTutorial_RewardDailyGift;
				switch (num3)
				{
				case 4:
					this.RewardDailyGift_Step4();
					break;
				default:
					if (num3 != 0)
					{
						if (num3 == 10)
						{
							this.RewardDailyGift_Step10();
						}
					}
					else
					{
						this.RewardDailyGift_Step0();
					}
					break;
				case 6:
					this.RewardDailyGift_Step6();
					break;
				}
			}
			if (this.currentStepTutorial_UpgradePlane >= 2 && GameContext.maxLevelUnlocked >= 5)
			{
				if (!CacheGame.IsOwnedDrone(GameContext.Drone.GatlingGun))
				{
					int num4 = this.currentStepTutorial_EquipDrone;
					switch (num4)
					{
					case 0:
						this.EquipDrone_Step0();
						break;
					default:
						switch (num4)
						{
						case 6:
							this.EquipDrone_Step6();
							break;
						case 8:
							this.EquipDrone_Step8();
							break;
						}
						break;
					case 2:
						this.EquipDrone_Step2();
						break;
					}
				}
				else if (this.currentStepTutorial_EquipDrone <= 10)
				{
					this.currentStepTutorial_EquipDrone = 10;
					PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EquipDrone", this.currentStepTutorial_EquipDrone);
				}
			}
			if (this.currentStepTutorial_EquipDrone >= 2 && GameContext.maxLevelUnlocked >= 6)
			{
				switch (this.currentStepTutorial_OpenBox)
				{
				case 0:
					this.OpenBox_Step0();
					break;
				case 2:
					this.OpenBox_Step2();
					break;
				case 4:
					this.OpenBox_Step4();
					break;
				case 6:
					this.OpenBox_Step6();
					break;
				}
			}
			if (this.currentStepTutorial_OpenBox >= 2 && GameContext.maxLevelUnlocked >= 11)
			{
				int num5 = this.currentStepTutorial_EvovlePlane;
				if (num5 != 0)
				{
					if (num5 == 2)
					{
						this.EvovlePlane_Step2();
					}
				}
				else
				{
					this.EvovlePlane_Step0();
				}
			}
		}
	}

	private void SetObjForcus(GameObject obj, Transform transform)
	{
		obj.transform.parent = transform;
		obj.transform.localPosition = Vector3.zero;
		obj.transform.localScale = new Vector3(1f, 1f, 1f);
		if (!obj.activeInHierarchy)
		{
			obj.SetActive(true);
		}
	}

	public void HideObjForcus()
	{
		for (int i = 0; i < this.listFxForcus.Length; i++)
		{
			this.listFxForcus[i].transform.parent = null;
			this.listFxForcus[i].SetActive(false);
		}
	}

	public void UseSkillPlane_Step0()
	{
		if (this.currentStepTutorial_UseSkillPlane != 0)
		{
			return;
		}
		this.chatBoxController.ShowChatBox(0, true);
		this.currentStepTutorial_UseSkillPlane = 1;
	}

	public void UseSkillPlane_Step1()
	{
		if (this.currentStepTutorial_UseSkillPlane != 1)
		{
			return;
		}
		this.chatBoxController.NewChangeText(1);
		this.currentStepTutorial_UseSkillPlane = 2;
	}

	public void UseSkillPlane_Step2()
	{
		if (this.currentStepTutorial_UseSkillPlane != 2)
		{
			return;
		}
		this.chatBoxController.HideChatBox();
		this.currentStepTutorial_UseSkillPlane = 3;
	}

	public void UseSkillPlane_Step3()
	{
		if (this.currentStepTutorial_UseSkillPlane != 3)
		{
			return;
		}
		UIGameManager.current.ShowButtonSkillPlane();
		this.chatBoxController.ShowChatBox(2, true);
		this.SetObjForcus(this.listFxForcus[0], this.posBtnSkillPlane);
		this.currentStepTutorial_UseSkillPlane = 4;
	}

	public void UseSkillPlane_Step4()
	{
		if (this.currentStepTutorial_UseSkillPlane != 4)
		{
			return;
		}
		this.HideObjForcus();
		this.chatBoxController.HideChatBox();
		this.currentStepTutorial_UseSkillPlane = 5;
	}

	public void UseSkillPlane_Step5()
	{
		if (this.currentStepTutorial_UseSkillPlane != 5)
		{
			return;
		}
		this.chatBoxController.ShowChatBox(3, true);
		this.currentStepTutorial_UseSkillPlane = 6;
	}

	public void UseSkillPlane_Step6()
	{
		if (this.currentStepTutorial_UseSkillPlane != 6)
		{
			return;
		}
		this.chatBoxController.HideChatBox();
		this.currentStepTutorial_UseSkillPlane = 7;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_UseSkillPlane", this.currentStepTutorial_UseSkillPlane);
		if (NewTutorial.isOldUser)
		{
			UnityEngine.Debug.LogError("isOldUser: " + GameContext.maxLevelUnlocked);
			this.LoadDataFirstGame();
		}
		else
		{
			UnityEngine.Debug.LogError("isNewUser: " + GameContext.maxLevelUnlocked);
		}
	}

	public void BuyItems_Step0()
	{
		if (this.currentStepTutorial_BuyItems != 0)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnItemShield);
		this.chatBoxController.ShowChatBox(4, true);
		this.currentStepTutorial_BuyItems = 1;
	}

	public void BuyItems_Step1()
	{
		if (this.currentStepTutorial_BuyItems != 1)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_BuyItems = 2;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_BuyItems", this.currentStepTutorial_BuyItems);
		this.BuyItems_Step2();
	}

	public void BuyItems_Step2()
	{
		if (this.currentStepTutorial_BuyItems != 2)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnBuyItems);
		this.currentStepTutorial_BuyItems = 3;
	}

	public void BuyItems_Step3()
	{
		if (this.currentStepTutorial_BuyItems != 3)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_BuyItems = 4;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_BuyItems", this.currentStepTutorial_BuyItems);
		this.BuyItems_Step4();
	}

	public void BuyItems_Step4()
	{
		if (this.currentStepTutorial_BuyItems != 4)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnExitBuyItems);
		this.currentStepTutorial_BuyItems = 5;
	}

	public void BuyItems_Step5()
	{
		if (this.currentStepTutorial_BuyItems != 5)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_BuyItems = 6;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_BuyItems", this.currentStepTutorial_BuyItems);
		this.BuyItems_Step6();
	}

	public void BuyItems_Step6()
	{
		if (this.currentStepTutorial_BuyItems != 6)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnItemShield);
		this.currentStepTutorial_BuyItems = 7;
	}

	public void BuyItems_Step7()
	{
		if (this.currentStepTutorial_BuyItems != 7)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_BuyItems = 8;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_BuyItems", this.currentStepTutorial_BuyItems);
		this.BuyItems_Step8();
	}

	public void BuyItems_Step8()
	{
		this.chatBoxController.HideChatBox();
		this.ShowForcusBtnPlayGame();
		this.currentStepTutorial_BuyItems = 9;
	}

	public void BuyItems_Step9()
	{
		if (this.currentStepTutorial_BuyItems != 9)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_BuyItems = 10;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_BuyItems", this.currentStepTutorial_BuyItems);
	}

	public void BuyItems_Step10()
	{
		if (this.currentStepTutorial_BuyItems != 10)
		{
			return;
		}
		this.chatBoxController.ShowChatBox(5, true);
		this.SetObjForcus(this.listFxForcus[1], this.posBtnSkillShield);
		this.currentStepTutorial_BuyItems = 11;
	}

	public void BuyItems_Step11()
	{
		if (this.currentStepTutorial_BuyItems != 11)
		{
			return;
		}
		this.HideObjForcus();
		this.chatBoxController.HideChatBox();
		this.currentStepTutorial_BuyItems = 12;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_BuyItems", this.currentStepTutorial_BuyItems);
	}

	public void UpgradePlane_Step0()
	{
		if (this.currentStepTutorial_UpgradePlane != 0)
		{
			return;
		}
		this.popupReward.SetActive(true);
		this.giftPlane.SetActive(true);
		this.giftDrone.SetActive(false);
		this.giftBox.SetActive(false);
		this.giftCard.SetActive(false);
		this.SetObjForcus(this.listFxForcus[1], this.posBtnClaimReward);
		this.currentStepTutorial_UpgradePlane = 1;
	}

	public void UpgradePlane_Step1()
	{
		if (this.currentStepTutorial_UpgradePlane != 1)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_UpgradePlane = 2;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_UpgradePlane", this.currentStepTutorial_UpgradePlane);
		this.UpgradePlane_Step2();
	}

	public void UpgradePlane_Step2()
	{
		if (this.currentStepTutorial_UpgradePlane != 2)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posMainPlane);
		this.currentStepTutorial_UpgradePlane = 3;
	}

	public void UpgradePlane_Step3()
	{
		if (this.currentStepTutorial_UpgradePlane != 3)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_UpgradePlane = 4;
		this.UpgradePlane_Step4();
	}

	public void UpgradePlane_Step4()
	{
		if (this.currentStepTutorial_UpgradePlane != 4)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posFuryPlane);
		this.currentStepTutorial_UpgradePlane = 5;
	}

	public void UpgradePlane_Step5()
	{
		if (this.currentStepTutorial_UpgradePlane != 5)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_UpgradePlane = 6;
		this.UpgradePlane_Step6();
	}

	public void UpgradePlane_Step6()
	{
		if (this.currentStepTutorial_UpgradePlane != 6)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnSwitch);
		this.currentStepTutorial_UpgradePlane = 7;
	}

	public void UpgradePlane_Step7()
	{
		if (this.currentStepTutorial_UpgradePlane != 7)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_UpgradePlane = 8;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_UpgradePlane", this.currentStepTutorial_UpgradePlane);
		this.UpgradePlane_Step8();
	}

	public void UpgradePlane_Step8()
	{
		if (this.currentStepTutorial_UpgradePlane != 8)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnUpgrade);
		this.currentStepTutorial_UpgradePlane = 9;
	}

	public void UpgradePlane_Step9()
	{
		if (this.currentStepTutorial_UpgradePlane != 9)
		{
			return;
		}
		this.numberClickBtnUpgrade++;
		if (this.numberClickBtnUpgrade >= 2)
		{
			this.HideObjForcus();
			this.currentStepTutorial_UpgradePlane = 10;
			PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_UpgradePlane", this.currentStepTutorial_UpgradePlane);
			this.UpgradePlane_Step10();
		}
	}

	public void UpgradePlane_Step10()
	{
		if (this.currentStepTutorial_UpgradePlane != 10)
		{
			return;
		}
		this.chatBoxController.ShowChatBox(6, true);
		this.currentStepTutorial_UpgradePlane = 11;
	}

	public void UpgradePlane_Step11()
	{
		if (this.currentStepTutorial_UpgradePlane != 11)
		{
			return;
		}
		this.chatBoxController.HideChatBox();
		this.currentStepTutorial_UpgradePlane = 12;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_UpgradePlane", this.currentStepTutorial_UpgradePlane);
		this.UpgradePlane_Step12();
	}

	public void UpgradePlane_Step12()
	{
		if (this.currentStepTutorial_UpgradePlane != 12)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnBack);
		this.currentStepTutorial_UpgradePlane = 13;
	}

	public void UpgradePlane_Step13()
	{
		if (this.currentStepTutorial_UpgradePlane != 13)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_UpgradePlane = 14;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_UpgradePlane", this.currentStepTutorial_UpgradePlane);
		this.RewardDailyGift_Step0();
	}

	public void RewardDailyGift_Step0()
	{
		if (this.currentStepTutorial_RewardDailyGift != 0)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnQuest);
		this.currentStepTutorial_RewardDailyGift = 1;
	}

	public void RewardDailyGift_Step1()
	{
		if (this.currentStepTutorial_RewardDailyGift != 1)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_RewardDailyGift = 2;
		this.RewardDailyGift_Step2();
	}

	public void RewardDailyGift_Step2()
	{
		if (this.currentStepTutorial_RewardDailyGift != 2)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnClaimQuest);
		this.currentStepTutorial_RewardDailyGift = 3;
	}

	public void RewardDailyGift_Step3()
	{
		if (this.currentStepTutorial_RewardDailyGift != 3)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_RewardDailyGift = 4;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_RewardDailyGift", this.currentStepTutorial_RewardDailyGift);
		this.RewardDailyGift_Step4();
	}

	public void RewardDailyGift_Step4()
	{
		if (this.currentStepTutorial_RewardDailyGift != 4)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnDaily);
		this.currentStepTutorial_RewardDailyGift = 5;
	}

	public void RewardDailyGift_Step5()
	{
		if (this.currentStepTutorial_RewardDailyGift != 5)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_RewardDailyGift = 6;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_RewardDailyGift", this.currentStepTutorial_RewardDailyGift);
		this.RewardDailyGift_Step6();
	}

	public void RewardDailyGift_Step6()
	{
		if (this.currentStepTutorial_RewardDailyGift != 6)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnClaimDaily);
		this.currentStepTutorial_RewardDailyGift = 7;
	}

	public void RewardDailyGift_Step7()
	{
		if (this.currentStepTutorial_RewardDailyGift != 7)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_RewardDailyGift = 8;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_RewardDailyGift", this.currentStepTutorial_RewardDailyGift);
		this.RewardDailyGift_Step8();
	}

	public void RewardDailyGift_Step8()
	{
		if (this.currentStepTutorial_RewardDailyGift != 8)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnExit);
		this.currentStepTutorial_RewardDailyGift = 9;
	}

	public void RewardDailyGift_Step9()
	{
		if (this.currentStepTutorial_RewardDailyGift != 9)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_RewardDailyGift = 10;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_RewardDailyGift", this.currentStepTutorial_RewardDailyGift);
		this.RewardDailyGift_Step10();
	}

	public void RewardDailyGift_Step10()
	{
		if (this.currentStepTutorial_RewardDailyGift != 10)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnTabMainHome);
		this.currentStepTutorial_RewardDailyGift = 11;
	}

	public void RewardDailyGift_Step11()
	{
		if (this.currentStepTutorial_RewardDailyGift != 11)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_RewardDailyGift = 12;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_RewardDailyGift", this.currentStepTutorial_RewardDailyGift);
		if (GameContext.maxLevelUnlocked >= 5)
		{
			this.EquipDrone_Step0();
		}
		else
		{
			this.ShowForcusBtnStartGame();
		}
	}

	public void EquipDrone_Step0()
	{
		if (this.currentStepTutorial_EquipDrone != 0)
		{
			return;
		}
		this.popupReward.SetActive(true);
		this.giftPlane.SetActive(false);
		this.giftDrone.SetActive(true);
		this.giftBox.SetActive(false);
		this.giftCard.SetActive(false);
		this.SetObjForcus(this.listFxForcus[1], this.posBtnClaimReward);
		this.currentStepTutorial_EquipDrone = 1;
	}

	public void EquipDrone_Step1()
	{
		if (this.currentStepTutorial_EquipDrone != 1)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_EquipDrone = 2;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EquipDrone", this.currentStepTutorial_EquipDrone);
		this.EquipDrone_Step2();
	}

	public void EquipDrone_Step2()
	{
		if (this.currentStepTutorial_EquipDrone != 2)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posDrone);
		this.currentStepTutorial_EquipDrone = 3;
	}

	public void EquipDrone_Step3()
	{
		if (this.currentStepTutorial_EquipDrone != 3)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_EquipDrone = 4;
		this.EquipDrone_Step4();
	}

	public void EquipDrone_Step4()
	{
		if (this.currentStepTutorial_EquipDrone != 4)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnEquip);
		this.currentStepTutorial_EquipDrone = 5;
	}

	public void EquipDrone_Step5()
	{
		if (this.currentStepTutorial_EquipDrone != 5)
		{
			return;
		}
		this.currentStepTutorial_EquipDrone = 6;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EquipDrone", this.currentStepTutorial_EquipDrone);
		this.EquipDrone_Step6();
	}

	public void EquipDrone_Step6()
	{
		if (this.currentStepTutorial_EquipDrone != 6)
		{
			return;
		}
		this.chatBoxController.ShowChatBox(7, true);
		this.currentStepTutorial_EquipDrone = 7;
	}

	public void EquipDrone_Step7()
	{
		if (this.currentStepTutorial_EquipDrone != 7)
		{
			return;
		}
		this.chatBoxController.HideChatBox();
		this.currentStepTutorial_EquipDrone = 8;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EquipDrone", this.currentStepTutorial_EquipDrone);
		this.EquipDrone_Step8();
	}

	public void EquipDrone_Step8()
	{
		if (this.currentStepTutorial_EquipDrone != 8)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnBack);
		this.currentStepTutorial_EquipDrone = 9;
	}

	public void EquipDrone_Step9()
	{
		if (this.currentStepTutorial_EquipDrone != 9)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_EquipDrone = 10;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EquipDrone", this.currentStepTutorial_EquipDrone);
		if (GameContext.maxLevelUnlocked >= 6)
		{
			this.OpenBox_Step0();
		}
		else
		{
			this.ShowForcusBtnStartGame();
		}
	}

	public void EquipDrone_Step10()
	{
		if (this.currentStepTutorial_EquipDrone != 10)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnSkillDrone);
		this.chatBoxController.ShowChatBox(8, true);
		this.currentStepTutorial_EquipDrone = 11;
	}

	public void EquipDrone_Step11()
	{
		if (this.currentStepTutorial_EquipDrone != 11)
		{
			return;
		}
		this.currentStepTutorial_EquipDrone = 12;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EquipDrone", this.currentStepTutorial_EquipDrone);
		this.HideObjForcus();
		this.chatBoxController.HideChatBox();
	}

	public void OpenBox_Step0()
	{
		if (this.currentStepTutorial_OpenBox != 0)
		{
			return;
		}
		this.popupReward.SetActive(true);
		this.giftPlane.SetActive(false);
		this.giftDrone.SetActive(false);
		this.giftBox.SetActive(true);
		this.giftCard.SetActive(false);
		this.SetObjForcus(this.listFxForcus[1], this.posBtnClaimReward);
		this.currentStepTutorial_OpenBox = 1;
	}

	public void OpenBox_Step1()
	{
		if (this.currentStepTutorial_OpenBox != 1)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_OpenBox = 2;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_OpenBox", this.currentStepTutorial_OpenBox);
		this.OpenBox_Step2();
	}

	public void OpenBox_Step2()
	{
		if (this.currentStepTutorial_OpenBox != 2)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnLuckyBox);
		this.currentStepTutorial_OpenBox = 3;
	}

	public void OpenBox_Step3()
	{
		if (this.currentStepTutorial_OpenBox != 3)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_OpenBox = 4;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_OpenBox", this.currentStepTutorial_OpenBox);
		this.OpenBox_Step4();
	}

	public void OpenBox_Step4()
	{
		if (this.currentStepTutorial_OpenBox != 4)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnOpenLuckyBox);
		this.currentStepTutorial_OpenBox = 5;
	}

	public void OpenBox_Step5()
	{
		if (this.currentStepTutorial_OpenBox != 5)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_OpenBox = 6;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_OpenBox", this.currentStepTutorial_OpenBox);
		this.OpenBox_Step6();
	}

	public void OpenBox_Step6()
	{
		if (this.currentStepTutorial_OpenBox != 6)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnBackLuckyBox);
		this.currentStepTutorial_OpenBox = 7;
	}

	public void OpenBox_Step7()
	{
		if (this.currentStepTutorial_OpenBox != 7)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_OpenBox = 8;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_OpenBox", this.currentStepTutorial_OpenBox);
		this.ShowForcusBtnStartGame();
		if (GameContext.maxLevelUnlocked >= 11)
		{
			this.EvovlePlane_Step0();
		}
		else
		{
			this.ShowForcusBtnStartGame();
		}
	}

	public void OpenBossMode_Step0()
	{
		this.SetObjForcus(this.listFxForcus[0], this.posBtnBossMode);
		this.currentStepTutorial_OpenBossMode = 1;
	}

	public void OpenBossMode_Step1()
	{
		this.HideObjForcus();
		this.currentStepTutorial_OpenBossMode = 2;
		this.OpenBossMode_Step2();
	}

	public void OpenBossMode_Step2()
	{
		if (this.currentStepTutorial_OpenBossMode != 2)
		{
			return;
		}
		this.chatBoxController.ShowChatBox(9, true);
		this.currentStepTutorial_OpenBossMode = 3;
	}

	public void OpenBossMode_Step3()
	{
		if (this.currentStepTutorial_OpenBossMode != 3)
		{
			return;
		}
		this.chatBoxController.HideChatBox();
		this.currentStepTutorial_OpenBossMode = 4;
		this.OpenBossMode_Step4();
	}

	public void OpenBossMode_Step4()
	{
		this.SetObjForcus(this.listFxForcus[0], this.posBtnBossModeLevel1);
		this.currentStepTutorial_OpenBossMode = 5;
	}

	public void OpenBossMode_Step5()
	{
		this.HideObjForcus();
		this.currentStepTutorial_OpenBossMode = 6;
		this.OpenBossMode_Step6();
	}

	public void OpenBossMode_Step6()
	{
		this.SetObjForcus(this.listFxForcus[0], this.posBtnPlayBossMode);
		this.currentStepTutorial_OpenBossMode = 7;
	}

	public void OpenBossMode_Step7()
	{
		this.HideObjForcus();
		this.currentStepTutorial_OpenBossMode = 8;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_OpenBossMode", this.currentStepTutorial_OpenBossMode);
	}

	public void EvovlePlane_Step0()
	{
		if (this.currentStepTutorial_EvovlePlane != 0)
		{
			return;
		}
		this.popupReward.SetActive(true);
		this.giftPlane.SetActive(false);
		this.giftDrone.SetActive(false);
		this.giftBox.SetActive(false);
		this.giftCard.SetActive(true);
		this.SetObjForcus(this.listFxForcus[1], this.posBtnClaimReward);
		this.currentStepTutorial_EvovlePlane = 1;
	}

	public void EvovlePlane_Step1()
	{
		if (this.currentStepTutorial_EvovlePlane != 1)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_EvovlePlane = 2;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EvovlePlane", this.currentStepTutorial_EvovlePlane);
		if (DataGame.Current.IndexRankPlane(GameContext.Plane.FuryOfAres) == 1)
		{
			this.EvovlePlane_Step2();
		}
		else
		{
			this.currentStepTutorial_EvovlePlane = 12;
			PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EvovlePlane", this.currentStepTutorial_EvovlePlane);
		}
	}

	public void EvovlePlane_Step2()
	{
		if (this.currentStepTutorial_EvovlePlane != 2)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posMainPlane);
		this.currentStepTutorial_EvovlePlane = 3;
	}

	public void EvovlePlane_Step3()
	{
		if (this.currentStepTutorial_EvovlePlane != 3)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_EvovlePlane = 4;
		this.EvovlePlane_Step4();
	}

	public void EvovlePlane_Step4()
	{
		if (this.currentStepTutorial_EvovlePlane != 4)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnPlus);
		this.currentStepTutorial_EvovlePlane = 5;
	}

	public void EvovlePlane_Step5()
	{
		if (this.currentStepTutorial_EvovlePlane != 5)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_EvovlePlane = 6;
		this.EvovlePlane_Step6();
	}

	public void EvovlePlane_Step6()
	{
		if (this.currentStepTutorial_EvovlePlane != 6)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnEvovle);
		this.currentStepTutorial_EvovlePlane = 7;
	}

	public void EvovlePlane_Step7()
	{
		if (this.currentStepTutorial_EvovlePlane != 7)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_EvovlePlane = 8;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EvovlePlane", this.currentStepTutorial_EvovlePlane);
		this.EvovlePlane_Step8();
	}

	public void EvovlePlane_Step8()
	{
		if (this.currentStepTutorial_EvovlePlane != 8)
		{
			return;
		}
		this.chatBoxController.ShowChatBox(10, true);
		this.currentStepTutorial_EvovlePlane = 9;
	}

	public void EvovlePlane_Step9()
	{
		if (this.currentStepTutorial_EvovlePlane != 9)
		{
			return;
		}
		this.chatBoxController.HideChatBox();
		this.currentStepTutorial_EvovlePlane = 10;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EvovlePlane", this.currentStepTutorial_EvovlePlane);
		this.EvovlePlane_Step10();
	}

	public void EvovlePlane_Step10()
	{
		if (this.currentStepTutorial_EvovlePlane != 10)
		{
			return;
		}
		this.SetObjForcus(this.listFxForcus[1], this.posBtnBackEvovle);
		this.currentStepTutorial_EvovlePlane = 11;
	}

	public void EvovlePlane_Step11()
	{
		if (this.currentStepTutorial_EvovlePlane != 11)
		{
			return;
		}
		this.HideObjForcus();
		this.currentStepTutorial_EvovlePlane = 12;
		PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_EvovlePlane", this.currentStepTutorial_EvovlePlane);
		this.ShowForcusBtnStartGame();
	}

	public void ShowForcusBtnNextStage()
	{
		this.SetObjForcus(this.listFxForcus[1], this.posBtnNextStage);
	}

	public void ShowForcusBtnPlayGame()
	{
		this.SetObjForcus(this.listFxForcus[1], this.posBtnPlayGame);
	}

	public void ShowForcusBtnRestart()
	{
		this.SetObjForcus(this.listFxForcus[1], this.posBtnRestart);
	}

	public void ShowForcusBtnTakeGift()
	{
		this.SetObjForcus(this.listFxForcus[1], this.posBtnTakeGiftTutorial);
	}

	public void ShowForcusBtnStartGame()
	{
		this.SetObjForcus(this.listFxForcus[1], this.posBtnStartGame);
	}

	public static bool isOldUser;

	public static NewTutorial _current;

	[SerializeField]
	private ChatBoxController chatBoxController;

	[SerializeField]
	private GameObject[] listFxForcus;

	public NewTutorial.Scene scene;

	public int currentStepTutorial_UseSkillPlane;

	public int currentStepTutorial_BuyItems;

	public int currentStepTutorial_UpgradePlane;

	public int currentStepTutorial_RewardDailyGift;

	public int currentStepTutorial_EquipDrone;

	public int currentStepTutorial_OpenBox;

	public int currentStepTutorial_OpenBossMode;

	public int currentStepTutorial_EvovlePlane;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private GameObject popupReward;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private GameObject giftPlane;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private GameObject giftDrone;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private GameObject giftBox;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private GameObject giftCard;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnClaimReward;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnStartGame;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posMainPlane;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posFuryPlane;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnSwitch;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnUpgrade;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnBack;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posDrone;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnEquip;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnLuckyBox;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnOpenLuckyBox;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnBackLuckyBox;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnEvovle;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnPlus;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnBackEvovle;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnQuest;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnClaimQuest;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnDaily;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnClaimDaily;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnExit;

	[ShowIf("IsSceneHome", true)]
	[SerializeField]
	private Transform posBtnTabMainHome;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posBtnNextStage;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posBtnPlayGame;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posBtnRestart;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posLevel1;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posLevel2;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posLevel3;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posLevel4;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posLevel5;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posLevel6;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posLevel7;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posLevel8;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posLevel9;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posLevel10;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posBtnItemShield;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posBtnBuyItems;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posBtnExitBuyItems;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posBtnTakeGiftTutorial;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posBtnBossMode;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posBtnBossModeLevel1;

	[ShowIf("IsSceneSelectLevel", true)]
	[SerializeField]
	private Transform posBtnPlayBossMode;

	[ShowIf("IsSceneSelectLevel", true)]
	public GameObject barBottom;

	[ShowIf("IsSceneSelectLevel", true)]
	public Mask maskBarBottom;

	[ShowIf("IsSceneSelectLevel", true)]
	public Image imgBarBottom;

	[ShowIf("IsSceneLevel", true)]
	[SerializeField]
	private Transform posBtnSkillPlane;

	[ShowIf("IsSceneLevel", true)]
	[SerializeField]
	private Transform posBtnSkillShield;

	[ShowIf("IsSceneLevel", true)]
	[SerializeField]
	private Transform posBtnSkillDrone;

	private int numberClickBtnUpgrade;

	public enum Scene
	{
		Home,
		SelectLevel,
		Level
	}
}
