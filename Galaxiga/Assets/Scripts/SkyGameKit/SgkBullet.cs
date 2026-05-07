using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkyGameKit
{
	[RequireComponent(typeof(UbhBullet))]
	public class SgkBullet : MonoBehaviour
	{
		public virtual void Explosion()
		{
			if (base.gameObject.activeInHierarchy)
			{
				Fu.SpawnExplosion(this.explosionPrefab, base.transform.position, Quaternion.identity);
				UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this.ubhBullet, false);
			}
		}

		protected virtual void Awake()
		{
			this.randomNumber = UnityEngine.Random.Range(0, 4);
			this.ubhBullet = base.GetComponent<UbhBullet>();
		}

		protected virtual void Start()
		{
			if (SgkBullet.bottomLeftWithOffset.x < 0.01f)
			{
				SgkBullet.bottomLeftWithOffset = SgkCamera.bottomLeft - 1f * Vector2.one;
				SgkBullet.topRightWithOffset = SgkCamera.topRight + 1f * Vector2.one;
			}
		}

		protected virtual void Update()
		{
			if (Time.frameCount % 5 == this.randomNumber && Fu.Outside(base.transform.position, SgkBullet.bottomLeftWithOffset, SgkBullet.topRightWithOffset))
			{
				UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(this.ubhBullet, false);
			}
		}

		protected virtual void OnEnable()
		{
			SgkBullet.onScreenBulletList.Add(this);
		}

		protected virtual void OnDisable()
		{
			SgkBullet.onScreenBulletList.Remove(this);
		}

		public static List<SgkBullet> onScreenBulletList = new List<SgkBullet>();

		[Tooltip("Có thể gây damage cho nhiều đối tượng một lúc không")]
		public bool spreadDamage;

		public int power;

		public GameObject explosionPrefab;

		private static Vector2 bottomLeftWithOffset;

		private static Vector2 topRightWithOffset;

		private int randomNumber;

		private UbhBullet ubhBullet;
	}
}
