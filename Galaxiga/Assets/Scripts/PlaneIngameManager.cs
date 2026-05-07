using System;
using DG.Tweening;
using Hellmade.Sound;
using I2.Loc;
using SkyGameKit;
using UnityEngine;
using UnityEngine.UI;

public class PlaneIngameManager : MonoBehaviour
{
	public static PlaneIngameManager current
	{
		get
		{
			if (PlaneIngameManager._ins == null)
			{
				PlaneIngameManager._ins = UnityEngine.Object.FindObjectOfType<PlaneIngameManager>();
			}
			return PlaneIngameManager._ins;
		}
	}

	public Transform CurrentTransformPlayer
	{
		get
		{
			return this.currentPlayerController.TransformPlane;
		}
	}

	private void Start()
	{
		this.CheckActiveBtnSkillPlane();
		GameContext.isPlayerDie = false;
		if (GameContext.isBonusShield)
		{
			this.ShowButtonActiveShield();
		}
		if (GameContext.isBonusLife)
		{
			this.objHeart.SetActive(true);
		}
	}

	private void CheckActiveBtnSkillPlane()
	{
		this.textNumberSkill.text = string.Empty + GameContext.currentNumberSkillPlane;
		if (GameContext.currentNumberSkillPlane == 0)
		{
			this.imgFadeBtnSkillPlane.gameObject.SetActive(true);
			this.imgFadeBtnSkillPlane.fillAmount = 1f;
		}
	}

	private void Update()
	{
		if (this.needSetCooldownSkillPlane)
		{
			this.StartCooldownSkill();
		}
		if (this.needSetCooldownShield)
		{
			this.StartCooldownShield();
		}
		if (this.needSetCountdownTimeSkillActive)
		{
			this.SetCoundownDurationSkillPlane();
		}
	}

	private void ActivePlaneEquiped(GameContext.Plane planeID)
	{
		this.currentIndexPlane = (int)planeID;
		this.ActivePlane(this.arrPlanes[this.currentIndexPlane].poolName, this.postionSpawnPlane.position);
		this.currenPlaneActive = planeID;
	}

	public void ActivePlaneEquiped()
	{
		if (GameContext.isTryPlane)
		{
			this.planeEquiped = (int)GameContext.planeTry;
		}
		else
		{
			this.planeEquiped = CacheGame.GetPlaneEquiped();
		}
		this.currentIndexPlane = this.planeEquiped;
		this.ActivePlane(this.arrPlanes[this.planeEquiped].poolName, this.postionSpawnPlane.position);
		GameContext.currentPlaneIDEquiped = this.planeEquiped;
		this.currenPlaneActive = this.arrPlanes[this.planeEquiped].planeID;
		UnityEngine.Debug.Log("--------------ActivePlaneEquiped: ");
		this.droneManager.SpawnDroneEquiped();
	}

	public void RevivePlane()
	{
		UnityEngine.Debug.Log("Revive Plane " + this.arrPlanes[this.currentIndexPlane].poolName);
		this.ActivePlane(this.arrPlanes[this.currentIndexPlane].poolName, this.postionSpawnPlane.position);
		this.droneManager.ShowDrone();
		this.currentPlayerController.ActiveShieldTimer(5f);
		GameContext.isPlayerDie = false;
		if (GameContext.skillIsActivating)
		{
			this.currentPlayerController.StopSpecialShot();
			this.OnEndSpecialShot();
		}
		GameScreenManager.current.ResumeMusic();
	}

	public void BackupPlane(GameContext.Plane planeID)
	{
		GameContext.isPlayerDie = false;
		this.currentIndexPlane = (int)planeID;
		this.ActivePlane(this.arrPlanes[this.currentIndexPlane].poolName, this.postionSpawnPlane.position);
		this.droneManager.ShowDrone();
		this.currentPlayerController.ActiveShieldTimer(5f);
		this.currentPlayerController.playerMovement.isReady = true;
		this.currenPlaneActive = planeID;
		if (GameContext.skillIsActivating)
		{
			this.currentPlayerController.StopSpecialShot();
			this.OnEndSpecialShot();
		}
	}

