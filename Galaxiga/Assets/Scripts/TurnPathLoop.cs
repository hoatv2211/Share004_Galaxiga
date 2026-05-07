using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using SkyGameKit;
using SWS;
using UnityEngine;

public class TurnPathLoop : SpawnByNumberExtend
{
	private void SetPathEnemy(BaseEnemy enemy)
	{
		TurnPathLoop.StylePath stylePath = this._stylePath;
		if (stylePath != TurnPathLoop.StylePath.PathLoop)
		{
			if (stylePath == TurnPathLoop.StylePath.PathToPathLoop)
			{
				if (this.pointChangePath == 0)
				{
					this.pointChangePath = this.pathEnemy.GetWaypointCount() - 1;
				}
				(enemy as EnemyGeneral).SetPathInPathCurve(this.pathEnemy, this.speed, this.pathLoop, this.speedPathLoop, this.ease, this.easeLoop, this.curve, this.curveLoop, this.pointChangePath);
			}
		}
		else
		{
			(enemy as EnemyGeneral).SetPathLoopCurve(this.pathEnemy, this.speed, this.curve, this.ease);
		}
	}

	protected override IEnumerator SpawnListEnemy()
	{
		for (int i = 0; i < this.extendEnemies.Length; i++)
		{
			for (int j = 0; j < this.extendEnemies[i].number; j++)
			{
				BaseEnemy ene = this.SpawnEnemy(this.extendEnemies[i].enemy);
				this.SetPathEnemy(ene);
				yield return new WaitForSeconds(this.timeToNextEnemy);
			}
		}
		yield break;
	}

	public TurnPathLoop.StylePath _stylePath;

	public PathManager pathEnemy;

	public float speed = 2f;

	public Ease ease = Ease.Linear;

	[ShowIf("ease", Ease.Unset, false)]
	public AnimationCurve curve;

	[ShowIf("_stylePath", TurnPathLoop.StylePath.PathToPathLoop, false)]
	public int pointChangePath;

	[ShowIf("_stylePath", TurnPathLoop.StylePath.PathToPathLoop, false)]
	public PathManager pathLoop;

	[ShowIf("_stylePath", TurnPathLoop.StylePath.PathToPathLoop, false)]
	public float speedPathLoop = 2f;

	[ShowIf("_stylePath", TurnPathLoop.StylePath.PathToPathLoop, false)]
	public Ease easeLoop = Ease.Linear;

	[ShowIf("_stylePath", TurnPathLoop.StylePath.PathToPathLoop, false)]
	[ShowIf("easeLoop", false)]
	public AnimationCurve curveLoop;

	public enum StylePath
	{
		PathLoop,
		PathToPathLoop
	}
}
