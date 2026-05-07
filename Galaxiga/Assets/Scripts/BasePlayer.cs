using System;
using System.Collections;
using Hellmade.Sound;
using SkyGameKit;
using UnityEngine;

public class BasePlayer : MonoBehaviour
{
	public GameObject ObjectPlane
	{
		get
		{
			return base.gameObject;
		}
	}

	public Transform TransformPlane
	{
		get
		{
			return base.gameObject.transform;
		}
	}

	public virtual void Awake()
	{
		this.SetBaseData();
	}

	private void OnEnable()
	{
	}

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	private void SetBaseData()
	{
		this.rankID = DataGame.Current.ConverRankPlaneToIndex(this.planeData.CurrentRank);
		this.planeID = this.planeData.plane;
		this.namePlane = this.planeData.name;
		this.power = this.planeData.CurrentMainDamage;
		this.subPower = this.planeData.CurrentSubPower;
		this.specPower = this.planeData.CurrentSuperDamage;
		this.durationSkill = this.planeData.DurationSkill;
		this.fireRateSkill = this.planeData.fireRateSkill;
		this.fireRateSubBullet = this.planeData.fireRateSubBullet;
		base.gameObject.name = this.namePlane;
		this.currentDurationSkill = this.durationSkill;
		if (this.rankID >= 4)
		{
			this.power += Mathf.RoundToInt((float)this.power * 0.15f);
		}
	}

	public void GetDataPower()
	{
		this.power = this.planeData.CurrentMainDamage;
		if (this.rankID >= 4)
		{
			this.power += Mathf.RoundToInt((float)this.power * 0.15f);
		}
	}

	public virtual void StartSpecialShotNow(Action actionStartSkill, Action skillComplete)
	{
		base.StartCoroutine(this.DelayStopSpecialGun(actionStartSkill, skillComplete, this.durationSkill));
	}

	public virtual void StartSpecialShotNow(Action actionStartSkill, Action skillComplete, float _time)
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.StartCoroutine(this.DelayStopSpecialGun(actionStartSkill, skillComplete, _time));
		}
	}

	private IEnumerator DelayStopSpecialGun(Action OnStartSkill, Action OnSkillComplete, float _time)
	{
		OnStartSkill();
		this.StartSpecialShot();
		this.currentDurationSkill = _time;
		while (this.currentDurationSkill > 0f)
		{
			yield return null;
			this.currentDurationSkill -= Time.deltaTime;
		}
		this.StopSpecialShot();
		OnSkillComplete();
		yield break;
	}

	public virtual void StartSpecialShot()
	{
	}

	public virtual void StopSpecialShot()
	{
	}

	public void DeactivePlane()
	{
		if (base.gameObject.activeInHierarchy)
		{
			GameUtil.ObjectPoolDespawn("Plane", base.gameObject);
		}
	}

	public virtual void Die()
	{
	}

	public virtual void ActiveShield()
	{
		if (!this.shield.activeInHierarchy)
		{
			this.shield.SetActive(true);
		}
		this.isActiveShield = true;
		EazySoundManager.PlaySound(AudioCache.Sound.active_skill);
	}

	public void ActiveShieldTimer(float timer)
	{
		if (PlaneIngameManager.current != null)
		{
			PlaneIngameManager.current.SetCooldowShield(timer);
		}
		this.immortal = true;
		if (!this.shield.activeInHierarchy)
		{
			this.shield.SetActive(true);
		}
		this.isActiveShield = true;
		this.currentTimeShield = timer;
		base.StopCoroutine("DelayDiactiveShield");
		base.StartCoroutine("DelayDiactiveShield", timer);
		EazySoundManager.PlaySound(AudioCache.Sound.active_skill);
	}

	public virtual void DeactiveShield()
	{
		if (this.shield.activeInHierarchy)
		{
			this.shield.SetActive(false);
		}
		this.isActiveShield = false;
		this.immortal = false;
	}

	public void StopCoroutineActiveShieldTimer()
	{
		try
		{
			base.StopCoroutine("DelayDiactiveShield");
		}
		catch (Exception)
		{
			throw;
		}
		this.DeactiveShield();
		this.currentTimeShield = 0f;
	}

	private IEnumerator DelayDiactiveShield(float timer)
	{
		yield return new WaitForSeconds(timer);
		this.currentTimeShield = timer;
		this.DeactiveShield();
		yield break;
	}

	public void StartCoroutineActiveShieldTimer(float timer)
	{
		UnityEngine.Debug.Log("current shield timer " + timer);
		base.StartCoroutine("DelayDiactiveShield", timer);
	}

	public void CheckRankPlane()
	{
		switch (this.rankID)
		{
		case 1:
			if (!GameContext.isBonusPower)
			{
				GameContext.power = 1;
			}
			break;
		case 2:
			if (!GameContext.isBonusPower)
			{
				GameContext.power = 3;
			}
			break;
		case 3:
			if (!GameContext.isBonusPower)
			{
				GameContext.power = 5;
			}
			break;
		case 4:
			if (!GameContext.isBonusPower)
			{
				GameContext.power = 5;
			}
			break;
		case 5:
			if (!GameContext.isBonusPower)
			{
				GameContext.power = 8;
			}
			break;
		case 6:
			if (!GameContext.isBonusPower)
			{
				GameContext.power = 8;
			}
			this.ActiveShield();
			break;
		}
		if (GameContext.currentModeGamePlay == GameContext.ModeGamePlay.Boss)
		{
			GameContext.power = 10;
		}
		else if (GameContext.savePower > GameContext.power)
		{
			GameContext.power = GameContext.savePower;
		}
		this.SetSkinPlane();
		UIGameManager.current.SetTextPower();
	}

	public void SetSkinPlane()
	{
		if (this.rankID > 0)
		{
			this.avatar.Rank = this.rankID;
		}
	}

	public PlaneData planeData;

	public GameContext.Plane planeID;

	public string namePlane;

	public int power;

	public int subPower;

	public int specPower;

	public float durationSkill;

	public float fireRateSkill;

	public float fireRateSubBullet;

	public PlayerHealManager playerHealManager;

	public GameObject shield;

	[HideInInspector]
	public bool isActiveShield;

	public bool immortal;

	[HideInInspector]
	public int rankID;

	public float currentDurationSkill;

	public PlaneSpineCtrl avatar;

	[HideInInspector]
	public float currentTimeShield;
}
