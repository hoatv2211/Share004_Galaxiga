using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Examples
{
	public class MoveTransform : MonoBehaviour
	{
		private void Update()
		{
			Vector3 position = base.transform.position;
			position.x += UnityEngine.Input.GetAxis("Horizontal") * this.speed;
			position.z += UnityEngine.Input.GetAxis("Vertical") * this.speed;
			base.transform.position = position;
		}

		public float speed = 0.1f;
	}
}
