using System;
using DG.Tweening;
using SWS;
using UnityEngine;

public class ShapeTurnExtend : ShapeTurn
{
	private void Awake()
	{
		this._splineMove = base.GetComponent<splineMove>();
	}

	public void SetMovePath(PathManager pathMoveTurn, float speed)
	{
		this._splineMove.speed = speed;
		this._splineMove.loopType = splineMove.LoopType.loop;
		this._splineMove.SetPath(pathMoveTurn);
	}

	public void SetMoveCicrle(float duration)
	{
		base.transform.DORotate(new Vector3(0f, 0f, 360f), duration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
	}

	private splineMove _splineMove;

	[Header("MOVE_TURN")]
	public bool stMove = true;
}
