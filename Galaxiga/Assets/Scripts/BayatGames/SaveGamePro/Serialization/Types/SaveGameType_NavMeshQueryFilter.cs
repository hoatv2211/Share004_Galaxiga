using System;
using UnityEngine.AI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_NavMeshQueryFilter : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(NavMeshQueryFilter);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			NavMeshQueryFilter navMeshQueryFilter = (NavMeshQueryFilter)value;
			writer.WriteProperty<int>("areaMask", navMeshQueryFilter.areaMask);
			writer.WriteProperty<int>("agentTypeID", navMeshQueryFilter.agentTypeID);
		}

		public override object Read(ISaveGameReader reader)
		{
			NavMeshQueryFilter navMeshQueryFilter = default(NavMeshQueryFilter);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "areaMask"))
					{
						if (text == "agentTypeID")
						{
							navMeshQueryFilter.agentTypeID = reader.ReadProperty<int>();
						}
					}
					else
					{
						navMeshQueryFilter.areaMask = reader.ReadProperty<int>();
					}
				}
			}
			return navMeshQueryFilter;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
