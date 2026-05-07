using System;
using System.Collections;
using DG.Tweening;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
	public static Notification Current { get; private set; }

	private void Awake()
	{
		if (Notification.Current == null)
		{
			Notification.Current = this;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void ShowNotification(string notification)
	{
		EazySoundManager.PlayUISound(AudioCache.UISound.message);
		this.objNotification.SetActive(true);
		this.textNofication.text = notification;
		DOTween.Restart("NOTIFICATION", true, -1f);
		DOTween.Play("NOTIFICATION");
		base.StopCoroutine("DelayHide");
		base.StartCoroutine("DelayHide");
	}

	private IEnumerator DelayHide()
	{
		yield return new WaitForSecondsRealtime(2f);
		DOTween.PlayBackwards("NOTIFICATION");
		yield return new WaitForSecondsRealtime(0.25f);
		this.objNotification.SetActive(false);
		yield break;
	}

	public GameObject objNotification;

	public Text textNofication;
}
