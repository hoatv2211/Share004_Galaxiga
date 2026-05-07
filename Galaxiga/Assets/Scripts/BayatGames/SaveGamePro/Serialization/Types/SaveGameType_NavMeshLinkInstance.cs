using System;
using UnityEngine;
using UnityEngine.AI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_NavMeshLinkInstance : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(NavMeshLinkInstance);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			NavMeshLinkInstance navMeshLinkInstance = (NavMeshLinkInstance)value;
			writer.WriteProperty<string>("ownerType", navMeshLinkInstance.owner.GetType().AssemblyQualifiedName);
			writer.WriteProperty<UnityEngine.Object>("owner", navMeshLinkInstance.owner);
		}

		public override object Read(ISaveGameReader reader)
		{
			NavMeshLinkInstance navMeshLinkInstance = default(NavMeshLinkInstance);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (text == "owner")
					{
						Type type = Type.GetType(reader.ReadProperty<string>());
						navMeshLinkInstance.owner = (UnityEngine.Object)reader.ReadProperty(type);
					}
				}
			}
			return navMeshLinkInstance;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
