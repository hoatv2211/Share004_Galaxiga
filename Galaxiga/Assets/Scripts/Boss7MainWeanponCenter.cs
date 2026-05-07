using System;
using System.Collections;
using UnityEngine;

public class Boss7MainWeanponCenter : MonoBehaviour
{
	private void Awake()
	{
		this.anim = base.GetComponent<Animator>();
	}

	private void Start()
	{
	}

	private IEnumerator StartShootTest()
	{
		this.ShootMainWeaponStep1();
		for (;;)
		{
			yield return new WaitForSeconds(8f);
			if (!base.gameObject.activeInHierarchy)
			{
				break;
			}
			this.ShootMainWeaponStep1();
		}
		base.StopAllCoroutines();
		yield break;
	}

	public void ShootMainWeaponStep1()
	{
		this.anim.SetTrigger("isAttack1");
	}

	public void ShootMainWeaponStep2()
	{
		this.anim.SetTrigger("isAttack2");
		if (base.gameObject.activeInHierarchy)
		{
			foreach (UbhShotCtrl ubhShotCtrl in this.gunCtrlList)
			{
				ubhShotCtrl.StartShotRoutine();
			}
		}
	}

	public void ShootMainWeaponStep3()
	{
		this.anim.SetTrigger("isAttack3");
		foreach (UbhShotCtrl ubhShotCtrl in this.gunCtrlList)
		{
			ubhShotCtrl.StopShotRoutine();
		}
	}

	public void ChangeAnim_Atk_To_Idle()
	{
	}

	public void FinishTweenShootMainWeapon()
	{
		this.StopShootMainWeapon();
	}

	private void StopShootMainWeapon()
	{
		foreach (UbhShotCtrl ubhShotCtrl in this.gunCtrlList)
		{
			ubhShotCtrl.StopShotRoutine();
		}
	}

	private Animator anim;

	public UbhShotCtrl[] gunCtrlList;
}
