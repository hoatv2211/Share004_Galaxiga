using System;
using System.Collections;
using System.Collections.Generic;
using SkyGameKit;
using UnityEngine;

public class GroupTurn : GalagaTurn
{
	protected override ShapeManager GetFirstShape()
	{
		return this.groupInfo.shape;
	}

	protected override int Spawn()
	{
		return base.Spawn() * this.numberOfGroup;
	}

	protected override IEnumerator SpawnAndMove()
	{
		if (this.numberOfGroup <= 0 || this.timeToNextGroup <= 0f)
		{
			yield break;
		}
		for (int i = 0; i < this.numberOfGroup; i++)
		{
			GroupParent groupParent = new GameObject("Parent").AddComponent<GroupParent>();
			groupParent.transform.parent = base.transform;
			groupParent.transform.localPosition = Vector3.zero;
			for (int j = 0; j < this.firstShape.points.Count; j++)
			{
				PointOfShape p = this.firstShape.points[j];
				BaseEnemy newEnemy = this.SpawnEnemy(this.enemies[p.enemyType]);
				newEnemy.transform.parent = groupParent.transform;
				newEnemy.transform.localPosition = p.Point;
				groupParent.childAndPosition.Add(newEnemy as TweenEnemy, newEnemy.transform.localPosition);
				yield return null;
			}
			groupParent.transformations = this.groupInfo.transformations;
			groupParent.StartMove();
			this.groupParents.Add(groupParent);
			yield return new WaitForSeconds(this.timeToNextGroup);
		}
		yield break;
	}

	protected override bool BeforeSetFieldAndAction(BaseEnemy enemy)
	{
		PointOfShape pointOfShape = this.firstShape.points[enemy.Index % this.firstShape.points.Count];
		TweenEnemy tweenEnemy = enemy as TweenEnemy;
		tweenEnemy.hasAction = pointOfShape.hasAction;
		return base.BeforeSetFieldAndAction(enemy);
	}

	protected override IEnumerator ChoosingEnemyAndInvokeAction(float delay, bool infinityLoop = false)
	{
		if (delay > 0f)
		{
			yield return new WaitForSeconds(delay);
			while (base.IsRunning)
			{
				base.ChoosingEnemyAndInvokeAction();
				yield return new WaitForSeconds(this.timeToNextAction);
			}
		}
		yield break;
	}

	public GroupInfo groupInfo;

	public int numberOfGroup = 1;

	public float timeToNextGroup = 1f;

	private List<GroupParent> groupParents = new List<GroupParent>();
}
