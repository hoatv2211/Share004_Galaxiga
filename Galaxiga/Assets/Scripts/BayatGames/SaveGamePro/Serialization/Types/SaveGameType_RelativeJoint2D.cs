using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_RelativeJoint2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(RelativeJoint2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			RelativeJoint2D relativeJoint2D = (RelativeJoint2D)value;
			writer.WriteProperty<float>("maxForce", relativeJoint2D.maxForce);
			writer.WriteProperty<float>("maxTorque", relativeJoint2D.maxTorque);
			writer.WriteProperty<float>("correctionScale", relativeJoint2D.correctionScale);
			writer.WriteProperty<bool>("autoConfigureOffset", relativeJoint2D.autoConfigureOffset);
			writer.WriteProperty<Vector2>("linearOffset", relativeJoint2D.linearOffset);
			writer.WriteProperty<float>("angularOffset", relativeJoint2D.angularOffset);
			writer.WriteProperty<Rigidbody2D>("connectedBody", relativeJoint2D.connectedBody);
			writer.WriteProperty<bool>("enableCollision", relativeJoint2D.enableCollision);
			writer.WriteProperty<float>("breakForce", relativeJoint2D.breakForce);
			writer.WriteProperty<float>("breakTorque", relativeJoint2D.breakTorque);
			writer.WriteProperty<bool>("enabled", relativeJoint2D.enabled);
			writer.WriteProperty<string>("tag", relativeJoint2D.tag);
			writer.WriteProperty<string>("name", relativeJoint2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", relativeJoint2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			RelativeJoint2D relativeJoint2D = SaveGameType.CreateComponent<RelativeJoint2D>();
			this.ReadInto(relativeJoint2D, reader);
			return relativeJoint2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			RelativeJoint2D relativeJoint2D = (RelativeJoint2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "maxForce":
					relativeJoint2D.maxForce = reader.ReadProperty<float>();
					break;
				case "maxTorque":
					relativeJoint2D.maxTorque = reader.ReadProperty<float>();
					break;
				case "correctionScale":
					relativeJoint2D.correctionScale = reader.ReadProperty<float>();
					break;
				case "autoConfigureOffset":
					relativeJoint2D.autoConfigureOffset = reader.ReadProperty<bool>();
					break;
				case "linearOffset":
					relativeJoint2D.linearOffset = reader.ReadProperty<Vector2>();
					break;
				case "angularOffset":
					relativeJoint2D.angularOffset = reader.ReadProperty<float>();
					break;
				case "connectedBody":
					if (relativeJoint2D.connectedBody == null)
					{
						relativeJoint2D.connectedBody = reader.ReadProperty<Rigidbody2D>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody2D>(relativeJoint2D.connectedBody);
					}
					break;
				case "enableCollision":
					relativeJoint2D.enableCollision = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					relativeJoint2D.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					relativeJoint2D.breakTorque = reader.ReadProperty<float>();
					break;
				case "enabled":
					relativeJoint2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					relativeJoint2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					relativeJoint2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					relativeJoint2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
