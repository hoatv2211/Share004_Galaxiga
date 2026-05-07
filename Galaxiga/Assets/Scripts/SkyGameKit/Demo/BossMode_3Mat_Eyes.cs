using System;
using UnityEngine;

namespace SkyGameKit.Demo
{
	public class BossMode_3Mat_Eyes : MonoBehaviour
	{
		private void Update()
		{
			if (this.isFall)
			{
				if (!this.isAttackDestroyEyes)
				{
					this.DestroyEyes();
				}
				else
				{
					base.gameObject.transform.Translate(Vector3.down * Time.deltaTime * this.speedFall);
					if (base.gameObject.transform.position.y <= 0f)
					{
						this.AttackBeforeDestroy();
					}
				}
			}
		}

		public void ActiveFall()
		{
			this.isFall = true;
			CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
			Fu.SpawnExplosion(this.effectFall, base.gameObject.transform.position, Quaternion.identity);
			this.effectFalling.SetActive(true);
		}

		private void AttackBeforeDestroy()
		{
			CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
			this.isFall = false;
			this.objDestroy.StartShotRoutine();
			this.DestroyEyes();
		}

		private void DestroyEyes()
		{
			CameraManager.curret.StartShake(CameraManager.ShakeType.EatBullet);
			Fu.SpawnExplosion(this.effectDestroy, base.gameObject.transform.position, Quaternion.identity);
			base.gameObject.SetActive(false);
		}

		public bool isFall;

		[SerializeField]
		private GameObject effectFall;

		[SerializeField]
		private GameObject effectFalling;

		[SerializeField]
		private GameObject effectDestroy;

		[SerializeField]
		private UbhShotCtrl objDestroy;

		[SerializeField]
		private UbhShotCtrl objAttack;

		public bool isAttackDestroyEyes;

		public float speedFall;
	}
}
