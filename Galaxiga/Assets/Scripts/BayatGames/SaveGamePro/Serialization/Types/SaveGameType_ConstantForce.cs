using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ConstantForce : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ConstantForce);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ConstantForce constantForce = (ConstantForce)value;
			writer.WriteProperty<Vector3>("force", constantForce.force);
			writer.WriteProperty<Vector3>("relativeForce", constantForce.relativeForce);
			writer.WriteProperty<Vector3>("torque", constantForce.torque);
			writer.WriteProperty<Vector3>("relativeTorque", constantForce.relativeTorque);
			writer.WriteProperty<bool>("enabled", constantForce.enabled);
			writer.WriteProperty<string>("tag", constantForce.tag);
			writer.WriteProperty<string>("name", constantForce.name);
			writer.WriteProperty<HideFlags>("hideFlags", constantForce.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			ConstantForce constantForce = SaveGameType.CreateComponent<ConstantForce>();
			this.ReadInto(constantForce, reader);
			return constantForce;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			ConstantForce constantForce = (ConstantForce)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "force":
					constantForce.force = reader.ReadProperty<Vector3>();
					break;
				case "relativeForce":
					constantForce.relativeForce = reader.ReadProperty<Vector3>();
					break;
				case "torque":
					constantForce.torque = reader.ReadProperty<Vector3>();
					break;
				case "relativeTorque":
					constantForce.relativeTorque = reader.ReadProperty<Vector3>();
					break;
				case "enabled":
					constantForce.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					constantForce.tag = reader.ReadProperty<string>();
					break;
				case "name":
					constantForce.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					constantForce.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
