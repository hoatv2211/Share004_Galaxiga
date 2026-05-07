using System;
using UnityEngine;
using UnityEngine.AI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_NavMeshLinkData : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(NavMeshLinkData);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			NavMeshLinkData navMeshLinkData = (NavMeshLinkData)value;
			writer.WriteProperty<Vector3>("startPosition", navMeshLinkData.startPosition);
			writer.WriteProperty<Vector3>("endPosition", navMeshLinkData.endPosition);
			writer.WriteProperty<float>("costModifier", navMeshLinkData.costModifier);
			writer.WriteProperty<bool>("bidirectional", navMeshLinkData.bidirectional);
			writer.WriteProperty<float>("width", navMeshLinkData.width);
			writer.WriteProperty<int>("area", navMeshLinkData.area);
			writer.WriteProperty<int>("agentTypeID", navMeshLinkData.agentTypeID);
		}

		public override object Read(ISaveGameReader reader)
		{
			NavMeshLinkData navMeshLinkData = default(NavMeshLinkData);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "startPosition":
					navMeshLinkData.startPosition = reader.ReadProperty<Vector3>();
					break;
				case "endPosition":
					navMeshLinkData.endPosition = reader.ReadProperty<Vector3>();
					break;
				case "costModifier":
					navMeshLinkData.costModifier = reader.ReadProperty<float>();
					break;
				case "bidirectional":
					navMeshLinkData.bidirectional = reader.ReadProperty<bool>();
					break;
				case "width":
					navMeshLinkData.width = reader.ReadProperty<float>();
					break;
				case "area":
					navMeshLinkData.area = reader.ReadProperty<int>();
					break;
				case "agentTypeID":
					navMeshLinkData.agentTypeID = reader.ReadProperty<int>();
					break;
				}
			}
			return navMeshLinkData;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
