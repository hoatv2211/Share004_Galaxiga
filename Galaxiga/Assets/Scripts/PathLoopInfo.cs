using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using SWS;
using UnityEngine;

[Serializable]
public class PathLoopInfo
{
	public PathManager pathEnemy;

	public PathType pathType;

	public float speed;

	public int loop;

	public Ease ease = Ease.Linear;

	[ShowIf("ease", Ease.Unset, false)]
	public AnimationCurve curve;
}
