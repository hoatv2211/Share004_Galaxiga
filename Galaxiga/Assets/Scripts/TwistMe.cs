using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class TwistMe : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_TouchStart2Fingers += this.On_TouchStart2Fingers;
		EasyTouch.On_Twist += this.On_Twist;
		EasyTouch.On_TwistEnd += this.On_TwistEnd;
		EasyTouch.On_Cancel2Fingers += this.On_Cancel2Fingers;
	}

	private void OnDisable()
	{
		this.UnsubscribeEvent();
	}

	private void OnDestroy()
	{
		this.UnsubscribeEvent();
	}

	private void UnsubscribeEvent()
	{
		EasyTouch.On_TouchStart2Fingers -= this.On_TouchStart2Fingers;
		EasyTouch.On_Twist -= this.On_Twist;
		EasyTouch.On_TwistEnd -= this.On_TwistEnd;
		EasyTouch.On_Cancel2Fingers -= this.On_Cancel2Fingers;
	}

	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
	}

	private void On_TouchStart2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			EasyTouch.SetEnableTwist(true);
			EasyTouch.SetEnablePinch(false);
		}
	}

	private void On_Twist(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.transform.Rotate(new Vector3(0f, 0f, gesture.twistAngle));
			this.textMesh.text = "Delta angle : " + gesture.twistAngle.ToString();
		}
	}

	private void On_TwistEnd(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			EasyTouch.SetEnablePinch(true);
			base.transform.rotation = Quaternion.identity;
			this.textMesh.text = "Twist me";
		}
	}

	private void On_Cancel2Fingers(Gesture gesture)
	{
		EasyTouch.SetEnablePinch(true);
		base.transform.rotation = Quaternion.identity;
		this.textMesh.text = "Twist me";
	}

	private TextMesh textMesh;
}
