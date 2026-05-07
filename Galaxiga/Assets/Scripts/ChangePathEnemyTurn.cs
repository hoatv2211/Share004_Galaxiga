using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class ChangePathEnemyTurn : SerializedMonoBehaviour
{
	private void Start()
	{
		base.Invoke("StartTurn", 10f);
	}

	private void Update()
	{
		if (this.startTurn)
		{
			this.SetStateEnm();
			if (this.isShot)
			{
				this.isShot = false;
				if (!this.st3Enemy)
				{
					this.CheckObj1();
				}
				else
				{
					this.CheckObj2();
				}
			}
		}
	}

	private void SetStateEnm()
	{
		if (Time.time > this.timeFire)
		{
			this.isShot = true;
			this.timeFire = Time.time + this.timeRate;
		}
	}

	private void CheckObj1()
	{
	}

	private void CheckObj2()
	{
	}

	private void StartTurn()
	{
		this.startTurn = true;
	}

	public float timeRate = 2f;

	private bool isShot;

	private float timeFire;

	private bool startTurn;

	public bool isLeft = true;

	public bool st3Enemy;
}
