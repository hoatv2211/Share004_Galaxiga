using System;
using System.Collections;
using SkyGameKit;
using UnityEngine;

public class Boss7MainWeanponHead : MonoBehaviour
{
	private void Awake()
	{
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

	private void OnDisable()
	{
		this.effectStep1Gun.gameObject.SetActive(false);
	}

	public void ShootMainWeaponStep1()
	{
		this.effectStep1Gun.gameObject.SetActive(true);
		this.effectStep1Gun.Play(true);
		this.ShootMainWeaponStep2();
	}

	public void ShootMainWeaponStep2()
	{
		this.Delay(2f, delegate
		{
			if (base.gameObject.activeInHierarchy)
			{
				foreach (UbhShotCtrl ubhShotCtrl in this.gunCtrlList)
				{
					ubhShotCtrl.StartShotRoutine();
				}
			}
			this.ShootMainWeaponStep3();
		}, false);
	}

	public void ShootMainWeaponStep3()
	{
		this.Delay(2f, delegate
		{
			foreach (UbhShotCtrl ubhShotCtrl in this.gunCtrlList)
			{
				ubhShotCtrl.StopShotRoutine();
			}
			this.effectStep1Gun.Stop();
			this.effectStep1Gun.gameObject.SetActive(false);
		}, false);
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

	public ParticleSystem effectStep1Gun;

	public UbhShotCtrl[] gunCtrlList;
}
