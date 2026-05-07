using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace SkyGameKit.Demo
{
	public class EnemyDemo : BaseEnemy
	{
		[EnemyAction]
		public void Move(Vector2 pivot, float r)
		{
			this.angle = 0f;
			float duration = 4f;
			DOTween.To(() => this.angle, delegate(float x)
			{
				this.angle = x;
			}, 360f, duration).OnUpdate(delegate
			{
				this.transform.position = pivot + Fu.RotateVector2(Vector2.left * r, this.angle);
			}).SetLoops(-1, LoopType.Restart);
		}

		[EnemyAction]
		public void TestAction(string param2 = "999999", int x = 10, float y = 1000f)
		{
			MonoBehaviour.print(param2);
			MonoBehaviour.print(x);
			MonoBehaviour.print(y);
		}

		private float angle;
	}
}
