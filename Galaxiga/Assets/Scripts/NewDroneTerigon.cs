using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SkyGameKit;
using UnityEngine;

public class NewDroneTerigon : Drone
{
	private List<BaseEnemy> ListEnemyAlive
	{
		get
		{
			return SgkSingleton<LevelManager>.Instance.AliveEnemy;
		}
	}

	public override void Start()
	{
	}

	public override void Update()
	{
		if (this.target == null || !this.target.activeInHierarchy)
		{
			this.SetTarget();
		}
		if (this.target != null)
		{
			this.LookForward();
		}
		if (this.isStartSkill)
		{
			this.objSkill.transform.position = PlaneIngameManager.current.currentPlane.transform.position;
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha5))
		{
			this.StartShot();
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha6))
		{
			this.StartSkill();
		}
	}

	private void SetTarget()
	{
		if (SgkSingleton<LevelManager>.Instance == null)
		{
			return;
		}
		if (this.ListEnemyAlive.Count > 0)
		{
			BaseEnemy baseEnemy = this.GetTarget();
			if (baseEnemy != null)
			{
				this.target = baseEnemy.gameObject;
			}
		}
		else
		{
			this.target = null;
			this.droneTerigon.transform.rotation = Quaternion.Slerp(this.droneTerigon.transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 30f);
			this.ubhLinearShot[0].m_angle = this.droneTerigon.transform.eulerAngles.z;
			this.ubhLinearShot[1].m_angle = this.droneTerigon.transform.eulerAngles.z;
		}
	}

	private void LookForward()
	{
		Vector3 vector = this.target.transform.position - this.transformPlayer.position;
		vector.Normalize();
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		this.droneTerigon.transform.rotation = Quaternion.Slerp(this.droneTerigon.transform.rotation, Quaternion.Euler(0f, 0f, num - 90f), Time.deltaTime * 30f);
		this.ubhLinearShot[0].m_angle = this.droneTerigon.transform.eulerAngles.z;
		this.ubhLinearShot[1].m_angle = this.droneTerigon.transform.eulerAngles.z;
	}

	private BaseEnemy GetTarget()
	{
		return (from x in this.ListEnemyAlive
		where !Fu.Outside(x.transform.position, CameraManager.bottomLeft, CameraManager.topRight)
		orderby Vector2.Distance(x.transform.position, this.transformPlayer.position)
		select x).FirstOrDefault<BaseEnemy>();
	}

	public override void StartShot()
	{
		this.fx_Shot.SetActive(true);
		this.fx_Shot.GetComponent<ParticleSystem>().Play();
		this.ubhShot.StartShotRoutine();
	}

	public override void StopShot()
	{
		this.fx_Shot.SetActive(false);
		this.fx_Shot.GetComponent<ParticleSystem>().Stop();
		this.ubhShot.StopShotRoutine();
	}

	public override void StartSkill()
	{
		if (this.isStartSkill)
		{
			return;
		}
		base.StartSkill();
		this.isStartSkill = true;
		this.ActiveSkill();
		base.StartCoroutine(this.delayStopSkill());
	}

	public override void StopSkill()
	{
		base.StopSkill();
		this.InactiveSkill();
	}

	private IEnumerator delayStopSkill()
	{
		yield return new WaitForSeconds(this.durationSkill);
		this.InactiveSkill();
		yield break;
	}

	public void ActiveSkill()
	{
		if (!this.objSkill.activeInHierarchy)
		{
			this.objSkill.SetActive(true);
		}
	}

	private void InactiveSkill()
	{
		this.isStartSkill = false;
		this.objSkill.SetActive(false);
	}

	private bool isStartSkill;

	public GameObject target;

	public Transform transformPlayer;

	[SerializeField]
	private UbhShotCtrl ubhShot;

	[SerializeField]
	private UbhLinearShot[] ubhLinearShot;

	[SerializeField]
	private GameObject droneTerigon;

	[SerializeField]
	private GameObject objSkill;

	[SerializeField]
	private GameObject fx_Shot;

	[SerializeField]
	private ParticleSystem effectSpawBullet;
}
