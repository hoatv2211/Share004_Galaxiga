using System;
using System.Collections;
using DG.Tweening;
using I2.Loc;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
	private void Awake()
	{
		PopupManager.current = this;
	}

	public void ShowPopupSetting()
	{
		this.ShowPopup(this.popupSetting, "SETTING");
		EscapeManager.Current.AddAction(new Action(this.HidePopupSetting));
	}

	public void HidePopupSetting()
	{
		this.HidePopup(this.popupSetting, "SETTING");
		SettingManager.Curret.SaveDataSetting();
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupSetting));
	}

	public void ShowPopupDailyRewards()
	{
		Notification.Current.ShowNotification(ScriptLocalization.notify_coming_soon);
	}

	public void ShowPopupConfirmQuitGame()
	{
		this.ShowPopup(this.popupConfirmQuitGame, "CONFIRM_QUIT_GAME");
		EscapeManager.Current.AddAction(new Action(this.HidePopupConfirmQuitGame));
	}

	public void HidePopupConfirmQuitGame()
	{
		this.HidePopup(this.popupConfirmQuitGame, "CONFIRM_QUIT_GAME");
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupConfirmQuitGame));
	}

	public void QuitGame()
	{
		PlayerPrefs.Save();
		Application.Quit();
	}

	public void ShowPopupNewGame()
	{
		this.ShowPopup(this.popupStartNewGame, "NEW_GAME");
		EscapeManager.Current.AddAction(new Action(this.HidePopupNewGame));
	}

	public void HidePopupNewGame()
	{
		this.HidePopup(this.popupStartNewGame, "NEW_GAME");
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupNewGame));
	}

	private void ShowPopup(GameObject popup, string idTweening)
	{
		popup.SetActive(true);
		DOTween.Restart(idTweening, true, -1f);
		DOTween.Play(idTweening);
	}

	private void HidePopup(GameObject popup, string idTweening)
	{
		DOTween.PlayBackwards(idTweening);
		base.StartCoroutine(this.DelayHidePopup(popup));
	}

	private IEnumerator DelayHidePopup(GameObject popup)
	{
		yield return new WaitForSeconds(0.1f);
		popup.SetActive(false);
		yield break;
	}

	public static PopupManager current;

	[Header("Popup Setting")]
	public GameObject popupSetting;

	[Header("Popup Quit Game")]
	public GameObject popupConfirmQuitGame;

	[Header("Popup Quit Game")]
	public GameObject popupStartNewGame;
}
