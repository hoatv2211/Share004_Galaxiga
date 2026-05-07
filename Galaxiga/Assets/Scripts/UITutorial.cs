using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UITutorial : MonoBehaviour
{
	public static UITutorial current
	{
		get
		{
			if (UITutorial._current == null)
			{
				UITutorial._current = UnityEngine.Object.FindObjectOfType<UITutorial>();
			}
			return UITutorial._current;
		}
	}

	private void Start()
	{
		if (!UITutorial.isTurnOnTutorial)
		{
			this.currentStepTutorial = PlayerPrefs.GetInt("UITutorialcurrentStepTutorial", 0);
			this.currentStepTutorialBossMode = PlayerPrefs.GetInt("UITutorialcurrentStepTutorialBossMode", 0);
			this.currentStepTutorialDaily = PlayerPrefs.GetInt("UITutorialcurrentStepTutorialDaily", 0);
			this.currentStepTutorialUseItems = PlayerPrefs.GetInt("UITutorialcurrentStepTutorialUseItems", 0);
			if (this.IsSceneSelectLevel())
			{
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
				int num = this.currentStepTutorial;
				switch (num)
				{
				case 5:
					if (GameContext.maxLevelUnlocked > 1)
					{
						this.Step5();
					}
					break;
				default:
					switch (num)
					{
					case 25:
						if (GameContext.maxLevelUnlocked > 4)
						{
							this.ForcusBtnBack();
						}
						break;
					case 26:
						if (GameContext.maxLevelUnlocked > 4)
						{
							this.ForcusBtnBack();
						}
						break;
					case 27:
						if (GameContext.maxLevelUnlocked > 4)
						{
							this.ForcusBtnBack();
						}
						break;
					default:
						switch (num)
						{
						case 43:
							MonoBehaviour.print("GameContext.maxLevelUnlocked: " + GameContext.maxLevelUnlocked);
							if (GameContext.maxLevelUnlocked > 10)
							{
								this.ForcusBtnBack();
							}
							break;
						case 44:
							if (GameContext.maxLevelUnlocked > 10)
							{
								this.ForcusBtnBack();
							}
							break;
						case 45:
							if (GameContext.maxLevelUnlocked > 10)
							{
								this.ForcusBtnBack();
							}
							break;
						default:
							if (num == 37)
							{
								if (GameContext.maxLevelUnlocked > 5)
								{
									this.ForcusBtnBack();
								}
							}
							break;
						}
						break;
					}
					break;
				case 7:
					if (GameContext.maxLevelUnlocked > 2)
					{
						this.Step7();
					}
					break;
				case 9:
					if (GameContext.maxLevelUnlocked > 3)
					{
						this.Step9();
					}
					break;
				case 11:
					if (GameContext.maxLevelUnlocked > 3)
					{
						this.Step11();
					}
					break;
				}
				if (GameContext.maxLevelUnlocked > 5)
				{
					switch (this.currentStepTutorialBossMode)
					{
					case 0:
						this.BM_Step0();
						break;
					case 1:
						this.BM_Step0();
						break;
					case 2:
						this.BM_Step2();
						break;
					case 3:
						this.BM_Step2();
						break;
					case 4:
						this.BM_Step4();
						break;
					case 5:
						this.BM_Step4();
						break;
					case 6:
						this.BM_Step6();
						break;
					case 7:
						this.BM_Step6();
						break;
					}
				}
			}
			else if (this.IsSceneHome())
			{
				if (GameContext.maxLevelUnlocked > 3 && this.currentStepTutorial < 25)
				{
					switch (this.currentStepTutorial)
					{
					case 11:
						this.Step13();
						break;
					case 12:
						this.Step13();
						break;
					case 13:
						this.Step13();
						break;
					case 15:
						this.Step15();
						break;
					case 17:
						this.Step17();
						break;
					case 19:
						this.Step19();
						break;
					case 21:
						this.Step21();
						break;
					case 23:
						this.Step23();
						break;
					}
				}
				else if (GameContext.maxLevelUnlocked > 4 && this.currentStepTutorial < 35)
				{
					switch (this.currentStepTutorial)
					{
					case 25:
						this.Step27();
						break;
					case 26:
						this.Step27();
						break;
					case 27:
						this.Step27();
						break;
					case 29:
						this.Step29();
						break;
					case 31:
						this.Step31();
						break;
					}
				}
				else if (GameContext.maxLevelUnlocked > 5 && this.currentStepTutorial < 43)
				{
					switch (this.currentStepTutorial)
					{
					case 35:
						this.Step37();
						break;
					case 36:
						this.Step37();
						break;
					case 37:
						this.Step37();
						break;
					case 39:
						this.Step39();
						break;
					case 41:
						this.Step41();
						break;
					}
				}
				else if (GameContext.maxLevelUnlocked > 10)
				{
					switch (this.currentStepTutorial)
					{
					case 43:
						this.Step45();
						break;
					case 44:
						this.Step45();
						break;
					case 45:
						this.Step45();
						break;
					case 47:
						this.Step47();
						break;
					case 49:
						this.Step49();
						break;
					case 51:
						this.Step51();
						break;
					}
				}
				if (this.currentStepTutorial >= 27)
				{
					switch (this.currentStepTutorialDaily)
					{
					case 0:
						this.Daily_Step0();
						break;
					case 2:
						this.Daily_Step2();
						break;
					case 4:
						this.Daily_Step4();
						break;
					case 6:
						this.Daily_Step6();
						break;
					}
				}
			}
		}
		else
		{
			this.currentStepTutorial = 55;
			this.currentStepTutorialBossMode = 8;
			this.currentStepTutorialDaily = 14;
			PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
			PlayerPrefs.SetInt("UITutorialcurrentStepTutorialBossMode", this.currentStepTutorialBossMode);
			this.currentStepTutorialDaily = PlayerPrefs.GetInt("UITutorialcurrentStepTutorialDaily", 0);
		}
	}

	public bool IsSceneHome()
	{
		return this.scene == UITutorial.Scene.Home;
	}

	public bool IsSceneSelectLevel()
	{
		return this.scene == UITutorial.Scene.Select_Level;
	}

	public bool IsSceneLevel1()
	{
		return this.scene == UITutorial.Scene.Level1;
	}

	public void ShowChatBox(int idMess, bool isActiveBtnNext = true)
	{
		if (!this.isShowChatBox)
		{
			this.isShowChatBox = true;
			this.btnNextStep.SetActive(isActiveBtnNext);
			this.popupChatBox.SetActive(true);
			this.animPopupChatBox.SetTrigger("animShow");
			this.txtChatBox.text = this.tutorialData.mess[idMess];
		}
	}

	public void HideChatBox()
	{
		if (this.isShowChatBox)
		{
			this.isShowChatBox = false;
			this.animPopupChatBox.SetTrigger("animHide");
		}
	}

	public void ChangeText(int idMess)
	{
		PopupChatBox.current.idMess = idMess;
		this.animPopupChatBox.SetTrigger("animChangeText");
	}

	public void BtnNext()
	{
		if (this.currentStepTutorialBossMode == 3)
		{
			this.BM_Step3();
		}
		else
		{
			int num = this.currentStepTutorial;
			switch (num)
			{
			case 1:
				this.Step1();
				break;
			case 2:
				this.HideChatBox();
				break;
			default:
				if (num != 24)
				{
					if (num == 34)
					{
						this.Step34();
					}
				}
				else
				{
					this.Step24();
				}
				break;
			case 5:
				this.HideChatBox();
				break;
			}
		}
	}

	private void SetForcusBtn(GameObject obj, Transform transform)
	{
		MonoBehaviour.print("---------" + transform.gameObject.name);
		obj.transform.parent = transform;
		obj.transform.localPosition = Vector3.zero;
		if (!obj.activeInHierarchy)
		{
			obj.SetActive(true);
		}
	}

	public void HideObjForcus()
	{
		for (int i = 0; i < this.objForcus.Length; i++)
		{
			this.objForcus[i].transform.parent = null;
			this.objForcus[i].SetActive(false);
		}
	}

	public void Step0()
	{
		UITutorial.current.ShowChatBox(0, true);
		this.currentStepTutorial = 1;
	}

	public void Step1()
	{
		this.ChangeText(1);
		this.currentStepTutorial = 2;
	}

	public void Step2()
	{
		UIGameManager.current.ShowButtonSkillPlane();
		this.ShowChatBox(2, false);
		this.SetForcusBtn(this.objForcus[0], this.btnSkill);
		this.currentStepTutorial = 3;
	}

	public void Step3()
	{
		this.HideObjForcus();
		this.HideChatBox();
		this.currentStepTutorial = 4;
	}

	public void Step4()
	{
		this.ShowChatBox(3, true);
		this.currentStepTutorial = 5;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
	}

	public void Step5()
	{
		this.SetForcusBtn(this.objForcus[0], this.level2);
		this.SetForcusBtn(this.objForcus[1], this.btnNextStage);
		this.currentStepTutorial = 6;
	}

	public void Step6()
	{
		this.HideObjForcus();
		this.SetForcusBtn(this.objForcus[1], this.btnStartGame);
		this.currentStepTutorial = 7;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
	}

	public void Step7()
	{
		this.SetForcusBtn(this.objForcus[0], this.level3);
		this.SetForcusBtn(this.objForcus[1], this.btnNextStage);
		this.currentStepTutorial = 8;
	}

	public void Step8()
	{
		this.HideObjForcus();
		this.SetForcusBtn(this.objForcus[1], this.btnStartGame);
		this.currentStepTutorial = 9;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
	}

	public void Step9()
	{
		this.SetForcusBtn(this.objForcus[1], this.btnBack);
		this.currentStepTutorial = 10;
	}

	public void Step10()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 11;
		this.Step11();
	}

	public void Step11()
	{
		this.SetForcusBtn(this.objForcus[1], this.btnBack);
		this.currentStepTutorial = 12;
	}

	public void Step12()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 13;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
	}

	public void Step13()
	{
		this.popupReward.SetActive(true);
		this.giftPlane.SetActive(true);
		this.giftDrone.SetActive(false);
		this.giftBox.SetActive(false);
		this.giftCard.SetActive(false);
		this.SetForcusBtn(this.objForcus[1], this.posBtnClaimGift);
		this.currentStepTutorial = 14;
	}

	public void Step14()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 15;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
		this.Step15();
	}

	public void Step15()
	{
		this.SetForcusBtn(this.objForcus[1], this.posMainPlane);
		this.currentStepTutorial = 16;
	}

	public void Step16()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 17;
		this.Step17();
	}

	public void Step17()
	{
		this.SetForcusBtn(this.objForcus[1], this.posFuryPlane);
		this.currentStepTutorial = 18;
	}

	public void Step18()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 19;
		this.Step19();
	}

	public void Step19()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnSwitch);
		this.currentStepTutorial = 20;
	}

	public void Step20()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 21;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
		this.Step21();
	}

	public void Step21()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnUpgrade);
		this.currentStepTutorial = 22;
	}

	public void Step22()
	{
		this.numberClickStep22++;
		if (this.numberClickStep22 >= 2)
		{
			this.HideObjForcus();
			this.currentStepTutorial = 23;
			PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
			this.Step23();
		}
	}

	public void Step23()
	{
		UITutorial.current.ShowChatBox(4, true);
		this.currentStepTutorial = 24;
	}

	public void Step24()
	{
		this.HideChatBox();
		this.currentStepTutorial = 25;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
		this.Step25();
	}

	public void Step25()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnBack);
		this.currentStepTutorial = 26;
	}

	public void Step26()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 27;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
		this.Daily_Step0();
	}

	public void Step27()
	{
		this.popupReward.SetActive(true);
		this.giftPlane.SetActive(false);
		this.giftDrone.SetActive(true);
		this.giftBox.SetActive(false);
		this.giftCard.SetActive(false);
		this.SetForcusBtn(this.objForcus[1], this.posBtnClaimGift);
		this.currentStepTutorial = 28;
	}

	public void Step28()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 29;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
		this.Step29();
	}

	public void Step29()
	{
		this.SetForcusBtn(this.objForcus[1], this.posDrone);
		this.currentStepTutorial = 30;
	}

	public void Step30()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 31;
		this.Step31();
	}

	public void Step31()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnEquip);
		this.currentStepTutorial = 32;
	}

	public void Step32()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 33;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
		this.Step33();
	}

	public void Step33()
	{
		UITutorial.current.ShowChatBox(5, true);
		this.currentStepTutorial = 34;
	}

	public void Step34()
	{
		this.HideChatBox();
		this.currentStepTutorial = 35;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
		this.Step35();
	}

	public void Step35()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnBack);
		this.currentStepTutorial = 36;
	}

	public void Step36()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 37;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
	}

	public void Step37()
	{
		this.popupReward.SetActive(true);
		this.giftPlane.SetActive(false);
		this.giftDrone.SetActive(false);
		this.giftBox.SetActive(true);
		this.giftCard.SetActive(false);
		this.SetForcusBtn(this.objForcus[1], this.posBtnClaimGift);
		this.currentStepTutorial = 38;
	}

	public void Step38()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 39;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
		this.Step39();
	}

	public void Step39()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnPrize);
		this.currentStepTutorial = 40;
	}

	public void Step40()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 41;
		this.Step41();
	}

	public void Step41()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnOpenPrize);
		this.currentStepTutorial = 42;
	}

	public void Step42()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 43;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
		this.Step43();
	}

	public void Step43()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnBack);
		this.currentStepTutorial = 44;
	}

	public void Step44()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 45;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
	}

	public void Step45()
	{
		this.popupReward.SetActive(true);
		this.giftPlane.SetActive(false);
		this.giftDrone.SetActive(false);
		this.giftBox.SetActive(false);
		this.giftCard.SetActive(true);
		this.SetForcusBtn(this.objForcus[1], this.posBtnClaimGift);
		this.currentStepTutorial = 46;
	}

	public void Step46()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 47;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
		this.Step47();
	}

	public void Step47()
	{
		this.SetForcusBtn(this.objForcus[1], this.posMainPlane);
		this.currentStepTutorial = 48;
	}

	public void Step48()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 49;
		this.Step49();
	}

	public void Step49()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnPlus);
		this.currentStepTutorial = 50;
	}

	public void Step50()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 51;
		this.Step51();
	}

	public void Step51()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnEvovle);
		this.currentStepTutorial = 52;
	}

	public void Step52()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 53;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
	}

	public void Step53()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnBackEvovle);
		this.currentStepTutorial = 54;
	}

	public void Step54()
	{
		this.HideObjForcus();
		this.currentStepTutorial = 55;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorial", this.currentStepTutorial);
	}

	public void BM_Step0()
	{
		this.SetForcusBtn(this.objForcus[0], this.posBtnBossMode);
		this.currentStepTutorialBossMode = 1;
	}

	public void BM_Step1()
	{
		this.HideObjForcus();
		this.currentStepTutorialBossMode = 2;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialBossMode", this.currentStepTutorialBossMode);
		this.BM_Step2();
	}

	public void BM_Step2()
	{
		UITutorial.current.ShowChatBox(6, true);
		this.currentStepTutorialBossMode = 3;
	}

	public void BM_Step3()
	{
		this.HideChatBox();
		this.currentStepTutorialBossMode = 4;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialBossMode", this.currentStepTutorialBossMode);
		this.BM_Step4();
	}

	public void BM_Step4()
	{
		UnityEngine.Debug.LogError("NewTutorial BossMode: Step 4");
		this.SetForcusBtn(this.objForcus[1], this.posLevelBossMode1);
		this.currentStepTutorialBossMode = 5;
	}

	public void BM_Step5()
	{
		this.HideObjForcus();
		this.currentStepTutorialBossMode = 6;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialBossMode", this.currentStepTutorialBossMode);
		this.BM_Step6();
	}

	public void BM_Step6()
	{
		this.SetForcusBtn(this.objForcus[1], this.posPlayBossMode);
		this.currentStepTutorialBossMode = 7;
	}

	public void BM_Step7()
	{
		this.HideObjForcus();
		this.currentStepTutorialBossMode = 8;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialBossMode", this.currentStepTutorialBossMode);
	}

	public void Daily_Step0()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnQuest);
		this.currentStepTutorialDaily = 1;
	}

	public void Daily_Step1()
	{
		this.HideObjForcus();
		this.currentStepTutorialDaily = 2;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialDaily", this.currentStepTutorialDaily);
		this.Daily_Step2();
	}

	public void Daily_Step2()
	{
		this.SetForcusBtn(this.objForcus[2], this.posBtnClaimQuest);
		this.currentStepTutorialDaily = 3;
	}

	public void Daily_Step3()
	{
		this.HideObjForcus();
		this.currentStepTutorialDaily = 4;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialDaily", this.currentStepTutorialDaily);
		this.Daily_Step4();
	}

	public void Daily_Step4()
	{
		this.SetForcusBtn(this.objForcus[2], this.posBtnDaily);
		this.currentStepTutorialDaily = 5;
	}

	public void Daily_Step5()
	{
		this.HideObjForcus();
		this.currentStepTutorialDaily = 6;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialDaily", this.currentStepTutorialDaily);
		this.Daily_Step6();
	}

	public void Daily_Step6()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnClaimDaily);
		this.currentStepTutorialDaily = 7;
	}

	public void Daily_Step7()
	{
		this.HideObjForcus();
		this.currentStepTutorialDaily = 8;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialDaily", this.currentStepTutorialDaily);
		this.Daily_Step8();
	}

	public void Daily_Step8()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnExit);
		this.currentStepTutorialDaily = 9;
	}

	public void Daily_Step9()
	{
		this.HideObjForcus();
		this.currentStepTutorialDaily = 10;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialDaily", this.currentStepTutorialDaily);
		this.Daily_Step10();
	}

	public void Daily_Step10()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnTabMainHome);
		this.currentStepTutorialDaily = 11;
	}

	public void Daily_Step11()
	{
		this.HideObjForcus();
		this.currentStepTutorialDaily = 12;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialDaily", this.currentStepTutorialDaily);
		this.Daily_Step12();
	}

	public void Daily_Step12()
	{
		this.SetForcusBtn(this.objForcus[1], this.posBtnPlay);
		this.currentStepTutorialDaily = 13;
	}

	public void Daily_Step13()
	{
		this.HideObjForcus();
		this.currentStepTutorialDaily = 14;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialDaily", this.currentStepTutorialDaily);
	}

	public void UseItems_Step0()
	{
		MonoBehaviour.print("UseItems_Step0");
		this.SetForcusBtn(this.objForcus[1], this.posBtnItemShield);
		this.ShowChatBox(7, false);
		this.currentStepTutorialUseItems = 1;
	}

	public void UseItems_Step1()
	{
		MonoBehaviour.print("UseItems_Step1");
		this.HideObjForcus();
		this.currentStepTutorialUseItems = 2;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialUseItems", this.currentStepTutorialUseItems);
		this.UseItems_Step2();
	}

	public void UseItems_Step2()
	{
		MonoBehaviour.print("UseItems_Step2");
		this.SetForcusBtn(this.objForcus[1], this.posBtnBuyItems);
		this.currentStepTutorialUseItems = 3;
	}

	public void UseItems_Step3()
	{
		MonoBehaviour.print("UseItems_Step3");
		this.HideObjForcus();
		this.currentStepTutorialUseItems = 4;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialUseItems", this.currentStepTutorialUseItems);
		this.UseItems_Step4();
	}

	public void UseItems_Step4()
	{
		MonoBehaviour.print("UseItems_Step4");
		this.SetForcusBtn(this.objForcus[1], this.posBtnExitBuyItems);
		this.currentStepTutorialUseItems = 5;
	}

	public void UseItems_Step5()
	{
		MonoBehaviour.print("UseItems_Step5");
		this.HideObjForcus();
		this.currentStepTutorialUseItems = 6;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialUseItems", this.currentStepTutorialUseItems);
		this.UseItems_Step6();
	}

	public void UseItems_Step6()
	{
		MonoBehaviour.print("UseItems_Step6");
		this.SetForcusBtn(this.objForcus[1], this.posBtnItemShield);
		this.currentStepTutorialUseItems = 7;
	}

	public void UseItems_Step7()
	{
		MonoBehaviour.print("UseItems_Step7");
		this.HideChatBox();
		this.currentStepTutorialUseItems = 8;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialUseItems", this.currentStepTutorialUseItems);
		this.Step6();
	}

	public void UseItems_Step8()
	{
		MonoBehaviour.print("UseItems_Step8");
		this.ShowChatBox(11, false);
		this.SetForcusBtn(this.objForcus[1], this.btnShield);
		this.currentStepTutorialUseItems = 9;
	}

	public void UseItems_Step9()
	{
		MonoBehaviour.print("UseItems_Step9");
		this.HideChatBox();
		this.currentStepTutorialUseItems = 10;
		PlayerPrefs.SetInt("UITutorialcurrentStepTutorialUseItems", this.currentStepTutorialUseItems);
	}

	public void ForcusBtnBack()
	{
		UnityEngine.Debug.LogError("NewTutorial: ForcusBtnBack");
		this.SetForcusBtn(this.objForcus[1], this.btnBack);
	}

	public static UITutorial _current;

	public int currentStepTutorial;

	public int currentStepTutorialBossMode;

	public int currentStepTutorialDaily;

	public int currentStepTutorialUseItems;

	public static bool isTurnOnTutorial;

	public UITutorial.Scene scene;

	public GameObject[] objForcus;

	[SerializeField]
	private GameObject btnNextStep;

	[SerializeField]
	private TutorialData tutorialData;

	[SerializeField]
	private GameObject popupChatBox;

	[SerializeField]
	private Animator animPopupChatBox;

	[SerializeField]
	private Text txtChatBox;

	[ShowIf("IsSceneHome", true)]
	public GameObject popupReward;

	[ShowIf("IsSceneHome", true)]
	public GameObject giftPlane;

	[ShowIf("IsSceneHome", true)]
	public GameObject giftDrone;

	[ShowIf("IsSceneHome", true)]
	public GameObject giftBox;

	[ShowIf("IsSceneHome", true)]
	public GameObject giftCard;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnClaimGift;

	[ShowIf("IsSceneHome", true)]
	public Transform posMainPlane;

	[ShowIf("IsSceneHome", true)]
	public Transform posFuryPlane;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnSwitch;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnUpgrade;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnBack;

	[ShowIf("IsSceneHome", true)]
	public Transform posDrone;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnEquip;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnPrize;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnOpenPrize;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnEvovle;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnPlus;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnBackEvovle;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnQuest;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnClaimQuest;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnDaily;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnClaimDaily;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnExit;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnTabMainHome;

	[ShowIf("IsSceneHome", true)]
	public Transform posBtnPlay;

	[ShowIf("IsSceneSelectLevel", true)]
	public Transform btnBack;

	[ShowIf("IsSceneSelectLevel", true)]
	public Transform btnNextStage;

	[ShowIf("IsSceneSelectLevel", true)]
	public Transform btnStartGame;

	[ShowIf("IsSceneSelectLevel", true)]
	public Transform level2;

	[ShowIf("IsSceneSelectLevel", true)]
	public Transform level3;

	[ShowIf("IsSceneSelectLevel", true)]
	public Transform posBtnBossMode;

	[ShowIf("IsSceneSelectLevel", true)]
	public Transform posLevelBossMode1;

	[ShowIf("IsSceneSelectLevel", true)]
	public Transform posPlayBossMode;

	[ShowIf("IsSceneSelectLevel", true)]
	public Transform posBtnItemShield;

	[ShowIf("IsSceneSelectLevel", true)]
	public Transform posBtnBuyItems;

	[ShowIf("IsSceneSelectLevel", true)]
	public Transform posBtnExitBuyItems;

	[ShowIf("IsSceneLevel1", true)]
	public Transform btnSkill;

	[ShowIf("IsSceneLevel1", true)]
	public Transform btnShield;

	[ShowIf("IsSceneSelectLevel", true)]
	public GameObject barBottom;

	[ShowIf("IsSceneSelectLevel", true)]
	public Mask maskBarBottom;

	[ShowIf("IsSceneSelectLevel", true)]
	public Image imgBarBottom;

	private bool isShowChatBox;

	private int numberClickStep22;

	public enum Scene
	{
		Home,
		Select_Level,
		Level1
	}
}
