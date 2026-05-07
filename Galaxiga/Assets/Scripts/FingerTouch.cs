using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class FingerTouch : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchUp += this.On_TouchUp;
		EasyTouch.On_Swipe += this.On_Swipe;
		EasyTouch.On_Drag += this.On_Drag;
		EasyTouch.On_DoubleTap += this.On_DoubleTap;
		this.textMesh = base.GetComponentInChildren<TextMesh>();
	}

	private void OnDestroy()
	{
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
		EasyTouch.On_Swipe -= this.On_Swipe;
		EasyTouch.On_Drag -= this.On_Drag;
		EasyTouch.On_DoubleTap -= this.On_DoubleTap;
	}

	private void On_Drag(Gesture gesture)
	{
		if (gesture.pickedObject.transform.IsChildOf(base.gameObject.transform) && this.fingerId == gesture.fingerIndex)
		{
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			base.transform.position = touchToWorldPoint - this.deltaPosition;
		}
	}

	private void On_Swipe(Gesture gesture)
	{
		if (this.fingerId == gesture.fingerIndex)
		{
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(base.transform.position);
			base.transform.position = touchToWorldPoint - this.deltaPosition;
		}
	}

	private void On_TouchStart(Gesture gesture)
	{
		if (gesture.pickedObject != null && gesture.pickedObject.transform.IsChildOf(base.gameObject.transform))
		{
			this.fingerId = gesture.fingerIndex;
			this.textMesh.text = this.fingerId.ToString();
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			this.deltaPosition = touchToWorldPoint - base.transform.position;
		}
	}

	private void On_TouchUp(Gesture gesture)
	{
		if (gesture.fingerIndex == this.fingerId)
		{
			this.fingerId = -1;
			this.textMesh.text = string.Empty;
		}
	}

	public void InitTouch(int ind)
	{
		this.fingerId = ind;
		this.textMesh.text = this.fingerId.ToString();
	}

	private void On_DoubleTap(Gesture gesture)
	{
		if (gesture.pickedObject != null && gesture.pickedObject.transform.IsChildOf(base.gameObject.transform))
		{
			UnityEngine.Object.DestroyImmediate(base.transform.gameObject);
		}
	}

	private TextMesh textMesh;

	public Vector3 deltaPosition = Vector2.zero;

	public int fingerId = -1;
}
