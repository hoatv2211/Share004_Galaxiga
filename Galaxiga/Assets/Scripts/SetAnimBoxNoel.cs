using System;
using Spine.Unity;
using UnityEngine;

public class SetAnimBoxNoel : MonoBehaviour
{
	private void Awake()
	{
		this.skeletonGraphic = base.GetComponent<SkeletonGraphic>();
	}

	public void SetAnimOpen()
	{
		this.skeletonGraphic.AnimationState.SetAnimation(0, this.animOpen, false);
	}

	public void SetAnimShake()
	{
		this.skeletonGraphic.AnimationState.SetAnimation(0, this.animShake, true);
	}

	[SpineAnimation("", "", true, false)]
	public string animOpen;

	[SpineAnimation("", "", true, false)]
	public string animShake;

	private SkeletonGraphic skeletonGraphic;
}
