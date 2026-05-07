using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UbhDestroyArea : UbhMonoBehaviour
{
	private void Start()
	{
		if (this.m_colCenter == null || this.m_colTop == null || this.m_colBottom == null || this.m_colRight == null || this.m_colLeft == null)
		{
			return;
		}
		UbhGameManager ubhGameManager = UnityEngine.Object.FindObjectOfType<UbhGameManager>();
		if (ubhGameManager != null && ubhGameManager.m_scaleToFit)
		{
			Vector2 a = Camera.main.ViewportToWorldPoint(UbhUtil.VECTOR2_ONE);
			Vector2 size = a * 2f;
			size.x += 0.5f;
			size.y += 0.5f;
			Vector2 vector2_ZERO = UbhUtil.VECTOR2_ZERO;
			this.m_colCenter.size = size;
			this.m_colTop.size = size;
			vector2_ZERO.x = this.m_colTop.offset.x;
			vector2_ZERO.y = size.y;
			this.m_colTop.offset = vector2_ZERO;
			this.m_colBottom.size = size;
			vector2_ZERO.x = this.m_colBottom.offset.x;
			vector2_ZERO.y = -size.y;
			this.m_colBottom.offset = vector2_ZERO;
			Vector2 vector2_ZERO2 = UbhUtil.VECTOR2_ZERO;
			vector2_ZERO2.x = size.y;
			vector2_ZERO2.y = size.x;
			this.m_colRight.size = vector2_ZERO2;
			vector2_ZERO.x = size.x / 2f + vector2_ZERO2.x / 2f;
			vector2_ZERO.y = this.m_colRight.offset.y;
			this.m_colRight.offset = vector2_ZERO;
			this.m_colLeft.size = vector2_ZERO2;
			vector2_ZERO.x = -(size.x / 2f) - vector2_ZERO2.x / 2f;
			vector2_ZERO.y = this.m_colLeft.offset.y;
			this.m_colLeft.offset = vector2_ZERO;
		}
		this.m_colCenter.enabled = this.m_useCenterCollider;
		this.m_colTop.enabled = !this.m_useCenterCollider;
		this.m_colBottom.enabled = !this.m_useCenterCollider;
		this.m_colRight.enabled = !this.m_useCenterCollider;
		this.m_colLeft.enabled = !this.m_useCenterCollider;
	}

	private void OnTriggerEnter2D(Collider2D c)
	{
		if (this.m_useCenterCollider)
		{
			return;
		}
		this.HitCheck(c.transform);
	}

	private void OnTriggerExit2D(Collider2D c)
	{
		if (!this.m_useCenterCollider)
		{
			return;
		}
		this.HitCheck(c.transform);
	}

	private void OnTriggerEnter(Collider c)
	{
		if (this.m_useCenterCollider)
		{
			return;
		}
		this.HitCheck(c.transform);
	}

	private void OnTriggerExit(Collider c)
	{
		if (!this.m_useCenterCollider)
		{
			return;
		}
		this.HitCheck(c.transform);
	}

	private void HitCheck(Transform colTrans)
	{
		string name = colTrans.name;
		if (name.Contains("EnemyBullet") || name.Contains("PlayerBullet"))
		{
			UbhBullet componentInParent = colTrans.parent.GetComponentInParent<UbhBullet>();
			if (componentInParent != null && componentInParent.isActive)
			{
				UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(componentInParent, false);
			}
		}
		else if (!name.Contains("Player"))
		{
			UnityEngine.Object.Destroy(colTrans.gameObject);
		}
	}

	[SerializeField]
	[FormerlySerializedAs("_UseCenterCollider")]
	private bool m_useCenterCollider;

	[SerializeField]
	[FormerlySerializedAs("_ColCenter")]
	private BoxCollider2D m_colCenter;

	[SerializeField]
	[FormerlySerializedAs("_ColTop")]
	private BoxCollider2D m_colTop;

	[SerializeField]
	[FormerlySerializedAs("_ColBottom")]
	private BoxCollider2D m_colBottom;

	[SerializeField]
	[FormerlySerializedAs("_ColRight")]
	private BoxCollider2D m_colRight;

	[SerializeField]
	[FormerlySerializedAs("_ColLeft")]
	private BoxCollider2D m_colLeft;
}