	private void ActivePlane(string namePlane, Vector2 position)
	{
		this.currentPlane = GameUtil.ObjectPoolSpawnGameObject("Plane", namePlane, position, Quaternion.identity);
		this.currentPlayerController = this.currentPlane.GetComponent<PlayerController>();
		this.currentPlayerController.CheckRankPlane();
		this.currentPlayerController.StartMoveDotween();
		this.currentPlayerController.playerMovement.ResetPosition();
		this.oldPlane = this.currentPlane;
		this.droneManager.SetPositionLaserSkill();
	}

	public void ChangePlaneID(GameContext.Plane _planeID)
	{
		if (GameContext.isMissionComplete || GameContext.isMissionFail)
		{
			return;
		}
		this.currentIndexPlane = (int)_planeID;
		this.currentPlane = GameUtil.ObjectPoolSpawnGameObject("Plane", this.arrPlanes[this.currentIndexPlane].poolName, this.currentPositionPlane, Quaternion.identity);
		if (this.currentPlayerController != null)
		{
			if (GameContext.skillIsActivating)
			{
				this.currentDurationSkill = this.currentPlayerController.currentDurationSkill;
			}
			if (this.currentPlayerController.isActiveShield)
			{
				this.isActiveShield = true;
				this.currentTimeShield = this.currentPlayerController.currentTimeShield;
				this.currentPlayerController.StopCoroutineActiveShieldTimer();
			}
		}
		PlayerController component = this.oldPlane.GetComponent<PlayerController>();
		this.currentPlayerController = this.currentPlane.GetComponent<PlayerController>();
		this.currentPlayerController.playerMovement.isReady = true;
		this.currentPlayerController.playerMovement.SetPosition(component.playerMovement.lastMousePos);
		this.currentPlayerController.CheckRankPlane();
		GameScreenManager.current.StartAllShot();
		this.oldPlane = this.currentPlane;
		this.currenPlaneActive = _planeID;
		if (GameContext.skillIsActivating)
		{
			this.StartSkillPlane(this.currentDurationSkill);
		}
		if (this.isActiveShield)
		{
			this.currentPlayerController.ActiveShieldTimer(this.currentTimeShield);
			this.isActiveShield = false;
			this.currentTimeShield = 0f;
		}
		this.droneManager.SetPositionLaserSkill();
	}

	public void ChangePlane(GameContext.Plane planeID)
	{
		this.currentPositionPlane = this.currentPlane.transform.localPosition;
		this.currentPlayerController.DeactivePlane();
		this.ChangePlaneID(planeID);
		EazySoundManager.PlaySound(AudioCache.Sound.switch_airship);
	}

	public void StartSpecialShot()
	{
		if (GameContext.currentNumberSkillPlane > 0)
		{
			if (GameContext.currentLevel == 1)
			{
				NewTutorial.current.UseSkillPlane_Step4();
			}
			SaveDataQuestEndless.SetProcessQuest(EndlessDailyQuestManager.Quest.use_skill_times0, 1);
			GameContext.currentNumberSkillPlane--;
			CacheGame.MinusSkillPlane(1);
			if (this.currentPlayerController != null)
			{
				this.currentPlayerController.StartSpecialShotNow(new Action(this.OnStartSpecialShot), new Action(this.OnEndSpecialShot));
			}
			this.StartCoundownTimeSkillActive(this.currentPlayerController.durationSkill);
			this.CheckActiveBtnSkillPlane();
			EazySoundManager.PlaySound(AudioCache.Sound.bullet_upgrade);
			if (SgkSingleton<LevelInfo>.Instance.mode == LevelInfo.modeLevel.EndLess)
			{
				SaveDataQuestEndless.SetProcessQuest(EndlessDailyQuestManager.Quest.use_skill_times0, 1);
			}
		}
		else
		{
			Notification.Current.ShowNotification(ScriptLocalization.notify_you_need_buy_more + " Overdrive");
		}
	}

