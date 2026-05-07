using System;
using System.Collections;
using DG.Tweening;
using SkyGameKit;
using UnityEngine;

public class BossButterfly : BossGeneral
{
	private void Start()
	{
		this._animGunLeft = this.gunLeft.GetComponent<Animator>();
		this._animGunRight = this.gunRight.GetComponent<Animator>();
	}

	[EnemyAction(displayName = "Boss/Bắn Laze Ngoài Vào Trong")]
	public void ActionAttackLaze(float timeHold, float timeAttack, float timeOffLaze)
	{
		this._animGunLeft.SetTrigger("hold");
		this._animGunRight.SetTrigger("hold");
		base.StartCoroutine(this.ActiveLazeOutIn(timeHold, timeAttack, timeOffLaze));
	}

	[EnemyAction(displayName = "Boss/Bắn Laze Trong Ra Ngoài")]
	public void ActionAttackLazeInOut(float timeHold, float timeAttack, float timeOffLaze)
	{
		this._animGunLeft.SetTrigger("hold");
		this._animGunRight.SetTrigger("hold");
		base.StartCoroutine(this.ActiveLazeInOut(timeHold, timeAttack, timeOffLaze));
	}

	[EnemyAction(displayName = "Boss/Bắn 20 Wave Turn All Rank")]
	public void ActionAttack20WaveTurnAllRank(float timeAttack)
	{
		base.StartCoroutine(this.Attack20Wave(timeAttack));
	}

	[EnemyAction(displayName = "Boss/Bắn RanDom")]
	public void ActionRanDom(float timeAttack)
	{
		base.StartCoroutine(this.AttackRanDom(timeAttack));
	}

	[EnemyAction(displayName = "Boss/Bắn 3Wave")]
	public void Action3Wave(float timeAttack)
	{
		base.StartCoroutine(this.Attack3Wave(timeAttack));
	}

	[EnemyAction(displayName = "Boss/ Hình Tròn")]
	public void ActionCicrle(float timeAttack)
	{
		base.StartCoroutine(this.AttackCicrle(timeAttack));
	}

	private IEnumerator AttackCicrle(float timeAttack)
	{
		yield return new WaitForSeconds(timeAttack);
		this.cicrleUbh.StartShotRoutine();
		yield return null;
		base.CheckAction();
		yield break;
	}

	private IEnumerator Attack3Wave(float timeAttack)
	{
		yield return new WaitForSeconds(timeAttack);
		this.way2Left.StartShotRoutine();
		this.way2Right.StartShotRoutine();
		yield return null;
		base.CheckAction();
		yield break;
	}

	private IEnumerator Attack20Wave(float timeAttack)
	{
		yield return new WaitForSeconds(timeAttack);
		this.attack20wave.StartShotRoutine();
		yield return null;
		base.CheckAction();
		yield break;
	}

	private IEnumerator AttackRanDom(float timeAttack)
	{
		yield return new WaitForSeconds(timeAttack);
		this.randomUbh.StartShotRoutine();
		yield return null;
		base.CheckAction();
		yield break;
	}

	private IEnumerator ActiveLazeOutIn(float timeHold, float timeAttack, float timeOffLaze)
	{
		yield return new WaitForSeconds(timeHold);
		this.gunLeft.rotation = Quaternion.Euler(0f, 0f, -18f);
		this.gunRight.rotation = Quaternion.Euler(0f, 0f, 18f);
		this._animGunLeft.SetTrigger("attack");
		this._animGunRight.SetTrigger("attack");
		this.lazeLeft.SetActive(true);
		this.lazeRight.SetActive(true);
		base.StartCoroutine(this.AttackLaze(timeAttack, timeOffLaze));
		yield return null;
		yield break;
	}

	private IEnumerator AttackLaze(float timeAttack, float timeOffLaze)
	{
		yield return new WaitForSeconds(timeAttack);
		this.gunLeft.DORotate(new Vector3(0f, 0f, 0f), 3f, RotateMode.Fast).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InSine).OnComplete(delegate
		{
			this.StartCoroutine(this.OffLaze(timeOffLaze));
		});
		this.gunRight.DORotate(new Vector3(0f, 0f, 0f), 3f, RotateMode.Fast).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InSine);
		yield return null;
		yield break;
	}

	private IEnumerator OffLaze(float timeOffLaze)
	{
		yield return new WaitForSeconds(timeOffLaze);
		this._animGunLeft.SetTrigger("idle");
		this._animGunRight.SetTrigger("idle");
		this.gunLeft.rotation = Quaternion.Euler(0f, 0f, -18f);
		this.gunRight.rotation = Quaternion.Euler(0f, 0f, 18f);
		this.gunLeft.DOKill(false);
		this.gunRight.DOKill(false);
		this.lazeLeft.SetActive(false);
		this.lazeRight.SetActive(false);
		yield return null;
		base.CheckAction();
		yield break;
	}

	private IEnumerator ActiveLazeInOut(float timeHold, float timeAttack, float timeOffLaze)
	{
		this.gunLeft.rotation = Quaternion.Euler(0f, 0f, 5f);
		this.gunRight.rotation = Quaternion.Euler(0f, 0f, -5f);
		yield return new WaitForSeconds(timeHold);
		this._animGunLeft.SetTrigger("attack");
		this._animGunRight.SetTrigger("attack");
		this.lazeLeft.SetActive(true);
		this.lazeRight.SetActive(true);
		base.StartCoroutine(this.AttackLazeInOut(timeAttack, timeOffLaze));
		yield return null;
		yield break;
	}

	private IEnumerator AttackLazeInOut(float timeAttack, float timeOffLaze)
	{
		yield return new WaitForSeconds(timeAttack);
		this.gunLeft.DORotate(new Vector3(0f, 0f, -18f), 3f, RotateMode.Fast).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InSine).OnComplete(delegate
		{
			this.StartCoroutine(this.OffLaze(timeOffLaze));
		});
		this.gunRight.DORotate(new Vector3(0f, 0f, 18f), 3f, RotateMode.Fast).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InSine);
		yield return null;
		yield break;
	}

	public Transform gunLeft;

	public Transform gunRight;

	public GameObject lazeLeft;

	public GameObject lazeRight;

	private Animator _animGunLeft;

	private Animator _animGunRight;

	public UbhShotCtrl attack20wave;

	public UbhShotCtrl randomUbh;

	public UbhShotCtrl way2Left;

	public UbhShotCtrl way2Right;

	public UbhShotCtrl cicrleUbh;
}
