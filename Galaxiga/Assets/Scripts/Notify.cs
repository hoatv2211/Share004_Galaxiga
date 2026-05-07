using System;
using UnityEngine;

public class Notify : MonoBehaviour
{
	public static Notify Current
	{
		get
		{
			if (Notify._instance == null)
			{
				Notify._instance = UnityEngine.Object.FindObjectOfType<Notify>();
			}
			return Notify._instance;
		}
	}

	private void Start()
	{
	}

	public void ShowNotificationQuest()
	{
		this.notifyQuest.SetActive(true);
	}

	public void HideNotificationQuest()
	{
		this.notifyQuest.SetActive(true);
	}

	public static Notify _instance;

	public GameObject notifyQuest;
}
