using System;
using UnityEngine;

public class UbhSingletonMonoBehavior<T> : UbhMonoBehaviour where T : UbhMonoBehaviour
{
	public static T instance
	{
		get
		{
			if (UbhSingletonMonoBehavior<T>.m_isQuitting || !Application.isPlaying)
			{
				return (T)((object)null);
			}
			if (UbhSingletonMonoBehavior<T>.m_instance == null)
			{
				UbhSingletonMonoBehavior<T>.m_instance = UnityEngine.Object.FindObjectOfType<T>();
				if (UbhSingletonMonoBehavior<T>.m_instance == null)
				{
					UnityEngine.Debug.Log("Created " + typeof(T).Name);
					UbhSingletonMonoBehavior<T>.m_instance = new GameObject(typeof(T).Name).AddComponent<T>();
				}
			}
			return UbhSingletonMonoBehavior<T>.m_instance;
		}
	}

	protected virtual void Awake()
	{
		if (this != UbhSingletonMonoBehavior<T>.instance)
		{
			GameObject gameObject = base.gameObject;
			UnityEngine.Object.Destroy(this);
			UnityEngine.Object.Destroy(gameObject);
			return;
		}
	}

	protected virtual void OnDestroy()
	{
		if (this == UbhSingletonMonoBehavior<T>.m_instance)
		{
			UbhSingletonMonoBehavior<T>.m_instance = (T)((object)null);
		}
	}

	protected virtual void OnApplicationQuit()
	{
		UbhSingletonMonoBehavior<T>.m_isQuitting = true;
	}

	private static T m_instance;

	private static bool m_isQuitting;
}
