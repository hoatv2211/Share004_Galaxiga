using System;
using System.Collections.Generic;
using UnityEngine;

public class BossMultyPoint : BossGeneral, MultiPoint
{
	public List<Transform> GetList()
	{
		return this.ListPoint;
	}

	public List<Transform> ListPoint = new List<Transform>();
}
