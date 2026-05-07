using System;
using SkyGameKit;
using UnityEngine;

public class MuiTenXanhEnemy : EnemyGeneral
{
	protected override void Update()
	{
		base.Update();
		this.avatar.transform.rotation = Quaternion.Lerp(this.avatar.transform.rotation, Fu.LookAt2D(this.avatar.transform.position - PlaneIngameManager.current.CurrentTransformPlayer.position, 0f), 2f * Time.deltaTime);
	}

	[EnemyAction(displayName = "Attack - Mui Ten Xanh /Giữ >> Tấn Công")]
	public override void Attack_Hold(float timeHold, bool random, int numStyAttack)
	{
		base.GetComponent<SmoothFollow>().enabled = false;
		base.Attack_Hold(timeHold, random, numStyAttack);
		this.Delay(timeHold + 0.1f, delegate
		{
			this.EnableMove();
		}, false);
	}

	private void EnableMove()
	{
		base.GetComponent<SmoothFollow>().enabled = true;
	}
}
