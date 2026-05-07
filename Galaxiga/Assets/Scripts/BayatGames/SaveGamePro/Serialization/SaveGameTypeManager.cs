using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BayatGames.SaveGamePro.Serialization.Types;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization
{
	public static class SaveGameTypeManager
	{
		public static Dictionary<Type, SaveGameType> Types
		{
			get
			{
				if (SaveGameTypeManager.m_Types == null)
				{
					SaveGameTypeManager.Initialize();
				}
				return SaveGameTypeManager.m_Types;
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
            m_Types = new Dictionary<Type, SaveGameType>();
            Type type = typeof(SaveGameType);
            Type[] array = (from p in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly s) => s.GetTypes())
                            where type.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract
                            select p).ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                SaveGameType saveGameType = (SaveGameType)Activator.CreateInstance(array[i]);
                m_Types.Add(saveGameType.AssociatedType, saveGameType);
            }
        }

		public static void AddType(Type type, SaveGameType saveGameType)
		{
			if (!SaveGameTypeManager.HasType(type))
			{
				SaveGameTypeManager.Types.Add(type, saveGameType);
			}
		}

		public static void RemoveType(Type type)
		{
			SaveGameTypeManager.Types.Remove(type);
		}

		public static SaveGameType GetType(Type type)
		{
			return SaveGameTypeManager.Types[type];
		}

		public static bool HasType(Type type)
		{
			return SaveGameTypeManager.Types.ContainsKey(type);
		}

		private static Dictionary<Type, SaveGameType> m_Types;
	}
}
