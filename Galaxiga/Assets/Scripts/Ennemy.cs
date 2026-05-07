using System;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
	private void Start()
	{
		this.m_animator = base.GetComponent<Animator>();
	}

	private void OnCollisionEnter(Collision input)
	{
		UnityEngine.Object.Destroy(input.gameObject);
		this.m_animator.SetTrigger("Hit");
	}

	private void OnCollisionEnter2D(Collision2D input)
	{
		UnityEngine.Object.Destroy(input.gameObject);
		this.m_animator.SetTrigger("Hit");
	}

	private Animator m_animator;
}
