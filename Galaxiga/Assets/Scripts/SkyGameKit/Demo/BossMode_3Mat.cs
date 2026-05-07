using System;
using System.Collections;
using Hellmade.Sound;
using UnityEngine;

namespace SkyGameKit.Demo
{
	public class BossMode_3Mat : NewBoss
	{
		[EnemyAction(displayName = "Tuan/StartAttack")]
		public void StartAttack()
		{
			this.ActiveTakeDamage();
			base.StartCoroutine(this.AttackHorn());
		}

		[EnemyAction(displayName = "Tuan/ActiveStatusAngry")]
		public void ActiveStatusAngry()
		{
			this.isAngry = true;
			base.ShowRedScreen();
			EazySoundManager.PlaySound(this.listSound[1], 0.45f);
		}

		[EnemyAction(displayName = "Tuan/Skill0_AttackHorn")]
		public void Skill0_AttackHorn(float timeDelay, int numberBullet, float speedBullet, float timeDelayShot)
		{
			this.timeDelay_AttackHorn = timeDelay;
			this.numberBullet_AttackHorn = numberBullet;
			this.speed_AttackHorn = speedBullet;
			this.timeDelay_Shot = timeDelayShot;
		}

		[EnemyAction(displayName = "Tuan/Skill1_AttackMouth")]
		public void Skill1_AttackMouth(float timeDelay, int numberBullet, float speedBullet)
		{
			this.timeDelay_AttackMouth = timeDelay;
			for (int i = 0; i < this.ubhRandomShot.Length; i++)
			{
				this.ubhRandomShot[i].m_bulletNum = numberBullet;
				this.ubhRandomShot[i].m_randomSpeedMin = speedBullet - 1f;
				this.ubhRandomShot[i].m_randomSpeedMax = speedBullet;
			}
		}

		[EnemyAction(displayName = "Tuan/Skill2_AttackEyes")]
		public void Skill2_AttackEyes(float timeDelay, int numberBullet, float speedBullet)
		{
			this.timeDelay_AttackEyes = timeDelay;
			for (int i = 0; i < this.ubhSpiralShot.Length; i++)
			{
				this.ubhSpiralShot[i].m_bulletNum = numberBullet;
				this.ubhSpiralShot[i].m_bulletSpeed = speedBullet;
			}
		}

		[EnemyAction(displayName = "Tuan/AttackDestroyEyes")]
		public void StatAttackDestroyEyes(bool isAttackDestroyEyes, int timeFly)
		{
			for (int i = 0; i < this.bossMode_3Mat_Eyes.Length; i++)
			{
				this.bossMode_3Mat_Eyes[i].isAttackDestroyEyes = isAttackDestroyEyes;
				this.bossMode_3Mat_Eyes[i].speedFall = (float)timeFly;
			}
		}

		[EnemyAction(displayName = "Tuan/EyesFalling")]
		public void EyesFalling(int idEyes)
		{
			this.bossMode_3Mat_Eyes[idEyes].ActiveFall();
		}

		public override void Restart()
		{
			base.Restart();
			if (this.BaseStatBoss != null)
			{
				this.BaseStatBoss();
			}
			this.anim = base.GetComponent<Animator>();
			base.ShowHealthBar();
			base.SetNameBoss("Three Eyes Frog");
		}

