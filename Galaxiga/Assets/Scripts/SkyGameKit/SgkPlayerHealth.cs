using System;
using SkyGameKit.QuickAccess;
using UnityEngine;

namespace SkyGameKit
{
	public class SgkPlayerHealth : APlayerHealth
	{
		protected virtual void Start()
		{
			this.playerTransform = base.transform.parent;
			this.playerC = this.playerTransform.GetComponent<APlayerMove>();
			this.playerS = this.playerTransform.GetComponent<APlayerState>();
			this.playerA = this.playerTransform.GetComponent<APlayerAttack>();
			this.startPos = this.playerTransform.position;
			this.playerCanTakeDamage = true;
			if (this.maxHP <= 0)
			{
				this.maxHP = 3;
			}
			this.CurrentHP = this.maxHP;
		}

		protected virtual void OnTriggerEnter2D(Collider2D c)
		{
			if (c.tag.Contains("Enemy"))
			{
				string tag = c.tag;
				if (tag != null)
				{
					if (!(tag == "Enemy"))
					{
						if (tag == "EnemyBullet")
						{
							UbhBullet component = c.GetComponent<UbhBullet>();
							if (component != null)
							{
								UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(component, false);
							}
						}
					}
					else
					{
						BaseEnemy component2 = c.transform.GetComponent<BaseEnemy>();
						if (component2 != null)
						{
							component2.Die(EnemyKilledBy.Player);
						}
					}
				}
				if (this.playerCanTakeDamage)
				{
					if (this.CurrentHP > 0)
					{
						this.Respawn();
					}
					else
					{
						this.Die();
					}
					this.CurrentHP--;
				}
			}
		}

		protected virtual void Die()
		{
			this.GameOver();
		}

		public override void Respawn()
		{
			Fu.SpawnExplosion(this.explosionPrefab, base.transform.position, Quaternion.identity);
			this.playerC.AutoMove(this.spawnPos, this.startPos, 1f, null);
			this.playerS.SetShieldActive(this.playerS.itemActiveTime);
		}

		protected virtual void GameOver()
		{
			if (SgkSingleton<LevelManager>.Instance != null)
			{
				SgkSingleton<LevelManager>.Instance.EndLevel(false);
			}
			else
			{
				SgkLog.LogError("Không có LevelManager");
			}
			this.playerTransform.position = new Vector3(this.spawnPos.x, this.spawnPos.y, this.playerTransform.position.z);
			this.playerC.enabled = false;
			this.playerA.LockShot = true;
		}

		public GameObject explosionPrefab;

		[Tooltip("Vị trí player bắt đầu đi từ dưới lên sau khi chết")]
		public Vector2 spawnPos;

		private Vector3 startPos;

		private Transform playerTransform;

		private APlayerMove playerC;

		private APlayerState playerS;

		private APlayerAttack playerA;
	}
}
