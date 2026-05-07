using System;
using System.Collections;
using UnityEngine;

public class AvatarSpawnLaze : AvatarSpawnBullet
{
	private void Awake()
	{
		this._anim = base.GetComponent<Animator>();
	}

	private void OnBecameInvisible()
	{
		this.objLaze.SetActive(false);
		this._anim.SetTrigger("idle");
		if (this.stFlowPlayer)
		{
			base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			this.stFlowPlayer = false;
		}
	}

	public override void SpawBullet()
	{
		this.warningLaze.SetActive(false);
		this.objLaze.SetActive(true);
		if (this.effectHold != null)
		{
			this.effectHold.SetActive(false);
		}
		if (this.effectAttack != null)
		{
			this.effectAttack.SetActive(true);
		}
		base.StartCoroutine(this.OffEffectAttack());
		base.StartCoroutine(this.OffLaze());
	}

	private IEnumerator OffLaze()
	{
		yield return new WaitForSeconds(this.timeOffLaze);
		this.objLaze.SetActive(false);
		this._anim.SetTrigger("idle");
		if (this.stFlowPlayer)
		{
			base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			this.stFlowPlayer = false;
		}
		yield return null;
		yield break;
	}

	public override IEnumerator OffEffectAttack()
	{
		yield return new WaitForSeconds(0.5f);
		if (this.effectHold != null)
		{
			this.effectHold.SetActive(false);
		}
		if (this.effectAttack != null)
		{
			this.effectAttack.SetActive(false);
		}
		yield return null;
		yield break;
	}

	public override void ActiveEffect()
	{
		base.ActiveEffect();
		this.warningLaze.SetActive(true);
	}

	public void OffFollowPlayer()
	{
		if (this.stFlowPlayer)
		{
			base.GetComponent<EnemyFollowPlayer>().enabled = false;
		}
	}

	public GameObject objLaze;

	public GameObject warningLaze;

	public float timeOffLaze;

	private Animator _anim;
}
