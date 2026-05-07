using System;
using UnityEngine;

namespace OneSoftCrossPromotion.Scripts.Singleton
{
	public class PersistentHumbleSingleton<T> : MonoBehaviour where T : Component
	{
		public static T Instance
		{
			get
			{
				if (PersistentHumbleSingleton<T>._instance == null)
				{
					PersistentHumbleSingleton<T>._instance = UnityEngine.Object.FindObjectOfType<T>();
					if (PersistentHumbleSingleton<T>._instance == null)
					{
						PersistentHumbleSingleton<T>._instance = new GameObject
						{
							hideFlags = HideFlags.HideAndDontSave
						}.AddComponent<T>();
					}
				}
				return PersistentHumbleSingleton<T>._instance;
			}
		}

		protected virtual void Awake()
		{
			this.InitializationTime = Time.time;
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			T[] array = UnityEngine.Object.FindObjectsOfType<T>();
			foreach (T t in array)
			{
				if (t != this && t.GetComponent<PersistentHumbleSingleton<T>>().InitializationTime < this.InitializationTime)
				{
					UnityEngine.Object.Destroy(t.gameObject);
				}
			}
			if (PersistentHumbleSingleton<T>._instance == null)
			{
				PersistentHumbleSingleton<T>._instance = (this as T);
			}
		}

		protected static T _instance;

		public float InitializationTime;
	}
}