	public void StartSkillPlane()
	{
		if (this.currentPlayerController != null)
		{
			float num = this.currentPlayerController.durationSkill * 0.6f;
			this.currentPlayerController.StartSpecialShotNow(new Action(this.OnStartSpecialShot), new Action(this.OnEndSpecialShot), num);
			this.StartCoundownTimeSkillActive(num);
			EazySoundManager.PlaySound(AudioCache.Sound.bullet_upgrade);
		}
	}

	public void StartSkillPlane(float _time)
	{
		if (this.currentPlayerController != null)
		{
			this.currentPlayerController.StartSpecialShotNow(new Action(this.OnStartSpecialShot), new Action(this.OnEndSpecialShot), _time);
			this.StartCoundownTimeSkillActive(_time);
			EazySoundManager.PlaySound(AudioCache.Sound.bullet_upgrade);
		}
	}

	private void OnStartSpecialShot()
	{
		this.groupBtnSkill.SetActive(false);
	}

	private void OnEndSpecialShot()
	{
		this.groupBtnSkill.SetActive(true);
		this.SetCoolDown();
	}

	private void SetCoolDown()
	{
		this.cooldownTime = 1f;
		this.needSetCooldownSkillPlane = true;
		this.imgFadeBtnSkillPlane.gameObject.SetActive(true);
	}

	private void StartCooldownSkill()
	{
		if (this.cooldownTime > 0f)
		{
			this.cooldownTime -= Time.deltaTime;
		}
		if (this.cooldownTime < 0f)
		{
			this.needSetCooldownSkillPlane = false;
			this.cooldownTime = 1f;
			if (GameContext.currentNumberSkillPlane == 0)
			{
				this.imgFadeBtnSkillPlane.fillAmount = 1f;
			}
			else
			{
				this.imgFadeBtnSkillPlane.gameObject.SetActive(false);
			}
			DOTween.Play("PUNCH_SKILL_PLANE");
		}
		float fillAmount = this.cooldownTime / 1f;
		this.imgFadeBtnSkillPlane.fillAmount = fillAmount;
	}

	public void PlaneStartShot()
	{
		if (this.currentPlayerController != null)
		{
			this.currentPlayerController.StartShot();
		}
	}

	public void PlaneStopShot()
	{
		if (this.currentPlayerController != null)
		{
			this.currentPlayerController.StopShot();
		}
	}

	public void TakeDamagePlayer()
	{
		this.currentPositionPlane = this.currentPlane.transform.localPosition;
		this.currentPlayerController.CheckDie();
	}

	public void MissionComplete(TweenCallback tweenCallback)
	{
		this.currentPlayerController.StartVictoryMove(tweenCallback);
	}

	public void PowerUpPlane()
	{
		this.currentPlayerController.PowerUpPlane();
		EazySoundManager.PlaySound(AudioCache.Sound.Item_get2);
	}

	public void PowerDownPlane()
	{
		this.currentPlayerController.PowerDownPlane();
	}

	public void SetPlayerImortal()
	{
		this.currentPlayerController.SetPlayerImmortal();
	}

	public void SetPlayerNoImortal()
	{
		this.currentPlayerController.SetPlayerNoImmortal();
	}

	public bool IsOwened(GameContext.Plane _planeID)
	{
		bool result = false;
		for (int i = 0; i < this.arrPlanes.Length; i++)
		{
			if (this.arrPlanes[i].planeID == _planeID)
			{
				result = this.arrPlanes[i].IsOwned;
				break;
			}
		}
		return result;
	}

	public void ShowButtonActiveShield()
	{
		this.btnActiveShield.SetActive(true);
	}

