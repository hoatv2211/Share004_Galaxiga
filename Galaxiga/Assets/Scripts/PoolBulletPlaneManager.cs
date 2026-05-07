using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolBulletPlaneManager : MonoBehaviour
{
	public void CheckPoolBulletPlane(List<UbhObjectPool.InitializePool> initializes)
	{
		List<GameContext.Plane> listPlaneOwned = DataGame.Current.ListPlaneOwned;
		for (int i = 0; i < this.arrPlanes.Length; i++)
		{
			if (this.arrPlanes[i].autoPool || listPlaneOwned.Contains(this.arrPlanes[i].plane))
			{
				for (int j = 0; j < this.arrPlanes[i].bulletPools.Length; j++)
				{
					initializes.Add(this.arrPlanes[i].bulletPools[j]);
				}
			}
		}
		if (this.arrDrones.Length > 0)
		{
			List<GameContext.Drone> listDroneOwned = DataGame.Current.ListDroneOwned;
			for (int k = 0; k < this.arrDrones.Length; k++)
			{
				if (this.arrDrones[k].autoPool || listDroneOwned.Contains(this.arrDrones[k].drone))
				{
					for (int l = 0; l < this.arrDrones[k].bulletPools.Length; l++)
					{
						initializes.Add(this.arrDrones[k].bulletPools[l]);
					}
				}
			}
		}
	}

	public PoolBulletPlaneManager.Plane[] arrPlanes;

	public PoolBulletPlaneManager.Drone[] arrDrones;

	public PoolBulletPlaneManager.OtherObject[] other;

	[Serializable]
	public class Plane
	{
		public GameContext.Plane plane;

		public bool autoPool;

		public UbhObjectPool.InitializePool[] bulletPools;
	}

	[Serializable]
	public class Drone
	{
		public GameContext.Drone drone;

		public bool autoPool;

		public UbhObjectPool.InitializePool[] bulletPools;
	}

	[Serializable]
	public class OtherObject
	{
		public PoolBulletPlaneManager.OtherObject.ItemType itemType;

		public bool autoPool;

		public UbhObjectPool.InitializePool[] bulletPools;

		[Serializable]
		public enum ItemType
		{
			Turret_Bullet,
			Turret_Homing,
			Turret_Laser,
			Chainsaw,
			Bullet_Item_Homing
		}
	}
}
