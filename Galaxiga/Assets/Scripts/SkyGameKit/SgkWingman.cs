using System;
using SkyGameKit.QuickAccess;
using UnityEngine;

namespace SkyGameKit
{
	public class SgkWingman : MonoBehaviour
	{
		protected virtual void Update()
		{
			Vector3 position = Player.transform.position;
			position.y -= 0.5f;
			base.transform.position = Vector3.Lerp(base.transform.position, position, Time.deltaTime * this.wingmanSpeed);
		}

		[Range(10f, 50f)]
		public float wingmanSpeed = 20f;
	}
}
