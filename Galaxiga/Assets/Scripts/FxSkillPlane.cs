using System;
using Spine.Unity;
using UnityEngine;

public class FxSkillPlane : MonoBehaviour
{
	private void Awake()
	{
		this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
	}

	private void Start()
	{
	}

	public void StartFxSkill()
	{
		this.skeletonAnimation.AnimationState.SetAnimation(0, this.animStart, false);
		this.skeletonAnimation.AnimationState.AddAnimation(1, this.animIdle, true, 0.5f);
	}

	public void StopFxSkill()
	{
		this.skeletonAnimation.AnimationState.SetAnimation(1, this.animHide, false);
	}

	[SpineAnimation("", "", true, false)]
	public string animStart;

	[SpineAnimation("", "", true, false)]
	public string animIdle;

	[SpineAnimation("", "", true, false)]
	public string animHide;

	private SkeletonAnimation skeletonAnimation;
}
