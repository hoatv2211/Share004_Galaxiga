using System;
using UnityEngine;

public abstract class UbhMonoBehaviour : MonoBehaviour
{
	public new GameObject gameObject
	{
		get
		{
			if (this.m_gameObject == null)
			{
				this.m_gameObject = base.gameObject;
			}
			return this.m_gameObject;
		}
	}

	public new Transform transform
	{
		get
		{
			if (this.m_transform == null)
			{
				this.m_transform = base.GetComponent<Transform>();
			}
			return this.m_transform;
		}
	}

	public Renderer renderer
	{
		get
		{
			if (this.m_renderer == null)
			{
				this.m_renderer = base.GetComponent<Renderer>();
			}
			return this.m_renderer;
		}
	}

	public Rigidbody rigidbody
	{
		get
		{
			if (this.m_rigidbody == null)
			{
				this.m_rigidbody = base.GetComponent<Rigidbody>();
			}
			return this.m_rigidbody;
		}
	}

	public Rigidbody2D rigidbody2D
	{
		get
		{
			if (this.m_rigidbody2D == null)
			{
				this.m_rigidbody2D = base.GetComponent<Rigidbody2D>();
			}
			return this.m_rigidbody2D;
		}
	}

	private GameObject m_gameObject;

	private Transform m_transform;

	private Renderer m_renderer;

	private Rigidbody m_rigidbody;

	private Rigidbody2D m_rigidbody2D;
}
