using System;
using System.Collections;
using SkyGameKit;
using UnityEngine;

public class SplitshotAlienEnemy : EnemyGeneral
{
	public override void Restart()
	{
		base.Restart();
		this._anim = this.avatar.GetComponent<Animator>();
	}

	public override void Attack(bool randomTime, int percentAttack, int numStyleAttack, float delaytime = 1f)
	{
		base.Attack(randomTime, percentAttack, numStyleAttack, delaytime);
		this.effDefend.SetActive(false);
		this.hitbox.SetActive(true);
		base.StartCoroutine(this.ResetAttack(delaytime));
	}

	public void SetIdle()
	{
		this.effDefend.SetActive(true);
		this.hitbox.SetActive(false);
	}

	private IEnumerator ResetAttack(float delaytime)
	{
		yield return new WaitForSeconds(delaytime + 2f);
		this.SetIdle();
		this._anim.SetTrigger("idle");
		yield return null;
		yield break;
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		this.SetIdle();
	}

	public GameObject effDefend;

	public GameObject hitbox;

	private Animator _anim;
}
