using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class TwoTouchMe : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_TouchStart2Fingers += this.On_TouchStart2Fingers;
		EasyTouch.On_TouchDown2Fingers += this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers += this.On_TouchUp2Fingers;
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
		EasyTouch.On_TouchDown2Fingers -= this.On_TouchDown2Fingers;
		EasyTouch.On_TouchUp2Fingers -= this.On_TouchUp2Fingers;
		EasyTouch.On_Cancel2Fingers -= this.On_Cancel2Fingers;
	}

	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startColor = base.gameObject.GetComponent<Renderer>().material.color;
	}

	private void On_TouchStart2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.RandomColor();
		}
	}

	private void On_TouchDown2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.textMesh.text = "Down since :" + gesture.actionTime.ToString("f2");
		}
	}

	private void On_TouchUp2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = this.startColor;
			this.textMesh.text = "Touch me";
		}
	}

	private void On_Cancel2Fingers(Gesture gesture)
	{
		this.On_TouchUp2Fingers(gesture);
	}

	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	private TextMesh textMesh;

	private Color startColor;
}
