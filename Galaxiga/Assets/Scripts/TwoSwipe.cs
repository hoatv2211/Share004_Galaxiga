using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;
using UnityEngine.UI;

public class TwoSwipe : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_SwipeStart2Fingers += this.On_SwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers += this.On_Swipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers += this.On_SwipeEnd2Fingers;
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
		EasyTouch.On_SwipeStart2Fingers -= this.On_SwipeStart2Fingers;
		EasyTouch.On_Swipe2Fingers -= this.On_Swipe2Fingers;
		EasyTouch.On_SwipeEnd2Fingers -= this.On_SwipeEnd2Fingers;
	}

	private void On_SwipeStart2Fingers(Gesture gesture)
	{
		this.swipeData.text = "You start a swipe";
	}

	private void On_Swipe2Fingers(Gesture gesture)
	{
		Vector3 touchToWorldPoint = gesture.GetTouchToWorldPoint(5f);
		this.trail.transform.position = touchToWorldPoint;
	}

	private void On_SwipeEnd2Fingers(Gesture gesture)
	{
		float swipeOrDragAngle = gesture.GetSwipeOrDragAngle();
		this.swipeData.text = string.Concat(new object[]
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

	public Text swipeData;
}
