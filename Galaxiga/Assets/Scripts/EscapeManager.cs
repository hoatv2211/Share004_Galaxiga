using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeManager : MonoBehaviour
{
	public static EscapeManager Current
	{
		get
		{
			if (EscapeManager._instance == null)
			{
				EscapeManager._instance = UnityEngine.Object.FindObjectOfType<EscapeManager>();
			}
			return EscapeManager._instance;
		}
	}

	private void Awake()
	{
		this.actionsHidePopup = new List<Action>();
		this.numberAction = 0;
	}

	private void Start()
	{
		this.scene = SceneManager.GetActiveScene();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && !ChatBoxController.isShowChatBox)
		{
			if (this.numberAction > 0)
			{
				this.actionsHidePopup[this.numberAction - 1]();
			}
			else
			{
				string name = this.scene.name;
				if (name != null)
				{
					if (name == "Home")
					{
						PopupManager.current.ShowPopupConfirmQuitGame();
						return;
					}
					if (name == "SelectLevel")
					{
						if (GameContext.maxLevelUnlocked > 3)
						{
							SelectLevelManager.current.BackToHome();
						}
						return;
					}
				}
				if (!GameContext.isPlayerDie && !GameContext.isMissionComplete && !GameContext.isMissionFail && !UIGameManager.current.isPause)
				{
					GameScreenManager.current.PauseGame();
				}
			}
		}
	}

	public void AddAction(Action actionT)
	{
		if (!this.actionsHidePopup.Contains(actionT))
		{
			this.actionsHidePopup.Add(actionT);
			this.numberAction++;
		}
	}

	public void RemoveAction(Action actionT)
	{
		if (this.actionsHidePopup.Contains(actionT))
		{
			this.actionsHidePopup.Remove(actionT);
			this.numberAction--;
		}
	}

	public static EscapeManager _instance;

	public List<Action> actionsHidePopup;

	public int numberAction;

	private Scene scene;
}
