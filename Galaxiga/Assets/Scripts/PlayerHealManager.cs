using System;
using UnityEngine;

public class PlayerHealManager : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.T))
		{
			this.TakeDamage(10);
		}
	}

	public void SetHealth(int numberHP)
	{
		this.totalHP = numberHP;
		this.currentHP = numberHP;
		HealthBarManager.current.SetHP(numberHP);
	}

	public void TakeDamage(int damage)
	{
		if (this.currentHP > 0)
		{
			this.currentHP -= damage;
			if (this.currentHP < 0)
			{
				this.currentHP = 0;
			}
			HealthBarManager.current.SetBar(this.currentHP);
			if (this.currentHP == 0)
			{
				UnityEngine.Debug.Log("Player Die");
			}
		}
	}

	public int currentHP;

	[SerializeField]
	private int totalHP;
}
