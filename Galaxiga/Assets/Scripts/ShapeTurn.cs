using System;
using System.Collections;
using System.Collections.Generic;
using SkyGameKit;
using UnityEngine;

public class ShapeTurn : GalagaTurn
{
	protected override ShapeManager GetFirstShape()
	{
		return this.transformations[0].shape;
	}

	protected override IEnumerator SpawnAndMove()
	{
		for (int i = 0; i < this.firstShape.points.Count; i++)
		{
			PointOfShape p = this.firstShape.points[this.enemyIndex];
			this.SpawnEnemy(this.enemies[p.enemyType]);
			yield return null;
		}
		yield return this.MoveEnemy(this.transformations, 1, TransformationLoopType.Restart);
		yield break;
	}

	protected virtual int GetNumberOfTransformations()
	{
		return this.transformations.Length;
	}

	protected override bool BeforeSetFieldAndAction(BaseEnemy enemy)
	{
		PointOfShape pointOfShape = this.firstShape.points[enemy.Index];
		TweenEnemy tweenEnemy = enemy as TweenEnemy;
		tweenEnemy.numberOfTransformations = this.GetNumberOfTransformations();
		tweenEnemy.hasAction = pointOfShape.hasAction;
		tweenEnemy.whenPointLessThanEnemy = this.whenPointLessThanEnemy;
		TweenEnemy tweenEnemy2 = tweenEnemy;
		tweenEnemy2.OnAllTransformComplete = (Action<TweenEnemy>)Delegate.Combine(tweenEnemy2.OnAllTransformComplete, new Action<TweenEnemy>(this.OnEnemyStop));
		return base.BeforeSetFieldAndAction(enemy);
	}

	protected virtual IEnumerator MoveEnemy(ShapeTransformation[] transformations, int loop = 1, TransformationLoopType loopType = TransformationLoopType.Restart)
	{
		if (transformations == null)
		{
			yield return null;
		}
		for (int i = 0; i < loop; i++)
		{
			for (int transIndex = 0; transIndex < transformations.Length; transIndex++)
			{
				this.previousTrans = this.currentTrans;
				if (loopType != TransformationLoopType.Yoyo)
				{
					if (loopType != TransformationLoopType.Random)
					{
						this.currentTrans = transformations[transIndex];
					}
					else
					{
						this.currentTrans = transformations[UnityEngine.Random.Range(0, 12) % transformations.Length];
					}
				}
				else if (i % 2 == 0)
				{
					this.currentTrans = transformations[transIndex];
				}
				else
				{
					this.currentTrans = transformations[transformations.Length - 1 - transIndex];
				}
				List<BaseEnemy> aliveEnemies = base.GetAliveEnemy();
				this.countTransformNotComplete = aliveEnemies.Count;
				for (int j = 0; j < aliveEnemies.Count; j++)
				{
					TweenEnemy tweenEnemy = aliveEnemies[j] as TweenEnemy;
					if (this.previousTrans != null && this.previousTrans.holdDuration > 0f)
					{
						tweenEnemy.OnHoldPositionEnd(this.countTransform - 1, this.previousTrans.shape.name);
					}
					tweenEnemy.transformations.Enqueue(this.currentTrans);
					tweenEnemy.delayTransform = this.currentTrans.together * (float)j;
					if (this.currentTrans.holdDuration > 0f)
					{
						base.StartCoroutine(tweenEnemy.DoTransformations());
					}
				}
				if (this.currentTrans.holdDuration > 0f)
				{
					yield return this.WaitForAllTransformComplete();
					foreach (BaseEnemy baseEnemy in base.GetAliveEnemy())
					{
						TweenEnemy tweenEnemy2 = (TweenEnemy)baseEnemy;
						tweenEnemy2.OnTransformShapeComplete(this.countTransform, this.currentTrans.ShapeName);
					}
					if (this.timeToBeginAction < 0f)
					{
						yield return this.ChoosingEnemyAndInvokeAction(this.currentTrans.holdDuration, false);
					}
					else
					{
						yield return new WaitForSeconds(this.currentTrans.holdDuration);
					}
				}
				this.countTransform++;
			}
		}
		if (this.countTransform == this.GetNumberOfTransformations())
		{
			yield return this.ChoosingEnemyAndInvokeAction(this.timeToNextAction, true);
		}
		yield break;
	}

	protected virtual void OnEnemyStop(TweenEnemy enemy)
	{
		if (enemy.State != EnemyState.InPool && this.currentTrans != null)
		{
			enemy.OnHoldPositionStart(this.countTransform, this.currentTrans.ShapeName);
		}
		this.countTransformNotComplete--;
	}

	protected virtual IEnumerator WaitForAllTransformComplete()
	{
		while (this.countTransformNotComplete > 0)
		{
			yield return null;
		}
		yield return null;
		yield break;
	}

	protected override IEnumerator ChoosingEnemyAndInvokeAction(float delay, bool infinityLoop = false)
	{
		float countTime = 0f;
		if (infinityLoop && delay > 0f)
		{
			yield return new WaitForSeconds(delay);
			while (base.IsRunning)
			{
				base.ChoosingEnemyAndInvokeAction();
				yield return new WaitForSeconds(this.timeToNextAction);
			}
		}
		else if (this.timeToNextAction > 0f && this.numberOfEnemySelected > 0 && delay > 0f)
		{
			do
			{
				base.ChoosingEnemyAndInvokeAction();
				yield return new WaitForSeconds(this.timeToNextAction);
				countTime += this.timeToNextAction;
			}
			while (countTime < delay);
		}
		else
		{
			yield return null;
		}
		yield break;
	}

	public WhenPointLessThanEnemy whenPointLessThanEnemy = WhenPointLessThanEnemy.M_111_222_33;

	public ShapeTransformation[] transformations;

	protected int countTransform;

	protected ShapeTransformation previousTrans;

	protected ShapeTransformation currentTrans;

	protected int countTransformNotComplete;
}
