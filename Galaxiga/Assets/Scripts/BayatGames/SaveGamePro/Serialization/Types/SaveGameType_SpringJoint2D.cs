using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SpringJoint2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SpringJoint2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SpringJoint2D springJoint2D = (SpringJoint2D)value;
			writer.WriteProperty<bool>("autoConfigureDistance", springJoint2D.autoConfigureDistance);
			writer.WriteProperty<float>("distance", springJoint2D.distance);
			writer.WriteProperty<float>("dampingRatio", springJoint2D.dampingRatio);
			writer.WriteProperty<float>("frequency", springJoint2D.frequency);
			writer.WriteProperty<Vector2>("anchor", springJoint2D.anchor);
			writer.WriteProperty<Vector2>("connectedAnchor", springJoint2D.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", springJoint2D.autoConfigureConnectedAnchor);
			writer.WriteProperty<Rigidbody2D>("connectedBody", springJoint2D.connectedBody);
			writer.WriteProperty<bool>("enableCollision", springJoint2D.enableCollision);
			writer.WriteProperty<float>("breakForce", springJoint2D.breakForce);
			writer.WriteProperty<float>("breakTorque", springJoint2D.breakTorque);
			writer.WriteProperty<bool>("enabled", springJoint2D.enabled);
			writer.WriteProperty<string>("tag", springJoint2D.tag);
			writer.WriteProperty<string>("name", springJoint2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", springJoint2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			SpringJoint2D springJoint2D = SaveGameType.CreateComponent<SpringJoint2D>();
			this.ReadInto(springJoint2D, reader);
			return springJoint2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			SpringJoint2D springJoint2D = (SpringJoint2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "autoConfigureDistance":
					springJoint2D.autoConfigureDistance = reader.ReadProperty<bool>();
					break;
				case "distance":
					springJoint2D.distance = reader.ReadProperty<float>();
					break;
				case "dampingRatio":
					springJoint2D.dampingRatio = reader.ReadProperty<float>();
					break;
				case "frequency":
					springJoint2D.frequency = reader.ReadProperty<float>();
					break;
				case "anchor":
					springJoint2D.anchor = reader.ReadProperty<Vector2>();
					break;
				case "connectedAnchor":
					springJoint2D.connectedAnchor = reader.ReadProperty<Vector2>();
					break;
				case "autoConfigureConnectedAnchor":
					springJoint2D.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "connectedBody":
					if (springJoint2D.connectedBody == null)
					{
						springJoint2D.connectedBody = reader.ReadProperty<Rigidbody2D>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody2D>(springJoint2D.connectedBody);
					}
					break;
				case "enableCollision":
					springJoint2D.enableCollision = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					springJoint2D.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					springJoint2D.breakTorque = reader.ReadProperty<float>();
					break;
				case "enabled":
					springJoint2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					springJoint2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					springJoint2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					springJoint2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
