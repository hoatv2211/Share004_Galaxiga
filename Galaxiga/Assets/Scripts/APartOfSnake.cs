using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

public class APartOfSnake : BaseEnemy
{
	public override int CurrentHP
	{
		get
		{
			return base.CurrentHP;
		}
		set
		{
			if (this.canTakeDamage || value > base.CurrentHP)
			{
				base.CurrentHP = value;
			}
		}
	}

	public override void Restart()
	{
		base.Restart();
		this.bodySprite.sortingOrder = 100 - this.Index;
	}

	public Vector2[] CreatePath(Vector2 startPos, Vector2 firstDir)
	{
		Vector2 b = firstDir.normalized * 2f;
		Vector2 vector = startPos + b;
		List<Vector2> list = new List<Vector2>
		{
			startPos,
			vector
		};
		for (int i = 2; i < 6; i++)
		{
			float degrees = UnityEngine.Random.Range(-60f, 60f);
			Vector2 vector2 = Fu.RotateVector2(b.normalized * 2f, degrees);
			Vector2 point = vector + b;
			if (Fu.Outside(point, this.bottomLeft, this.topRight))
			{
				Vector2 vector3 = Fu.RotateVector2(b.normalized * 2f * 2f, -90f);
				Vector2 vector4 = Fu.RotateVector2(b.normalized * 2f * 2f, 90f);
				b = (((vector + vector3).magnitude >= (vector + vector4).magnitude) ? vector4 : vector3);
			}
			else
			{
				b = vector2;
			}
			vector += b;
			list.Add(vector);
		}
		return list.ToArray();
	}

	public Vector2[] CreatePath(bool changeDirection, Vector2 lastDir)
	{
		if (changeDirection)
		{
			return this.CreatePath(base.transform.position, Fu.RotateVector2(lastDir, (float)((UnityEngine.Random.value <= 0.5f) ? -45 : 45)));
		}
		return this.CreatePath(base.transform.position, lastDir);
	}

	public List<TransformData> Cache { get; private set; } = new List<TransformData>();

	public void CacheTransform()
	{
		if (Vector2.Distance(base.transform.position, this.oldPos) < 0.001f)
		{
			return;
		}
		this.oldPos = base.transform.position;
		this.Cache.Insert(0, new TransformData(base.transform));
		if (this.Cache.Count > 60)
		{
			this.Cache.RemoveRange(60, this.Cache.Count - 60);
		}
	}

	public void RestoreTransform(List<TransformData> beforeCache, Transform trans)
	{
		for (int i = 1; i < beforeCache.Count; i++)
		{
			if (Time.time - beforeCache[i].T > this.timeBetween2Part)
			{
				float t = (Time.time - beforeCache[i].T - this.timeBetween2Part) / (beforeCache[i - 1].T - beforeCache[i].T);
				trans.position = Vector3.Lerp(beforeCache[i].P, beforeCache[i - 1].P, t);
				trans.rotation = Quaternion.Lerp(beforeCache[i].R, beforeCache[i - 1].R, t);
				return;
			}
		}
	}

	protected override void ClearData()
	{
		this.beforeMe = (this.afterMe = null);
		this.moveTween.Kill(false);
		base.ClearData();
	}

	public void UpdateTransform()
	{
		if (this.beforeMe != null && this.beforeMe.State != EnemyState.InPool)
		{
			this.RestoreTransform(this.beforeMe.Cache, base.transform);
		}
		this.CacheTransform();
	}

	public IEnumerator CreatePathAndMove()
	{
		Vector2[] path = null;
		Vector2 dir = Vector2.down;
		if (this.beforeMe == null)
		{
			path = this.CreatePath(base.transform.position, Vector2.down);
		}
		else
		{
			if (this.Cache.Count > 2)
			{
				dir = this.Cache[0].P - this.Cache[1].P;
			}
			path = this.CreatePath(true, dir);
		}
		while (this.State != EnemyState.InPool)
		{
			this.moveTween = this.BaseMoveCurve(this.speed, Ease.Linear, PathMode.TopDown2D, path);
			yield return this.moveTween.WaitForCompletion();
			if (this.Cache.Count > 2)
			{
				dir = this.Cache[0].P - this.Cache[1].P;
			}
			path = this.CreatePath(false, dir);
		}
		yield break;
	}

	public void OpenEye()
	{
		base.StartCoroutine(this.CreatePathAndMove());
		this.bodyAnim.SetBool("isHead", true);
		UnityEngine.Object.Instantiate<Transform>(this.flame, base.transform);
	}

	public void GrowTail()
	{
		this.tail.Grow();
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		if (this.beforeMe != null && this.beforeMe.State != EnemyState.InPool)
		{
			this.beforeMe.GrowTail();
		}
		if (this.afterMe != null && this.afterMe.State != EnemyState.InPool)
		{
			this.afterMe.OpenEye();
		}
		base.Die(type);
	}

	private const float part_length = 2f;

	private const float maxAngle = 60f;

	private const int path_length = 6;

	private const int sortingOrderMax = 100;

	public Vector2 bottomLeft;

	public Vector2 topRight;

	public SpriteRenderer bodySprite;

	public SnakeTail tail;

	public Animator bodyAnim;

	public Transform flame;

	[DisplayAsString]
	public bool canTakeDamage;

	[EnemyField]
	public float speed;

	[EnemyField]
	public float timeBetween2Part;

	[HideInInspector]
	public APartOfSnake beforeMe;

	[HideInInspector]
	public APartOfSnake afterMe;

	public Tweener moveTween;

	private Vector2 oldPos;

	private const int CACHE_NUMBER = 60;
}
