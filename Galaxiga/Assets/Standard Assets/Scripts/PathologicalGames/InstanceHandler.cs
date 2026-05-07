using System;
using UnityEngine;

namespace PathologicalGames
{
	public static class InstanceHandler
	{
		internal static GameObject InstantiatePrefab(GameObject prefab, Vector3 pos, Quaternion rot)
		{
			if (InstanceHandler.InstantiateDelegates != null)
			{
				return InstanceHandler.InstantiateDelegates(prefab, pos, rot);
			}
			return UnityEngine.Object.Instantiate<GameObject>(prefab, pos, rot);
		}

		internal static void DestroyInstance(GameObject instance)
		{
			if (InstanceHandler.DestroyDelegates != null)
			{
				InstanceHandler.DestroyDelegates(instance);
			}
			else
			{
				UnityEngine.Object.Destroy(instance);
			}
		}

		public static InstanceHandler.InstantiateDelegate InstantiateDelegates;

		public static InstanceHandler.DestroyDelegate DestroyDelegates;

		public delegate GameObject InstantiateDelegate(GameObject prefab, Vector3 pos, Quaternion rot);

		public delegate void DestroyDelegate(GameObject instance);
	}
}
