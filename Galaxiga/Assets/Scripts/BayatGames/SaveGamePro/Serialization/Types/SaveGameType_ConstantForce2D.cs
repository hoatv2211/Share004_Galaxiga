using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ConstantForce2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ConstantForce2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ConstantForce2D constantForce2D = (ConstantForce2D)value;
			writer.WriteProperty<Vector2>("force", constantForce2D.force);
			writer.WriteProperty<Vector2>("relativeForce", constantForce2D.relativeForce);
			writer.WriteProperty<float>("torque", constantForce2D.torque);
			writer.WriteProperty<bool>("enabled", constantForce2D.enabled);
			writer.WriteProperty<string>("tag", constantForce2D.tag);
			writer.WriteProperty<string>("name", constantForce2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", constantForce2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			ConstantForce2D constantForce2D = SaveGameType.CreateComponent<ConstantForce2D>();
			this.ReadInto(constantForce2D, reader);
			return constantForce2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			ConstantForce2D constantForce2D = (ConstantForce2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "force":
					constantForce2D.force = reader.ReadProperty<Vector2>();
					break;
				case "relativeForce":
					constantForce2D.relativeForce = reader.ReadProperty<Vector2>();
					break;
				case "torque":
					constantForce2D.torque = reader.ReadProperty<float>();
					break;
				case "enabled":
					constantForce2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					constantForce2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					constantForce2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					constantForce2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
