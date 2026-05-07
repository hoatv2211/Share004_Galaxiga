using System;
using DG.Tweening;
using UnityEngine;

public class ResetBullet : MonoBehaviour
{
	private void OnBecameVisible()
	{
		if (!this.isBulletDroneNighturge)
		{
			DOTween.Restart(base.gameObject, true, -1f);
			base.transform.DORestart(true);
			base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		else
		{
			base.gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
			DOTween.Restart(base.gameObject, true, -1f);
			base.transform.DORestart(true);
		}
	}

	public bool isBulletDroneNighturge;
}
