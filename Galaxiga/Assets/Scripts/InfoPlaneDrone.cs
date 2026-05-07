using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InfoPlaneDrone : MonoBehaviour
{
	private void Awake()
	{
		this.btnShow = base.GetComponent<Button>();
		if (this.btnShow != null)
		{
			this.btnShow.onClick.AddListener(new UnityAction(this.ShowInfo));
		}
	}

	private void ShowInfo()
	{
		if (this.isPlane)
		{
			ShowInfoPlaneDrone.Current.ShowInfoPlane(this.planeID);
		}
		else
		{
			ShowInfoPlaneDrone.Current.ShowInfoDrone(this.droneID);
		}
	}

	public bool isPlane = true;

	[ShowIf("isPlane", true)]
	public GameContext.Plane planeID;

	[HideIf("isPlane", true)]
	public GameContext.Drone droneID;

	private Button btnShow;
}
