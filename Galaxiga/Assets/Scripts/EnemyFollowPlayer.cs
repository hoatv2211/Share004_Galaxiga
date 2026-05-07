using System;
using SkyGameKit;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
	private void Update()
	{
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, Fu.LookAt2D(base.transform.position - PlaneIngameManager.current.CurrentTransformPlayer.position, 0f), this.speedFollow * Time.deltaTime);
	}

	public float speedFollow = 2f;
}
