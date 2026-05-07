using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using SkyGameKit;
using SWS;
using UnityEngine;

[Serializable]
public class ShapeTransformation
{
	private bool ShapeIsNull()
	{
		return this.shape == null;
	}

	private bool PathIsNull()
	{
		return this.transformPath == null;
	}

	private bool HoldDurationGreater0()
	{
		return this.holdDuration > 0f;
	}

	public string ShapeName
	{
		get
		{
			return (!(this.shape != null)) ? string.Empty : this.shape.name;
		}
	}

	[GUIColor(1f, 0.6f, 0.4f, 1f)]
	public ShapeManager shape;

	[HideIf("ShapeIsNull", false)]
	public Vector2 offset;

	[HideIf("ShapeIsNull", false)]
	public Vector2 scale = Vector2.one;

	[HideIf("ShapeIsNull", false)]
	public float rotation;

	public PathManager transformPath;

	[HideIf("PathIsNull", false)]
	public bool movePathToEnemy = true;

	[HideIf("PathIsNull", false)]
	public PathType pathType = PathType.CatmullRom;

	[HideIf("PathIsNull", false)]
	public PathMode pathMode;

	public Ease ease = Ease.Linear;

	[ShowIf("ease", Ease.Unset, false)]
	public AnimationCurve curve;

	public float speedOrDuration = 5f;

	public bool isSpeed = true;

	[Tooltip("Khoảng thời gian đứng chờ sau khi xếp hình xong, nếu <=0 thì sẽ ngay lập tức chuyển sang hình mới mà không chờ enemy khác")]
	public float holdDuration = 1f;

	[Tooltip("Khoảng chênh lệch khi các enemy di chuyển, nếu <=0 thì sẽ di chuyển cùng nhau")]
	[ShowIf("HoldDurationGreater0", false)]
	public float together = 0.25f;
}
