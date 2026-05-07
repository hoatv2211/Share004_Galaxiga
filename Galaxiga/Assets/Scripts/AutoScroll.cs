using System;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
	private void Update()
	{
		if (this.isVerticalScroll)
		{
			this.scrollRect.verticalNormalizedPosition += Time.deltaTime * this.speedScroll;
		}
		else
		{
			this.scrollRect.horizontalNormalizedPosition += Time.deltaTime * this.speedScroll;
		}
	}

	public ScrollRect scrollRect;

	public float speedScroll = 0.1f;

	public bool isVerticalScroll = true;
}
