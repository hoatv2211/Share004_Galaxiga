using System;
using System.Collections;
using UnityEngine;

public class BossTutorial : BossGeneral
{
	private void Start()
	{
		base.StartCoroutine(this.Attack1());
		this._animGunLeft = this.gunLeft.GetComponent<Animator>();
		this._animGunRight = this.gunRight.GetComponent<Animator>();
	}

	private IEnumerator Attack1()
	{
		yield return new WaitForSeconds(this.timeAttack1);
		this._animGunLeft.SetTrigger("attack");
		this._animGunRight.SetTrigger("attack");
		this.objAttack1Left.StartShotRoutine();
		this.objAttack1Right.StartShotRoutine();
		yield return null;
		base.StartCoroutine(this.Attack2());
		yield break;
	}

	private IEnumerator Attack2()
	{
		yield return new WaitForSeconds(this.timeAttack2);
		this._animGunLeft.SetTrigger("attack");
		this._animGunRight.SetTrigger("attack");
		this.objAttack2Left.StartShotRoutine();
		this.objAttack2Right.StartShotRoutine();
		yield return null;
		base.StartCoroutine(this.Attack3());
		yield break;
	}

	private IEnumerator Attack3()
	{
		yield return new WaitForSeconds(this.timeAttack2);
		this.objAttack3.StartShotRoutine();
		yield return null;
		base.StartCoroutine(this.Attack1());
		yield break;
	}

	[Header("ATTACK_BOSS")]
	public float timeAttack1;

	public float timeAttack2;

	public GameObject gunLeft;

	public GameObject gunRight;

	public UbhShotCtrl objAttack1Left;

	public UbhShotCtrl objAttack1Right;

	public UbhShotCtrl objAttack2Left;

	public UbhShotCtrl objAttack2Right;

	public UbhShotCtrl objAttack3;

	public bool stSpawItem;

	private Animator _animGunLeft;

	private Animator _animGunRight;
}
