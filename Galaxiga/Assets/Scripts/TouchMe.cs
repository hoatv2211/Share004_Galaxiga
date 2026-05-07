using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class TouchMe : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_TouchStart += this.On_TouchStart;
		EasyTouch.On_TouchDown += this.On_TouchDown;
		EasyTouch.On_TouchUp += this.On_TouchUp;
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
		EasyTouch.On_TouchStart -= this.On_TouchStart;
		EasyTouch.On_TouchDown -= this.On_TouchDown;
		EasyTouch.On_TouchUp -= this.On_TouchUp;
	}

	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startColor = base.gameObject.GetComponent<Renderer>().material.color;
	}

	private void On_TouchStart(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.RandomColor();
		}
	}

	private void On_TouchDown(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.textMesh.text = "Down since :" + gesture.actionTime.ToString("f2");
		}
	}

	private void On_TouchUp(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = this.startColor;
			this.textMesh.text = "Touch me";
		}
	}

	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	private TextMesh textMesh;

	private Color startColor;
}
