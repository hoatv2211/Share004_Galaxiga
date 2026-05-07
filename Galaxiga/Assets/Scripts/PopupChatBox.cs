using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class PopupChatBox : MonoBehaviour
{
	public static PopupChatBox current
	{
		get
		{
			if (PopupChatBox._current == null)
			{
				PopupChatBox._current = UnityEngine.Object.FindObjectOfType<PopupChatBox>();
			}
			return PopupChatBox._current;
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
		}
	}

	public void ShowChatBox(int idMess, bool isActiveBtnNext = true)
	{
		if (!this.isShowChatBox)
		{
			this.isShowChatBox = true;
			if (isActiveBtnNext)
			{
				this.btnNextStep.SetActive(true);
			}
			else
			{
				this.btnNextStep.SetActive(false);
			}
			base.gameObject.SetActive(true);
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
			}
		}
	}

	public void HideChatBox()
	{
		if (this.isShowChatBox)
		{
			this.isShowChatBox = false;
			this.anim.SetTrigger("animHide");
		}
	}

	public void NewChangeText(int idMess)
	{
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
		}
	}

	public void ClickBtnNextStep()
	{
		NewTutorial.current.UseSkillPlane_Step1();
		NewTutorial.current.UseSkillPlane_Step2();
		NewTutorial.current.UseSkillPlane_Step4();
		NewTutorial.current.UseSkillPlane_Step6();
		NewTutorial.current.UpgradePlane_Step13();
		NewTutorial.current.EquipDrone_Step9();
		NewTutorial.current.OpenBossMode_Step3();
		NewTutorial.current.EvovlePlane_Step11();
	}

	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	public void TimeScale()
	{
		if (this.uITutorial.IsSceneLevel1())
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

	public static PopupChatBox _current;

	[SerializeField]
	private UITutorial uITutorial;

	[SerializeField]
	private TutorialData tutorialData;

	[SerializeField]
	private Text txtChatBox;

	public int idMess;

	private bool isShowChatBox;

	[SerializeField]
	private GameObject btnNextStep;

	[SerializeField]
	private Animator anim;
}
