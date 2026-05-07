using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DG.Tweening;
using SkyGameKit;
using UnityEngine;

public abstract class TweenEnemy : BaseEnemy
{
	protected virtual void Update()
	{
        if (lockRotation)
        {
            if (point.magnitude > 100f)
            {
                base.transform.rotation = Fu.LookAt2D(Vector2.down, degrees);
            }
            else
            {
                base.transform.rotation = Fu.LookAt2D((Vector2)base.transform.position - point, degrees);
            }
        }
    }

	public virtual IEnumerator DoTransformations()
	{
		if (this.delayTransform > 0f)
		{
			yield return new WaitForSeconds(this.delayTransform);
		}
		ShapeTransformation t;
		while (this.transformations.Count > 0)
		{
			t = this.transformations.Dequeue();
			Vector2[] points = t.shape.points.Select(delegate(PointOfShape x)
			{
				Vector2 vector = x.Point;
				vector.x *= t.scale.x;
				vector.y *= t.scale.y;
				vector = Fu.RotateVector2(vector, t.rotation);
				return vector + t.offset;
			}).ToArray<Vector2>();
			int pointIndex = 0;
			if (base.MotherTurn.TotalEnemy <= points.Length)
			{
				pointIndex = this.Index;
			}
			else
			{
				if (this.whenPointLessThanEnemy == WhenPointLessThanEnemy.DontMove)
				{
					continue;
				}
				if (this.whenPointLessThanEnemy == WhenPointLessThanEnemy.M_111_222_33)
				{
					pointIndex = this.Index / Mathf.CeilToInt((float)base.MotherTurn.TotalEnemy / (float)points.Length);
				}
				else if (this.whenPointLessThanEnemy == WhenPointLessThanEnemy.M_123_123_12)
				{
					pointIndex = this.Index % points.Length;
				}
			}
			if (t.shape != null)
			{
				if (t.transformPath != null)
				{
					List<Vector3> list;
					if (t.movePathToEnemy)
					{
						list = Fu.MovePathToPoint(t.transformPath.GetPathPoints(false), base.transform.position).ToList<Vector3>();
					}
					else
					{
						list = new List<Vector3>
						{
							base.transform.position
						};
						list.AddRange(t.transformPath.GetPathPoints(false));
					}
					list.Add(points[pointIndex]);
					this.tweener = base.transform.DOPath(list.ToArray(), t.speedOrDuration, t.pathType, t.pathMode, 10, null).SetLookAt(0f, null, null);
				}
				else
				{
					this.tweener = base.transform.DOMove(points[pointIndex], t.speedOrDuration, false);
				}
			}
			else if (t.transformPath != null)
			{
				Vector3[] path = Fu.MovePathToPoint(t.transformPath.GetPathPoints(false), base.transform.position);
				this.tweener = base.transform.DOPath(path, t.speedOrDuration, t.pathType, t.pathMode, 10, null).SetLookAt(0f, null, null);
			}
			if (this.tweener != null)
			{
				this.tweener.SetSpeedBased(t.isSpeed);
				if (t.ease != Ease.Unset)
				{
					this.tweener.SetEase(t.ease);
				}
				else
				{
					this.tweener.SetEase(t.curve);
				}
			}
			yield return this.tweener.WaitForCompletion();
			if (t.transformPath != null && t.pathMode == PathMode.TopDown2D)
			{
				this.tweener = base.transform.DORotate(Vector3.back * 90f, 0.1f, RotateMode.Fast);
				yield return this.tweener.WaitForCompletion();
			}
		}
		if (this.OnAllTransformComplete != null)
		{
			this.OnAllTransformComplete(this);
		}
		yield break;
	}

	public override void SetAction(Dictionary<FieldInfo, List<EnemyFinalEvent>> finalEvents)
	{
		if (this.hasAction)
		{
			base.SetAction(finalEvents);
		}
	}

	protected override void ClearData()
	{
		base.ClearData();
		this.hasAction = true;
		if (this.OnAllTransformComplete != null)
		{
			this.OnAllTransformComplete(this);
		}
		this.OnAllTransformComplete = null;
		this.transformations.Clear();
		this.tweener.Kill(false);
	}

	public abstract void OnTransformShapeComplete(int shapeIndex, string shapeName);

	public abstract void OnHoldPositionStart(int shapeIndex, string shapeName);

	public abstract void OnHoldPositionEnd(int shapeIndex, string shapeName);

	public abstract void OnChoosingMe();

	public bool hasAction = true;

	[HideInInspector]
	public int numberOfTransformations;

	public Queue<ShapeTransformation> transformations = new Queue<ShapeTransformation>();

	public Action<TweenEnemy> OnAllTransformComplete;

	[HideInInspector]
	public float delayTransform;

	[HideInInspector]
	public WhenPointLessThanEnemy whenPointLessThanEnemy;

	public Tweener tweener;

	[EnemyField]
	public bool lockRotation;

	[EnemyField]
	public float degrees;

	[EnemyField]
	public Vector2 point;
}
