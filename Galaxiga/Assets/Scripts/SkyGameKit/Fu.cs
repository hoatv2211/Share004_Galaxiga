using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using PathologicalGames;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyGameKit
{
	public static class Fu
	{
		public static bool IsPointerOverUIObject()
		{
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			pointerEventData.position = new Vector2(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
			List<RaycastResult> list = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerEventData, list);
			return list.Count > 0;
		}

		public static Transform SpawnExplosion(GameObject explosion, Vector3 pos, Quaternion rot)
		{
			if (!(explosion != null))
			{
				return null;
			}
			ParticleSystem component = explosion.GetComponent<ParticleSystem>();
			if (component != null)
			{
				return PoolManager.Pools["ExplosionPool"].Spawn(component, pos, rot).transform;
			}
			return PoolManager.Pools["ExplosionPool"].Spawn(explosion, pos, rot);
		}

		public static bool TH(params int[] a)
		{
			int num = a[0];
			for (int i = 1; i < a.Length; i++)
			{
				if (num == a[i])
				{
					return true;
				}
			}
			return false;
		}

		public static float GoTo0(float value, float speed)
		{
			speed = Math.Abs(speed);
			if (Math.Abs(value) <= speed)
			{
				return 0f;
			}
			return value + ((value <= 0f) ? speed : (-speed));
		}

		public static float GoToX(float x, float currentValue, float speed)
		{
			speed = Math.Abs(speed);
			float num = x - currentValue;
			if (Math.Abs(num) <= speed)
			{
				return x;
			}
			return currentValue + ((num >= 0f) ? speed : (-speed));
		}

		public static int RandomInt
		{
			[CompilerGenerated]
			get
			{
				return UnityEngine.Random.Range(0, 1000000);
			}
		}

		public static int[] RandomElementsArray(int k, int n)
		{
			int[] array = Enumerable.Range(0, n + 1).ToArray<int>();
			for (int i = 0; i < n; i++)
			{
				Fu.Swap<int>(ref array[UnityEngine.Random.Range(0, n)], ref array[UnityEngine.Random.Range(0, n)]);
			}
			return array.Take(k).ToArray<int>();
		}

		public static void Swap<T>(ref T lhs, ref T rhs)
		{
			T t = lhs;
			lhs = rhs;
			rhs = t;
		}

		public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
		{
			T value = list[indexA];
			list[indexA] = list[indexB];
			list[indexB] = value;
			return list;
		}

		public static bool Outside(Vector2 point, Vector2 bottomLeft, Vector2 topRight)
		{
			return point.x > topRight.x || point.x < bottomLeft.x || point.y > topRight.y || point.y < bottomLeft.y;
		}

		public static Vector2 RotateVector2(Vector2 v, float degrees)
		{
			float num = Mathf.Sin(degrees * 0.0174532924f);
			float num2 = Mathf.Cos(degrees * 0.0174532924f);
			float x = v.x;
			float y = v.y;
			v.x = num2 * x - num * y;
			v.y = num * x + num2 * y;
			return v;
		}

		public static Quaternion LookAt2D(Vector2 worldPosition, float degreesOffset = 0f)
		{
			float num = Mathf.Atan2(worldPosition.y, worldPosition.x) * 57.29578f;
			return Quaternion.Euler(0f, 0f, num - 90f - degreesOffset);
		}

		public static bool IsNullOrEmpty(Array array)
		{
			return array == null || array.Length == 0;
		}

		public static Vector3[] MovePathToPoint(Vector3[] waypoints, Vector3 pointPosition)
		{
			Vector3 b = pointPosition - waypoints[0];
			if (b.magnitude > 0.05f)
			{
				for (int i = 0; i < waypoints.Length; i++)
				{
					waypoints[i] += b;
				}
			}
			return waypoints;
		}
	}
}
