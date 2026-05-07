using System;
using System.Collections.Generic;
using EasyMobile;
using MoreMountains.Tools;
using UnityEngine;

public class NotificationsEasyMobileManager : PersistentSingleton<NotificationsEasyMobileManager>
{
	private void Start()
	{
		this.Init();
		this.CancelAllPendingLocalNotifications();
	}

	public void PushNotificationX3PackGem(int timeDelay)
	{
		this.ScheduleLocalNotification("Don't miss out on Galaxiga's SUPER OFFER! Get VIP status now!", timeDelay);
	}

	public void PushNotificationSale(float percentSale, int timeDelay)
	{
		this.ScheduleLocalNotification(string.Format("Big Sale up to {0}, only 1 days left", percentSale), timeDelay);
	}

	private void PushNotification()
	{
		this.ScheduleLocalNotification("Daily gift is available FREE now. Check it out!", 24);
		this.ScheduleLocalNotification("Don't miss something new in Galaxiga. Join now!", 72);
		this.ScheduleLocalNotification("Long time no see. We have new features for you. Let's check it!", 168);
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			CacheGame.LastTimeQuitGame = DateTime.Now.ToBinary().ToString();
			if (PrizeManager.current != null)
			{
				PrizeManager.current.SaveTimeLeftFreeBox();
			}
		}
	}

	public void OnApplicationQuit()
	{
		this.PushNotification();
		CacheGame.LastTimeQuitGame = DateTime.Now.ToBinary().ToString();
		PlayerPrefs.Save();
		if (PrizeManager.current != null)
		{
			PrizeManager.current.SaveTimeLeftFreeBox();
		}
	}

	public void Init()
	{
		
	}

	public void ScheduleLocalNotification(string contentBody, int timedelay)
	{
		if (!this.InitCheck())
		{
			return;
		}
		
		DateTime dateTime = DateTime.Now + new TimeSpan(timedelay, 0, 10);
	}

	

	public void CancelAllPendingLocalNotifications()
	{
		if (!this.InitCheck())
		{
			return;
		}
		
	}

	public void RemoveAllDeliveredNotifications()
	{
		
	}

	private bool InitCheck()
	{
		
		return false;
	}

	public const string MESS_1DAY = "Daily gift is available FREE now. Check it out!";

	public const string MESS_3DAY = "Don't miss something new in Galaxiga. Join now!";

	public const string MESS_7DAY = "Long time no see. We have new features for you. Let's check it!";

	public const string MESS_X3_GEM = "Don't miss out on Galaxiga's SUPER OFFER! Get VIP status now!";

	public const string MESS_SALE = "Big Sale up to {0}, only 1 days left";

	public string categoryId;

	private string orgNotificationListText;
}
