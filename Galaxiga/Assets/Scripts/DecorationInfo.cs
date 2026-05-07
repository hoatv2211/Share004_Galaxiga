using System;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

[Serializable]
public class DecorationInfo
{
	public bool FollowBackgroundIsNull
	{
		get
		{
			return this.followBackground == null;
		}
	}

	public Transform transform;

	public OffsetScroller followBackground;

	[ShowIf("FollowBackgroundIsNull", true)]
	public float speed = 2f;

	public bool randomX = true;

	public bool randomY;

	[ShowIf("randomY", true)]
	public float maxRandomY = 30f;
}
