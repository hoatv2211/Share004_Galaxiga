using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSpawnBullet : MonoBehaviour
{
	public virtual void SpawBullet()
	{
		if (this.numberAttack >= this.objSpawnBullet.Count)
		{
			this.numberAttack = 0;
		}
		if (this.objSpawnBullet[this.numberAttack] != null)
		{
			this.objSpawnBullet[this.numberAttack].StartShotRoutine();
		}
		if (this.effectHold != null)
		{
			this.effectHold.SetActive(false);
		}
		if (this.effectAttack != null)
		{
			this.effectAttack.SetActive(true);
		}
		base.StartCoroutine(this.OffEffectAttack());
	}

	public virtual void ActiveEffect()
	{
		if (this.effectHold != null)
		{
			this.effectHold.SetActive(true);
		}
	}

	public virtual IEnumerator OffEffectAttack()
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
		if (this.stFlowPlayer)
		{
			base.GetComponent<EnemyFollowPlayer>().enabled = false;
			base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			this.stFlowPlayer = false;
		}
		yield return null;
		yield break;
	}

	public void OnObj()
	{
	}

	public List<UbhShotCtrl> objSpawnBullet = new List<UbhShotCtrl>();

	public int numberAttack;

	public GameObject effectHold;

	public GameObject effectAttack;

	public Quaternion rotAvatar;

	public bool stFlowPlayer;

	public bool stSpawnLaze;
}
