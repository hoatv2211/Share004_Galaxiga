using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckListLevelABtesting : MonoBehaviour
{
	private void Awake()
	{
		UnityEngine.Debug.LogError("Value AB Testing Level " + RemoteConfigContext.value_ab_level);
		if (this.listLevel.Count >= 1)
		{
			for (int i = 0; i < this.listLevel.Count; i++)
			{
				this.listLevel[i].SetActive(false);
			}
		}
		int value_ab_level = RemoteConfigContext.value_ab_level;
		if (value_ab_level != 0)
		{
			if (value_ab_level == 1)
			{
				this.listLevel[1].SetActive(true);
			}
		}
		else
		{
			this.listLevel[0].SetActive(true);
		}
	}

	public List<GameObject> listLevel = new List<GameObject>();
}
