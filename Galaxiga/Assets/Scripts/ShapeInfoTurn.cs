using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class ShapeInfoTurn
{
	[HorizontalGroup("Split", 0.3f, 0, 0, 0)]
	[BoxGroup("Split/Name", true, false, 0)]
	public List<string> namePoint;

	[BoxGroup("Split/ListPosition1", true, false, 0)]
	public List<Vector3> points;

	[BoxGroup("Split/StAttack", true, false, 0)]
	public bool stAttack;
}
