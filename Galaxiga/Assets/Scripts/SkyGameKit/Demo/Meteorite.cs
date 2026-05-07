using System;
using UnityEngine;

namespace SkyGameKit.Demo
{
	public class Meteorite : BaseEnemy
	{
		private void Update()
		{
			base.transform.position += Vector3.down * 5f * Time.deltaTime;
		}
	}
}
