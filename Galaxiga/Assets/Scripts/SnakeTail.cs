using System;
using DG.Tweening;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
	public void Grow()
	{
		this.tailAnim.enabled = false;
		base.gameObject.SetActive(true);
		base.transform.DOLocalMoveY(1f, 0.5f, false).OnComplete(delegate
		{
			this.tailAnim.enabled = true;
		});
	}

	public Animator tailAnim;
}
