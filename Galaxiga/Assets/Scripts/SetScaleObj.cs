using System;
using UnityEngine;

public class SetScaleObj : MonoBehaviour
{
	private void Start()
	{
		base.transform.localScale = this.scale;
	}

	private void Update()
	{
	}

	public Vector2 scale;
}
