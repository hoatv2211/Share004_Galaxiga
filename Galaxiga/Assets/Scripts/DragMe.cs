using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class DragMe : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_Drag += this.On_Drag;
		EasyTouch.On_DragStart += this.On_DragStart;
		EasyTouch.On_DragEnd += this.On_DragEnd;
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
		EasyTouch.On_Drag -= this.On_Drag;
		EasyTouch.On_DragStart -= this.On_DragStart;
		EasyTouch.On_DragEnd -= this.On_DragEnd;
	}

	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startColor = base.gameObject.GetComponent<Renderer>().material.color;
	}

	private void On_DragStart(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.fingerIndex = gesture.fingerIndex;
			this.RandomColor();
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			this.deltaPosition = touchToWorldPoint - base.transform.position;
		}
	}

	private void On_Drag(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject && this.fingerIndex == gesture.fingerIndex)
		{
			Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(gesture.pickedObject.transform.position);
			base.transform.position = touchToWorldPoint - this.deltaPosition;
			float swipeOrDragAngle = gesture.GetSwipeOrDragAngle();
			this.textMesh.text = swipeOrDragAngle.ToString("f2") + " / " + gesture.swipe.ToString();
		}
	}

	private void On_DragEnd(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = this.startColor;
			this.textMesh.text = "Drag me";
		}
	}

	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	private TextMesh textMesh;

	private Color startColor;

	private Vector3 deltaPosition;

	private int fingerIndex;
}
