using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;
using UnityEngine.UI;

public class MultiCameraTouch : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchUp += this.On_TouchUp;
	}

	private void OnDestroy()
	{
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
	}

	private void On_TouchDown(Gesture gesture)
	{
		if (gesture.pickedObject != null)
		{
			this.label.text = "You touch : " + gesture.pickedObject.name + " on camera : " + gesture.pickedCamera.name;
		}
	}

	private void On_TouchUp(Gesture gesture)
	{
		this.label.text = string.Empty;
	}

	public Text label;
}
