using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_HingeJoint2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(HingeJoint2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			HingeJoint2D hingeJoint2D = (HingeJoint2D)value;
			writer.WriteProperty<bool>("useMotor", hingeJoint2D.useMotor);
			writer.WriteProperty<bool>("useLimits", hingeJoint2D.useLimits);
			writer.WriteProperty<JointMotor2D>("motor", hingeJoint2D.motor);
			writer.WriteProperty<JointAngleLimits2D>("limits", hingeJoint2D.limits);
			writer.WriteProperty<Vector2>("anchor", hingeJoint2D.anchor);
			writer.WriteProperty<Vector2>("connectedAnchor", hingeJoint2D.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", hingeJoint2D.autoConfigureConnectedAnchor);
			writer.WriteProperty<Rigidbody2D>("connectedBody", hingeJoint2D.connectedBody);
			writer.WriteProperty<bool>("enableCollision", hingeJoint2D.enableCollision);
			writer.WriteProperty<float>("breakForce", hingeJoint2D.breakForce);
			writer.WriteProperty<float>("breakTorque", hingeJoint2D.breakTorque);
			writer.WriteProperty<bool>("enabled", hingeJoint2D.enabled);
			writer.WriteProperty<string>("tag", hingeJoint2D.tag);
			writer.WriteProperty<string>("name", hingeJoint2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", hingeJoint2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			HingeJoint2D hingeJoint2D = SaveGameType.CreateComponent<HingeJoint2D>();
			this.ReadInto(hingeJoint2D, reader);
			return hingeJoint2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			HingeJoint2D hingeJoint2D = (HingeJoint2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "useMotor":
					hingeJoint2D.useMotor = reader.ReadProperty<bool>();
					break;
				case "useLimits":
					hingeJoint2D.useLimits = reader.ReadProperty<bool>();
					break;
				case "motor":
					hingeJoint2D.motor = reader.ReadProperty<JointMotor2D>();
					break;
				case "limits":
					hingeJoint2D.limits = reader.ReadProperty<JointAngleLimits2D>();
					break;
				case "anchor":
					hingeJoint2D.anchor = reader.ReadProperty<Vector2>();
					break;
				case "connectedAnchor":
					hingeJoint2D.connectedAnchor = reader.ReadProperty<Vector2>();
					break;
				case "autoConfigureConnectedAnchor":
					hingeJoint2D.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "connectedBody":
					if (hingeJoint2D.connectedBody == null)
					{
						hingeJoint2D.connectedBody = reader.ReadProperty<Rigidbody2D>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody2D>(hingeJoint2D.connectedBody);
					}
					break;
				case "enableCollision":
					hingeJoint2D.enableCollision = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					hingeJoint2D.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					hingeJoint2D.breakTorque = reader.ReadProperty<float>();
					break;
				case "enabled":
					hingeJoint2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					hingeJoint2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					hingeJoint2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					hingeJoint2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
