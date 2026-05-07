using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_PlatformEffector2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(PlatformEffector2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			PlatformEffector2D platformEffector2D = (PlatformEffector2D)value;
			writer.WriteProperty<bool>("useOneWay", platformEffector2D.useOneWay);
			writer.WriteProperty<bool>("useOneWayGrouping", platformEffector2D.useOneWayGrouping);
			writer.WriteProperty<bool>("useSideFriction", platformEffector2D.useSideFriction);
			writer.WriteProperty<bool>("useSideBounce", platformEffector2D.useSideBounce);
			writer.WriteProperty<float>("surfaceArc", platformEffector2D.surfaceArc);
			writer.WriteProperty<float>("sideArc", platformEffector2D.sideArc);
			writer.WriteProperty<float>("rotationalOffset", platformEffector2D.rotationalOffset);
			writer.WriteProperty<bool>("useColliderMask", platformEffector2D.useColliderMask);
			writer.WriteProperty<int>("colliderMask", platformEffector2D.colliderMask);
			writer.WriteProperty<bool>("enabled", platformEffector2D.enabled);
			writer.WriteProperty<string>("tag", platformEffector2D.tag);
			writer.WriteProperty<string>("name", platformEffector2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", platformEffector2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			PlatformEffector2D platformEffector2D = SaveGameType.CreateComponent<PlatformEffector2D>();
			this.ReadInto(platformEffector2D, reader);
			return platformEffector2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			PlatformEffector2D platformEffector2D = (PlatformEffector2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "useOneWay":
					platformEffector2D.useOneWay = reader.ReadProperty<bool>();
					break;
				case "useOneWayGrouping":
					platformEffector2D.useOneWayGrouping = reader.ReadProperty<bool>();
					break;
				case "useSideFriction":
					platformEffector2D.useSideFriction = reader.ReadProperty<bool>();
					break;
				case "useSideBounce":
					platformEffector2D.useSideBounce = reader.ReadProperty<bool>();
					break;
				case "surfaceArc":
					platformEffector2D.surfaceArc = reader.ReadProperty<float>();
					break;
				case "sideArc":
					platformEffector2D.sideArc = reader.ReadProperty<float>();
					break;
				case "rotationalOffset":
					platformEffector2D.rotationalOffset = reader.ReadProperty<float>();
					break;
				case "useColliderMask":
					platformEffector2D.useColliderMask = reader.ReadProperty<bool>();
					break;
				case "colliderMask":
					platformEffector2D.colliderMask = reader.ReadProperty<int>();
					break;
				case "enabled":
					platformEffector2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					platformEffector2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					platformEffector2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					platformEffector2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
