using System;
using System.Collections;
using UnityEngine;

namespace SkyGameKit.Demo
{
	public class Boss2 : BossGeneral
	{
		private void Start()
		{
			base.StartCoroutine(this.Attack1());
		}

		private IEnumerator Attack1()
		{
			this.lightAttack.SetActive(true);
			yield return new WaitForSeconds(this.timeAttack1);
			this.objAttack1.StartShotRoutine();
			yield return null;
			this.lightAttack.SetActive(false);
			base.StartCoroutine(this.Attack2());
			yield break;
		}

		private IEnumerator Attack2()
		{
			this.lightAttack.SetActive(true);
			yield return new WaitForSeconds(this.timeAttack2);
			this.objAttack2.StartShotRoutine();
			yield return null;
			this.lightAttack.SetActive(false);
			base.StartCoroutine(this.Attack3());
			yield break;
		}

		private IEnumerator Attack3()
		{
			yield return new WaitForSeconds(this.timeAttack2);
			this.objAttack3.StartShotRoutine();
			yield return null;
			base.StartCoroutine(this.Attack1());
			yield break;
		}

		[Header("ATTACK_BOSS")]
		public float timeAttack1;

		public float timeAttack2;

		public GameObject lightAttack;

		public UbhShotCtrl objAttack1;

		public UbhShotCtrl objAttack2;

		public UbhShotCtrl objAttack3;

		public bool stSpawItem;
	}
}
