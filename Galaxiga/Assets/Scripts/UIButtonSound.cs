using System;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButtonSound : MonoBehaviour
{
	private void Awake()
	{
		this.button = base.GetComponent<Button>();
	}

	private void Start()
	{
		this.button.onClick.AddListener(new UnityAction(this.PlaySound));
	}

	private void PlaySound()
	{
		EazySoundManager.PlayUISound(AudioCache.UISound.tap);
	}

	private Button button;
}
