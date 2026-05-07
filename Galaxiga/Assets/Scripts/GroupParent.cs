using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using SkyGameKit;
using UnityEngine;

public class GroupParent : MonoBehaviour
{
	private void Scale(Vector2 ratio)
	{
		foreach (KeyValuePair<TweenEnemy, Vector2> keyValuePair in this.childAndPosition)
		{
			keyValuePair.Key.transform.localPosition = ratio * keyValuePair.Value;
			if (keyValuePair.Key.State == EnemyState.InPool)
			{
				this.removals.Add(keyValuePair.Key);
			}
		}
		if (this.removals.Count > 0)
		{
			foreach (TweenEnemy key in this.removals)
			{
				this.childAndPosition.Remove(key);
			}
			this.removals.Clear();
		}
	}

	public void StartMove()
	{
		this.currentPoint = base.transform.position;
		this.sequence = DOTween.Sequence();
		int i = 0;
		while (i < this.transformations.Length)
		{
			GroupTransformation groupTransformation = this.transformations[i];
			Tweener tweener = null;
			if (groupTransformation.type == GroupTransformationType.Path)
			{
				if (!(groupTransformation.path == null))
				{
					Vector3[] array = Fu.MovePathToPoint(groupTransformation.path.GetPathPoints(false), this.currentPoint);
					this.currentPoint = array[array.Length - 1];
					tweener = base.transform.DOPath(array, groupTransformation.duration, groupTransformation.pathType, groupTransformation.pathMode, 10, null).SetLookAt(0f, null, null);
					goto IL_190;
				}
			}
			else
			{
				if (groupTransformation.type == GroupTransformationType.Move)
				{
					this.currentPoint = groupTransformation.endValue;
					tweener = base.transform.DOMove(groupTransformation.endValue, groupTransformation.duration, false);
					goto IL_190;
				}
				if (groupTransformation.type == GroupTransformationType.Rotation)
				{
					tweener = base.transform.DOLocalRotate(Vector3.forward * groupTransformation.angle, groupTransformation.duration, RotateMode.FastBeyond360);
					goto IL_190;
				}
				if (groupTransformation.type == GroupTransformationType.Scale)
				{
					tweener = DOTween.To(() => this.currentRatio, delegate(Vector2 x)
					{
						this.currentRatio = x;
					}, groupTransformation.endValue, groupTransformation.duration).OnUpdate(delegate
					{
						this.Scale(this.currentRatio);
					});
					goto IL_190;
				}
				goto IL_190;
			}
			IL_242:
			i++;
			continue;
			IL_190:
			if (tweener != null)
			{
				if (groupTransformation.ease != Ease.Unset)
				{
					tweener.SetEase(groupTransformation.ease);
				}
				else
				{
					tweener.SetEase(groupTransformation.curve);
				}
				tweener.SetLoops(groupTransformation.loops, groupTransformation.loopType);
			}
			if (groupTransformation.type == GroupTransformationType.HoldPosition)
			{
				this.sequence.AppendInterval(groupTransformation.duration);
				goto IL_242;
			}
			if (groupTransformation.startTime < 0f || groupTransformation.type == GroupTransformationType.Path || groupTransformation.type == GroupTransformationType.Move)
			{
				this.sequence.Append(tweener);
				goto IL_242;
			}
			this.sequence.Insert(groupTransformation.startTime, tweener);
			goto IL_242;
		}
	}

	public Dictionary<TweenEnemy, Vector2> childAndPosition = new Dictionary<TweenEnemy, Vector2>();

	public GroupTransformation[] transformations;

	private Sequence sequence;

	private Vector2 currentRatio = Vector2.one;

	private Vector2 currentPoint;

	private List<TweenEnemy> removals = new List<TweenEnemy>();
}
