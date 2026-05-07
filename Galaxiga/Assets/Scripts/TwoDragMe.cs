using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class TwoDragMe : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_DragStart2Fingers += this.On_DragStart2Fingers;
		EasyTouch.On_Drag2Fingers += this.On_Drag2Fingers;
		EasyTouch.On_DragEnd2Fingers += this.On_DragEnd2Fingers;
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
		EasyTouch.On_DragStart2Fingers -= this.On_DragStart2Fingers;
		EasyTouch.On_Drag2Fingers -= this.On_Drag2Fingers;
		EasyTouch.On_DragEnd2Fingers -= this.On_DragEnd2Fingers;
		EasyTouch.On_Cancel2Fingers -= this.On_Cancel2Fingers;
	}

	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startColor = base.gameObject.GetComponent<Renderer>().material.color;
	}

	private void On_DragStart2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.RandomColor();
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			this.deltaPosition = touchToWorldPoint - base.transform.position;
		}
	}

	private void On_Drag2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			base.transform.position = touchToWorldPoint - this.deltaPosition;
			float swipeOrDragAngle = gesture.GetSwipeOrDragAngle();
			this.textMesh.text = swipeOrDragAngle.ToString("f2") + " / " + gesture.swipe.ToString();
		}
	}

	private void On_DragEnd2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = this.startColor;
			this.textMesh.text = "Drag me";
		}
	}

	private void On_Cancel2Fingers(Gesture gesture)
	{
		this.On_DragEnd2Fingers(gesture);
	}

	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	private TextMesh textMesh;

	private Vector3 deltaPosition;

	private Color startColor;
}
