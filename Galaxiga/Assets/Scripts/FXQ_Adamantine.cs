using System;
using UnityEngine;

public class FXQ_Adamantine : MonoBehaviour
{
	private void Awake()
	{
		this.anim = base.GetComponent<Animator>();
	}

	private void Start()
	{
		this.anim.SetBool("UnderAttack", false);
	}

	private void Update()
	{
	}

	public void UnderAttack()
	{
		if (this.anim == null)
		{
			return;
		}
		if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
		{
			this.anim.Play("Hurt");
		}
	}

	private Animator anim;
}
