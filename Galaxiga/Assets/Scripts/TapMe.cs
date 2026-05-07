using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class TapMe : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_SimpleTap += this.On_SimpleTap;
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
		EasyTouch.On_SimpleTap -= this.On_SimpleTap;
	}

	private void On_SimpleTap(Gesture gesture)
	{
		if (gesture.pickedObject == base.gameObject)
		{
			base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
		}
	}
}
