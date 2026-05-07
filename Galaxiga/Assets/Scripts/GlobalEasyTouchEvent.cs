using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;
using UnityEngine.UI;

public class GlobalEasyTouchEvent : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchUp += this.On_TouchUp;
		EasyTouch.On_OverUIElement += this.On_OverUIElement;
		EasyTouch.On_UIElementTouchUp += this.On_UIElementTouchUp;
	}

	private void OnDestroy()
	{
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_OverUIElement -= this.On_OverUIElement;
		EasyTouch.On_UIElementTouchUp -= this.On_UIElementTouchUp;
	}

	private void On_TouchDown(Gesture gesture)
	{
		this.statText.transform.SetAsFirstSibling();
		if (gesture.pickedUIElement != null)
		{
			this.statText.text = "You touch UI Element : " + gesture.pickedUIElement.name + " (from gesture event)";
		}
		if (!gesture.isOverGui && gesture.pickedObject == null)
		{
			this.statText.text = "You touch an empty area";
		}
		if (gesture.pickedObject != null && !gesture.isOverGui)
		{
			this.statText.text = "You touch a 3D Object";
		}
	}

	private void On_OverUIElement(Gesture gesture)
	{
		this.statText.text = "You touch UI Element : " + gesture.pickedUIElement.name + " (from On_OverUIElement event)";
	}

	private void On_UIElementTouchUp(Gesture gesture)
	{
		this.statText.text = string.Empty;
	}

	private void On_TouchUp(Gesture gesture)
	{
		this.statText.text = string.Empty;
	}

	public Text statText;
}
