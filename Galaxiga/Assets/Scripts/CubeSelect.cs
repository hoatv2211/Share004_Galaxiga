using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class CubeSelect : MonoBehaviour
{
	private void OnEnable()
	{
		EasyTouch.On_SimpleTap += this.On_SimpleTap;
	}

	private void OnDestroy()
	{
		EasyTouch.On_SimpleTap -= this.On_SimpleTap;
	}

	private void Start()
	{
		this.cube = null;
	}

	private void On_SimpleTap(Gesture gesture)
	{
		if (gesture.pickedObject != null && gesture.pickedObject.name == "Cube")
		{
			this.ResteColor();
			this.cube = gesture.pickedObject;
			this.cube.GetComponent<Renderer>().material.color = Color.red;
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
