using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace SkyGameKit
{
	public class ReflectiveLaser : MonoBehaviour
	{
		private Vector2 bottomLeft
		{
			[CompilerGenerated]
			get
			{
				return SgkCamera.bottomLeft + this.bottomLeftOffset;
			}
		}

		private Vector2 topRight
		{
			[CompilerGenerated]
			get
			{
				return SgkCamera.topRight + this.topRightOffset;
			}
		}

		protected virtual void OnHit(LaserHitInfo target)
		{
		}

		protected virtual void OnEnable()
		{
			if (Fu.IsNullOrEmpty(this.lines))
			{
				SgkLog.LogError("Không có LineRenderer");
				return;
			}
			for (int i = 0; i < this.lines.Length; i++)
			{
				this.lines[i].positionCount = 0;
			}
			this.positions = new Vector2[this.lines.Length + 1];
		}

		protected virtual void Update()
		{
			this.current = (this.positions[0] = base.transform.position);
			this.smallAngle = ReflectiveLaser.Clamp0360(base.transform.rotation.eulerAngles.z);
			if (Fu.Outside(this.current, this.bottomLeft, this.topRight))
			{
				this.SetPositonForLine(true);
				return;
			}
			this.endPointIndex = 1;
			while (this.endPointIndex < this.positions.Length)
			{
				this.CalculateDirect();
				this.hit = Physics2D.Raycast(this.current, this.direct, this.direct.magnitude, this.layerMask);
				if (this.hit.collider != null)
				{
					this.positions[this.endPointIndex] = this.hit.point;
					break;
				}
				this.current += this.direct;
				this.smallAngle = ReflectiveLaser.Clamp0360(180f - this.smallAngle);
				this.positions[this.endPointIndex] = this.current;
				if (this.current.y < this.bottomLeft.y + 0.1f || this.current.y > this.topRight.y - 0.1f)
				{
					break;
				}
				this.endPointIndex++;
			}
			if (this.hit.collider != null)
			{
				if (this.hit.collider == this.lastCollider)
				{
					this.laserHitInfo.SetValue(Time.deltaTime, this.hit.point, this.hit.collider);
				}
				else
				{
					this.laserHitInfo.SetValue(0f, this.hit.point, this.hit.collider);
					this.lastCollider = this.hit.collider;
				}
				if (this.onHit != null)
				{
					this.onHit(this.laserHitInfo);
				}
				this.OnHit(this.laserHitInfo);
			}
			else
			{
				this.lastCollider = null;
			}
			this.SetPositonForLine(false);
		}

		private void CalculateDirect()
		{
			Vector2 a = Fu.RotateVector2(Vector2.right, this.smallAngle);
			float num;
			if (a.x == 0f)
			{
				num = 1000000f;
			}
			else
			{
				this.direct = a * (((a.x <= 0f) ? this.bottomLeft.x : this.topRight.x) - this.current.x) / a.x;
				num = this.current.y + this.direct.y;
			}
			if (num < this.bottomLeft.y || num > this.topRight.y)
			{
				this.direct = a * (((a.y <= 0f) ? this.bottomLeft.y : this.topRight.y) - this.current.y) / a.y;
			}
		}

		public static float Clamp0360(float eulerAngles)
		{
			float num = eulerAngles - (float)Mathf.CeilToInt(eulerAngles / 360f) * 360f;
			if (num < 0f)
			{
				num += 360f;
			}
			return num;
		}

		public void SetPositonForLine(bool clear = false)
		{
			if (clear)
			{
				for (int i = 0; i < this.lines.Length; i++)
				{
					this.lines[i].positionCount = 0;
				}
			}
			else
			{
				for (int j = 0; j < this.lines.Length; j++)
				{
					if (j < this.endPointIndex)
					{
						this.lines[j].positionCount = 2;
						this.lines[j].SetPosition(0, this.positions[j]);
						this.lines[j].SetPosition(1, this.positions[j + 1]);
					}
					else
					{
						this.lines[j].positionCount = 0;
					}
				}
				if (this.hitFx != null && this.endPointIndex < this.positions.Length)
				{
					this.hitFx.position = this.positions[this.endPointIndex];
				}
			}
		}

		public LineRenderer[] lines;

		public Transform hitFx;

		public LayerMask layerMask;

		public Vector2 bottomLeftOffset;

		public Vector2 topRightOffset;

		public Action<LaserHitInfo> onHit;

		private Vector2 current;

		private float smallAngle;

		private Vector2 direct;

		private Vector2[] positions;

		private int endPointIndex;

		private Collider2D lastCollider;

		private RaycastHit2D hit;

		private LaserHitInfo laserHitInfo;
	}
}
