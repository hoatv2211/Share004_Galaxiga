using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;
using UnityEngine.UI;

public class MultiLayerTouch : MonoBehaviour
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
			if (!EasyTouch.GetAutoUpdatePickedObject())
			{
				this.label.text = string.Concat(new object[]
				{
					"Picked object from event : ",
					gesture.pickedObject.name,
					" : ",
					gesture.position
				});
			}
			else
			{
				this.label.text = string.Concat(new object[]
				{
					"Picked object from event : ",
					gesture.pickedObject.name,
					" : ",
					gesture.position
				});
			}
		}
		else if (!EasyTouch.GetAutoUpdatePickedObject())
		{
			this.label.text = "Picked object from event :  none";
		}
		else
		{
			this.label.text = "Picked object from event : none";
		}
		this.label2.text = string.Empty;
		if (!EasyTouch.GetAutoUpdatePickedObject())
		{
			GameObject currentPickedObject = gesture.GetCurrentPickedObject(false);
			if (currentPickedObject != null)
			{
				this.label2.text = "Picked object from GetCurrentPickedObject : " + currentPickedObject.name;
			}
			else
			{
				this.label2.text = "Picked object from GetCurrentPickedObject : none";
			}
		}
	}

	private void On_TouchUp(Gesture gesture)
	{
		this.label.text = string.Empty;
		this.label2.text = string.Empty;
	}

	public Text label;

	public Text label2;
}
