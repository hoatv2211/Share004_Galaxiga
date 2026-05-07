using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_FixedJoint : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(FixedJoint);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			FixedJoint fixedJoint = (FixedJoint)value;
			writer.WriteProperty<Rigidbody>("connectedBody", fixedJoint.connectedBody);
			writer.WriteProperty<Vector3>("axis", fixedJoint.axis);
			writer.WriteProperty<Vector3>("anchor", fixedJoint.anchor);
			writer.WriteProperty<Vector3>("connectedAnchor", fixedJoint.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", fixedJoint.autoConfigureConnectedAnchor);
			writer.WriteProperty<float>("breakForce", fixedJoint.breakForce);
			writer.WriteProperty<float>("breakTorque", fixedJoint.breakTorque);
			writer.WriteProperty<bool>("enableCollision", fixedJoint.enableCollision);
			writer.WriteProperty<bool>("enablePreprocessing", fixedJoint.enablePreprocessing);
			writer.WriteProperty<float>("massScale", fixedJoint.massScale);
			writer.WriteProperty<float>("connectedMassScale", fixedJoint.connectedMassScale);
			writer.WriteProperty<string>("tag", fixedJoint.tag);
			writer.WriteProperty<string>("name", fixedJoint.name);
			writer.WriteProperty<HideFlags>("hideFlags", fixedJoint.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			FixedJoint fixedJoint = SaveGameType.CreateComponent<FixedJoint>();
			this.ReadInto(fixedJoint, reader);
			return fixedJoint;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			FixedJoint fixedJoint = (FixedJoint)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "connectedBody":
					if (fixedJoint.connectedBody == null)
					{
						fixedJoint.connectedBody = reader.ReadProperty<Rigidbody>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody>(fixedJoint.connectedBody);
					}
					break;
				case "axis":
					fixedJoint.axis = reader.ReadProperty<Vector3>();
					break;
				case "anchor":
					fixedJoint.anchor = reader.ReadProperty<Vector3>();
					break;
				case "connectedAnchor":
					fixedJoint.connectedAnchor = reader.ReadProperty<Vector3>();
					break;
				case "autoConfigureConnectedAnchor":
					fixedJoint.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					fixedJoint.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					fixedJoint.breakTorque = reader.ReadProperty<float>();
					break;
				case "enableCollision":
					fixedJoint.enableCollision = reader.ReadProperty<bool>();
					break;
				case "enablePreprocessing":
					fixedJoint.enablePreprocessing = reader.ReadProperty<bool>();
					break;
				case "massScale":
					fixedJoint.massScale = reader.ReadProperty<float>();
					break;
				case "connectedMassScale":
					fixedJoint.connectedMassScale = reader.ReadProperty<float>();
					break;
				case "tag":
					fixedJoint.tag = reader.ReadProperty<string>();
					break;
				case "name":
					fixedJoint.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					fixedJoint.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
