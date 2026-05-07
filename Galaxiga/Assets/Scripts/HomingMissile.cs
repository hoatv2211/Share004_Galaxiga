using System;
using System.Collections;
using System.Collections.Generic;
using SkyGameKit;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
	public List<BaseEnemy> ListAliveEnemy
	{
		get
		{
			return SgkSingleton<LevelManager>.Instance.AliveEnemy;
		}
	}

	private void Awake()
	{
		this.rb = base.GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
	}

	public void SetTarget(Transform enemy)
	{
		this.target = enemy;
	}

	private void Update()
	{
        if (needFollow)
        {
            if (target != null && target.gameObject.activeInHierarchy)
            {
                point2Target = (Vector2)target.position - rb.position;
                point2Target.Normalize();
                value = Vector3.Cross(point2Target, base.transform.up).z;
                rb.angularVelocity = (0f - value) * rotatingSpeed;
                rb.linearVelocity = base.transform.up * speedMove;
            }
            else if (needFindTarget)
            {
                FindRandomTarget();
            }
            else
            {
                MoveBullet();
            }
        }
        else
        {
            MoveBullet();
        }
    }

	private void MoveBullet()
	{
		this.rb.angularVelocity = -this.value * this.rotatingSpeedNoTarget;
		this.rb.linearVelocity = base.transform.up * this.speedMove;
	}

	public void FindRandomTarget()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, this.rangeAttack, this.layerEnemy);
		if (array.Length > 0)
		{
			GameObject gameObject = array[UnityEngine.Random.Range(0, array.Length)].gameObject;
			this.target = gameObject.transform;
		}
		else
		{
			this.target = null;
			this.MoveBullet();
		}
	}

	public void SetFollowTarget()
	{
		base.StartCoroutine(this.DelayFollowTarget(0.25f));
	}

	private IEnumerator DelayFollowTarget(float _time)
	{
		this.needFollow = false;
		if (this.needFindTarget && this.target == null)
		{
			this.FindRandomTarget();
		}
		yield return new WaitForSeconds(_time);
		this.needFollow = true;
		yield return new WaitForSeconds(5f);
		this.needFollow = false;
		yield break;
	}

	public void SetFollowTarget(float _time)
	{
		base.StartCoroutine(this.DelayFollowTarget(_time));
	}

	public bool needFollow = true;

	public bool needFindTarget = true;

	public float speedMove = 11f;

	public Transform target;

	private Rigidbody2D rb;

	private float value;

	public float rotatingSpeed = 360f;

	private float rotatingSpeedNoTarget = 10f;

	private Vector2 point2Target;

	private float rangeAttack = 15f;

	public LayerMask layerEnemy;
}
