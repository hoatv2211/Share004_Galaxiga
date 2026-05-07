using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class ETWindow : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchStart += this.On_TouchStart;
	}

	private void OnDestroy()
	{
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchStart -= this.On_TouchStart;
	}

	private void On_TouchStart(Gesture gesture)
	{
		this.drag = false;
		if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)))
		{
			base.transform.SetAsLastSibling();
			this.drag = true;
		}
	}

	private void On_TouchDown(Gesture gesture)
	{
        if (gesture.isOverGui && (gesture.pickedUIElement == base.gameObject || gesture.pickedUIElement.transform.IsChildOf(base.transform)) && drag)
        {
            base.transform.position += (Vector3)gesture.deltaPosition;
        }
    }

	private bool drag;
}
