using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Shot Pattern/Paint Shot")]
public class UbhPaintShot : UbhBaseShot
{
	public override void Shot()
	{
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		if (this.m_bulletSpeed <= 0f || this.m_paintDataText == null)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because BulletSpeed or PaintDataText is not set.");
			yield break;
		}
		if (this.m_shooting)
		{
			yield break;
		}
		this.m_shooting = true;
		List<List<int>> paintData = this.LoadPaintData();
		if (paintData != null)
		{
			float paintStartAngle = this.m_paintCenterAngle;
			if (0 < paintData.Count)
			{
				paintStartAngle -= ((paintData[0].Count % 2 != 0) ? (this.m_betweenAngle * Mathf.Floor((float)paintData[0].Count / 2f)) : (this.m_betweenAngle * (float)paintData[0].Count / 2f + this.m_betweenAngle / 2f));
			}
			for (int lineCnt = 0; lineCnt < paintData.Count; lineCnt++)
			{
				List<int> line = paintData[lineCnt];
				if (0 < lineCnt && 0f < this.m_nextLineDelay)
				{
					base.FiredShot();
					yield return UbhUtil.WaitForSeconds(this.m_nextLineDelay);
				}
				for (int i = 0; i < line.Count; i++)
				{
					if (line[i] == 1)
					{
						UbhBullet bullet = base.GetBullet(base.transform.position, false);
						if (bullet == null)
						{
							break;
						}
						float angle = paintStartAngle + this.m_betweenAngle * (float)i;
						base.ShotBullet(bullet, this.m_bulletSpeed, angle, false, null, 0f, false, 0f, 0f);
					}
				}
			}
		}
		base.FiredShot();
		base.FinishedShot();
		yield break;
	}

	private List<List<int>> LoadPaintData()
	{
		if (string.IsNullOrEmpty(this.m_paintDataText.text))
		{
			UnityEngine.Debug.LogWarning("Cannot load paint data because PaintDataText file is empty.");
			return null;
		}
		string[] array = this.m_paintDataText.text.Split(UbhPaintShot.SPLIT_VAL, StringSplitOptions.RemoveEmptyEntries);
		List<List<int>> list = new List<List<int>>(array.Length);
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].StartsWith("#"))
			{
				list.Add(new List<int>(array[i].Length));
				for (int j = 0; j < array[i].Length; j++)
				{
					list[list.Count - 1].Add((array[i][j] != '*') ? 0 : 1);
				}
			}
		}
		list.Reverse();
		return list;
	}

	private static readonly string[] SPLIT_VAL = new string[]
	{
		"\n",
		"\r",
		"\r\n"
	};

	[Header("===== PaintShot Settings =====")]
	[FormerlySerializedAs("_PaintDataText")]
	public TextAsset m_paintDataText;

	[Range(0f, 360f)]
	[FormerlySerializedAs("_PaintCenterAngle")]
	public float m_paintCenterAngle = 180f;

	[Range(0f, 360f)]
	[FormerlySerializedAs("_BetweenAngle")]
	public float m_betweenAngle = 3f;

	[FormerlySerializedAs("_NextLineDelay")]
	public float m_nextLineDelay = 0.1f;
}
