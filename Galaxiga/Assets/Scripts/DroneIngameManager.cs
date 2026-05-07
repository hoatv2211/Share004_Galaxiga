using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DroneIngameManager : MonoBehaviour
{
	private Transform CurrentPlane
	{
		get
		{
			if (this.planeIngame.currentPlayerController == null)
			{
				return null;
			}
			return this.planeIngame.currentPlayerController.TransformPlane;
		}
	}

	private void Awake()
	{
		this.idDroneLeftEquiped = CacheGame.GetDroneLeftEquiped();
		this.idDroneRightEquiped = CacheGame.GetDroneRightEquiped();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.CurrentPlane == null)
		{
			return;
		}
		this.target = this.CurrentPlane.position;
		this.target.y = this.target.y - 0.5f;
		base.transform.position = Vector3.Lerp(base.transform.position, this.target, Time.deltaTime * this.droneSpeed);
		if (this.needSetCoolDownSkillDroneLeft)
		{
			this.SetCooldownSkillDroneLeft();
		}
		if (this.needSetCoolDownSkillDroneRight)
		{
			this.SetCooldownSkillDroneRight();
		}
		if (this.needSetCountdownTimeSkillActive)
		{
			this.SetCoundownDurationSkillDrone();
		}
	}

	public void SpawnDroneEquiped()
	{
		if (this.idDroneLeftEquiped != 0)
		{
			this.objDroneLeft = this.SpawnDrone(this.idDroneLeftEquiped, this.droneLeft);
			this.objDroneLeft.transform.parent = this.droneLeft;
			this.droneLeftScript = this.objDroneLeft.GetComponent<Drone>();
			this.currentDroneIDLeft = this.DroneID(this.idDroneLeftEquiped);
			this.timeCountDownSkillDroneLeft = this.droneLeftScript.cooldownSkill;
			this.SetImgSkill(this.imgSkillDroneLeft, this.currentDroneIDLeft);
		}
		else
		{
			this.btnSkilldroneLeft.SetActive(false);
		}
		if (this.idDroneRightEquiped != 0)
		{
			this.objDroneRight = this.SpawnDrone(this.idDroneRightEquiped, this.droneRight);
			this.objDroneRight.transform.parent = this.droneRight;
			this.droneRightScript = this.objDroneRight.GetComponent<Drone>();
			this.currentDroneIDRight = this.DroneID(this.idDroneRightEquiped);
			this.timeCountDownSkillDroneRight = this.droneRightScript.cooldownSkill;
			this.SetImgSkill(this.imgSkillDroneRight, this.currentDroneIDRight);
		}
		else
		{
			this.btnSkillDroneRight.SetActive(false);
		}
	}

	private GameObject SpawnDrone(int droneID, Transform _transform)
	{
		GameObject gameObject;
		switch (droneID)
		{
		case 1:
			gameObject = GameUtil.ObjectPoolSpawnGameObject("Drone", "Gatling Gun", _transform.position, Quaternion.identity);
			break;
		case 2:
			gameObject = GameUtil.ObjectPoolSpawnGameObject("Drone", "Auto Gatling Gun", _transform.position, Quaternion.identity);
			break;
		case 3:
			gameObject = GameUtil.ObjectPoolSpawnGameObject("Drone", "Laser", _transform.position, Quaternion.identity);
			this.droneLaser = gameObject.GetComponent<DroneLaser>();
			this.droneLaser.SetPositionObjSkill(this.CurrentPlane);
			break;
		case 4:
			gameObject = GameUtil.ObjectPoolSpawnGameObject("Drone", "Nighturge", _transform.position, Quaternion.identity);
			break;
		case 5:
			gameObject = GameUtil.ObjectPoolSpawnGameObject("Drone", "GodOfThunder", _transform.position, Quaternion.identity);
			break;
		case 6:
			gameObject = GameUtil.ObjectPoolSpawnGameObject("Drone", "Terigon", _transform.position, Quaternion.identity);
			break;
		default:
			gameObject = new GameObject("null");
			break;
		}
		return gameObject;
	}

	public void SetPositionLaserSkill()
	{
		if (this.droneLaser != null)
		{
			this.droneLaser.SetPositionObjSkill(this.CurrentPlane);
		}
	}

	public GameContext.Drone DroneID(int droneID)
	{
		return (GameContext.Drone)droneID;
	}

	private void SetImgSkill(Image img, GameContext.Drone droneID)
	{
		DataGame.Current.SetSpriteImageSkillDrone(img, droneID);
	}

	public void StartSkillDroneLeft()
	{
		if (!GameContext.isPlayerDie)
		{
			this.droneLeftScript.StartSkillNow(new Action(this.StartSkill), new Action(this.SetCooldown), GameContext.DronePosition.Left);
			this.StartCoundownTimeSkillActive(this.droneLeftScript.durationSkill, true);
		}
	}

	public void StartSkillDroneRight()
	{
		if (!GameContext.isPlayerDie)
		{
			this.droneRightScript.StartSkillNow(new Action(this.StartSkill), new Action(this.SetCooldown), GameContext.DronePosition.Right);
			this.StartCoundownTimeSkillActive(this.droneLeftScript.durationSkill, false);
		}
	}

	public void StopSkillDrone()
	{
		if (this.droneLeftScript != null && this.droneLeftScript.skillIsActive)
		{
			this.droneLeftScript.StopSkill();
		}
		if (this.droneRightScript != null && this.droneRightScript.skillIsActive)
		{
			this.droneRightScript.StopSkill();
		}
	}

	public void SetCooldownSkill()
	{
		if (this.droneRightScript != null && this.droneRightScript.skillIsActive)
		{
			this.droneRightScript.skillIsActive = false;
			this.SetCooldown();
		}
		if (this.droneLeftScript != null && this.droneLeftScript.skillIsActive)
		{
			this.droneLeftScript.skillIsActive = false;
			this.SetCooldown();
		}
	}

	private void StartSkill()
	{
		this.groupBtnSkill.SetActive(false);
	}

	public void SetCooldown()
	{
		this.groupBtnSkill.SetActive(true);
		GameContext.DronePosition currentDroneActiveSkill = GameContext.currentDroneActiveSkill;
		if (currentDroneActiveSkill != GameContext.DronePosition.Left)
		{
			if (currentDroneActiveSkill == GameContext.DronePosition.Right)
			{
				this.imgCoolDownSkillDroneRight.gameObject.SetActive(true);
				this.timeCountSkillDroneRight = this.timeCountDownSkillDroneRight;
				this.needSetCoolDownSkillDroneRight = true;
			}
		}
		else
		{
			this.imgCoolDownSkillDroneLeft.gameObject.SetActive(true);
			this.timeCountSkillDroneLeft = this.timeCountDownSkillDroneLeft;
			this.needSetCoolDownSkillDroneLeft = true;
		}
	}

	private void SetCooldownSkillDroneLeft()
	{
		if (this.timeCountSkillDroneLeft > 0f)
		{
			this.timeCountSkillDroneLeft -= Time.deltaTime;
		}
		if (this.timeCountSkillDroneLeft < 0f)
		{
			this.needSetCoolDownSkillDroneLeft = false;
			this.timeCountSkillDroneLeft = this.timeCountDownSkillDroneLeft;
			this.imgCoolDownSkillDroneLeft.gameObject.SetActive(false);
			this.droneLeftScript.skillIsCooldown = false;
			DOTween.Play("PUNCH_SKILL_DRONE_LEFT");
		}
		float fillAmount = this.timeCountSkillDroneLeft / this.timeCountDownSkillDroneLeft;
		this.imgCoolDownSkillDroneLeft.fillAmount = fillAmount;
	}

	private void SetCooldownSkillDroneRight()
	{
		if (this.timeCountSkillDroneRight > 0f)
		{
			this.timeCountSkillDroneRight -= Time.deltaTime;
		}
		if (this.timeCountSkillDroneRight < 0f)
		{
			this.needSetCoolDownSkillDroneRight = false;
			this.timeCountSkillDroneRight = this.timeCountDownSkillDroneRight;
			this.imgCoolDownSkillDroneRight.gameObject.SetActive(false);
			this.droneRightScript.skillIsCooldown = false;
			DOTween.Play("PUNCH_SKILL_DRONE_RIGHT");
		}
		float fillAmount = this.timeCountSkillDroneRight / this.timeCountDownSkillDroneRight;
		this.imgCoolDownSkillDroneRight.fillAmount = fillAmount;
	}

	public void StartShot()
	{
		if (this.droneLeftScript != null)
		{
			this.droneLeftScript.StartShot();
		}
		if (this.droneRightScript != null)
		{
			this.droneRightScript.StartShot();
		}
	}

	public void StopShot()
	{
		if (this.droneLeftScript != null)
		{
			this.droneLeftScript.StopShot();
		}
		if (this.droneRightScript != null)
		{
			this.droneRightScript.StopShot();
		}
	}

	public void ShowDrone()
	{
		this.droneLeft.gameObject.SetActive(true);
		this.droneRight.gameObject.SetActive(true);
		this.SetCooldownSkill();
	}

	public void HideDrone()
	{
		this.StopSkillDrone();
		this.droneLeft.gameObject.SetActive(false);
		this.droneRight.gameObject.SetActive(false);
	}

	private void StartCoundownTimeSkillActive(float _timer, bool isDroneLeft)
	{
		this.currentTimeSkillActive = _timer;
		this.durationSkill = _timer;
		this.needSetCountdownTimeSkillActive = true;
		if (isDroneLeft)
		{
			this.imgSkillDrone.sprite = this.imgSkillDroneLeft.sprite;
		}
		else
		{
			this.imgSkillDrone.sprite = this.imgSkillDroneRight.sprite;
		}
		this.objSkillDrone.SetActive(true);
		this.imgFadeSkillDrone.gameObject.SetActive(true);
	}

	private void SetCoundownDurationSkillDrone()
	{
		if (this.currentTimeSkillActive > 0f)
		{
			this.currentTimeSkillActive -= Time.deltaTime;
		}
		if (this.currentTimeSkillActive < 0f)
		{
			this.needSetCountdownTimeSkillActive = false;
			this.imgFadeSkillDrone.gameObject.SetActive(false);
			this.objSkillDrone.SetActive(false);
		}
		float fillAmount = this.currentTimeSkillActive / this.durationSkill;
		this.imgFadeSkillDrone.fillAmount = fillAmount;
	}

	[Range(10f, 50f)]
	public float droneSpeed = 20f;

	public PlaneIngameManager planeIngame;

	public Transform droneLeft;

	public Transform droneRight;

	private Drone droneLeftScript;

	private Drone droneRightScript;

	private GameContext.Drone currentDroneIDLeft;

	private GameContext.Drone currentDroneIDRight;

	public Image imgCoolDownSkillDroneLeft;

	public Image imgCoolDownSkillDroneRight;

	public Image imgSkillDroneLeft;

	public Image imgSkillDroneRight;

	public Image imgSkillDrone;

	public Image imgFadeSkillDrone;

	public GameObject groupBtnSkill;

	public GameObject btnSkilldroneLeft;

	public GameObject btnSkillDroneRight;

	public GameObject objSkillDrone;

	private float timeCountSkillDroneLeft;

	private float timeCountDownSkillDroneLeft;

	private float timeCountSkillDroneRight;

	private float timeCountDownSkillDroneRight;

	private bool needSetCoolDownSkillDroneLeft;

	private bool needSetCoolDownSkillDroneRight;

	private int idDroneLeftEquiped;

	private int idDroneRightEquiped;

	private Vector3 target;

	private GameObject objDroneLeft;

	private GameObject objDroneRight;

	private DroneLaser droneLaser;

	private float currentTimeSkillActive;

	private float durationSkill;

	private bool needSetCountdownTimeSkillActive;
}
