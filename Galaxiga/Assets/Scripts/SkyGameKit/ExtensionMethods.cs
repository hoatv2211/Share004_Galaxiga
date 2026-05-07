using System;
using System.Collections;
using UnityEngine;

namespace SkyGameKit
{
	public static class ExtensionMethods
	{
		public static void LookAt2D(this Transform transform, Vector2 worldPosition, float degreesOffset = 0f)
		{
            transform.rotation = Fu.LookAt2D((Vector2)transform.position - worldPosition, degreesOffset);
        }

		public static IDisposable Delay<T>(this T t, float timeSec, Action action, bool ignoreTimeScale = false) where T : MonoBehaviour
		{
			if (action == null || !t.gameObject.activeInHierarchy)
			{
				return null;
			}
			if (timeSec < 0f)
			{
				return null;
			}
			if (timeSec == 0f)
			{
				action();
				return null;
			}
			return new DisposableCoroutine(t, t.StartCoroutine(ExtensionMethods.DelayCoroutine(timeSec, action, ignoreTimeScale)));
		}

		private static IEnumerator DelayCoroutine(float timeSec, Action action, bool ignoreTimeScale = false)
		{
			if (ignoreTimeScale)
			{
				yield return new WaitForSecondsRealtime(timeSec);
			}
			else
			{
				yield return new WaitForSeconds(timeSec);
			}
			action();
			yield break;
		}

		public static IDisposable DoActionEveryTime<T>(this T t, float timeSec, int count, Action action, bool doOnStart = false, bool ignoreTimeScale = false) where T : MonoBehaviour
		{
			return t.DoActionEveryTime(timeSec, count, action, null, doOnStart, ignoreTimeScale);
		}

		public static IDisposable DoActionEveryTime<T>(this T t, float timeSec, Action action, Action compeleteAction, bool doOnStart = false, bool ignoreTimeScale = false) where T : MonoBehaviour
		{
			return t.DoActionEveryTime(timeSec, -1, action, compeleteAction, doOnStart, ignoreTimeScale);
		}

		public static IDisposable DoActionEveryTime<T>(this T t, float timeSec, Action action, bool doOnStart = false, bool ignoreTimeScale = false) where T : MonoBehaviour
		{
			return t.DoActionEveryTime(timeSec, -1, action, null, doOnStart, ignoreTimeScale);
		}

		public static IDisposable DoActionEveryTime<T>(this T t, float timeSec, int count, Action action, Action compeleteAction, bool doOnStart = false, bool ignoreTimeScale = false) where T : MonoBehaviour
		{
			if (action == null || !t.gameObject.activeInHierarchy)
			{
				return null;
			}
			if (timeSec == 0f && count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					action();
				}
				if (compeleteAction != null)
				{
					compeleteAction();
				}
				return null;
			}
			if (timeSec > 0f)
			{
				if (doOnStart)
				{
					count--;
					action();
				}
				return new DisposableCoroutine(t, t.StartCoroutine(ExtensionMethods.DoActionEveryTimeCoroutine(timeSec, count, action, compeleteAction, ignoreTimeScale)));
			}
			return null;
		}

		private static IEnumerator DoActionEveryTimeCoroutine(float timeSec, int count, Action action, Action compeleteAction, bool ignoreTimeScale = false)
		{
			while (count != 0)
			{
				if (ignoreTimeScale)
				{
					yield return new WaitForSecondsRealtime(timeSec);
				}
				else
				{
					yield return new WaitForSeconds(timeSec);
				}
				if (count > 0)
				{
					count--;
				}
				action();
			}
			if (compeleteAction != null)
			{
				compeleteAction();
			}
			yield break;
		}
	}
}
