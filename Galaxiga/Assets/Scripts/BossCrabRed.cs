using System;
using System.Collections;
using DG.Tweening;
using SkyGameKit;
using UnityEngine;

public class BossCrabRed : BossGeneral
{
	private void Awake()
	{
		this._animGunRight = this.gunRight.GetComponent<Animator>();
		this._animGunLeft = this.gunLeft.GetComponent<Animator>();
		this._animBelly = this.belly.GetComponent<Animator>();
	}

	[EnemyAction(displayName = "BossCrabRed/SpawnEnemy")]
	public void StartSpawnEnemy(float timeHold, float timeEnd, SpawnByBoss sBB, Vector2 off)
	{
		this._animBelly.SetBool("hold", true);
		base.StartCoroutine(this.SpawnEnemyIE(timeHold, timeEnd, sBB, off));
	}

	private IEnumerator SpawnEnemyIE(float timeHold, float timeReturn, SpawnByBoss sBB, Vector2 off)
	{
		yield return new WaitForSeconds(timeHold);
		this.SpawnEnemy(sBB, off);
		base.StartCoroutine(this.EndSpawn(timeReturn));
		yield return null;
		yield break;
	}

	public void SpawnEnemy(SpawnByBoss sBB, Vector2 off)
	{
		sBB.boss = this.belly;
		sBB.offset = off;
		this.turnByBoss = FreeWave.StartTurn(sBB);
	}

	private IEnumerator EndSpawn(float timeReturn)
	{
		yield return new WaitForSeconds(timeReturn);
		this._animBelly.SetBool("hold", false);
		yield return null;
		base.CheckAction();
		yield break;
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		this.turnByBoss.ForceStopAndKillAllEnemies();
	}

	[EnemyAction(displayName = "BossCrabRed/Phi Thân")]
	public void StartLaze(float timeHold, float speed, float timeReturn)
	{
		this._animBelly.SetBool("hold", true);
		this._animGunLeft.SetBool("attack", true);
		this._animGunRight.SetBool("attack", true);
		base.StartCoroutine(this.ActiveLaze(timeHold, speed, timeReturn));
	}

	private IEnumerator ActiveLaze(float timeHold, float speed, float timeReturn)
	{
		yield return new WaitForSeconds(timeHold);
		this._animBelly.SetBool("attack", true);
		this._animGunLeft.SetBool("hold", true);
		this._animGunRight.SetBool("hold", true);
		this.laze.SetActive(true);
		base.StartCoroutine(this.MoveSpider(timeHold, speed, timeReturn));
		yield return null;
		yield break;
	}

	private IEnumerator MoveSpider(float timeHold, float speed, float timeReturn)
	{
		yield return new WaitForSeconds(timeHold);
		base.gameObject.GetComponent<SmoothFollow>().enabled = false;
		Vector3 posMoveBoss = new Vector3(base.transform.position.x, SgkCamera.bottomLeft.y + 2f, 0f);
		base.transform.DOMove(posMoveBoss, speed, false).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(delegate
		{
			this.transform.DOKill(false);
			this.StartCoroutine(this.ReturnSpider(timeHold));
		});
		yield return null;
		yield break;
	}

	private IEnumerator ReturnSpider(float timeHold)
	{
		yield return new WaitForSeconds(timeHold);
		Vector3 posMoveBoss = new Vector3(base.transform.position.x, SgkCamera.topRight.y - 2f, 0f);
		base.transform.DOMove(posMoveBoss, 3f, false).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(delegate
		{
			base.transform.DOKill(false);
			this.laze.SetActive(false);
			base.gameObject.GetComponent<SmoothFollow>().enabled = true;
			this._animBelly.SetBool("attack", false);
			this._animBelly.SetBool("hold", false);
			this._animGunLeft.SetBool("hold", false);
			this._animGunRight.SetBool("hold", false);
		});
		yield return null;
		base.CheckAction();
		yield break;
	}

	[EnemyAction(displayName = "BossCrabRed/Bắn 20 Way Turn")]
	public void Action20Way(float timeAttack)
	{
		base.StartCoroutine(this.Attack1UBh(timeAttack, this.way20Turn));
	}

	private IEnumerator Attack1UBh(float timeAttack, UbhShotCtrl ubh)
	{
		yield return new WaitForSeconds(timeAttack);
		ubh.StartShotRoutine();
		yield return null;
		base.CheckAction();
		yield break;
	}

	private IEnumerator Attack2GunUbh(float timeAttack, UbhShotCtrl ubh1, UbhShotCtrl ubh2)
	{
		yield return new WaitForSeconds(timeAttack);
		ubh2.StartShotRoutine();
		ubh1.StartShotRoutine();
		yield return null;
		base.CheckAction();
		yield break;
	}

	[EnemyAction(displayName = "BossCrabRed/Bắn 20 Way Accel")]
	public void Action20WayAccel(float timeAttack)
	{
		base.StartCoroutine(this.Attack1UBh(timeAttack, this.way20Accel));
	}

	[EnemyAction(displayName = "BossCrabRed/Bắn 2 Sin")]
	public void Action2Sin(float timeAttack)
	{
		base.StartCoroutine(this.Attack2GunUbh(timeAttack, this.sinLeft, this.sinRight));
	}

	[EnemyAction(displayName = "BossCrabRed/Bắn 5 Way")]
	public void Action5Way(float timeAttack)
	{
		base.StartCoroutine(this.Attack2GunUbh(timeAttack, this.way5Left, this.way5Right));
	}

	[EnemyAction(displayName = "BossCrabRed/Bắn Linear Aim")]
	public void ActionLinear(float timeAttack)
	{
		base.StartCoroutine(this.Attack2GunUbh(timeAttack, this.linearAimLeft, this.linearAimRight));
	}

	public Transform gunLeft;

	public Transform gunRight;

	public Transform belly;

	public GameObject laze;

	private Animator _animGunLeft;

	private Animator _animGunRight;

	private Animator _animBelly;

	public UbhShotCtrl way5Left;

	public UbhShotCtrl way5Right;

	public UbhShotCtrl linearAimLeft;

	public UbhShotCtrl linearAimRight;

	public UbhShotCtrl sinLeft;

	public UbhShotCtrl sinRight;

	public UbhShotCtrl way20Turn;

	public UbhShotCtrl way20Accel;

	private TurnManager turnByBoss;
}
