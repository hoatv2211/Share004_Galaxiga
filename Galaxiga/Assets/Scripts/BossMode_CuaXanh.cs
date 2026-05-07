using System;
using System.Collections;
using DG.Tweening;
using Hellmade.Sound;
using SkyGameKit;
using Spine.Unity;
using SWS;
using UnityEngine;

public class BossMode_CuaXanh : NewBoss
{
	[EnemyAction(displayName = "Tuan/StartAttack")]
	public void StartAttack()
	{
		this.RandomSkill();
	}

	[EnemyAction(displayName = "Tuan/ActiveStatusAngry")]
	public void ActiveStatusAngry(float stat)
	{
		this.angryStat = stat;
		this.isActiveAngry = true;
		base.ShowRedScreen();
		this.skeletonAnimation.timeScale = 1f + (1f - this.angryStat) * 2f;
		EazySoundManager.PlaySound(this.listSound[1], 1f);
	}

	[EnemyAction(displayName = "Tuan/Skill0_ShotByGuns")]
	public void Skill0_ShotByGuns(float timeDelay, int numberBullet, float speed)
	{
		this.timeDelay_ShotByGuns = timeDelay;
		this.ubh_LinearShot[0].m_bulletNum = numberBullet;
		this.ubh_LinearShot[0].m_bulletSpeed = speed;
		this.ubh_LinearShot[1].m_bulletNum = numberBullet;
		this.ubh_LinearShot[1].m_bulletSpeed = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill1_ShotByMouth")]
	public void Skill1_ShotByMouth(float timeDelay, int numberBullet, float speedBullet, int numberWay, float speedBigRocket)
	{
		this.timeDelay_ShotByMouth = timeDelay;
		this.ubh_SinWave.m_bulletNum = numberBullet;
		this.ubh_SinWave.m_bulletSpeed = speedBullet;
		this.ubh_SinWave.m_wayNum = numberWay;
		this.speed_BigRocket = speedBigRocket;
	}

	[EnemyAction(displayName = "Tuan/Skill2_ThrowLeftHand")]
	public void Skill2_ThrowLeftHand(float timeDelay, float speed)
	{
		this.timeDelay_ThrowLeftHand = timeDelay;
		this.speed_ThrowLeftHand = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill3_ThrowRightHand")]
	public void Skill3_ThrowRightHand(float timeDelay, float speed)
	{
		this.timeDelay_ThrowRightHand = timeDelay;
		this.speed_ThrowRightHand = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill4_ThrowTwoHand")]
	public void Skill4_ThrowTwoHand(float timeDelay, float speed)
	{
		this.timeDelay_ThrowTwoHand = timeDelay;
		this.speed_ThrowTwoHand = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill5_Dash")]
	public void Skill5_Dash(float timeDelay, float speed)
	{
		this.timeDelay_Dash = timeDelay;
		this.speed_Dash = speed;
	}

	[EnemyAction(displayName = "Tuan/Skill6_ShotByTail")]
	public void Skill6_ShotByTail(float timeDelay, int numberBullet, float timeFollow)
	{
		this.timeDelay_ShotByTail = timeDelay;
		this.numberBullet_ShoyByTail = numberBullet;
		this.timeFollow_Rocket = timeFollow;
	}

	public override void Restart()
	{
		base.Restart();
		if (this.BaseStatBoss != null)
		{
			this.BaseStatBoss();
		}
		base.ShowHealthBar();
		base.SetNameBoss("King Crab");
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha0))
		{
			base.StartCoroutine(this.AttackShotByGuns());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1))
		{
			base.StartCoroutine(this.AttackShotByMouth());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha2))
		{
			base.StartCoroutine(this.AttackThrowLeftHand());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha3))
		{
			base.StartCoroutine(this.AttackThrowRightHand());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha4))
		{
			base.StartCoroutine(this.AttackThrowTwoHand());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha5))
		{
			base.StartCoroutine(this.AttackDash());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha6))
		{
			base.StartCoroutine(this.AttackShotByTail());
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.I))
		{
			this.ActiveStatusAngry(0.5f);
		}
		if (this.player != null && !this.isBlind)
		{
			Vector3 vector = this.player.position - base.transform.position;
			vector.Normalize();
			float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(0f, 0f, num + 90f), Time.deltaTime * 2f);
		}
	}

	private void OnEnable()
	{
		this.FindRandomTarget();
		EazySoundManager.PlaySound(this.listSound[0], 1f);
	}

	public void FindRandomTarget()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, this.rangeAttack, this.layerPlayer);
		if (array.Length > 0)
		{
			GameObject gameObject = array[UnityEngine.Random.Range(0, array.Length)].gameObject;
			this.player = gameObject.transform;
		}
	}

	private void RandomSkill()
	{
		int randomValue;
		if (this.isActiveAngry)
		{
			randomValue = this.GetRandomValue(new BossMode_CuaXanh.RandomSelection[]
			{
				new BossMode_CuaXanh.RandomSelection(0, 0, 0.1f),
				new BossMode_CuaXanh.RandomSelection(1, 1, 0.15f),
				new BossMode_CuaXanh.RandomSelection(2, 2, 0.1f),
				new BossMode_CuaXanh.RandomSelection(3, 3, 0.1f),
				new BossMode_CuaXanh.RandomSelection(4, 4, 0.15f),
				new BossMode_CuaXanh.RandomSelection(5, 5, 0.2f),
				new BossMode_CuaXanh.RandomSelection(6, 6, 0.2f)
			});
		}
		else
		{
			randomValue = this.GetRandomValue(new BossMode_CuaXanh.RandomSelection[]
			{
				new BossMode_CuaXanh.RandomSelection(0, 0, 0.25f),
				new BossMode_CuaXanh.RandomSelection(1, 1, 0.25f),
				new BossMode_CuaXanh.RandomSelection(2, 2, 0.2f),
				new BossMode_CuaXanh.RandomSelection(3, 3, 0.2f),
				new BossMode_CuaXanh.RandomSelection(4, 4, 0.1f)
			});
		}
		if (randomValue == this.currentIdSkill)
		{
			this.RandomSkill();
		}
		else
		{
			this.currentIdSkill = randomValue;
			this.Attack();
		}
	}

	private void Attack()
	{
		if (this.isTransforming)
		{
			return;
		}
		switch (this.currentIdSkill)
		{
		case 0:
			base.StartCoroutine(this.AttackShotByGuns());
			break;
		case 1:
			base.StartCoroutine(this.AttackShotByMouth());
			break;
		case 2:
			base.StartCoroutine(this.AttackThrowLeftHand());
			break;
		case 3:
			base.StartCoroutine(this.AttackThrowRightHand());
			break;
		case 4:
			base.StartCoroutine(this.AttackDash());
			break;
		case 5:
			base.StartCoroutine(this.AttackThrowTwoHand());
			break;
		case 6:
			base.StartCoroutine(this.AttackShotByTail());
			break;
		}
	}

	private IEnumerator ChangeAnim(string anim, float timeDelay)
	{
		this.isTransforming = true;
		this.skeletonAnimation.AnimationName = anim;
		yield return new WaitForSeconds(timeDelay * this.angryStat);
		this.skeletonAnimation.AnimationName = this.animIdle;
		this.isTransforming = false;
		this.RandomSkill();
		yield break;
	}

	private IEnumerator AttackShotByGuns()
	{
		yield return new WaitForSeconds(this.timeDelay_ShotByGuns * this.angryStat);
		this.warning_ShotByGuns[0].SetActive(true);
		this.warning_ShotByGuns[1].SetActive(true);
		this.skeletonAnimation.AnimationName = this.animShotByGuns;
		base.StartCoroutine(this.ChangeAnim(this.animShotByGuns, 3.3f));
		EazySoundManager.PlaySound(this.listSound[3], 1f);
		yield return new WaitForSeconds(1.5f * this.angryStat);
		this.warning_ShotByGuns[0].SetActive(false);
		this.warning_ShotByGuns[1].SetActive(false);
		this.ubh_LinearShot[0].m_angle = 170f + base.gameObject.transform.eulerAngles.z;
		this.ubh_LinearShot[1].m_angle = 170f + base.gameObject.transform.eulerAngles.z;
		EazySoundManager.PlaySound(this.listSound[4], 1f);
		this.ubh_ShotByGuns.StartShotRoutine();
		this.fx_ShotByGuns[0].SetActive(true);
		this.fx_ShotByGuns[1].SetActive(true);
		yield return new WaitForSeconds(0.3f * this.angryStat);
		this.fx_ShotByGuns[0].SetActive(false);
		this.fx_ShotByGuns[1].SetActive(false);
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.ubh_LinearShot[0].m_angle = 220f + base.gameObject.transform.eulerAngles.z;
		this.ubh_LinearShot[1].m_angle = 220f + base.gameObject.transform.eulerAngles.z;
		EazySoundManager.PlaySound(this.listSound[4], 1f);
		this.ubh_ShotByGuns.StartShotRoutine();
		this.fx_ShotByGuns[0].SetActive(true);
		this.fx_ShotByGuns[1].SetActive(true);
		yield return new WaitForSeconds(0.3f * this.angryStat);
		this.fx_ShotByGuns[0].SetActive(false);
		this.fx_ShotByGuns[1].SetActive(false);
		yield break;
	}

	private IEnumerator AttackShotByMouth()
	{
		yield return new WaitForSeconds(this.timeDelay_ShotByMouth * this.angryStat);
		this.warning_ShotByMouth.SetActive(true);
		base.StartCoroutine(this.ChangeAnim(this.animShotByMouth, 2.7f));
		EazySoundManager.PlaySound(this.listSound[5], 1f);
		yield return new WaitForSeconds(1.4f * this.angryStat);
		this.ubh_SinWave.m_centerAngle = base.gameObject.transform.eulerAngles.z + 180f;
		this.ubh_ShotByMouth.StartShotRoutine();
		EazySoundManager.PlaySound(this.listSound[6], 1f);
		this.bfp_ThrowHand.Attack(0, 2, this.speed_BigRocket, null, false);
		this.bfp_ThrowHand.Attack(1, 2, this.speed_BigRocket, null, false);
		this.warning_ShotByMouth.SetActive(false);
		yield break;
	}

	private IEnumerator AttackThrowLeftHand()
	{
		yield return new WaitForSeconds(this.timeDelay_ThrowLeftHand * this.angryStat);
		this.warning_ThrowLeftHand.SetActive(true);
		base.StartCoroutine(this.ChangeAnim(this.animThrowLeftHand, 2.16f));
		EazySoundManager.PlaySound(this.listSound[7], 1f);
		yield return new WaitForSeconds(1f * this.angryStat);
		this.bfp_ThrowHand.Attack(2, 0, this.speed_ThrowLeftHand, this.player.transform, true);
		EazySoundManager.PlaySound(this.listSound[8], 1f);
		this.warning_ThrowLeftHand.SetActive(false);
		yield break;
	}

	private IEnumerator AttackThrowRightHand()
	{
		yield return new WaitForSeconds(this.timeDelay_ThrowRightHand * this.angryStat);
		this.warning_ThrowRightHand.SetActive(true);
		base.StartCoroutine(this.ChangeAnim(this.animThrowRightHand, 2.16f));
		EazySoundManager.PlaySound(this.listSound[9], 1f);
		yield return new WaitForSeconds(1f * this.angryStat);
		this.bfp_ThrowHand.Attack(3, 1, this.speed_ThrowRightHand, this.player.transform, true);
		EazySoundManager.PlaySound(this.listSound[10], 1f);
		this.warning_ThrowRightHand.SetActive(false);
		yield break;
	}

	private IEnumerator AttackThrowTwoHand()
	{
		yield return new WaitForSeconds(this.timeDelay_ThrowTwoHand * this.angryStat);
		this.warning_ThrowLeftHand.SetActive(true);
		this.warning_ThrowRightHand.SetActive(true);
		base.StartCoroutine(this.ChangeAnim(this.animThrowTwoHand, 2.16f));
		EazySoundManager.PlaySound(this.listSound[13], 1f);
		yield return new WaitForSeconds(0.9f * this.angryStat);
		this.bfp_ThrowHand.Attack(4, 0, this.speed_ThrowTwoHand, this.player.transform, true);
		this.bfp_ThrowHand.Attack(5, 1, this.speed_ThrowTwoHand, this.player.transform, true);
		EazySoundManager.PlaySound(this.listSound[14], 1f);
		this.warning_ThrowRightHand.SetActive(false);
		this.warning_ThrowLeftHand.SetActive(false);
		yield break;
	}

	private IEnumerator AttackDash()
	{
		yield return new WaitForSeconds(this.timeDelay_Dash * this.angryStat);
		this.isBlind = true;
		this.smoothFollow.isSmoothFollow = false;
		this.warning_dash.SetActive(true);
		float distance = Vector3.Distance(base.gameObject.transform.position, this.player.position);
		float speed = distance * this.speed_Dash;
		if (this.pathMoveDash.GetComponent<BezierPathManager>() != null)
		{
			this.pathMoveDash.GetComponent<BezierPathManager>().CalculatePath();
		}
		if (this.player.transform)
		{
			this.pathMoveDash.waypoints[this.pathMoveDash.waypoints.Length - 2].transform.position = new Vector3(this.player.transform.position.x, this.player.transform.position.y, 0f);
		}
		this.splineMove.speed = speed;
		EazySoundManager.PlaySound(this.listSound[11], 0.8f);
		yield return new WaitForSeconds(0.5f);
		this.warning_dash.SetActive(false);
		this.splineMove.SetPath(this.pathMoveDash);
		this.skeletonAnimation.timeScale = this.speed_Dash + 0.5f;
		yield return new WaitForSeconds(0.5f * this.angryStat);
		base.StartCoroutine(this.ChangeAnim(this.animDash, 2.16f / this.speed_Dash));
		EazySoundManager.PlaySound(this.listSound[12], 1f);
		yield return new WaitForSeconds(1.08f / this.speed_Dash * this.angryStat);
		if (this.isActiveAngry)
		{
			this.skeletonAnimation.timeScale = 1f + (1f - this.angryStat) * 2f;
		}
		else
		{
			this.skeletonAnimation.timeScale = 1f;
		}
		yield return new WaitForSeconds(1.08f / this.speed_Dash * this.angryStat);
		this.isBlind = false;
		this.smoothFollow.isSmoothFollow = true;
		yield break;
	}

	private IEnumerator AttackShotByTail()
	{
		yield return new WaitForSeconds(this.timeDelay_ShotByTail * this.angryStat);
		this.MoveMid();
		yield return new WaitForSeconds(1f);
		base.StartCoroutine(this.ChangeAnim(this.animShotByTail, 2.96f));
		EazySoundManager.PlaySound(this.listSound[15], 1f);
		yield return new WaitForSeconds(0.5f * this.angryStat);
		this.fx_ShotByTail.SetActive(true);
		yield return new WaitForSeconds(1f * this.angryStat);
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < this.numberBullet_ShoyByTail; j++)
			{
				UbhBullet objBullett = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.prefabsBullet, this.posSpawBullet.position, false);
				NewBoss_BulletHomming homingMissile = objBullett.GetComponent<NewBoss_BulletHomming>();
				homingMissile.SetFollowTargetX(this.timeFollow_Rocket);
				EazySoundManager.PlaySound(this.listSound[16], 1f);
				yield return new WaitForSeconds(0.4f / (float)this.numberBullet_ShoyByTail);
			}
		}
		this.fx_ShotByTail.SetActive(false);
		yield return new WaitForSeconds(2f * this.angryStat);
		this.smoothFollow.isSmoothFollow = true;
		yield break;
	}

	private void MoveMid()
	{
		this.smoothFollow.isSmoothFollow = false;
		this.pathMid = new Vector2[]
		{
			new Vector2(base.transform.position.x, base.transform.position.y),
			new Vector2(0f, 3f)
		};
		this.BaseMoveCurve(1.5f, Ease.Linear, PathMode.Ignore, this.pathMid);
	}

	private void MoveUp()
	{
		this.smoothFollow.isSmoothFollow = false;
		this.pathMid = new Vector2[]
		{
			new Vector2(base.transform.position.x, base.transform.position.y),
			new Vector2(0f, 5f)
		};
		this.BaseMoveCurve(1.5f, Ease.Linear, PathMode.Ignore, this.pathMid);
	}

	private void MoveDown()
	{
		this.smoothFollow.isSmoothFollow = false;
		this.pathMid = new Vector2[]
		{
			new Vector2(base.transform.position.x, base.transform.position.y),
			new Vector2(0f, -5f)
		};
		this.BaseMoveCurve(10f, Ease.Linear, PathMode.Ignore, this.pathMid);
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		EazySoundManager.PlaySound(this.listSound[2], 1f);
	}

	private int GetRandomValue(params BossMode_CuaXanh.RandomSelection[] selections)
	{
		float value = UnityEngine.Random.value;
		float num = 0f;
		foreach (BossMode_CuaXanh.RandomSelection randomSelection in selections)
		{
			num += randomSelection.probability;
			if (value <= num)
			{
				return randomSelection.GetValue();
			}
		}
		return -1;
	}

	private bool isActiveAngry;

	private float angryStat = 1f;

	private bool isBlind;

	public bool isTransforming;

	[SerializeField]
	private AudioClip[] listSound;

	[SerializeField]
	private SmoothFollow smoothFollow;

	[SerializeField]
	private SkeletonAnimation skeletonAnimation;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animIdle;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByGuns;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByMouth;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animThrowLeftHand;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animThrowRightHand;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animThrowTwoHand;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animDash;

	[SpineAnimation("", "", true, false)]
	[SerializeField]
	private string animShotByTail;

	private float timeDelay_ShotByGuns;

	[SerializeField]
	private UbhShotCtrl ubh_ShotByGuns;

	[SerializeField]
	private GameObject[] warning_ShotByGuns;

	[SerializeField]
	private GameObject[] fx_ShotByGuns;

	[SerializeField]
	private UbhLinearShot[] ubh_LinearShot;

	private float timeDelay_ShotByMouth;

	private float speed_BigRocket = 7f;

	[SerializeField]
	private UbhShotCtrl ubh_ShotByMouth;

	[SerializeField]
	private UbhSinWaveBulletNwayShot ubh_SinWave;

	[SerializeField]
	private GameObject warning_ShotByMouth;

	private float timeDelay_ThrowLeftHand;

	private float speed_ThrowLeftHand = 12f;

	[SerializeField]
	private GameObject warning_ThrowLeftHand;

	private float timeDelay_ThrowRightHand;

	private float speed_ThrowRightHand = 12f;

	[SerializeField]
	private GameObject warning_ThrowRightHand;

	private float timeDelay_ThrowTwoHand;

	private float speed_ThrowTwoHand = 12f;

	[SerializeField]
	private BulletFollowPath bfp_ThrowHand;

	private float timeDelay_Dash;

	private float speed_Dash = 1.2f;

	[SerializeField]
	private GameObject warning_dash;

	[SerializeField]
	private splineMove splineMove;

	[SerializeField]
	private PathManager pathMoveDash;

	private float timeDelay_ShotByTail;

	private int numberBullet_ShoyByTail = 2;

	private float timeFollow_Rocket = 3f;

	[SerializeField]
	private GameObject fx_ShotByTail;

	[SerializeField]
	private Transform posSpawBullet;

	[SerializeField]
	private GameObject prefabsBullet;

	[EnemyEventCustom(displayName = "BossTuan/CuaXanh_BaseStatBoss")]
	public EnemyEvent BaseStatBoss;

	private float rangeAttack = 30f;

	public LayerMask layerPlayer;

	public Transform player;

	private int currentIdSkill = -1;

	private Vector2[] pathMid;

	private Vector2[] pathUp;

	private Vector2[] pathDown;

	private struct RandomSelection
	{
		public RandomSelection(int minValue, int maxValue, float probability)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.probability = probability;
		}

		public int GetValue()
		{
			return UnityEngine.Random.Range(this.minValue, this.maxValue + 1);
		}

		private int minValue;

		private int maxValue;

		public float probability;
	}
}
