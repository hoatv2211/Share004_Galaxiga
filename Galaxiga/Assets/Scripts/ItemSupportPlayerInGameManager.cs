using System;
using System.Collections.Generic;
using SkyGameKit;
using UnityEngine;
using UnityEngine.UI;

public class ItemSupportPlayerInGameManager : MonoBehaviour
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
		ItemSupportPlayerInGameManager.current = this;
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.H))
		{
			this.ActiveChainsaw();
		}
		if (this.needCountDownTurretMachineGun)
		{
			this.StartCooldownTurret(0);
		}
		if (this.needCountDownTurretHoming)
		{
			this.StartCooldownTurret(1);
		}
		if (this.needCountDownTurretLaser)
		{
			this.StartCooldownTurret(2);
		}
	}

	public void ActiveTurretMachineGun()
	{
		this.arrTurret[0].StartCooldown();
		this.needCountDownTurretMachineGun = true;
		base.StartCoroutine(GameContext.Delay(this.arrTurret[0].saveCooldownTime, delegate
		{
			this.needCountDownTurretMachineGun = false;
			this.arrTurret[0].StopCooldown();
		}));
	}

	public void ActiveTurretHoming()
	{
		this.arrTurret[1].StartCooldown();
		this.needCountDownTurretHoming = true;
		base.StartCoroutine(GameContext.Delay(this.arrTurret[1].saveCooldownTime, delegate
		{
			this.needCountDownTurretHoming = false;
			this.arrTurret[1].StopCooldown();
		}));
	}

	public void ActiveTurretLaser()
	{
		this.arrTurret[2].StartCooldown();
		this.turretLaser.LaserStartFire();
		this.needCountDownTurretLaser = true;
		base.StartCoroutine(GameContext.Delay(this.arrTurret[2].saveCooldownTime, delegate
		{
			this.turretLaser.LaserStopFire();
			this.needCountDownTurretLaser = false;
			this.arrTurret[2].StopCooldown();
		}));
	}

	public void ActiveChainsaw()
	{
		this.chainsaw.SetActive(true);
	}

	public void ActiveIntanceHomingMissile()
	{
		Transform currentTransformPlayer = PlaneIngameManager.current.CurrentTransformPlayer;
		this.FireHomingMissile(currentTransformPlayer, 90f);
		this.FireHomingMissile(currentTransformPlayer, 75f);
		this.FireHomingMissile(currentTransformPlayer, -90f);
		this.FireHomingMissile(currentTransformPlayer, -75f);
	}

	private void FireHomingMissile(Transform posSpawn, float angleZ)
	{
		if (this.ListAliveEnemy.Count > 0)
		{
			this._target = this.ListAliveEnemy[UnityEngine.Random.Range(0, this.ListAliveEnemy.Count)].transform;
		}
		else
		{
			this._target = new GameObject
			{
				transform = 
				{
					position = new Vector2(0f, CameraManager.topRight.y)
				}
			}.transform;
		}
		GameObject gameObject = GameUtil.ObjectPoolSpawnGameObject("BulletPlayer", this.homingMissile.name, posSpawn.position, Quaternion.Euler(0f, 0f, angleZ));
		HomingMissile component = gameObject.GetComponent<HomingMissile>();
		component.target = this._target;
		BulletPlayerTakeDamage component2 = gameObject.GetComponent<BulletPlayerTakeDamage>();
		if (component2 != null)
		{
			component2.power = (float)(PlaneIngameManager.current.currentPlayerController.power * 10);
		}
		component.speedMove = 15f;
		component.SetFollowTarget(0f);
	}

	public void ActiveShieldPlayer()
	{
		UnityEngine.Debug.Log("active shield player");
	}

	private void StartCooldownTurret(int indexTurret)
	{
		if (this.arrTurret[indexTurret].cooldownTime > 0f)
		{
			this.arrTurret[indexTurret].cooldownTime -= Time.deltaTime;
			this.arrTurret[indexTurret].imgCooldown.fillAmount = this.arrTurret[indexTurret].cooldownTime / this.arrTurret[indexTurret].saveCooldownTime;
			return;
		}
	}

	public static ItemSupportPlayerInGameManager current;

	public GameObject chainsaw;

	public GameObject homingMissile;

	public Turret turretLaser;

	public ItemSupportPlayerInGameManager.TurretGame[] arrTurret;

	private bool needCountDownTurretMachineGun;

	private bool needCountDownTurretHoming;

	private bool needCountDownTurretLaser;

	private Transform _target;

	[Serializable]
	public class TurretGame
	{
		public void StartCooldown()
		{
			this.turret.SetActive(true);
			this.cooldownTime = this.saveCooldownTime;
			this.imgCooldown.fillAmount = 1f;
			this.objCooldown.SetActive(true);
		}

		public void StopCooldown()
		{
			this.turret.SetActive(false);
			this.objCooldown.SetActive(false);
		}

		public GameObject turret;

		public GameObject objCooldown;

		public Image imgCooldown;

		public float cooldownTime;

		public float saveCooldownTime;
	}
}
