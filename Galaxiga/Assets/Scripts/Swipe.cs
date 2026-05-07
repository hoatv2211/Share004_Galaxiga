using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_SwipeStart += this.On_SwipeStart;
		EasyTouch.On_Swipe += this.On_Swipe;
		EasyTouch.On_SwipeEnd += this.On_SwipeEnd;
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
		EasyTouch.On_SwipeStart -= this.On_SwipeStart;
		EasyTouch.On_Swipe -= this.On_Swipe;
		EasyTouch.On_SwipeEnd -= this.On_SwipeEnd;
	}

	private void On_SwipeStart(Gesture gesture)
	{
		this.swipeText.text = "You start a swipe";
	}

	private void On_Swipe(Gesture gesture)
	{
		Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(5f);
		this.trail.transform.position = touchToWorldPoint;
	}

	private void On_SwipeEnd(Gesture gesture)
	{
		float swipeOrDragAngle = gesture.GetSwipeOrDragAngle();
		this.swipeText.text = string.Concat(new object[]
		{
			"Last swipe : ",
			gesture.swipe.ToString(),
			" /  vector : ",
			gesture.swipeVector.normalized,
			" / angle : ",
			swipeOrDragAngle.ToString("f2")
		});
	}

	public GameObject trail;

	public Text swipeText;
}
