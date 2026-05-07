using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class MultiCameraUI : MonoBehaviour
{
	public void AddCamera2(bool value)
	{
		this.AddCamera(this.cam2, value);
	}

	public void AddCamera3(bool value)
	{
		this.AddCamera(this.cam3, value);
	}

	public void AddCamera(Camera cam, bool value)
	{
		if (value)
		{
			EasyTouch.AddCamera(cam, false);
		}
		else
		{
			EasyTouch.RemoveCamera(cam);
		}
	}

	public Camera cam2;

	public Camera cam3;
}
