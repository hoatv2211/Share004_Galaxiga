using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

public class SpawnByNumberStart : SpawnByNumberExtend, ITurnCanIntegrateEndless
{
	public int NumberOfEnemySelected
	{
		get
		{
			return this.numberOfEnemySelected;
		}
		set
		{
			value = this.numberOfEnemySelected;
		}
	}

	public float TimeToNextAction
	{
		get
		{
			return this.timeToNextAction;
		}
		set
		{
			this.timeToNextAction = value;
		}
	}

	private void Start()
	{
		if (!Fu.IsNullOrEmpty(this.extendEnemies) && this.extendEnemies[0].enemy != null)
		{
			this.enemy = this.extendEnemies[0].enemy;
		}
		if (this.stAttack)
		{
			this.turnManager = base.GetComponent<TurnManager>();
			this.SetAttack();
		}
	}

	protected override int Spawn()
	{
		int num = base.Spawn();
		this.randomIndex = Fu.RandomElementsArray(this.numberEnemySpawItem, num - 1);
		string arg = string.Empty;
		for (int i = 0; i < this.randomIndex.Length; i++)
		{
			arg = arg + " " + this.randomIndex[i];
		}
		return num;
	}

	protected override void InitEnemy(BaseEnemy enemy)
	{
		base.InitEnemy(enemy);
		this.Delay(0.5f, delegate
		{
			for (int i = 0; i < this.randomIndex.Length; i++)
			{
				if (enemy.Index == this.randomIndex[i])
				{
					break;
				}
			}
		}, false);
	}

	protected override void EndTurn()
	{
		if (this.spawnStream != null)
		{
			this.spawnStream.Dispose();
		}
		base.EndTurn();
	}

	protected override bool BeforeSetFieldAndAction(BaseEnemy enemy)
	{
		if (this.randomPoint)
		{
			enemy.transform.position = new Vector3(UnityEngine.Random.Range(this.minX, this.maxX), base.transform.position.y, base.transform.position.z);
		}
		else
		{
			enemy.transform.position = base.transform.position;
		}
		return base.BeforeSetFieldAndAction(enemy);
	}

	public void SetAttack()
	{
		base.StartCoroutine(this.AttackRandomTurn());
	}

	private IEnumerator AttackRandomTurn()
	{
		yield return new WaitForSeconds(this.timeToNextAction);
		List<BaseEnemy> listAttack = new List<BaseEnemy>();
		listAttack = this.turnManager.GetRandomAliveEnemy(this.numberOfEnemySelected);
		for (int i = 0; i < listAttack.Count; i++)
		{
			EnemyGeneral enemyGeneral = listAttack[i] as EnemyGeneral;
			if (enemyGeneral != null)
			{
				enemyGeneral.Attack(true, 100, 0, 1f);
			}
			else
			{
				MiniBoss miniBoss = listAttack[i] as MiniBoss;
				if (miniBoss != null)
				{
					miniBoss.Attack(true, 100, 2f);
				}
			}
		}
		base.StartCoroutine(this.AttackRandomTurn());
		yield return null;
		yield break;
	}

	[Tooltip("Số lượng enemy")]
	public bool randomPoint = true;

	[ShowIf("randomPoint", true)]
	public float minX = -4f;

	[ShowIf("randomPoint", true)]
	public float maxX = 4f;

	[Header("ATTACK_TURN")]
	public bool stAttack = true;

	[ShowIf("stAttack", true)]
	public int numberOfEnemySelected = 2;

	[ShowIf("stAttack", true)]
	public float timeToNextAction = 2f;

	[Header("ITEM_TURN")]
	public bool spawItem = true;

	[ShowIf("spawItem", true)]
	public int numberEnemySpawItem = 2;

	[ShowIf("spawItem", true)]
	public int idItem;

	private TurnManager turnManager;

	private int[] randomIndex;
}
