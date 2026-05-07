using System;
using UnityEngine;

public class PlayerGunController : MonoBehaviour
{
	private void Awake()
	{
		this.skillController = base.GetComponent<PlayerSkillController>();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void StopShot()
	{
		int num = this.homingMissile.Length;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				this.homingMissile[i].StopFire();
			}
		}
		int num2 = this.bulletLasers.Length;
		if (num2 > 0)
		{
			for (int j = 0; j < num2; j++)
			{
				this.bulletLasers[j].StopFireLaser();
			}
		}
		if (this.gunLightning != null)
		{
			this.gunLightning.StopFire();
		}
		if (this.gunHomingMissile != null)
		{
			this.gunHomingMissile.StopFire();
		}
		this.DisableShot(this.arrShot[GameContext.power - 1]);
	}

	public void StartShot()
	{
		int num = this.homingMissile.Length;
		if (num > 0)
		{
			for (int i = 0; i < num; i++)
			{
				this.homingMissile[i].StartFire();
			}
		}
		int num2 = this.bulletLasers.Length;
		if (num2 > 0)
		{
			for (int j = 0; j < num2; j++)
			{
				this.bulletLasers[j].StartFireLaser();
			}
		}
		if (this.gunLightning != null)
		{
			this.gunLightning.StartFire();
		}
		if (this.gunHomingMissile != null)
		{
			this.gunHomingMissile.StartFire();
		}
		this.EnableShot(this.arrShot[GameContext.power - 1]);
	}

	public void StartSpecialShot()
	{
		this.StopShot();
		this.skillController.StartSpecialSkill();
	}

	public void StopSpecialShot()
	{
		this.skillController.StopSpecialSkill();
		this.StartShot();
	}

	public void PowerUpPlane()
	{
		if (GameContext.power == 10)
		{
			return;
		}
		this.playerController.ShowFxGetItemUpgradeBullet();
		this.DeactiveCurrentShot();
		GameContext.power++;
		GameContext.savePower = GameContext.power;
		if (!GameContext.skillIsActivating)
		{
			this.ActiveShot();
		}
		UIGameManager.current.SetTextPower();
		if (this.homingMissile.Length > 0)
		{
			this.UpdateBulletHomingMissile();
		}
		if (this.gunHomingMissile != null)
		{
			this.gunHomingMissile.CheckPower();
		}
		this.playerController.SetPower();
	}

	public void PowerDownPlane()
	{
		if (GameContext.power == 1)
		{
			return;
		}
		this.DeactiveCurrentShot();
		GameContext.power--;
		this.ActiveShot();
		UIGameManager.current.SetTextPower();
		if (this.homingMissile.Length > 0)
		{
			this.UpdateBulletHomingMissile();
		}
	}

	private void ActiveShot()
	{
		this.EnableShot(this.arrShot[GameContext.power - 1]);
	}

	private void DeactiveCurrentShot()
	{
		this.DisableShot(this.arrShot[GameContext.power - 1]);
	}

	private void EnableShot(UbhShotCtrl ubhShotCtrl)
	{
		if (!GameContext.isPlayerDie)
		{
			ubhShotCtrl.gameObject.SetActive(true);
			ubhShotCtrl.StartShotRoutine();
		}
	}

	private void DisableShot(UbhShotCtrl ubhShotCtrl)
	{
		if (!GameContext.isPlayerDie)
		{
			ubhShotCtrl.StopShotRoutine();
			ubhShotCtrl.gameObject.SetActive(false);
		}
	}

	public void Shot(int indexParticle)
	{
		if (indexParticle >= 0 && indexParticle < this.effectSpawBullet.Length)
		{
			this.effectSpawBullet[indexParticle].gameObject.SetActive(true);
			this.effectSpawBullet[indexParticle].Play();
		}
	}

	private void UpdateBulletHomingMissile()
	{
		int num = this.homingMissile.Length;
		for (int i = 0; i < num; i++)
		{
			float num2 = (float)GameContext.power * 0.04f;
			this.homingMissile[i].fireRate = this.saveFireRate - num2;
		}
	}

	public void SetPower(int _power, int _subPower, int _powerSkill, float _fireRateSkill, float _fireRateSubBullet)
	{
		int num = this.homingMissile.Length;
		if (num > 0)
		{
			this.saveFireRate = _fireRateSubBullet;
		}
		int num2 = this.bulletLasers.Length;
		if (num2 > 0)
		{
			for (int i = 0; i < num2; i++)
			{
				this.bulletLasers[i].power = (float)_subPower;
				this.bulletLasers[i].fireRate = _fireRateSubBullet;
			}
		}
		if (this.gunLightning != null)
		{
			this.gunLightning.SetPower(_subPower, _fireRateSubBullet);
		}
		if (this.gunHomingMissile != null)
		{
			this.gunHomingMissile.SetPower(_fireRateSubBullet, _subPower);
		}
		this.skillController.SetPowerSkil(_powerSkill, _fireRateSkill);
	}

	public void ShotFinishedRoutine()
	{
		UnityEngine.Debug.Log("Shot Finish Routine");
	}

	public PlayerController playerController;

	public ParticleSystem[] effectSpawBullet;

	public UbhShotCtrl[] arrShot;

	public FireBullet[] homingMissile;

	public Laser[] bulletLasers;

	public GunLightning gunLightning;

	public GunHomingMissile gunHomingMissile;

	private PlayerSkillController skillController;

	private float saveFireRate;
}
