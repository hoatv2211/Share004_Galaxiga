using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public abstract class SaveGameType
	{
		public abstract Type AssociatedType { get; }

		public abstract void Write(object value, ISaveGameWriter writer);

		public virtual object Read(ISaveGameReader reader)
		{
			UnityEngine.Debug.LogWarningFormat("SaveGamePro: The {0} does not have a default reading method, use the LoadInto or ReadInto methods.", new object[]
			{
				this.AssociatedType.Name
			});
			return null;
		}

		public virtual void ReadInto(object value, ISaveGameReader reader)
		{
			UnityEngine.Debug.LogWarningFormat("SaveGamePro: The {0} does not have a ReadInto method, use the default Load or Read methods.", new object[]
			{
				this.AssociatedType.Name
			});
		}

		public static T CreateComponent<T>() where T : Component
		{
			return (T)((object)SaveGameType.CreateComponent(typeof(T)));
		}

		public static Component CreateComponent(Type type)
		{
			GameObject gameObject = new GameObject(type.Name);
			Component component = gameObject.GetComponent(type);
			if (component == null)
			{
				component = gameObject.AddComponent(type);
			}
			return component;
		}
	}
}
