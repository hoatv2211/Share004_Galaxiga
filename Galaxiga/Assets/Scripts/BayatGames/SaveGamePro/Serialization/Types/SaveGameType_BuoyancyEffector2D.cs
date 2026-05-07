using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_BuoyancyEffector2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(BuoyancyEffector2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			BuoyancyEffector2D buoyancyEffector2D = (BuoyancyEffector2D)value;
			writer.WriteProperty<float>("surfaceLevel", buoyancyEffector2D.surfaceLevel);
			writer.WriteProperty<float>("density", buoyancyEffector2D.density);
			writer.WriteProperty<float>("linearDrag", buoyancyEffector2D.linearDamping);
			writer.WriteProperty<float>("angularDrag", buoyancyEffector2D.angularDamping);
			writer.WriteProperty<float>("flowAngle", buoyancyEffector2D.flowAngle);
			writer.WriteProperty<float>("flowMagnitude", buoyancyEffector2D.flowMagnitude);
			writer.WriteProperty<float>("flowVariation", buoyancyEffector2D.flowVariation);
			writer.WriteProperty<bool>("useColliderMask", buoyancyEffector2D.useColliderMask);
			writer.WriteProperty<int>("colliderMask", buoyancyEffector2D.colliderMask);
			writer.WriteProperty<bool>("enabled", buoyancyEffector2D.enabled);
			writer.WriteProperty<string>("tag", buoyancyEffector2D.tag);
			writer.WriteProperty<string>("name", buoyancyEffector2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", buoyancyEffector2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			BuoyancyEffector2D buoyancyEffector2D = SaveGameType.CreateComponent<BuoyancyEffector2D>();
			this.ReadInto(buoyancyEffector2D, reader);
			return buoyancyEffector2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			BuoyancyEffector2D buoyancyEffector2D = (BuoyancyEffector2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "surfaceLevel":
					buoyancyEffector2D.surfaceLevel = reader.ReadProperty<float>();
					break;
				case "density":
					buoyancyEffector2D.density = reader.ReadProperty<float>();
					break;
				case "linearDrag":
					buoyancyEffector2D.linearDamping = reader.ReadProperty<float>();
					break;
				case "angularDrag":
					buoyancyEffector2D.angularDamping = reader.ReadProperty<float>();
					break;
				case "flowAngle":
					buoyancyEffector2D.flowAngle = reader.ReadProperty<float>();
					break;
				case "flowMagnitude":
					buoyancyEffector2D.flowMagnitude = reader.ReadProperty<float>();
					break;
				case "flowVariation":
					buoyancyEffector2D.flowVariation = reader.ReadProperty<float>();
					break;
				case "useColliderMask":
					buoyancyEffector2D.useColliderMask = reader.ReadProperty<bool>();
					break;
				case "colliderMask":
					buoyancyEffector2D.colliderMask = reader.ReadProperty<int>();
					break;
				case "enabled":
					buoyancyEffector2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					buoyancyEffector2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					buoyancyEffector2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					buoyancyEffector2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
