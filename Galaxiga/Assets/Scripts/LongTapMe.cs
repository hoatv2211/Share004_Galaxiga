using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class LongTapMe : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_LongTapStart += this.On_LongTapStart;
		EasyTouch.On_LongTap += this.On_LongTap;
		EasyTouch.On_LongTapEnd += this.On_LongTapEnd;
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
		EasyTouch.On_LongTapStart -= this.On_LongTapStart;
		EasyTouch.On_LongTap -= this.On_LongTap;
		EasyTouch.On_LongTapEnd -= this.On_LongTapEnd;
	}

	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startColor = base.gameObject.GetComponent<Renderer>().material.color;
	}

	private void On_LongTapStart(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.RandomColor();
		}
	}

	private void On_LongTap(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.textMesh.text = gesture.actionTime.ToString("f2");
		}
	}

	private void On_LongTapEnd(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = this.startColor;
			this.textMesh.text = "Long tap me";
		}
	}

	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	private TextMesh textMesh;

	private Color startColor;
}
