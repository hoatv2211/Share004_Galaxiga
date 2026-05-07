using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class SimpleActionExample : MonoBehaviour
{
	private void Start()
	{
		this.textMesh = base.GetComponentInChildren<TextMesh>();
		this.startScale = base.transform.localScale;
	}

	public void ChangeColor(Gesture gesture)
	{
		this.RandomColor();
	}

	public void TimePressed(Gesture gesture)
	{
		this.textMesh.text = "Down since :" + gesture.actionTime.ToString("f2");
	}

	public void DisplaySwipeAngle(Gesture gesture)
	{
		float swipeOrDragAngle = gesture.GetSwipeOrDragAngle();
		this.textMesh.text = swipeOrDragAngle.ToString("f2") + " / " + gesture.swipe.ToString();
	}

	public void ChangeText(string text)
	{
		this.textMesh.text = text;
	}

	public void ResetScale()
	{
		base.transform.localScale = this.startScale;
	}

	private void RandomColor()
	{
		base.gameObject.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
	}

	private TextMesh textMesh;

	private Vector3 startScale;
}
