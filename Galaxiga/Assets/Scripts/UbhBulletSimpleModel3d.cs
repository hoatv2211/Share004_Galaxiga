using System;
using UnityEngine;

public class UbhBulletSimpleModel3d : UbhBullet
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
		this.m_rigidbody3d.detectCollisions = isActive;
		if (this.m_collider3ds != null && this.m_collider3ds.Length > 0)
		{
			for (int i = 0; i < this.m_collider3ds.Length; i++)
			{
				this.m_collider3ds[i].enabled = isActive;
			}
		}
		if (this.m_meshRenderers != null && this.m_meshRenderers.Length > 0)
		{
			for (int j = 0; j < this.m_meshRenderers.Length; j++)
			{
				this.m_meshRenderers[j].enabled = isActive;
			}
		}
	}

	[SerializeField]
	private Rigidbody m_rigidbody3d;

	[SerializeField]
	private Collider[] m_collider3ds;

	[SerializeField]
	private MeshRenderer[] m_meshRenderers;

	private bool m_isActive;
}
