using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AreaEffector2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AreaEffector2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AreaEffector2D areaEffector2D = (AreaEffector2D)value;
			writer.WriteProperty<float>("forceAngle", areaEffector2D.forceAngle);
			writer.WriteProperty<bool>("useGlobalAngle", areaEffector2D.useGlobalAngle);
			writer.WriteProperty<float>("forceMagnitude", areaEffector2D.forceMagnitude);
			writer.WriteProperty<float>("forceVariation", areaEffector2D.forceVariation);
			writer.WriteProperty<float>("drag", areaEffector2D.linearDamping);
			writer.WriteProperty<float>("angularDrag", areaEffector2D.angularDamping);
			writer.WriteProperty<EffectorSelection2D>("forceTarget", areaEffector2D.forceTarget);
			writer.WriteProperty<bool>("useColliderMask", areaEffector2D.useColliderMask);
			writer.WriteProperty<int>("colliderMask", areaEffector2D.colliderMask);
			writer.WriteProperty<bool>("enabled", areaEffector2D.enabled);
			writer.WriteProperty<string>("tag", areaEffector2D.tag);
			writer.WriteProperty<string>("name", areaEffector2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", areaEffector2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AreaEffector2D areaEffector2D = SaveGameType.CreateComponent<AreaEffector2D>();
			this.ReadInto(areaEffector2D, reader);
			return areaEffector2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AreaEffector2D areaEffector2D = (AreaEffector2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "forceAngle":
					areaEffector2D.forceAngle = reader.ReadProperty<float>();
					break;
				case "useGlobalAngle":
					areaEffector2D.useGlobalAngle = reader.ReadProperty<bool>();
					break;
				case "forceMagnitude":
					areaEffector2D.forceMagnitude = reader.ReadProperty<float>();
					break;
				case "forceVariation":
					areaEffector2D.forceVariation = reader.ReadProperty<float>();
					break;
				case "drag":
					areaEffector2D.linearDamping = reader.ReadProperty<float>();
					break;
				case "angularDrag":
					areaEffector2D.angularDamping = reader.ReadProperty<float>();
					break;
				case "forceTarget":
					areaEffector2D.forceTarget = reader.ReadProperty<EffectorSelection2D>();
					break;
				case "useColliderMask":
					areaEffector2D.useColliderMask = reader.ReadProperty<bool>();
					break;
				case "colliderMask":
					areaEffector2D.colliderMask = reader.ReadProperty<int>();
					break;
				case "enabled":
					areaEffector2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					areaEffector2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					areaEffector2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					areaEffector2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
