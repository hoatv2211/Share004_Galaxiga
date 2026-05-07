using System;
using System.Collections;
using DG.Tweening;
using SkyGameKit;
using UnityEngine;

public class BossCrab : BossGeneral
{
	private void Start()
	{
	}

	[EnemyAction(displayName = "Boss/Bắn Đạn hình Sin")]
	public void ActionAttackSin(float timeAttack)
	{
		base.StartCoroutine(this.AttackSin(timeAttack));
	}

	[EnemyAction(displayName = "Boss/Bắn Đạn Homing")]
	public void ActionHoming(float timeAttack)
	{
		base.StartCoroutine(this.AttackHoming(timeAttack));
	}

	[EnemyAction(displayName = "Boss/Phi Tay Thẳng")]
	public void ActionAttack1(float timeAttack, float speed)
	{
		if (speed == 0f)
		{
			speed = 5f;
		}
		base.StartCoroutine(this.Attack1(timeAttack, speed));
	}

	[EnemyAction(displayName = "Boss/Phi Tay Về Player")]
	public void ActionAttack2(float timeAttack, float speed)
	{
		if (speed == 0f)
		{
			speed = 5f;
		}
		base.StartCoroutine(this.Attack2(timeAttack, speed));
	}

	[EnemyAction(displayName = "Boss/Bắn 2Way")]
	public void Action2Way(float timeAttack)
	{
		base.StartCoroutine(this.Attack2Way(timeAttack));
	}

	[EnemyAction(displayName = "Boss/Bắn Sprial")]
	public void ActionSprial(float timeAttack)
	{
		base.StartCoroutine(this.AttackSprial(timeAttack));
	}

	private IEnumerator Attack1(float timeAttack, float speed)
	{
		yield return new WaitForSeconds(this.timeAttack1);
		Transform Target = PlaneIngameManager.current.CurrentTransformPlayer;
		Vector3 posLocalCrabLeft = this.crabLeft.transform.localPosition;
		Vector3 posLocalCrabRight = this.crabRight.transform.localPosition;
		this.SetMoveRotateCrab(this.crabLeft, Target.position, posLocalCrabLeft, new Vector3(0f, 0f, 360f), speed);
		this.SetMoveRotateCrab(this.crabRight, Target.position, posLocalCrabRight, new Vector3(0f, 0f, -360f), speed);
		yield return null;
		base.CheckAction();
		yield break;
	}

	private IEnumerator Attack2(float timeAttack, float speed)
	{
		yield return new WaitForSeconds(timeAttack);
		Vector3 posLocalCrabLeft = this.crabLeft.transform.localPosition;
		Vector3 posLocalCrabRight = this.crabRight.transform.localPosition;
		Vector3 posLeft = new Vector3(this.crabLeft.transform.localPosition.x, this.crabLeft.transform.localPosition.y - 7f, this.crabLeft.transform.localPosition.z);
		Vector3 posRight = new Vector3(this.crabRight.transform.localPosition.x, this.crabRight.transform.localPosition.y - 7f, this.crabRight.transform.localPosition.z);
		this.SetMoveRotateCrab(this.crabLeft, posLeft, posLocalCrabLeft, new Vector3(0f, 0f, 360f), speed);
		this.SetMoveRotateCrab(this.crabRight, posRight, posLocalCrabRight, new Vector3(0f, 0f, -360f), speed);
		yield return null;
		base.CheckAction();
		yield break;
	}

	private void SetMoveRotateCrab(GameObject obj, Vector3 posMove, Vector3 posReturn, Vector3 rotateObj, float speed)
	{
		obj.transform.DOMove(posMove, speed, false).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(delegate
		{
			obj.transform.DOLocalMove(posReturn, speed, false).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(delegate
			{
				obj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
				obj.transform.DOKill(false);
			});
		});
		obj.transform.DOLocalRotate(rotateObj, speed, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
	}

	private IEnumerator Attack2Way(float timeAttack)
	{
		yield return new WaitForSeconds(timeAttack);
		this.way2Left.StartShotRoutine();
		this.way2Right.StartShotRoutine();
		yield return null;
		base.CheckAction();
		yield break;
	}

	private IEnumerator AttackSprial(float timeAttack)
	{
		yield return new WaitForSeconds(timeAttack);
		this.sprialUbh.StartShotRoutine();
		yield return null;
		base.CheckAction();
		yield break;
	}

	private IEnumerator AttackHoming(float timeAttack)
	{
		yield return new WaitForSeconds(timeAttack);
		this.homingLeft.StartShotRoutine();
		this.homingRight.StartShotRoutine();
		yield return null;
		base.CheckAction();
		yield break;
	}

	private IEnumerator AttackSin(float timeAttack)
	{
		yield return new WaitForSeconds(timeAttack);
		this.sinUbh.StartShotRoutine();
		yield return null;
		base.CheckAction();
		yield break;
	}

	public GameObject crabLeft;

	public GameObject crabRight;

	public GameObject belly;

	public float timeAttack1;

	public float timeAttack2;

	public UbhShotCtrl way2Left;

	public UbhShotCtrl way2Right;

	public UbhShotCtrl homingLeft;

	public UbhShotCtrl homingRight;

	public UbhShotCtrl sprialUbh;

	public UbhShotCtrl sinUbh;
}