		private void Update()
		{
			if (this.player != null && !this.isBlind)
			{
				Vector3 vector = this.player.position - base.transform.position;
				vector.Normalize();
				float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
				if (num < -50f && num > -130f)
				{
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(0f, 0f, num + 90f), Time.deltaTime * 1f);
				}
				else
				{
					base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * 1f);
				}
			}
		}

		private void OnEnable()
		{
			this.FindRandomTarget();
			EazySoundManager.PlaySound(this.listSound[0], 0.15f);
		}

		private void ActiveTakeDamage()
		{
			base.gameObject.GetComponent<BoxCollider2D>().enabled = true;
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

		private IEnumerator AttackHorn()
		{
			yield return new WaitForSeconds(this.timeDelay_AttackHorn);
			this.warning_AttackHorn[0].SetActive(true);
			this.warning_AttackHorn[1].SetActive(true);
			EazySoundManager.PlaySound(this.listSound[4], 0.6f);
			yield return new WaitForSeconds(1f);
			EazySoundManager.PlaySound(this.listSound[5], 0.15f);
			for (int i = 0; i < this.numberBullet_AttackHorn; i++)
			{
				this.anim.SetTrigger("attack_horn");
				this.warning_AttackHorn[0].SetActive(true);
				this.warning_AttackHorn[1].SetActive(true);
				yield return new WaitForSeconds(0.35f);
				if (!this.isAngry)
				{
					this.bfp_AttackHorn.Attack(0, 0, this.speed_AttackHorn, null, false);
					this.bfp_AttackHorn.Attack(1, 0, this.speed_AttackHorn, null, false);
				}
				else
				{
					this.bfp_AttackHorn.Attack(0, 1, this.speed_AttackHorn, null, false);
					this.bfp_AttackHorn.Attack(1, 1, this.speed_AttackHorn, null, false);
				}
				this.warning_AttackHorn[0].SetActive(false);
				this.warning_AttackHorn[1].SetActive(false);
				EazySoundManager.PlaySound(this.listSound[6], 1f);
				yield return new WaitForSeconds(this.timeDelay_Shot);
			}
			base.StartCoroutine(this.AttackMouth());
			yield break;
		}

		private IEnumerator AttackMouth()
		{
			yield return new WaitForSeconds(this.timeDelay_AttackMouth);
			this.warning_AttackMouth.SetActive(true);
			this.warning_AttackMouth.GetComponent<Animator>().SetTrigger("beforeattack");
			EazySoundManager.PlaySound(this.listSound[4]);
			yield return new WaitForSeconds(1f);
			this.anim.SetTrigger("attack_mouth");
			EazySoundManager.PlaySound(this.listSound[7], 0.4f);
			yield return new WaitForSeconds(0.1f);
			this.objAttackMouth.StartShotRoutine();
			yield return null;
			base.StartCoroutine(this.AttackEyes());
			EazySoundManager.PlaySound(this.listSound[8], 1f);
			float time = (float)this.ubhRandomShot[0].m_bulletNum * 0.2f;
			yield return new WaitForSeconds(time);
			this.anim.SetTrigger("idle");
			yield break;
		}

		private IEnumerator AttackEyes()
		{
			yield return new WaitForSeconds(this.timeDelay_AttackEyes);
			if (!this.isAngry)
			{
				int id = UnityEngine.Random.Range(0, 3);
				this.warning_AttackEyes[id].SetActive(true);
				EazySoundManager.PlaySound(this.listSound[9], 0.65f);
				yield return new WaitForSeconds(1f);
				EazySoundManager.PlaySound(this.listSound[10], 0.5f);
				if (id == 0)
				{
					this.anim.SetTrigger("attack_eyer");
				}
				else if (id == 1)
				{
					this.anim.SetTrigger("attack_eyem");
				}
				else if (id == 2)
				{
					this.anim.SetTrigger("attack_eyel");
				}
				yield return new WaitForSeconds(0.5f);
				this.warning_AttackEyes[id].SetActive(false);
				if (this.listEyes[id].activeSelf)
				{
					this.objAttackEyes[id].StartShotRoutine();
				}
				EazySoundManager.PlaySound(this.listSound[11], 1f);
			}
			else
			{
				for (int i = 0; i < this.listEyes.Length; i++)
				{
					this.warning_AttackEyes[i].SetActive(true);
				}
				EazySoundManager.PlaySound(this.listSound[9], 0.65f);
				yield return new WaitForSeconds(1f);
				EazySoundManager.PlaySound(this.listSound[10], 0.5f);
				this.anim.SetTrigger("attack_eyesall");
				yield return new WaitForSeconds(0.5f);
				for (int j = 0; j < this.listEyes.Length; j++)
				{
					this.warning_AttackEyes[j].SetActive(false);
					if (this.listEyes[j].activeSelf)
					{
						this.objAttackEyes[j].StartShotRoutine();
					}
					EazySoundManager.PlaySound(this.listSound[11], 1f);
				}
			}
			base.StartCoroutine(this.AttackHorn());
			yield break;
		}

		public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
		{
			base.Die(type);
			EazySoundManager.PlaySound(this.listSound[3], 0.45f);
		}

		[Header("ATTACK_BOSS")]
		[SerializeField]
		private bool isAngry;

		[SerializeField]
		private BoxCollider2D box;

		[SerializeField]
		private Animator anim;

		[SerializeField]
		private AudioClip[] listSound;

		private float timeDelay_AttackHorn;

		private float timeDelay_Shot = 0.5f;

		private int numberBullet_AttackHorn;

		private float speed_AttackHorn = 6f;

		[SerializeField]
		private GameObject[] warning_AttackHorn;

		[SerializeField]
		private BulletFollowPath bfp_AttackHorn;

		private float timeDelay_AttackMouth;

		[SerializeField]
		private GameObject warning_AttackMouth;

		[SerializeField]
		private UbhShotCtrl objAttackMouth;

		[SerializeField]
		private UbhRandomShot[] ubhRandomShot;

		private float timeDelay_AttackEyes;

		[SerializeField]
		private GameObject[] warning_AttackEyes;

		[SerializeField]
		private UbhShotCtrl[] objAttackEyes;

		[SerializeField]
		private GameObject[] listEyes;

		[SerializeField]
		private BossMode_3Mat_Eyes[] bossMode_3Mat_Eyes;

		[SerializeField]
		private UbhSpiralShot[] ubhSpiralShot;

		[EnemyEventCustom(displayName = "BossTuan/3Mat_BaseStatBoss")]
		public EnemyEvent BaseStatBoss;

		private bool isBlind;

		private float rangeAttack = 30f;

		public LayerMask layerPlayer;

		public Transform player;
	}
}
