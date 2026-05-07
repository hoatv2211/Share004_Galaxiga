using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using SkyGameKit;
using UnityEngine;

public class BossStar : BossGeneral
{
	private void Awake()
	{
		this._animStar = this.starAll.GetComponent<Animator>();
	}

	private void Start()
	{
		this.AddListMove();
	}

	[EnemyAction(displayName = "BossStar/Bắn Laze - Xoay 60 Độ")]
	public void ActionLaze60(float timeAttack, float timeRotate)
	{
		this.isRotateLeft *= -1;
		if (this.isRotateLeft < 0)
		{
			this.warningLeft.SetActive(true);
		}
		else
		{
			this.warningRight.SetActive(true);
		}
		this.effectHold.SetActive(true);
		this._animStar.SetTrigger("warning");
		base.StartCoroutine(this.ActiveLaze(timeAttack, timeRotate));
	}

	private IEnumerator ActiveLaze(float timeAttack, float timeRotate)
	{
		yield return new WaitForSeconds(timeAttack);
		this.ActiveGroupLaze();
		this._animStar.SetTrigger("attack");
		this.effectHold.SetActive(false);
		base.StartCoroutine(this.RotateLaze(this.star60.transform, this.isRotateLeft, timeRotate, 60f, true));
		yield return null;
		yield break;
	}

	private IEnumerator RotateLaze(Transform transLaze, int left, float timeRotate, float conner, bool loop)
	{
		this.warningLeft.SetActive(false);
		this.warningRight.SetActive(false);
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
		if (loop)
		{
			transLaze.transform.DOLocalRotate(rotationLaze, timeRotate, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo).OnComplete(delegate
			{
				transLaze.DOKill(false);
				this.StartCoroutine(this.ResetLaze(transLaze, timeRotate));
			});
		}
		else
		{
			transLaze.transform.DOLocalRotate(rotationLaze, timeRotate, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(delegate
			{
				transLaze.DOKill(false);
				this.StartCoroutine(this.ResetLaze(transLaze, timeRotate));
			});
		}
		yield return null;
		yield break;
	}

	private IEnumerator ResetLaze(Transform transLaze, float timeRotate)
	{
		yield return new WaitForSeconds(timeRotate);
		transLaze.rotation = Quaternion.Euler(0f, 0f, 0f);
		this._animStar.SetTrigger("idle");
		this.OffGroupLaze();
		base.CheckAction();
		yield return null;
		yield break;
	}

	public void ActiveGroupLaze()
	{
		this.groupLaze1.SetActive(true);
		this.groupLaze2.SetActive(true);
	}

	public void OffGroupLaze()
	{
		this.groupLaze1.SetActive(false);
		this.groupLaze2.SetActive(false);
	}

	[EnemyAction(displayName = "BossStar/Bắn Star")]
	public void ActionStar(float timeAttack, float timeRotate)
	{
		this.isRotateStarLeft *= -1;
		this.starAll.transform.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
		base.StartCoroutine(this.ActiveStar(timeAttack, timeRotate));
	}

	private IEnumerator ActiveStar(float timeAttack, float timeRotate)
	{
		yield return new WaitForSeconds(timeAttack);
		if (this.isRotateStarLeft > 0)
		{
			this.starAll.transform.DOPath(this.listLeft, timeRotate, PathType.Linear, PathMode.Full3D, 10, null).OnComplete(delegate
			{
				this.ResetStar();
			});
		}
		else
		{
			this.starAll.transform.DOPath(this.listRight, timeRotate, PathType.Linear, PathMode.Full3D, 10, null).OnComplete(delegate
			{
				this.ResetStar();
			});
		}
		yield return null;
		yield break;
	}

	private void AddListMove()
	{
		this.posStartStar = this.starAll.transform.localPosition;
		this.listLeft[0] = new Vector3(0f, SgkCamera.topRight.y, 0f);
		this.listLeft[1] = new Vector3(-4.5f, 0f, 0f);
		this.listLeft[2] = new Vector3(0f, SgkCamera.bottomLeft.y, 0f);
		this.listLeft[3] = new Vector3(4.5f, 0f, 0f);
		this.listLeft[4] = new Vector3(0f, SgkCamera.topRight.y, 0f);
		this.listRight[0] = new Vector3(0f, SgkCamera.topRight.y, 0f);
		this.listRight[1] = new Vector3(4.5f, 0f, 0f);
		this.listRight[2] = new Vector3(0f, SgkCamera.bottomLeft.y, 0f);
		this.listRight[3] = new Vector3(-4.5f, 0f, 0f);
		this.listRight[4] = new Vector3(0f, SgkCamera.topRight.y, 0f);
	}

	private void ResetStar()
	{
		this.starAll.transform.DOLocalMove(this.posStartStar, 2f, false).OnComplete(delegate
		{
			this.starAll.transform.DOKill(false);
			this.starAll.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			base.CheckAction();
		});
	}

	[EnemyAction(displayName = "BossStar/Bắn Laze - Xoay Tất cả Laze")]
	public void ActionLazeAll(float timeAttack, float timeRotate)
	{
		this.isRotateStarLeftAll *= -1;
		this.warningLeft.SetActive(true);
		this.warningRight.SetActive(true);
		this._animStar.SetTrigger("warning");
		base.StartCoroutine(this.ActiveLazeAll(timeAttack, timeRotate));
	}

	private IEnumerator ActiveLazeAll(float timeAttack, float timeRotate)
	{
		yield return new WaitForSeconds(timeAttack);
		this.ActiveGroupLaze();
		this._animStar.SetTrigger("attack");
		this.warningLeft.SetActive(false);
		this.warningRight.SetActive(false);
		base.StartCoroutine(this.RotateLaze(this.starAll.transform, this.isRotateStarLeftAll, timeRotate, 60f, false));
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "BossStar/Bắn Lưỡi Liềm")]
	public void ActionAttackLuoiLiem(float timeAttack, float timeSpawn)
	{
		base.StartCoroutine(this.AttackLuoiLiem(timeAttack, timeSpawn));
	}

