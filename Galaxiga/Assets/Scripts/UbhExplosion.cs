using System;
using UnityEngine;

public class UbhExplosion : UbhMonoBehaviour
{
	private void OnAnimationFinish()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
