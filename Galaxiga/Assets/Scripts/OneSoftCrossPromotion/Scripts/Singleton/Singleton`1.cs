using System;
using UnityEngine;

namespace OneSoftCrossPromotion.Scripts.Singleton
{
	public class Singleton<T> : MonoBehaviour where T : Component
	{
		public static T Instance
		{
			get
			{
				if (Singleton<T>._instance == null)
				{
					Singleton<T>._instance = UnityEngine.Object.FindObjectOfType<T>();
					if (Singleton<T>._instance == null)
					{
						GameObject gameObject = new GameObject();
						Singleton<T>._instance = gameObject.AddComponent<T>();
					}
				}
				return Singleton<T>._instance;
			}
		}

		protected virtual void Awake()
		{
			Singleton<T>._instance = (this as T);
		}

		protected static T _instance;
	}
}
