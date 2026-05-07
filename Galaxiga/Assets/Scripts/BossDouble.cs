using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using SkyGameKit;
using UnityEngine;

public class BossDouble : BossMultyPoint
{
	private void Start()
	{
		SgkSingleton<LevelManager>.Instance.AliveEnemy.Add(this.transBoss1.GetComponent<BaseEnemy>());
		SgkSingleton<LevelManager>.Instance.AliveEnemy.Add(this.transBoss2.GetComponent<BaseEnemy>());
		for (int i = 0; i < SgkSingleton<LevelManager>.Instance.AliveEnemy.Count; i++)
		{
			if (this == SgkSingleton<LevelManager>.Instance.AliveEnemy[i])
			{
				SgkSingleton<LevelManager>.Instance.AliveEnemy.Remove(SgkSingleton<LevelManager>.Instance.AliveEnemy[i]);
				break;
			}
		}
	}

	[EnemyAction(displayName = "Boss/Xếp Hình Start")]
	public void ActionStart(float timeAttack, float duration)
	{
		base.StartCoroutine(this.XepHinhStart(timeAttack, duration));
	}

	private IEnumerator XepHinhStart(float timeAttack, float duration)
	{
		yield return new WaitForSeconds(timeAttack);
		this.posStartBoss1 = new Vector3(-2.25f, SgkCamera.topRight.y / 2f, 0f);
		this.posStartBoss2 = new Vector3(2.25f, SgkCamera.topRight.y / 2f, 0f);
		this.transBoss1.DOMove(this.posStartBoss1, 2f, false);
		this.transBoss2.DOMove(this.posStartBoss2, 2f, false);
		yield return null;
		base.CheckAction();
		yield break;
	}

	[EnemyAction(displayName = "Boss/Xếp Hình Dọc")]
	public void ActionXepHinhDoc(float timeAttack, float duration, bool checkAction)
	{
		base.StartCoroutine(this.XepHinhDoc(timeAttack, duration, checkAction));
	}

	private IEnumerator XepHinhDoc(float timeAttack, float duration, bool checkAction)
	{
		yield return new WaitForSeconds(timeAttack);
		this.posStartBoss1 = new Vector3(0f, SgkCamera.topRight.y / 2f, 0f);
		this.posStartBoss2 = new Vector3(0f, SgkCamera.bottomLeft.y / 2f, 0f);
		this.transBoss1.DOMove(this.posStartBoss1, duration, false);
		this.transBoss2.DOMove(this.posStartBoss2, duration, false);
		yield return null;
		if (checkAction)
		{
			base.CheckAction();
		}
		yield break;
	}

	[EnemyAction(displayName = "Boss/Xếp Hình Ngang")]
	public void ActionXepHinhNgang(float timeAttack, float duration, bool checkAction)
	{
		base.StartCoroutine(this.XepHinhNgang(timeAttack, duration, checkAction));
	}

	private IEnumerator XepHinhNgang(float timeAttack, float duration, bool checkAction)
	{
		yield return new WaitForSeconds(timeAttack);
		this.posStartBoss1 = new Vector3(-2.5f, 0f, 0f);
		this.posStartBoss2 = new Vector3(2.5f, 0f, 0f);
		this.transBoss1.DOMove(this.posStartBoss1, duration, false);
		this.transBoss2.DOMove(this.posStartBoss2, duration, false);
		yield return null;
		if (checkAction)
		{
			base.CheckAction();
		}
		yield break;
	}

	[EnemyAction(displayName = "Boss/Xoay Vòng")]
	public void Move(Vector2 pivot, float r, int numberLoop, float duration, bool checkAction)
	{
		this.angle = 0f;
		duration = 2f;
		if (this.loopCicrle != null)
		{
			this.loopCicrle.ForEach(delegate(EnemyEventUnit<float> x)
			{
				this.doActionAttack.Add(this.DoActionEveryTime(x.param, new Action(x.Invoke), false, false));
			});
		}
		DOTween.To(() => this.angle, delegate(float x)
		{
			this.angle = x;
		}, 360f, duration).OnUpdate(delegate
		{
			this.transBoss1.transform.position = pivot + Fu.RotateVector2(Vector2.left * r, this.angle);
		}).SetLoops(numberLoop, LoopType.Restart).SetEase(Ease.Linear).OnComplete(delegate
		{
			this.doActionAttack.ForEach(delegate(IDisposable x)
			{
				x.Dispose();
			});
			if (checkAction)
			{
				this.CheckAction();
			}
		});
		this.angle1 = 0f;
		DOTween.To(() => this.angle1, delegate(float x)
		{
			this.angle1 = x;
		}, 360f, duration).OnUpdate(delegate
		{
			this.transBoss2.transform.position = pivot + Fu.RotateVector2(Vector2.right * r, this.angle1);
		}).SetLoops(numberLoop, LoopType.Restart).SetEase(Ease.Linear);
	}

