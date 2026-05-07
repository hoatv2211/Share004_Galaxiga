using System;
using System.Collections;
using UnityEngine;

public class NewBoss_BulletHomming : HomingMissile
{
	public void SetFollowTargetX(float time)
	{
		this.timeFollow = time;
		base.StartCoroutine(this.DelayFollowTargetX());
	}

	private IEnumerator DelayFollowTargetX()
	{
		this.needFollow = false;
		if (this.needFindTarget && this.target == null)
		{
			base.FindRandomTarget();
		}
		this.needFollow = true;
		yield return new WaitForSeconds(this.timeFollow);
		this.needFollow = false;
		yield break;
	}

	public float timeFollow = 3f;
}