	private IEnumerator AttackLuoiLiem(float timeAttack, float timeSpawn)
	{
		yield return new WaitForSeconds(timeAttack);
		this.handAttack.SetActive(true);
		base.StartCoroutine(this.SpawnBullet(timeSpawn));
		yield return null;
		yield break;
	}

	private IEnumerator SpawnBullet(float timeSpawn)
	{
		yield return new WaitForSeconds(timeSpawn);
		this.ubhShotLuoiLiem.StartShotRoutine();
		this.handAttack.SetActive(false);
		base.CheckAction();
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "BossStar/Bắn Đạn Tròn")]
	public void ActionAttackCircle(float timeAttack, float timeOff)
	{
		base.StartCoroutine(this.AttackCircle(timeAttack, timeOff));
	}

	private IEnumerator AttackCircle(float timeAttack, float timeOff)
	{
		this.starAll.transform.DORotate(new Vector3(0f, 0f, 360f), 0.5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
		yield return new WaitForSeconds(timeAttack);
		this.ubhShotCircle.StartShotRoutine();
		base.StartCoroutine(this.OffAttackCircle(timeOff));
		yield return null;
		yield break;
	}

	private IEnumerator OffAttackCircle(float timeOff)
	{
		yield return new WaitForSeconds(timeOff);
		this.starAll.transform.DOKill(false);
		this.starAll.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		base.CheckAction();
		yield return null;
		yield break;
	}

	public GameObject starAll;

	public GameObject star60;

	public GameObject star0;

	public GameObject warningLeft;

	public GameObject warningRight;

	public GameObject groupLaze1;

	public GameObject groupLaze2;

	public GameObject effectHold;

	private Vector3[] listLeft = new Vector3[5];

	private Vector3[] listRight = new Vector3[5];

	private Vector3 posStartStar = default(Vector3);

	private int isRotateLeft = 1;

	private int isRotateStarLeft = 1;

	private int isRotateStarLeftAll = 1;

	public UbhShotCtrl ubhShotLuoiLiem;

	public UbhShotCtrl ubhShotCircle;

	public GameObject handAttack;

	private Animator _animStar;
}
