using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UbhSpaceship : UbhMonoBehaviour
{
	private void Start()
	{
		this.m_animator = base.GetComponent<Animator>();
	}

	public void Explosion()
	{
		if (this.m_explosionPrefab != null)
		{
			UnityEngine.Object.Instantiate<GameObject>(this.m_explosionPrefab, base.transform.position, base.transform.rotation);
		}
	}

	public Animator GetAnimator()
	{
		return this.m_animator;
	}

	[FormerlySerializedAs("_Speed")]
	public float m_speed;

	[SerializeField]
	[FormerlySerializedAs("_ExplosionPrefab")]
	private GameObject m_explosionPrefab;

	private Animator m_animator;
}
