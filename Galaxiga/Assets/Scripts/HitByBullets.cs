using System;
using System.Collections;
using UnityEngine;

public class HitByBullets : MonoBehaviour
{
	protected virtual void OnTriggerEnter2D(Collider2D col)
	{
		string tag = col.tag;
		if (tag != null)
		{
			if (tag == "PlayerBullet")
			{
				if (base.gameObject.activeInHierarchy)
				{
					base.StartCoroutine(this.Flicker());
				}
			}
		}
	}

	public IEnumerator Flicker()
	{
		if (!this.isHit)
		{
			this.isHit = true;
			for (int i = 0; i < this.listSprite.Length; i++)
			{
				if (this.listSprite[i] != null)
				{
					this.listSprite[i].color = new Vector4(1f, 0.6f, 0.6f, 1f);
				}
			}
		}
		yield return new WaitForSeconds(0.3f);
		for (int j = 0; j < this.listSprite.Length; j++)
		{
			if (this.listSprite[j] != null)
			{
				this.listSprite[j].color = new Vector4(1f, 1f, 1f, 1f);
			}
		}
		this.isHit = false;
		yield break;
	}

	private bool isHit;

	[SerializeField]
	private SpriteRenderer[] listSprite;
}
