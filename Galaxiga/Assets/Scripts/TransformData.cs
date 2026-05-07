using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public struct TransformData
{
	public TransformData(Transform trans)
	{
		this.pos = trans.position;
		this.rot = trans.rotation.eulerAngles.z;
		this.T = Time.time;
	}

	public Quaternion R
	{
		[CompilerGenerated]
		get
		{
			return Quaternion.Euler(0f, 0f, this.rot);
		}
	}

	public Vector3 P
	{
		[CompilerGenerated]
		get
		{
			return this.pos;
		}
	}

	public float T { get; }

	private readonly Vector2 pos;

	private readonly float rot;
}
