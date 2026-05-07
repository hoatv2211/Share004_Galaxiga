using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Boss3 : BossGeneral
{
	private void Start()
	{
		this._anim = base.GetComponent<Animator>();
	}

	private void Update()
	{
		if ((float)this.CurrentHP < (float)this.startHP / 1.5f && !this.checkHealth)
		{
			this.checkHealth = true;
			this._anim.SetTrigger("Change");
		}
		if ((float)this.CurrentHP < (float)this.startHP / 3f && !this.checkHealth50)
		{
			this.checkHealth50 = true;
		}
	}

	private IEnumerator Attack1()
	{
		yield return new WaitForSeconds(this.timeAttack1);
		if (this.checkHealth)
		{
			this.objLase.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
		}
		this.objAttack1.StartShotRoutine();
		if (this.checkHealth50)
		{
			this.objAttack3.StartShotRoutine();
		}
		yield return null;
		base.StartCoroutine(this.Attack2());
		yield break;
	}

	private IEnumerator Attack2()
	{
		yield return new WaitForSeconds(this.timeAttack2);
		this.objAttack2.StartShotRoutine();
		yield return null;
		if (!this.checkHealth50)
		{
			base.StartCoroutine(this.Attack3());
		}
		else
		{
			base.StartCoroutine(this.AttackLase());
		}
		yield break;
	}

	private IEnumerator Attack3()
	{
		yield return new WaitForSeconds(this.timeAttack3);
		this.objAttack3.StartShotRoutine();
		yield return null;
		if (!this.checkHealth || this.checkHealth50)
		{
			base.StartCoroutine(this.Attack1());
		}
		else
		{
			base.StartCoroutine(this.AttackLase());
		}
		yield break;
	}

	private IEnumerator AttackLase()
	{
		this.warningLaze.SetActive(true);
		yield return new WaitForSeconds(this.timeAttackLase);
		this.warningLaze.SetActive(false);
		this._anim.SetTrigger("Attack");
		yield return null;
		yield break;
	}

	public void SetAttackLase()
	{
		this.objLase.SetActive(true);
		base.StartCoroutine(this.SetRotateLase());
	}

	private IEnumerator SetRotateLase()
	{
		yield return new WaitForSeconds(this.timeDelayLase);
		if (!this.left)
		{
			this.left = true;
			this.objLase.transform.DOLocalRotate(new Vector3(0f, 0f, 135f), this.timeDurationLaze, RotateMode.Fast).SetEase(Ease.InQuad).OnComplete(delegate
			{
				base.StartCoroutine(this.OffLaze());
				if (!this.checkHealth50)
				{
					base.StartCoroutine(this.Attack1());
				}
				else
				{
					base.StartCoroutine(this.Attack3());
				}
			});
		}
		else
		{
			this.left = false;
			this.objLase.transform.DOLocalRotate(new Vector3(0f, 0f, -45f), this.timeDurationLaze, RotateMode.Fast).SetEase(Ease.InQuad).OnComplete(delegate
			{
				base.StartCoroutine(this.OffLaze());
				if (!this.checkHealth50)
				{
					base.StartCoroutine(this.Attack1());
				}
				else
				{
					base.StartCoroutine(this.Attack3());
				}
			});
		}
		yield return null;
		yield break;
	}

	private IEnumerator OffLaze()
	{
		yield return new WaitForSeconds(this.timeOffLaze);
		this.objLase.SetActive(false);
		this.objLase.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
		yield return null;
		yield break;
	}

	public void StartAttack()
	{
		base.StartCoroutine(this.Attack1());
	}

	[Header("ATTACK_BOSS")]
	public float timeAttack1 = 3f;

	public float timeAttack2 = 3f;

	public float timeAttack3 = 3f;

	public float timeAttackLase = 3f;

	public float timeDelayLase = 3f;

	public float timeOffLaze = 3f;

	public UbhShotCtrl objAttack1;

	public UbhShotCtrl objAttack2;

	public UbhShotCtrl objAttack3;

	public GameObject objLase;

	public GameObject warningLaze;

	private bool checkHealth;

	private bool checkHealth50;

	private bool left;

	public float timeDurationLaze = 3f;

	private Animator _anim;
}
