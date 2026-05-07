using System;
using UnityEngine;

namespace SuperScrollView
{
	public class RotateScript : MonoBehaviour
	{
		private void Update()
		{
			Vector3 localEulerAngles = base.gameObject.transform.localEulerAngles;
			localEulerAngles.z += this.speed * Time.deltaTime;
			base.gameObject.transform.localEulerAngles = localEulerAngles;
		}

		public float speed = 1f;
	}
}
