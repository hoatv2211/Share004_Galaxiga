using System;
using UnityEngine;
using UnityEngine.AI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_NavMeshDataInstance : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(NavMeshDataInstance);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			NavMeshDataInstance navMeshDataInstance = (NavMeshDataInstance)value;
			writer.WriteProperty<string>("ownerType", navMeshDataInstance.owner.GetType().AssemblyQualifiedName);
			writer.WriteProperty<UnityEngine.Object>("owner", navMeshDataInstance.owner);
		}

		public override object Read(ISaveGameReader reader)
		{
			NavMeshDataInstance navMeshDataInstance = default(NavMeshDataInstance);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (text == "owner")
					{
						Type type = Type.GetType(reader.ReadProperty<string>());
						navMeshDataInstance.owner = (UnityEngine.Object)reader.ReadProperty(type);
					}
				}
			}
			return navMeshDataInstance;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
