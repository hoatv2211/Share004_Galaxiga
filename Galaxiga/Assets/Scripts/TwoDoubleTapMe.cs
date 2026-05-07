using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class TwoDoubleTapMe : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_DoubleTap2Fingers += this.On_DoubleTap2Fingers;
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
		EasyTouch.On_DoubleTap2Fingers -= this.On_DoubleTap2Fingers;
	}

	private void On_DoubleTap2Fingers(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		}
	}
}
