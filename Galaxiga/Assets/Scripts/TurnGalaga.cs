using System;
using System.Collections;
using System.Collections.Generic;
using SkyGameKit;
using UnityEngine;

public class TurnGalaga : ShapeTurn
{
	public void SetAttack()
	{
		for (int i = 0; i < this.ListActionGalaga.Count; i++)
		{
			base.StartCoroutine(this.AttackListGalaga(this.ListActionGalaga[i]));
		}
	}

	private IEnumerator AttackListGalaga(TurnGalagaInfo galagaInfo)
	{
		int add = 0;
		yield return new WaitForSeconds(galagaInfo.second);
		List<BaseEnemy> listCheck = new List<BaseEnemy>();
		if (galagaInfo.isLeft)
		{
			listCheck = this.ArrayListAliveEnemyLeft();
		}
		else
		{
			listCheck = this.ArrayListAliveEnemyRight();
		}
		List<BaseEnemy> listAction = new List<BaseEnemy>();
		for (int i = 0; i < listCheck.Count; i++)
		{
			if (!(listCheck[i] as EnemyGeneral).stattackPath && listCheck[i].Index <= galagaInfo.indexMax && listCheck[i].Index >= galagaInfo.indexMin)
			{
				listAction.Add(listCheck[i]);
				add++;
			}
			if (add >= galagaInfo.number)
			{
				break;
			}
		}
		float perC = (float)UnityEngine.Random.Range(0, 100);
		if (perC < galagaInfo.percentAction)
		{
			for (int j = 0; j < listAction.Count; j++)
			{
				(listAction[j] as EnemyGeneral).SetPathAttack2(galagaInfo);
			}
		}
		yield return null;
		base.StartCoroutine(this.AttackListGalaga(galagaInfo));
		yield break;
	}

	public List<BaseEnemy> ArrayListAliveEnemyLeft()
	{
		List<BaseEnemy> list = new List<BaseEnemy>();
		list = base.GetAliveEnemy();
		for (int i = 0; i < list.Count - 1; i++)
		{
			for (int j = i; j < list.Count; j++)
			{
				if (list[i].transform.position.x > list[j].transform.position.x || (list[i].transform.position.x == list[j].transform.position.x && list[i].transform.position.y < list[j].transform.position.y))
				{
					BaseEnemy value = list[i];
					list[i] = list[j];
					list[j] = value;
				}
			}
		}
		return list;
	}

	public List<BaseEnemy> ArrayListAliveEnemyRight()
	{
		List<BaseEnemy> list = new List<BaseEnemy>();
		list = base.GetAliveEnemy();
		for (int i = 0; i < list.Count - 1; i++)
		{
			for (int j = i; j < list.Count; j++)
			{
				if (list[i].transform.position.x < list[j].transform.position.x || (list[i].transform.position.x == list[j].transform.position.x && list[i].transform.position.y < list[j].transform.position.y))
				{
					BaseEnemy value = list[i];
					list[i] = list[j];
					list[j] = value;
				}
			}
		}
		return list;
	}

	protected override IEnumerator WaitForAllTransformComplete()
	{
		yield return base.WaitForAllTransformComplete();
		if (this.countTransform == this.GetNumberOfTransformations() - 1)
		{
			this.SetAttack();
		}
		yield break;
	}

	public List<TurnGalagaInfo> ListActionGalaga = new List<TurnGalagaInfo>();
}
