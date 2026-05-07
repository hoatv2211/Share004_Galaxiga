using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class ChatBoxController : MonoBehaviour
{
	public static ChatBoxController current
	{
		get
		{
			if (ChatBoxController._current == null)
			{
				ChatBoxController._current = UnityEngine.Object.FindObjectOfType<ChatBoxController>();
			}
			return ChatBoxController._current;
		}
	}

	private void Start()
	{
		this.newTutorial = NewTutorial.current;
	}

	public void ShowChatBox(int idMess, bool isActiveBtnNext = true)
	{
		if (!ChatBoxController.isShowChatBox)
		{
			ChatBoxController.isShowChatBox = true;
			if (isActiveBtnNext)
			{
				this.btnNextStep.SetActive(true);
			}
			else
			{
				this.btnNextStep.SetActive(false);
			}
			base.gameObject.SetActive(true);
			if (this.btnPause != null)
			{
				this.btnPause.enabled = false;
			}
			this.anim.SetTrigger("animShow");
			switch (idMess)
			{
			case 0:
				this.txtChatBox.text = ScriptLocalization.tutorial_mess0;
				break;
			case 1:
				this.txtChatBox.text = ScriptLocalization.tutorial_mess1;
				break;
			case 2:
				this.txtChatBox.text = ScriptLocalization.tutorial_mess2;
				break;
			case 3:
				this.txtChatBox.text = ScriptLocalization.tutorial_mess3;
				break;
			case 4:
				this.txtChatBox.text = ScriptLocalization.tutorial_mess4;
				break;
			case 5:
				this.txtChatBox.text = ScriptLocalization.tutorial_mess5;
				break;
			case 6:
				this.txtChatBox.text = ScriptLocalization.tutorial_mess6;
				break;
			case 7:
				this.txtChatBox.text = ScriptLocalization.tutorial_mess7;
				break;
			case 8:
				this.txtChatBox.text = ScriptLocalization.tutorial_mess8;
				break;
			case 9:
				this.txtChatBox.text = ScriptLocalization.tutorial_mess9;
				break;
			case 10:
				this.txtChatBox.text = ScriptLocalization.tutorial_mess10;
				break;
			}
		}
	}

	public void HideChatBox()
	{
		if (ChatBoxController.isShowChatBox)
		{
			ChatBoxController.isShowChatBox = false;
			if (this.btnPause != null)
			{
				this.btnPause.enabled = true;
			}
			this.anim.SetTrigger("animHide");
		}
	}

	public void NewChangeText(int id)
	{
		this.idMess = id;
		this.anim.SetTrigger("animChangeText");
	}

	public void ClickBtnNextStep()
	{
		if (this.newTutorial.currentStepTutorial_UseSkillPlane == 1)
		{
			this.newTutorial.UseSkillPlane_Step1();
		}
		else if (this.newTutorial.currentStepTutorial_UseSkillPlane == 2)
		{
			this.newTutorial.UseSkillPlane_Step2();
		}
		else if (this.newTutorial.currentStepTutorial_UseSkillPlane == 4)
		{
			this.newTutorial.UseSkillPlane_Step4();
		}
		else if (this.newTutorial.currentStepTutorial_UseSkillPlane == 6)
		{
			this.newTutorial.UseSkillPlane_Step6();
		}
		else if (this.newTutorial.currentStepTutorial_BuyItems > 0 && this.newTutorial.currentStepTutorial_BuyItems < 11)
		{
			this.newTutorial.BuyItems_Step8();
			GameContext.totalItemPowerShield++;
			SelectLevelManager.current.shopItemPowerUpCampaign.SelectItemShield();
			PlayerPrefs.SetInt("NewTutorialcurrentStepTutorial_BuyItems", 10);
		}
		else if (this.newTutorial.currentStepTutorial_BuyItems == 11)
		{
			this.newTutorial.BuyItems_Step11();
		}
		else if (this.newTutorial.currentStepTutorial_UpgradePlane == 11)
		{
			this.newTutorial.UpgradePlane_Step11();
		}
		else if (this.newTutorial.currentStepTutorial_EquipDrone == 7)
		{
			this.newTutorial.EquipDrone_Step7();
		}
		else if (this.newTutorial.currentStepTutorial_EquipDrone == 11)
		{
			this.newTutorial.EquipDrone_Step11();
		}
		else if (this.newTutorial.currentStepTutorial_OpenBossMode == 3)
		{
			this.newTutorial.OpenBossMode_Step3();
		}
		else if (this.newTutorial.currentStepTutorial_EvovlePlane == 9)
		{
			this.newTutorial.EvovlePlane_Step9();
		}
	}

	public void ChangeText()
	{
		switch (this.idMess)
		{
		case 0:
			this.txtChatBox.text = ScriptLocalization.tutorial_mess0;
			break;
		case 1:
			this.txtChatBox.text = ScriptLocalization.tutorial_mess1;
			break;
		case 2:
			this.txtChatBox.text = ScriptLocalization.tutorial_mess2;
			break;
		case 3:
			this.txtChatBox.text = ScriptLocalization.tutorial_mess3;
			break;
		case 4:
			this.txtChatBox.text = ScriptLocalization.tutorial_mess4;
			break;
		case 5:
			this.txtChatBox.text = ScriptLocalization.tutorial_mess5;
			break;
		case 6:
			this.txtChatBox.text = ScriptLocalization.tutorial_mess6;
			break;
		case 7:
			this.txtChatBox.text = ScriptLocalization.tutorial_mess7;
			break;
		case 8:
			this.txtChatBox.text = ScriptLocalization.tutorial_mess8;
			break;
		case 9:
			this.txtChatBox.text = ScriptLocalization.tutorial_mess9;
			break;
		case 10:
			this.txtChatBox.text = ScriptLocalization.tutorial_mess10;
			break;
		}
	}

	public void TimeScale()
	{
		if (this.newTutorial.IsSceneLevel())
		{
			if (Time.timeScale == 0f)
			{
				Time.timeScale = 1f;
			}
			else
			{
				Time.timeScale = 0f;
			}
		}
	}

	public int idMess;

	public static ChatBoxController _current;

	[SerializeField]
	private NewTutorial newTutorial;

	[SerializeField]
	private Text txtChatBox;

	public Button btnPause;

	public static bool isShowChatBox;

	[SerializeField]
	private GameObject btnNextStep;

	[SerializeField]
	private Animator anim;
}
