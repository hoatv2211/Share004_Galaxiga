using System;
using SkyGameKit;
using UnityEngine;

public class Boss7MainWeanponLeftRight : MonoBehaviour
{
	private void Awake()
	{
		this.anim = base.GetComponent<Animator>();
	}

	private void Update()
	{
		base.transform.rotation = Fu.LookAt2D(base.transform.position - PlaneIngameManager.current.CurrentTransformPlayer.position, 0f);
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

	public void ChangeAnim_Atk_To_Idle()
	{
	}

	public void ShootMainWeaponStep3()
	{
		this.anim.SetTrigger("isAttack3");
		this.StopShootMainWeapon();
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
