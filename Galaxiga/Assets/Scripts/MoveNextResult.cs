using System;
using UnityEngine;

namespace SkyGameKit
{
	public struct MoveNextResult
	{
		public MoveNextResult(Vector2 oldPos, Vector2 newPos)
		{
			this.pos = newPos;
			Vector2 vector = newPos - oldPos;
			this.rot = Mathf.Atan2(vector.y, vector.x) * 57.29578f + 90f;
		}

		public Vector2 pos;

		public float rot;
	}
}
