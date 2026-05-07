using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class WarningManager : MonoBehaviour
{
	public static WarningManager Current
	{
		get
		{
			if (WarningManager._instance == null)
			{
				WarningManager._instance = UnityEngine.Object.FindObjectOfType<WarningManager>();
			}
			return WarningManager._instance;
		}
	}

	public bool IsSceneHome()
	{
		return this.scene == WarningManager.Scene.Home;
	}

	public bool IsSceneSelectLevel()
	{
		return this.scene == WarningManager.Scene.Select_Level;
	}

	public void ShowWarningEvolvePlane()
	{
		if (!this.iconWarningEvolvePlane.activeInHierarchy)
		{
			this.iconWarningEvolvePlane.SetActive(true);
		}
	}

	public void HideWarningEvolvePlane()
	{
		this.iconWarningEvolvePlane.SetActive(false);
	}

	public void ShowWarningEvolveDrone()
	{
		if (!this.iconWarningEvolveDrone.activeInHierarchy)
		{
			this.iconWarningEvolveDrone.SetActive(true);
		}
	}

	public void HideWarningEvolveDrone()
	{
		this.iconWarningEvolveDrone.SetActive(false);
	}

	public void ShowNotificationQuest()
	{
		if (!this.iconNotifyQuest.activeInHierarchy)
		{
			this.iconNotifyQuest.SetActive(true);
		}
	}

	public void HideNotificationQuest()
	{
		if (this.iconNotifyQuest.activeInHierarchy)
		{
			this.iconNotifyQuest.SetActive(false);
		}
	}

	public void ShowNotificationPrize()
	{
		if (!this.iconNotifyPrize.activeInHierarchy)
		{
			this.iconNotifyPrize.SetActive(true);
		}
	}

	public void HideNotificationPrize()
	{
		if (this.iconNotifyPrize.activeInHierarchy)
		{
			this.iconNotifyPrize.SetActive(false);
		}
	}

	public void ShowNotificationTvTrailer()
	{
		if (!this.iconNotifyTVTrailer.activeInHierarchy)
		{
			this.iconNotifyTVTrailer.SetActive(true);
		}
	}

	public void HideNotificationTvTrailer()
	{
		if (this.iconNotifyTVTrailer.activeInHierarchy)
		{
			this.iconNotifyTVTrailer.SetActive(false);
		}
	}

	public void ShowNotificationShopCoinItem()
	{
		if (!this.iconNotifyShopCoinItem.activeInHierarchy)
		{
			this.iconNotifyShopCoinItem.SetActive(true);
		}
	}

	public void HideNotificationShopCoinItem()
	{
		if (this.iconNotifyShopCoinItem.activeInHierarchy)
		{
			this.iconNotifyShopCoinItem.SetActive(false);
		}
	}

	public WarningManager.Scene scene;

	private static WarningManager _instance;

	[HideIf("IsSceneSelectLevel", true)]
	public GameObject iconWarningEvolvePlane;

	[HideIf("IsSceneSelectLevel", true)]
	public GameObject iconWarningEvolveDrone;

	[HideIf("IsSceneSelectLevel", true)]
	public GameObject iconNotifyQuest;

	[HideIf("IsSceneSelectLevel", true)]
	public GameObject iconNotifyPrize;

	[HideIf("IsSceneSelectLevel", true)]
	public GameObject iconNotifyTVTrailer;

	[HideIf("IsSceneSelectLevel", true)]
	public GameObject iconNotifyShopCoinItem;

	[HideIf("IsSceneSelectLevel", true)]
	public GameObject iconWarningPlane;

	[HideIf("IsSceneSelectLevel", true)]
	public GameObject iconWarningDrone;

	public enum Scene
	{
		Home,
		Select_Level
	}
}
