using System;
using UnityEngine;

public class UbhBulletSimpleSprite2d : UbhBullet
{
	public override bool isActive
	{
		get
		{
			return this.m_isActive;
		}
	}

	public override void SetActive(bool isActive)
	{
		this.m_isActive = isActive;
		this.m_rigidbody2d.simulated = isActive;
		if (this.m_collider2ds != null && this.m_collider2ds.Length > 0)
		{
			for (int i = 0; i < this.m_collider2ds.Length; i++)
			{
				this.m_collider2ds[i].enabled = isActive;
			}
		}
		if (this.m_spriteRenderers != null && this.m_spriteRenderers.Length > 0)
		{
			for (int j = 0; j < this.m_spriteRenderers.Length; j++)
			{
				this.m_spriteRenderers[j].enabled = isActive;
			}
		}
	}

	[SerializeField]
	private Rigidbody2D m_rigidbody2d;

	[SerializeField]
	private Collider2D[] m_collider2ds;

	[SerializeField]
	private SpriteRenderer[] m_spriteRenderers;

	private bool m_isActive;
}
