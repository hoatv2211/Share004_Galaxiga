using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_FrictionJoint2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(FrictionJoint2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			FrictionJoint2D frictionJoint2D = (FrictionJoint2D)value;
			writer.WriteProperty<float>("maxForce", frictionJoint2D.maxForce);
			writer.WriteProperty<float>("maxTorque", frictionJoint2D.maxTorque);
			writer.WriteProperty<Vector2>("anchor", frictionJoint2D.anchor);
			writer.WriteProperty<Vector2>("connectedAnchor", frictionJoint2D.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", frictionJoint2D.autoConfigureConnectedAnchor);
			writer.WriteProperty<Rigidbody2D>("connectedBody", frictionJoint2D.connectedBody);
			writer.WriteProperty<bool>("enableCollision", frictionJoint2D.enableCollision);
			writer.WriteProperty<float>("breakForce", frictionJoint2D.breakForce);
			writer.WriteProperty<float>("breakTorque", frictionJoint2D.breakTorque);
			writer.WriteProperty<bool>("enabled", frictionJoint2D.enabled);
			writer.WriteProperty<string>("tag", frictionJoint2D.tag);
			writer.WriteProperty<string>("name", frictionJoint2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", frictionJoint2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			FrictionJoint2D frictionJoint2D = SaveGameType.CreateComponent<FrictionJoint2D>();
			this.ReadInto(frictionJoint2D, reader);
			return frictionJoint2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			FrictionJoint2D frictionJoint2D = (FrictionJoint2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "maxForce":
					frictionJoint2D.maxForce = reader.ReadProperty<float>();
					break;
				case "maxTorque":
					frictionJoint2D.maxTorque = reader.ReadProperty<float>();
					break;
				case "anchor":
					frictionJoint2D.anchor = reader.ReadProperty<Vector2>();
					break;
				case "connectedAnchor":
					frictionJoint2D.connectedAnchor = reader.ReadProperty<Vector2>();
					break;
				case "autoConfigureConnectedAnchor":
					frictionJoint2D.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "connectedBody":
					if (frictionJoint2D.connectedBody == null)
					{
						frictionJoint2D.connectedBody = reader.ReadProperty<Rigidbody2D>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody2D>(frictionJoint2D.connectedBody);
					}
					break;
				case "enableCollision":
					frictionJoint2D.enableCollision = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					frictionJoint2D.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					frictionJoint2D.breakTorque = reader.ReadProperty<float>();
					break;
				case "enabled":
					frictionJoint2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					frictionJoint2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					frictionJoint2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					frictionJoint2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
