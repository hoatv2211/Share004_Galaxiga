using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class UIDrag : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchUp += this.On_TouchUp;
		EasyTouch.On_TouchStart2Fingers += this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers += this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers += this.On_TouchUp2Fingers;
	}

	private void OnDestroy()
	{
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_TouchStart2Fingers -= this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers -= this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers -= this.On_TouchUp2Fingers;
	}

	private void On_TouchStart(Gesture gesture)
	{
		if (gesture.isOverGui && this.drag && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)) && this.fingerId == -1)
		{
			this.fingerId = gesture.fingerIndex;
			base.transform.SetAsLastSibling();
		}
	}

	private void On_TouchDown(Gesture gesture)
	{
		if (this.fingerId == gesture.fingerIndex && this.drag && gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
		{
			base.transform.position += (Vector3)gesture.deltaPosition;
		}
	}

	private void On_TouchUp(Gesture gesture)
	{
		if (this.fingerId == gesture.fingerIndex)
		{
			this.fingerId = -1;
		}
	}

	private void On_TouchStart2Fingers(Gesture gesture)
	{
		if (gesture.isOverGui && this.drag && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)) && this.fingerId == -1)
		{
			base.transform.SetAsLastSibling();
		}
	}

	private void On_TouchDown2Fingers(Gesture gesture)
	{
		if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
		{
			if (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform))
			{
				base.transform.position += (Vector3)gesture.deltaPosition;
			}
			this.drag = false;
		}
	}

	private void On_TouchUp2Fingers(Gesture gesture)
	{
		if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
		{
			this.drag = true;
		}
	}

	private int fingerId = -1;

	private bool drag = true;
}
