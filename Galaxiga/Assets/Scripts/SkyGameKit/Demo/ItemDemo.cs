using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using SkyGameKit.QuickAccess;
using UnityEngine;

namespace SkyGameKit.Demo
{
	public class ItemDemo : SgkItem
	{
		private Vector2 bottomLeft
		{
			[CompilerGenerated]
			get
			{
				return SgkCamera.bottomLeft;
			}
		}

		private Vector2 topRight
		{
			[CompilerGenerated]
			get
			{
				return SgkCamera.topRight;
			}
		}

		protected virtual void OnSpawned()
		{
			this.goToPlayer = false;
			this.directV = true;
			float d = UnityEngine.Random.Range(this.force.x, this.force.y);
			float degrees = UnityEngine.Random.Range(this.angle.x, this.angle.y);
			this.startVector = Fu.RotateVector2(Vector2.up * d, degrees);
			this.newPosition = (this.startPosition = base.transform.position);
			this.startTime = Time.time;
		}

		protected virtual void OnTriggerStay2D(Collider2D c)
		{
			if (c.name.Equals("ItemCheck") && !this.goToPlayer)
			{
				this.goToPlayer = true;
			}
		}

		protected virtual void OnTriggerEnter2D(Collider2D c)
		{
			if (c.name.Equals("BulletCheck"))
			{
				this.OnItemHitPlayer();
				this.Despawn();
			}
			else if (c.name.Equals("ItemCheck") && !this.goToPlayer)
			{
				this.goToPlayer = true;
			}
		}

		protected virtual void Update()
		{
			if (base.transform.position.x > this.topRight.x || base.transform.position.x < this.bottomLeft.x)
			{
				this.directV = !this.directV;
			}
			this.t = this.timeScale * (Time.time - this.startTime);
			if (this.goToPlayer)
			{
				this.goToPlayerSpeed += 50f * Time.deltaTime;
				base.transform.position = Vector3.MoveTowards(base.transform.position, Player.transform.position, this.goToPlayerSpeed * Time.deltaTime);
			}
			else
			{
				this.newPosition.x = this.newPosition.x + (float)((!this.directV) ? -1 : 1) * this.startVector.x * this.timeScale * Time.deltaTime;
				this.newPosition.y = this.startPosition.y + this.startVector.y * this.t - 0.5f * this.t * this.t;
				base.transform.position = this.newPosition;
			}
			if (base.transform.position.y < this.bottomLeft.y - 1f)
			{
				this.Despawn();
			}
		}

		[MinMaxSlider(0f, 10f, false)]
		public Vector2 force;

		[MinMaxSlider(-45f, 45f, false)]
		public Vector2 angle;

		public float timeScale = 2f;

		private const float g = 1f;

		private Vector2 startVector;

		private float startTime;

		private Vector3 startPosition;

		private float t;

		private bool directV = true;

		private bool goToPlayer;

		public float goToPlayerSpeed = 10f;

		private Vector3 newPosition;
	}
}
