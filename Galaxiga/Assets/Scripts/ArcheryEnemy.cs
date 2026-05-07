using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SkyGameKit;
using UnityEngine;

public class ArcheryEnemy : BoxItemEnemy
{
	private void Awake()
	{
		this.enemyFollowPlayer = this.avatar.GetComponent<EnemyFollowPlayer>();
		this._anim = this.avatar.GetComponent<Animator>();
	}

	public override void Restart()
	{
		base.StopAllCoroutines();
		this.score = BaseEnemyDataSheet.Get(this.id).scoreEnemy;
		base.Restart();
		this.startHP = this.CurrentHP;
		this.Delay(0.1f, delegate
		{
			if (this.EventLoop != null)
			{
				this.EventLoop.ForEach(delegate(EnemyEventUnit<float> x)
				{
					this.DoActionEveryTime(x.param, new Action(x.Invoke), false, false);
				});
			}
			if (this.Index == base.MotherTurn.TotalEnemy - 1)
			{
				List<BaseEnemy> list = new List<BaseEnemy>();
				list = base.MotherTurn.GetAliveEnemy();
				for (int i = 0; i < list.Count; i++)
				{
					(list[i] as ArcheryEnemy).SetEventLoop();
				}
			}
		}, false);
	}

	public void SetEventLoop()
	{
		if (this.EveryXSEvent != null)
		{
			this.EveryXSEvent.ForEach(delegate(EnemyEventUnit<float> x)
			{
				this.DoActionEveryTime(x.param, new Action(x.Invoke), false, false);
			});
		}
	}

	[EnemyAction(displayName = "Attack/Xếp Hình - Phi Thân")]
	public void PhiThanShape(float speed, Ease ease, float timeHold, float percentAttack)
	{
		float num = (float)UnityEngine.Random.Range(0, 100);
		if (num < percentAttack && !this.stAttack)
		{
			this.SetAttack(speed, ease);
			this.stShape = true;
			this.AttackPlayer(timeHold);
		}
	}

	[EnemyAction(displayName = "Attack/Phi Thân")]
	public void PhiThan(float speed, Ease ease, float timeHold, float percentAttack)
	{
		float num = (float)UnityEngine.Random.Range(0, 100);
		if (num < percentAttack && !this.stAttack)
		{
			base.GetComponent<SmoothFollow>().enabled = false;
			this.stShape = false;
			this.SetAttack(speed, ease);
			this.AttackPlayer(timeHold);
		}
	}

	private void AttackPlayer(float timeHold)
	{
		base.StartCoroutine(this.MoveAttackIE(timeHold));
	}

	private IEnumerator MoveAttackIE(float timeHold)
	{
		yield return new WaitForSeconds(timeHold);
		this.MoveAttack();
		yield return null;
		yield break;
	}

	private void SetAttack(float speed, Ease ease)
	{
		this._anim.SetTrigger("attack");
		this.speedAttack = speed;
		this.easeDf = ease;
		this.stAttack = true;
	}

	private void ResetAttack()
	{
		this._anim.SetTrigger("idle");
		this.effectHold.SetActive(false);
		this.enemyFollowPlayer.enabled = true;
	}

	public void MoveAttack()
	{
		if (this.stAttack)
		{
			this.effectHold.SetActive(true);
			this.enemyFollowPlayer.enabled = false;
			if (this.stShape)
			{
				this.pointLocal = base.transform.localPosition;
				base.transform.DOMove(PlaneIngameManager.current.CurrentTransformPlayer.position, this.speedAttack, false).SetSpeedBased(true).SetEase(this.easeDf).OnComplete(delegate
				{
					this.ResetAttack();
					base.transform.DOLocalMove(this.pointLocal, this.speedAttack / 3f, false).SetSpeedBased(true).OnComplete(delegate
					{
						this.stAttack = false;
					});
				});
			}
			else
			{
				base.transform.DOMove(PlaneIngameManager.current.CurrentTransformPlayer.position, this.speedAttack, false).SetSpeedBased(true).SetEase(this.easeDf).OnComplete(delegate
				{
					this.ResetAttack();
					this.stAttack = false;
					base.GetComponent<SmoothFollow>().enabled = true;
				});
			}
		}
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		this.ResetAttack();
	}

	private EnemyFollowPlayer enemyFollowPlayer;

	private Animator _anim;

	public GameObject effectHold;

	public GameObject avatar;

	private float speedAttack;

	private bool stShape;

	private Ease easeDf;

	private bool stAttack;

	private Vector3 pointLocal;

	private Vector3 pointPlayer;

	[EnemyEventCustom(paramName = "time", displayName = "BaseEnemy/Event Lặp sau Khi tất cả Enemy Sinh ra")]
	public EnemyEvent<float> EveryXSEvent = new EnemyEvent<float>();

	[EnemyEventCustom(paramName = "time", displayName = "BaseEnemy/Event Lặp Khi Enemy sinh ra")]
	public EnemyEvent<float> EventLoop = new EnemyEvent<float>();
}
