using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_WheelJoint2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(WheelJoint2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			WheelJoint2D wheelJoint2D = (WheelJoint2D)value;
			writer.WriteProperty<JointSuspension2D>("suspension", wheelJoint2D.suspension);
			writer.WriteProperty<bool>("useMotor", wheelJoint2D.useMotor);
			writer.WriteProperty<JointMotor2D>("motor", wheelJoint2D.motor);
			writer.WriteProperty<Vector2>("anchor", wheelJoint2D.anchor);
			writer.WriteProperty<Vector2>("connectedAnchor", wheelJoint2D.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", wheelJoint2D.autoConfigureConnectedAnchor);
			writer.WriteProperty<Rigidbody2D>("connectedBody", wheelJoint2D.connectedBody);
			writer.WriteProperty<bool>("enableCollision", wheelJoint2D.enableCollision);
			writer.WriteProperty<float>("breakForce", wheelJoint2D.breakForce);
			writer.WriteProperty<float>("breakTorque", wheelJoint2D.breakTorque);
			writer.WriteProperty<bool>("enabled", wheelJoint2D.enabled);
			writer.WriteProperty<string>("tag", wheelJoint2D.tag);
			writer.WriteProperty<string>("name", wheelJoint2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", wheelJoint2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			WheelJoint2D wheelJoint2D = SaveGameType.CreateComponent<WheelJoint2D>();
			this.ReadInto(wheelJoint2D, reader);
			return wheelJoint2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			WheelJoint2D wheelJoint2D = (WheelJoint2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "suspension":
					wheelJoint2D.suspension = reader.ReadProperty<JointSuspension2D>();
					break;
				case "useMotor":
					wheelJoint2D.useMotor = reader.ReadProperty<bool>();
					break;
				case "motor":
					wheelJoint2D.motor = reader.ReadProperty<JointMotor2D>();
					break;
				case "anchor":
					wheelJoint2D.anchor = reader.ReadProperty<Vector2>();
					break;
				case "connectedAnchor":
					wheelJoint2D.connectedAnchor = reader.ReadProperty<Vector2>();
					break;
				case "autoConfigureConnectedAnchor":
					wheelJoint2D.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "connectedBody":
					if (wheelJoint2D.connectedBody == null)
					{
						wheelJoint2D.connectedBody = reader.ReadProperty<Rigidbody2D>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody2D>(wheelJoint2D.connectedBody);
					}
					break;
				case "enableCollision":
					wheelJoint2D.enableCollision = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					wheelJoint2D.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					wheelJoint2D.breakTorque = reader.ReadProperty<float>();
					break;
				case "enabled":
					wheelJoint2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					wheelJoint2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					wheelJoint2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					wheelJoint2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
