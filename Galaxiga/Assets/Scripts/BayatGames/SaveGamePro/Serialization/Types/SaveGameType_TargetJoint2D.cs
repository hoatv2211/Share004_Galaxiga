using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_TargetJoint2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(TargetJoint2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			TargetJoint2D targetJoint2D = (TargetJoint2D)value;
			writer.WriteProperty<Vector2>("anchor", targetJoint2D.anchor);
			writer.WriteProperty<Vector2>("target", targetJoint2D.target);
			writer.WriteProperty<bool>("autoConfigureTarget", targetJoint2D.autoConfigureTarget);
			writer.WriteProperty<float>("maxForce", targetJoint2D.maxForce);
			writer.WriteProperty<float>("dampingRatio", targetJoint2D.dampingRatio);
			writer.WriteProperty<float>("frequency", targetJoint2D.frequency);
			writer.WriteProperty<Rigidbody2D>("connectedBody", targetJoint2D.connectedBody);
			writer.WriteProperty<bool>("enableCollision", targetJoint2D.enableCollision);
			writer.WriteProperty<float>("breakForce", targetJoint2D.breakForce);
			writer.WriteProperty<float>("breakTorque", targetJoint2D.breakTorque);
			writer.WriteProperty<bool>("enabled", targetJoint2D.enabled);
			writer.WriteProperty<string>("tag", targetJoint2D.tag);
			writer.WriteProperty<string>("name", targetJoint2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", targetJoint2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			TargetJoint2D targetJoint2D = SaveGameType.CreateComponent<TargetJoint2D>();
			this.ReadInto(targetJoint2D, reader);
			return targetJoint2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			TargetJoint2D targetJoint2D = (TargetJoint2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "anchor":
					targetJoint2D.anchor = reader.ReadProperty<Vector2>();
					break;
				case "target":
					targetJoint2D.target = reader.ReadProperty<Vector2>();
					break;
				case "autoConfigureTarget":
					targetJoint2D.autoConfigureTarget = reader.ReadProperty<bool>();
					break;
				case "maxForce":
					targetJoint2D.maxForce = reader.ReadProperty<float>();
					break;
				case "dampingRatio":
					targetJoint2D.dampingRatio = reader.ReadProperty<float>();
					break;
				case "frequency":
					targetJoint2D.frequency = reader.ReadProperty<float>();
					break;
				case "connectedBody":
					if (targetJoint2D.connectedBody == null)
					{
						targetJoint2D.connectedBody = reader.ReadProperty<Rigidbody2D>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody2D>(targetJoint2D.connectedBody);
					}
					break;
				case "enableCollision":
					targetJoint2D.enableCollision = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					targetJoint2D.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					targetJoint2D.breakTorque = reader.ReadProperty<float>();
					break;
				case "enabled":
					targetJoint2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					targetJoint2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					targetJoint2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					targetJoint2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
