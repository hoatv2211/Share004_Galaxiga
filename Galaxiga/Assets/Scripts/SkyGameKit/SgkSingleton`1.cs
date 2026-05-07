using System;
using UnityEngine;

namespace SkyGameKit
{
	public class SgkSingleton<T> : MonoBehaviour where T : Component
	{
		public static T Instance
		{
			get
			{
				if (SgkSingleton<T>._instance == null)
				{
					SgkSingleton<T>._instance = UnityEngine.Object.FindObjectOfType<T>();
				}
				return SgkSingleton<T>._instance;
			}
		}

		protected virtual void Awake()
		{
			SgkSingleton<T>._instance = (this as T);
		}

		protected static T _instance;
	}
}
