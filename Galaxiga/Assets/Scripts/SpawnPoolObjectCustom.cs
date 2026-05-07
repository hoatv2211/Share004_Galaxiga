using System;
using System.Collections.Generic;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnPoolObjectCustom : MonoBehaviour
{
	public void CheckPoolObject(List<PrefabPool> listPrefabPool)
	{
		SpawnPoolObjectCustom.TypeObject typeObject = this.typeObject;
		if (typeObject != SpawnPoolObjectCustom.TypeObject.Plane)
		{
			if (typeObject == SpawnPoolObjectCustom.TypeObject.Drone)
			{
				int num = this.arrDrones.Length;
				if (num > 0)
				{
					List<GameContext.Drone> listDroneOwned = DataGame.Current.ListDroneOwned;
					for (int i = 0; i < this.arrDrones.Length; i++)
					{
						if (this.arrDrones[i].autoPool || listDroneOwned.Contains(this.arrDrones[i].droneID))
						{
							for (int j = 0; j < this.arrDrones[i].prefabPool.Length; j++)
							{
								listPrefabPool.Add(this.arrDrones[i].prefabPool[j]);
							}
						}
					}
				}
			}
		}
		else
		{
			int num2 = this.arrPlanes.Length;
			if (num2 > 0)
			{
				List<GameContext.Plane> listPlaneOwned = DataGame.Current.ListPlaneOwned;
				for (int k = 0; k < this.arrPlanes.Length; k++)
				{
					if (this.arrPlanes[k].autoPool || listPlaneOwned.Contains(this.arrPlanes[k].planeID))
					{
						for (int l = 0; l < this.arrPlanes[k].prefabPool.Length; l++)
						{
							listPrefabPool.Add(this.arrPlanes[k].prefabPool[l]);
						}
					}
				}
			}
		}
	}

	public SpawnPoolObjectCustom.TypeObject typeObject;

	[HideIf("typeObject", SpawnPoolObjectCustom.TypeObject.Drone, true)]
	public SpawnPoolObjectCustom.Plane[] arrPlanes;

	[HideIf("typeObject", SpawnPoolObjectCustom.TypeObject.Plane, true)]
	public SpawnPoolObjectCustom.Drone[] arrDrones;

	[Serializable]
	public class Plane
	{
		public GameContext.Plane planeID;

		public bool autoPool;

		public PrefabPool[] prefabPool;
	}

	[Serializable]
	public class Drone
	{
		public GameContext.Drone droneID;

		public bool autoPool;

		public PrefabPool[] prefabPool;
	}

	public enum TypeObject
	{
		Plane,
		Drone
	}
}
