using System;
using UnityEngine;

namespace OSNet
{
	public class PersistentSingleton<T> : MonoBehaviour where T : Component
	{
		public static T Instance
		{
			get
			{
				if (PersistentSingleton<T>._instance == null)
				{
					PersistentSingleton<T>._instance = UnityEngine.Object.FindObjectOfType<T>();
					if (PersistentSingleton<T>._instance == null)
					{
						GameObject gameObject = new GameObject();
						PersistentSingleton<T>._instance = gameObject.AddComponent<T>();
					}
				}
				return PersistentSingleton<T>._instance;
			}
		}

		protected virtual void Awake()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (PersistentSingleton<T>._instance == null)
			{
				PersistentSingleton<T>._instance = (this as T);
				UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
				this._enabled = true;
			}
			else if (this != PersistentSingleton<T>._instance)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		protected static T _instance;

		protected bool _enabled;
	}
}
