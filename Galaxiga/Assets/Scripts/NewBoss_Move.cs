using System;
using UnityEngine;

public class NewBoss_Move : SmoothFollow
{
	private void Start()
	{
	}

	private void ActiveSmooth()
	{
		this.isSmoothFollow = true;
	}

	private void InActiveSmooth()
	{
		this.isSmoothFollow = false;
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.I))
		{
			this.ActiveSmooth();
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.O))
		{
			this.InActiveSmooth();
		}
	}

	private Vector2[] pathCross;

	private Vector2[] pathUp;

	private Vector2[] pathDown;
}
