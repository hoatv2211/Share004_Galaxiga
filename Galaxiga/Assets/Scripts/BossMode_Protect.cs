using System;
using System.Collections;
using SkyGameKit;
using UnityEngine;

public class BossMode_Protect : MonoBehaviour
{
	public void SetStat(float _time)
	{
		this.timeReactiveProtect = _time;
	}

	public void TakeDamge()
	{
		this.numberUnLock++;
		if (this.numberUnLock == 3)
		{
			this.numberUnLock = 0;
			this.DestroyProtect();
		}
	}

	private void ActiveProtect()
	{
		this.anim.SetTrigger("animshow");
		this.listLock[0].Lock();
		this.listLock[1].Lock();
		this.listLock[2].Lock();
		SgkSingleton<LevelManager>.Instance.AliveEnemy.Remove(this.boss);
	}

	private void DestroyProtect()
	{
		SgkSingleton<LevelManager>.Instance.AliveEnemy.Add(this.boss);
		this.anim.SetTrigger("animhide");
		base.StartCoroutine(this.CountDownActive());
	}

	private IEnumerator CountDownActive()
	{
		yield return new WaitForSeconds(this.timeReactiveProtect);
		if (!this.isActive)
		{
			this.ActiveProtect();
		}
		yield break;
	}

	private bool isActive;

	private int numberUnLock;

	public float timeReactiveProtect = 20f;

	[SerializeField]
	private BaseEnemy boss;

	[SerializeField]
	private Animator anim;

	[SerializeField]
	private BossMode_Lock[] listLock;
}
