using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class TwoTapMe : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_SimpleTap2Fingers += this.On_SimpleTap2Fingers;
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
		EasyTouch.On_SimpleTap2Fingers -= this.On_SimpleTap2Fingers;
	}

	private void On_SimpleTap2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			this.RandomColor();
		}
	}

	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}
}
