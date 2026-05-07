using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_FixedJoint2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(FixedJoint2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			FixedJoint2D fixedJoint2D = (FixedJoint2D)value;
			writer.WriteProperty<float>("dampingRatio", fixedJoint2D.dampingRatio);
			writer.WriteProperty<float>("frequency", fixedJoint2D.frequency);
			writer.WriteProperty<Vector2>("anchor", fixedJoint2D.anchor);
			writer.WriteProperty<Vector2>("connectedAnchor", fixedJoint2D.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", fixedJoint2D.autoConfigureConnectedAnchor);
			writer.WriteProperty<Rigidbody2D>("connectedBody", fixedJoint2D.connectedBody);
			writer.WriteProperty<bool>("enableCollision", fixedJoint2D.enableCollision);
			writer.WriteProperty<float>("breakForce", fixedJoint2D.breakForce);
			writer.WriteProperty<float>("breakTorque", fixedJoint2D.breakTorque);
			writer.WriteProperty<bool>("enabled", fixedJoint2D.enabled);
			writer.WriteProperty<string>("tag", fixedJoint2D.tag);
			writer.WriteProperty<string>("name", fixedJoint2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", fixedJoint2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			FixedJoint2D fixedJoint2D = SaveGameType.CreateComponent<FixedJoint2D>();
			this.ReadInto(fixedJoint2D, reader);
			return fixedJoint2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			FixedJoint2D fixedJoint2D = (FixedJoint2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "dampingRatio":
					fixedJoint2D.dampingRatio = reader.ReadProperty<float>();
					break;
				case "frequency":
					fixedJoint2D.frequency = reader.ReadProperty<float>();
					break;
				case "anchor":
					fixedJoint2D.anchor = reader.ReadProperty<Vector2>();
					break;
				case "connectedAnchor":
					fixedJoint2D.connectedAnchor = reader.ReadProperty<Vector2>();
					break;
				case "autoConfigureConnectedAnchor":
					fixedJoint2D.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "connectedBody":
					if (fixedJoint2D.connectedBody == null)
					{
						fixedJoint2D.connectedBody = reader.ReadProperty<Rigidbody2D>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody2D>(fixedJoint2D.connectedBody);
					}
					break;
				case "enableCollision":
					fixedJoint2D.enableCollision = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					fixedJoint2D.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					fixedJoint2D.breakTorque = reader.ReadProperty<float>();
					break;
				case "enabled":
					fixedJoint2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					fixedJoint2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					fixedJoint2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					fixedJoint2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
