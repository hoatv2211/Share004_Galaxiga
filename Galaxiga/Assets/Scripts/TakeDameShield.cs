using System;
using UnityEngine;

public class TakeDameShield : MonoBehaviour
{
	private void Start()
	{
	}

	public void TakeHealthShield(int heath)
	{
		int num = 0;
		for (int i = 0; i < this.listLock.Length; i++)
		{
			if (this.listLock[i].GetComponent<HealthLock>().CurrentHP <= 0)
			{
				num++;
			}
		}
		if (num == 3)
		{
			base.gameObject.SetActive(false);
			this.boss.SendMessage("Stun");
		}
	}

	public void ResetListLock()
	{
		this.healthShield = 3;
		for (int i = 0; i < this.listLock.Length; i++)
		{
			this.listLock[i].GetComponent<HealthLock>().ResetObj();
		}
	}

	public int healthShield = 3;

	public GameObject boss;

	public GameObject[] listLock;
}