	[EnemyAction(displayName = "Boss/Bắn Đạn Homing")]
	public void ActionHoming(float timeAttack, bool checkAction)
	{
		base.StartCoroutine(this.AttackHoming(timeAttack, checkAction));
	}

	private IEnumerator AttackHoming(float timeAttack, bool checkAction)
	{
		yield return new WaitForSeconds(timeAttack);
		this.ubhHomingLeft.StartShotRoutine();
		this.ubhHomingRight.StartShotRoutine();
		yield return null;
		if (checkAction)
		{
			base.CheckAction();
		}
		yield break;
	}

	[EnemyAction(displayName = "Boss/Bắn Đạn 6 way")]
	public void Action6Way(float timeAttack, bool checkAction)
	{
		base.StartCoroutine(this.Attack6Way(timeAttack, checkAction));
	}

	private IEnumerator Attack6Way(float timeAttack, bool checkAction)
	{
		yield return new WaitForSeconds(timeAttack);
		this.ubh6WayLeft.StartShotRoutine();
		this.ubh6WayRight.StartShotRoutine();
		yield return null;
		if (checkAction)
		{
			base.CheckAction();
		}
		yield break;
	}

	[EnemyAction(displayName = "Boss/Bắn 1 Viên")]
	public void Action1Bullet(float timeAttack, bool checkAction)
	{
		base.StartCoroutine(this.Attack1Bullet(timeAttack, checkAction));
	}

	private IEnumerator Attack1Bullet(float timeAttack, bool checkAction)
	{
		yield return new WaitForSeconds(timeAttack);
		this.ubh1Bullet1.StartShotRoutine();
		this.ubh1Bullet2.StartShotRoutine();
		yield return null;
		if (checkAction)
		{
			base.CheckAction();
		}
		yield break;
	}

	[EnemyAction(displayName = "Boss/Bắn đạn Tròn")]
	public void ActionCicrle(float timeAttack, bool checkAction)
	{
		base.StartCoroutine(this.AttackCicrle(timeAttack, checkAction));
	}

	private IEnumerator AttackCicrle(float timeAttack, bool checkAction)
	{
		yield return new WaitForSeconds(timeAttack);
		this.ubhCicrle1.StartShotRoutine();
		this.ubhCicrle2.StartShotRoutine();
		yield return null;
		if (checkAction)
		{
			base.CheckAction();
		}
		yield break;
	}

	[EnemyAction(displayName = "Boss/Bắn Sprial")]
	public void ActionSprial(float timeAttack, bool checkAction)
	{
		base.StartCoroutine(this.AttackSprial(timeAttack, checkAction));
	}

	private IEnumerator AttackSprial(float timeAttack, bool checkAction)
	{
		yield return new WaitForSeconds(timeAttack);
		this.ubhSprial1.StartShotRoutine();
		this.ubhSprial2.StartShotRoutine();
		yield return null;
		if (checkAction)
		{
			base.CheckAction();
		}
		yield break;
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		Fu.SpawnExplosion(this.dieExplosion, this.transBoss1.position, Quaternion.identity);
		Fu.SpawnExplosion(this.dieExplosion, this.transBoss2.position, Quaternion.identity);
	}

	public Transform transBoss1;

	public Transform transBoss2;

	private Vector3 posStartBoss1;

	private Vector3 posStartBoss2;

	public UbhShotCtrl ubhHomingLeft;

	public UbhShotCtrl ubhHomingRight;

	public UbhShotCtrl ubh6WayLeft;

	public UbhShotCtrl ubh6WayRight;

	public UbhShotCtrl ubh1Bullet1;

	public UbhShotCtrl ubh1Bullet2;

	public UbhShotCtrl ubhCicrle1;

	public UbhShotCtrl ubhCicrle2;

	public UbhShotCtrl ubhSprial1;

	public UbhShotCtrl ubhSprial2;

	[EnemyEventCustom(paramName = "Second", displayName = "Boss/Xoay Vòng")]
	public EnemyEvent<float> loopCicrle = new EnemyEvent<float>();

	private List<IDisposable> doActionAttack = new List<IDisposable>();

	private float angle;

	private float angle1;
}
