using System;
using System.Collections;
using UnityEngine;

public class UbhBulletHoming : MonoBehaviour
{
	private void Awake()
	{
		this.ubhBullet = base.GetComponent<UbhBullet>();
	}

	private void OnEnable()
	{
		base.StartCoroutine(this.StartShot());
	}

	private void OnDisable()
	{
		this.ubhShotCtrl.StopShotRoutine();
	}

	public IEnumerator StartShot()
	{
		yield return new WaitForSeconds(0.02f);
		this.ubhShotCtrl.StartShotRoutine();
		UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this.ubhBullet, false);
		yield break;
	}

	public UbhShotCtrl ubhShotCtrl;

	private UbhBullet ubhBullet;
}
