using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using SWS;
using UnityEngine;

[Serializable]
public class GroupTransformation
{
	[GUIColor(1f, 0.6f, 0.4f, 1f)]
	public GroupTransformationType type;

	[ShowIf("type", GroupTransformationType.Path, false)]
	public PathManager path;

	[ShowIf("type", GroupTransformationType.Path, false)]
	public PathType pathType = PathType.CatmullRom;

	[ShowIf("type", GroupTransformationType.Path, false)]
	public PathMode pathMode;

	[HideIf("type", GroupTransformationType.HoldPosition, false)]
	[HideIf("type", GroupTransformationType.Path, false)]
	[HideIf("type", GroupTransformationType.Rotation, false)]
	public Vector2 endValue;

	[ShowIf("type", GroupTransformationType.Rotation, false)]
	public float angle;

	[HideIf("type", GroupTransformationType.HoldPosition, false)]
	[HideIf("type", GroupTransformationType.Path, false)]
	[HideIf("type", GroupTransformationType.Move, false)]
	public float startTime = -1f;

	[HideIf("type", GroupTransformationType.HoldPosition, false)]
	public int loops = 1;

	[HideIf("type", GroupTransformationType.HoldPosition, false)]
	public LoopType loopType;

	public float duration = 1f;

	[HideIf("type", GroupTransformationType.HoldPosition, false)]
	public Ease ease = Ease.Linear;

	[ShowIf("ease", Ease.Unset, false)]
	public AnimationCurve curve;
}
