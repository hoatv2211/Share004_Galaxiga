using System;
using System.Collections;
using DG.Tweening;
using SkyGameKit;
using UnityEngine;

public class BossHLW : BossGeneral
{
	[EnemyAction(displayName = "BossHLW/Bắn Bí Ngô")]
	public void ActionSpawnPumpkin(float timeAttack, float percentHealth)
	{
		if (base.CheckHealthAction(percentHealth))
		{
			base.StartCoroutine(this.AttackPumpkin(timeAttack));
		}
		else
		{
			base.CheckAction();
		}
	}

	private IEnumerator AttackPumpkin(float timeAttack)
	{
		yield return new WaitForSeconds(timeAttack);
		this.ubhShot.StartShotRoutine();
		base.CheckAction();
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "BossHLW/Bắn Bí Ngô Đuổi")]
	public void ActionHoming(float timeAttack, float percentHealth)
	{
		if (base.CheckHealthAction(percentHealth))
		{
			base.StartCoroutine(this.AttackHoming(timeAttack));
		}
		else
		{
			base.CheckAction();
		}
	}

	private IEnumerator AttackHoming(float timeAttack)
	{
		yield return new WaitForSeconds(timeAttack);
		this.ubhShotHoming.StartShotRoutine();
		base.CheckAction();
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "BossHLW/Bắn Laze45")]
	public void ActionLaze45(float timeAttack, float timeRotate, float percentHealth)
	{
		if (base.CheckHealthAction(percentHealth))
		{
			this.isLeft45 *= -1;
			this.laze45.SetActive(true);
			this.warning.SetActive(true);
			base.StartCoroutine(this.AttackLaze45(timeAttack, timeRotate));
		}
		else
		{
			base.CheckAction();
		}
	}

	private IEnumerator AttackLaze45(float timeAttack, float timeRotate)
	{
		yield return new WaitForSeconds(timeAttack);
		this.groupLaze45.SetActive(true);
		base.StartCoroutine(this.RotateLaze(this.laze45.transform, this.isLeft45, timeRotate, this.groupLaze45, 90f));
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "BossHLW/Bắn Laze90")]
	public void ActionLaze90(float timeAttack, float timeRotate, float percentHealth)
	{
		if (base.CheckHealthAction(percentHealth))
		{
			this.isLeft90 *= -1;
			this.laze90.SetActive(true);
			this.warning.SetActive(true);
			base.StartCoroutine(this.AttackLaze90(timeAttack, timeRotate));
		}
		else
		{
			base.CheckAction();
		}
	}

	private IEnumerator AttackLaze90(float timeAttack, float timeRotate)
	{
		yield return new WaitForSeconds(timeAttack);
		this.groupLaze90.SetActive(true);
		base.StartCoroutine(this.RotateLaze(this.laze90.transform, this.isLeft90, timeRotate, this.groupLaze90, 90f));
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "BossHLW/Bắn 8 Laze")]
	public void Action8Laze(float timeAttack, float timeRotate, float percentHealth)
	{
		if (base.CheckHealthAction(percentHealth))
		{
			this.isLeft8 *= -1;
			this.allLaze.SetActive(true);
			this.warning.SetActive(true);
			base.StartCoroutine(this.Attack8Laze(timeAttack, timeRotate));
		}
		else
		{
			base.CheckAction();
		}
	}

	private IEnumerator Attack8Laze(float timeAttack, float timeRotate)
	{
		yield return new WaitForSeconds(timeAttack);
		this.groupAllLaze.SetActive(true);
		base.StartCoroutine(this.RotateLaze(this.allLaze.transform, this.isLeft8, timeRotate, this.groupAllLaze, 45f));
		yield return null;
		yield break;
	}

	private IEnumerator RotateLaze(Transform transLaze, int left, float timeRotate, GameObject groupLaze, float conner)
	{
		Vector3 rotationLaze = default(Vector3);
		yield return new WaitForSeconds(2f);
		if (left > 0)
		{
			rotationLaze = new Vector3(transLaze.rotation.x, transLaze.rotation.y, transLaze.rotation.z - conner);
		}
		else
		{
			rotationLaze = new Vector3(transLaze.rotation.x, transLaze.rotation.y, transLaze.rotation.z + conner);
		}
		transLaze.transform.DOLocalRotate(rotationLaze, timeRotate, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(delegate
		{
			transLaze.DOKill(false);
			this.StartCoroutine(this.ResetLaze(transLaze, groupLaze, timeRotate));
		});
		yield return null;
		yield break;
	}

	private IEnumerator ResetLaze(Transform transLaze, GameObject groupLaze, float timeRotate)
	{
		yield return new WaitForSeconds(timeRotate);
		UnityEngine.Debug.LogError("c");
		groupLaze.SetActive(false);
		transLaze.gameObject.SetActive(false);
		this.warning.SetActive(false);
		transLaze.rotation = Quaternion.Euler(0f, 0f, 0f);
		base.CheckAction();
		yield return null;
		yield break;
	}

	public GameObject laze45;

	public GameObject laze90;

	public GameObject allLaze;

	public GameObject groupLaze45;

	public GameObject groupLaze90;

	public GameObject groupAllLaze;

	private Animator _animLaze45;

	private Animator _animLaze90;

	public GameObject warning;

	private int isLeft45 = 1;

	private int isLeft90 = 1;

	private int isLeft8 = 1;

	public UbhShotCtrl ubhShot;

	public UbhShotCtrl ubhShotHoming;
}
