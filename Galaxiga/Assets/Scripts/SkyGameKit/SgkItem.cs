using System;
using PathologicalGames;
using SkyGameKit.QuickAccess;
using UnityEngine;

namespace SkyGameKit
{
	public class SgkItem : MonoBehaviour
	{
		public virtual void Despawn()
		{
			if (PoolManager.Pools["ItemPool"].IsSpawned(base.transform))
			{
				PoolManager.Pools["ItemPool"].Despawn(base.transform);
			}
		}

		public virtual void OnItemHitPlayer()
		{
			Player.State.PickUpItem(this);
		}

		public string itemName;

		public ItemType itemType;

		public int value;

		public string description;
	}
}
