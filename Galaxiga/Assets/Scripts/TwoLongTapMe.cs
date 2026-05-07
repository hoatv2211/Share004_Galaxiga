using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class TwoLongTapMe : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_LongTapStart2Fingers += this.On_LongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers += this.On_LongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers += this.On_LongTapEnd2Fingers;
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
		EasyTouch.On_LongTapStart2Fingers -= this.On_LongTapStart2Fingers;
		EasyTouch.On_LongTap2Fingers -= this.On_LongTap2Fingers;
		EasyTouch.On_LongTapEnd2Fingers -= this.On_LongTapEnd2Fingers;
		EasyTouch.On_Cancel2Fingers -= this.On_Cancel2Fingers;
	}

	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startColor = base.gameObject.GetComponent<Renderer>().material.color;
	}

	private void On_LongTapStart2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.RandomColor();
		}
	}

	private void On_LongTap2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.textMesh.text = gesture.actionTime.ToString("f2");
		}
	}

	private void On_LongTapEnd2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = this.startColor;
			this.textMesh.text = "Long tap me";
		}
	}

	private void On_Cancel2Fingers(Gesture gesture)
	{
		this.On_LongTapEnd2Fingers(gesture);
	}

	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	private TextMesh textMesh;

	private Color startColor;
}
