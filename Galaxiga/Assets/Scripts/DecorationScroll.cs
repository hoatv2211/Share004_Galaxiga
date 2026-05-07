using System;
using UnityEngine;

public class DecorationScroll : MonoBehaviour
{
	private void OnEnable()
	{
		foreach (DecorationInfo decorationInfo in this.decorList)
		{
			if (!decorationInfo.FollowBackgroundIsNull)
			{
				decorationInfo.speed = decorationInfo.followBackground.GetSpeed();
			}
		}
	}

	private void Update()
	{
		foreach (DecorationInfo decorationInfo in this.decorList)
		{
			decorationInfo.transform.position += Vector3.down * decorationInfo.speed * Time.deltaTime;
			if (decorationInfo.transform.position.y < -this.screenZone)
			{
				Vector3 position = decorationInfo.transform.position;
				if (decorationInfo.randomX)
				{
					position.x = UnityEngine.Random.Range(-4f, 4f);
				}
				if (decorationInfo.randomY)
				{
					position.y = UnityEngine.Random.Range(this.screenZone, decorationInfo.maxRandomY);
				}
				else
				{
					position.y += this.screenZone * 2f;
				}
				decorationInfo.transform.position = position;
			}
		}
	}

	public DecorationInfo[] decorList;

	public float screenZone = 15f;
}
