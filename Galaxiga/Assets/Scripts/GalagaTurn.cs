using System;
using System.Collections;
using System.Linq;
using SkyGameKit;
using UnityEngine;

public abstract class GalagaTurn : TurnManager, ITurnCanIntegrateEndless
{
	public int NumberOfEnemySelected
	{
		get
		{
			return this.numberOfEnemySelected;
		}
		set
		{
			this.numberOfEnemySelected = value;
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

	protected override int Spawn()
	{
		if (this.firstShape == null)
		{
			this.firstShape = this.GetFirstShape();
		}
		base.StartCoroutine(this.SpawnAndMove());
		if (this.timeToBeginAction > 0f)
		{
			base.StartCoroutine(this.ChoosingEnemyAndInvokeAction(this.timeToBeginAction, true));
		}
		return this.firstShape.points.Count;
	}

	protected abstract ShapeManager GetFirstShape();

	protected abstract IEnumerator SpawnAndMove();

	protected abstract IEnumerator ChoosingEnemyAndInvokeAction(float delay, bool infinityLoop = false);

	protected void ChoosingEnemyAndInvokeAction()
	{
		foreach (TweenEnemy tweenEnemy in (from TweenEnemy x in base.GetAliveEnemy()
		where x.hasAction
		orderby Fu.RandomInt
		select x).Take(this.numberOfEnemySelected))
		{
			tweenEnemy.OnChoosingMe();
		}
	}

	[Tooltip("Số enemy sẽ thực hiện Action mỗi đợi")]
	public int numberOfEnemySelected;

	[Tooltip("Khoảng thời gian giữa 2 đợi thực hiện Action")]
	public float timeToNextAction;

	[Tooltip("Sau x giây (từ khi Turn bắt đầu) thực hiện Action, Nếu x<0 thì thực hiện Action khi đã xếp hình xong, hoặc không xếp hình thì không làm gì cả")]
	public float timeToBeginAction = -1f;

	[Tooltip("Chỉ có tác dụng quyết định loại enemy, nếu null biến này sẽ tự tìm hình phù hợp")]
	public ShapeManager firstShape;

	public TweenEnemy[] enemies;
}