	public void ActiveShieldPowerUp()
	{
		NewTutorial.current.BuyItems_Step11();
		this.currentPlayerController.ActiveShieldTimer(15f);
		this.btnActiveShield.SetActive(false);
	}

	public void SetCooldowShield(float timer)
	{
		this.cooldownTimeShield = timer;
		this.totalTimeShield = timer;
		this.needSetCooldownShield = true;
		this.imgFadeShield.gameObject.SetActive(true);
		this.objShield.SetActive(true);
		this.btnActiveShield.SetActive(false);
	}

	private void StartCooldownShield()
	{
		if (this.cooldownTimeShield > 0f)
		{
			this.cooldownTimeShield -= Time.deltaTime;
		}
		if (this.cooldownTimeShield < 0f)
		{
			this.needSetCooldownShield = false;
			this.imgFadeShield.gameObject.SetActive(false);
			this.btnActiveShield.SetActive(false);
			this.objShield.SetActive(false);
		}
		float fillAmount = this.cooldownTimeShield / this.totalTimeShield;
		this.imgFadeShield.fillAmount = fillAmount;
	}

	public void HideIconPowerUpLive()
	{
		this.objHeart.SetActive(false);
	}

	private void StartCoundownTimeSkillActive(float _timer)
	{
		this.currentTimeSkillActive = _timer;
		this.durationSkill = _timer;
		this.needSetCountdownTimeSkillActive = true;
		this.objSuperSkill.SetActive(true);
		this.imgFadeSkillPlane.gameObject.SetActive(true);
		UIActiveSpecialSkill.current.ShowBlueScreen();
	}

	private void SetCoundownDurationSkillPlane()
	{
		if (this.currentTimeSkillActive > 0f)
		{
			this.currentTimeSkillActive -= Time.deltaTime;
		}
		if (this.currentTimeSkillActive < 0f)
		{
			this.needSetCountdownTimeSkillActive = false;
			this.imgFadeSkillPlane.gameObject.SetActive(false);
			this.objSuperSkill.SetActive(false);
			UIActiveSpecialSkill.current.HideBlueScreen();
		}
		float fillAmount = this.currentTimeSkillActive / this.durationSkill;
		this.imgFadeSkillPlane.fillAmount = fillAmount;
	}

	public static PlaneIngameManager _ins;

	public Transform postionSpawnPlane;

	public DroneIngameManager droneManager;

	public GameObject groupBtnSkill;

	public GameObject btnSkillPlane;

	public GameObject btnActiveShield;

	public Text textNumberSkill;

	public Image imgFadeBtnSkillPlane;

	public Image imgFadeShield;

	public Image imgFadeSkillPlane;

	public PlaneIngameManager.Plane[] arrPlanes;

	[HideInInspector]
	public PlayerController currentPlayerController;

	[HideInInspector]
	public int planeEquiped;

	public GameContext.Plane currenPlaneActive;

	public GameObject objHeart;

	public GameObject objShield;

	public GameObject objSuperSkill;

	public GameObject currentPlane;

	public GameObject oldPlane;

	private int currentIndexPlane;

	private bool needSetCooldownSkillPlane;

	private bool needSetCooldownShield;

	private float cooldownTime;

	private float cooldownTimeShield;

	private float totalTimeShield;

	private int index;

	private float currentDurationSkill;

	private bool isActiveShield;

	private float currentTimeShield;

	private Vector2 currentPositionPlane;

	private float currentTimeSkillActive;

	private float durationSkill;

	private bool needSetCountdownTimeSkillActive;

	[Serializable]
	public class Plane
	{
		public bool IsOwned
		{
			get
			{
				return this.planeData.IsOwned;
			}
		}

		public string poolName
		{
			get
			{
				return this.planeData.namePlane;
			}
		}

		public GameContext.Plane planeID;

		public PlaneData planeData;
	}
}
