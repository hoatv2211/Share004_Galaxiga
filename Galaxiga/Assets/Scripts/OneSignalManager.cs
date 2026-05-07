using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using OneSignalPush.MiniJSON;
using UnityEngine;

public class OneSignalManager : MonoBehaviour
{
	private void Start()
	{
        extraMessage = null;
        OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.VERBOSE, OneSignal.LOG_LEVEL.NONE);
        OneSignal.SetRequiresUserPrivacyConsent(requiresUserPrivacyConsent);
        OneSignal.StartInit(OneSignal_app_id).HandleNotificationReceived(HandleNotificationReceived).HandleNotificationOpened(HandleNotificationOpened)
            .EndInit();
        OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
        OneSignal.permissionObserver += OneSignal_permissionObserver;
        OneSignal.subscriptionObserver += OneSignal_subscriptionObserver;
        OneSignal.emailSubscriptionObserver += OneSignal_emailSubscriptionObserver;
        OSPermissionSubscriptionState permissionSubscriptionState = OneSignal.GetPermissionSubscriptionState();
    }

	private void OneSignal_subscriptionObserver(OSSubscriptionStateChanges stateChanges)
	{
		UnityEngine.Debug.Log("SUBSCRIPTION stateChanges: " + stateChanges);
		UnityEngine.Debug.Log("SUBSCRIPTION stateChanges.to.userId: " + stateChanges.to.userId);
		UnityEngine.Debug.Log("SUBSCRIPTION stateChanges.to.subscribed: " + stateChanges.to.subscribed);
	}

	private void OneSignal_permissionObserver(OSPermissionStateChanges stateChanges)
	{
		UnityEngine.Debug.Log("PERMISSION stateChanges.from.status: " + stateChanges.from.status);
		UnityEngine.Debug.Log("PERMISSION stateChanges.to.status: " + stateChanges.to.status);
	}

	private void OneSignal_emailSubscriptionObserver(OSEmailSubscriptionStateChanges stateChanges)
	{
		UnityEngine.Debug.Log("EMAIL stateChanges.from.status: " + stateChanges.from.emailUserId + ", " + stateChanges.from.emailAddress);
		UnityEngine.Debug.Log("EMAIL stateChanges.to.status: " + stateChanges.to.emailUserId + ", " + stateChanges.to.emailAddress);
	}

	private static void HandleNotificationReceived(OSNotification notification)
	{
		OSNotificationPayload payload = notification.payload;
		string body = payload.body;
		MonoBehaviour.print("GameControllerExample:HandleNotificationReceived: " + body);
		MonoBehaviour.print("displayType: " + notification.displayType);
		OneSignalManager.extraMessage = "Notification received with text: " + body;
		Dictionary<string, object> additionalData = payload.additionalData;
		if (additionalData == null)
		{
			UnityEngine.Debug.Log("[HandleNotificationReceived] Additional Data == null");
		}
		else
		{
			UnityEngine.Debug.Log("[HandleNotificationReceived] message " + body + ", additionalData: " + Json.Serialize(additionalData));
		}
	}

	public static void HandleNotificationOpened(OSNotificationOpenedResult result)
	{
		OSNotificationPayload payload = result.notification.payload;
		string body = payload.body;
		string actionID = result.action.actionID;
		MonoBehaviour.print("GameControllerExample:HandleNotificationOpened: " + body);
		OneSignalManager.extraMessage = "Notification opened with text: " + body;
		Dictionary<string, object> additionalData = payload.additionalData;
		if (additionalData == null)
		{
			UnityEngine.Debug.Log("[HandleNotificationOpened] Additional Data == null");
		}
		else
		{
			UnityEngine.Debug.Log("[HandleNotificationOpened] message " + body + ", additionalData: " + Json.Serialize(additionalData));
		}
		if (actionID != null)
		{
			OneSignalManager.extraMessage = "Pressed ButtonId: " + actionID;
		}
	}

	public string OneSignal_app_id = string.Empty;

	private static string extraMessage;

	private static bool requiresUserPrivacyConsent;

	
}
