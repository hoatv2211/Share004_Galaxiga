using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class RTS_NewSyntaxe : MonoBehaviour
{
	private void Start()
	{
		this.cube = null;
	}

	private void Update()
	{
		Gesture current = EasyTouch.current;
		if (current.type == EasyTouch.EvtType.On_SimpleTap && current.pickedObject != null && current.pickedObject.name == "Cube")
		{
			this.ResteColor();
			this.cube = current.pickedObject;
			this.cube.GetComponent<Renderer>().material.color = Color.red;
			base.transform.Translate(Vector2.up, Space.World);
		}
		if (current.type == EasyTouch.EvtType.On_Swipe && current.touchCount == 1)
		{
			base.transform.Translate(Vector3.left * current.deltaPosition.x / (float)Screen.width);
			base.transform.Translate(Vector3.back * current.deltaPosition.y / (float)Screen.height);
		}
		if (current.type == EasyTouch.EvtType.On_Pinch)
		{
			Camera.main.fieldOfView += current.deltaPinch * 10f * Time.deltaTime;
		}
		if (current.type == EasyTouch.EvtType.On_Twist)
		{
			base.transform.Rotate(Vector3.up * current.twistAngle);
		}
	}

	private void ResteColor()
	{
		if (this.cube != null)
		{
			this.cube.GetComponent<Renderer>().material.color = new Color(0.235294119f, 0.56078434f, 0.7882353f);
		}
	}

	private GameObject cube;
}
