using System;
using DG.Tweening;
using Hellmade.Sound;
using SkyGameKit;
using UnityEngine;

public class PlayerController : BasePlayer
{
	public override void Awake()
	{
		base.Awake();
	}

	public override void Start()
	{
		base.Start();
		this.playerGunController.SetPower(this.power, this.subPower, this.specPower, this.fireRateSkill, this.fireRateSubBullet);
		this.isViration = GameContext.isVibration;
		this.CheckSoundFire();
	}

	public void SetPower()
	{
		base.GetDataPower();
		this.playerGunController.SetPower(this.power, this.subPower, this.specPower, this.fireRateSkill, this.fireRateSubBullet);
	}

	public void CheckSoundFire()
	{
		switch (this.planeID)
		{
		case GameContext.Plane.BataFD01:
			this.soundFire = AudioCache.Sound.bata_shot;
			break;
		case GameContext.Plane.FuryOfAres:
			this.soundFire = AudioCache.Sound.bata_shot;
			break;
		case GameContext.Plane.SkyWraith:
			this.soundFire = AudioCache.Sound.skywraith_shot;
			break;
		case GameContext.Plane.TwilightX:
			this.soundFire = AudioCache.Sound.twilight_shot;
			break;
		case GameContext.Plane.Greataxe:
			this.soundFire = AudioCache.Sound.greataxe_shot;
			break;
		case GameContext.Plane.SSLightning:
			this.soundFire = AudioCache.Sound.shotting_3;
			break;
		case GameContext.Plane.Warlock:
			this.soundFire = AudioCache.Sound.shotting_4;
			break;
		}
	}

	public override void Update()
	{
		base.Update();
		if (this.needPlaySound)
		{
			this.PlaySound();
		}
	}

	private void PlaySound()
	{
		if (Time.time > this.nextTime)
		{
			this.nextTime = Time.time + this.soundRate;
			if (this.soundFire != null)
			{
				EazySoundManager.PlaySound(this.soundFire);
			}
		}
	}

	public void StopShot()
	{
		this.playerGunController.StopShot();
		this.needPlaySound = false;
	}

	public void StartShot()
	{
		if (!GameContext.skillIsActivating)
		{
			this.playerGunController.StartShot();
			this.needPlaySound = true;
		}
	}

	public override void StartSpecialShotNow(Action OnStartSkill, Action OnEndSkill)
	{
		base.StartSpecialShotNow(OnStartSkill, OnEndSkill);
	}

	public override void StartSpecialShotNow(Action OnStartSkill, Action OnEndSkill, float timer)
	{
		base.StartSpecialShotNow(OnStartSkill, OnEndSkill, timer);
	}

	public override void StartSpecialShot()
	{
		this.StartFxSkillPlane();
		this.playerGunController.StartSpecialShot();
		GameContext.skillIsActivating = true;
	}

	public override void StopSpecialShot()
	{
		this.StopFxSkillPlane();
		this.playerGunController.StopSpecialShot();
		GameContext.skillIsActivating = false;
	}

	public void StartMoveDotween()
	{
		this.playerMovement.StartDotweenMove();
	}

	public void StartVictoryMove(TweenCallback moveComplete)
	{
		this.playerMovement.VictoryDotweenMove(moveComplete);
	}

	public override void Die()
	{
		GameUtil.ObjectPoolSpawn("FxPlayer", "Fx_Player_Die", base.transform.position, Quaternion.identity);
		switch (GameContext.currentModeGamePlay)
		{
		case GameContext.ModeGamePlay.Endless:
			SpaceForceFirebaseLogger.PlayerDied(GameContext.ModeGamePlay.Endless, GameContext.ModeLevel.Easy, GameContext.currentWaveEndlessMode, string.Empty);
			break;
		case GameContext.ModeGamePlay.Campaign:
			if (SgkSingleton<LevelManager>.Instance != null)
			{
				SpaceForceFirebaseLogger.PlayerDied(GameContext.currentModeLevel, GameContext.currentLevel, SgkSingleton<LevelManager>.Instance.CurrentSequenceWave.name);
			}
			break;
		}
		if (this.isViration)
		{
#if UNITY_ANDROID || UNITY_IOS
			Handheld.Vibrate();
#endif
		}
		GameScreenManager.current.PlayerDie();
		base.DeactivePlane();
		GameContext.isPlayerDie = true;
		EazySoundManager.PlaySound(AudioCache.Sound.player_die);
	}

	public void CheckDie()
	{
		if (this.immortal || GameContext.isPlayerDie)
		{
			return;
		}
		if (this.isActiveShield)
		{
			this.DeactiveShield();
			EazySoundManager.PlaySound(AudioCache.Sound.break_shield);
			GameUtil.ObjectPoolSpawn("FxPlayer", "Fx_Player_Break_Shield", base.transform.position, Quaternion.identity);
			CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
		}
		else if (GameContext.currentLive > 0)
		{
			this.immortal = true;
			GameContext.currentLive--;
			if (GameContext.isBonusLife)
			{
				PlaneIngameManager.current.HideIconPowerUpLive();
			}
			base.ActiveShieldTimer(5f);
			GameUtil.ObjectPoolSpawn("FxPlayer", "Fx_Player_Break_Shield", base.transform.position, Quaternion.identity);
			CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
			if (this.isViration)
			{
#if UNITY_ANDROID || UNITY_IOS
			Handheld.Vibrate();
#endif
			}
		}
		else
		{
			CameraManager.curret.StartShake(CameraManager.ShakeType.PlayerDie);
			this.Die();
		}
	}

	public void SetPlayerImmortal()
	{
		this.immortal = true;
	}

	public void SetPlayerNoImmortal()
	{
		this.immortal = false;
	}

	public void PowerUpPlane()
	{
		this.playerGunController.PowerUpPlane();
	}

	public void PowerDownPlane()
	{
		this.playerGunController.PowerDownPlane();
	}

	public void StartFxSkillPlane()
	{
		this.fxSkillPlane.StartFxSkill();
		if (this.planeID == GameContext.Plane.TwilightX || this.planeID == GameContext.Plane.Greataxe)
		{
			this.needPlaySound = false;
		}
	}

	public void StopFxSkillPlane()
	{
		this.fxSkillPlane.StopFxSkill();
		if (this.planeID == GameContext.Plane.TwilightX || this.planeID == GameContext.Plane.Greataxe)
		{
			this.needPlaySound = true;
		}
	}

	public void ShowFxGetItemUpgradeBullet()
	{
		if (!this.fxPlayerGetItemUpgradeBullet.activeInHierarchy)
		{
			this.fxPlayerGetItemUpgradeBullet.SetActive(true);
		}
	}

	public PlayerMovement playerMovement;

	public PlayerGunController playerGunController;

	public GameObject fxSkill;

	public GameObject fxPlayerGetItemUpgradeBullet;

	public FxSkillPlane fxSkillPlane;

	private bool isViration;

	private bool needPlaySound;

	private float nextTime;

	private AudioClip soundFire;

	public float soundRate = 0.2f;
}
