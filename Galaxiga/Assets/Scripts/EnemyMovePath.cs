using System;
using SkyGameKit;
using SWS;
using UnityEngine;

public class EnemyMovePath : EnemyAttack
{
	private void Awake()
	{
		this._splineMove = base.GetComponent<splineMove>();
	}

	[EnemyAction(displayName = "Di Chuyển/Di Chuyển Theo Path")]
	public void SetPathStartSetPoint(PathManager pathEnemy, float speed, float speedEndPath, Vector3 pointStart)
	{
		if (pointStart != Vector3.zero)
		{
			base.transform.position = pointStart;
		}
		this.SetFieldPath(speed, pathEnemy);
	}

	[EnemyAction(displayName = "Di Chuyển/Di Chuyển Theo Path- Hết Path Di Chuyển Theo Path Loop")]
	public void SetPathInPath(PathManager pathEnemy, float speed, PathManager pathloop, float speedPathLoop)
	{
		this.SetFieldPath(speed, pathEnemy);
		this._splineMove.events[pathEnemy.GetWaypointCount() - 1].AddListener(delegate()
		{
			this._splineMove.events.Clear();
			this.SetPathLoop(pathloop, speedPathLoop);
		});
	}

	public void SetFieldPath(float speedStart, PathManager pathMove)
	{
		if (pathMove.GetComponent<BezierPathManager>() != null)
		{
			pathMove.GetComponent<BezierPathManager>().CalculatePath();
		}
		pathMove.transform.position = base.transform.position;
		this._splineMove.speed = speedStart;
		this._splineMove.SetPath(pathMove);
	}

	[EnemyAction(displayName = "Di Chuyển/Di Chuyển theo Path Loop")]
	public void SetPathLoop(PathManager pathloop, float speedpathloop)
	{
		if (pathloop.GetComponent<BezierPathManager>() != null)
		{
			pathloop.GetComponent<BezierPathManager>().CalculatePath();
		}
		pathloop.transform.position = base.transform.position;
		this._splineMove.loopType = splineMove.LoopType.loop;
		this._splineMove.speed = speedpathloop;
		this._splineMove.SetPath(pathloop);
	}

	private splineMove _splineMove;
}
