using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class RTSCamera : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_Swipe += this.On_Swipe;
		EasyTouch.On_Drag += this.On_Drag;
		EasyTouch.On_Twist += this.On_Twist;
		EasyTouch.On_Pinch += this.On_Pinch;
	}

	private void On_Twist(Gesture gesture)
	{
		base.transform.Rotate(Vector3.up * gesture.twistAngle);
	}

	private void OnDestroy()
	{
		EasyTouch.On_Swipe -= this.On_Swipe;
		EasyTouch.On_Drag -= this.On_Drag;
		EasyTouch.On_Twist -= this.On_Twist;
	}

	private void On_Drag(Gesture gesture)
	{
		this.On_Swipe(gesture);
	}

	private void On_Swipe(Gesture gesture)
	{
		base.transform.Translate(Vector3.left * gesture.deltaPosition.x / (float)Screen.width);
		base.transform.Translate(Vector3.back * gesture.deltaPosition.y / (float)Screen.height);
	}

	private void On_Pinch(Gesture gesture)
	{
		Camera.main.fieldOfView += gesture.deltaPinch * Time.deltaTime;
	}

	private Vector3 delta;
}
