using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SpringJoint : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SpringJoint);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SpringJoint springJoint = (SpringJoint)value;
			writer.WriteProperty<float>("spring", springJoint.spring);
			writer.WriteProperty<float>("damper", springJoint.damper);
			writer.WriteProperty<float>("minDistance", springJoint.minDistance);
			writer.WriteProperty<float>("maxDistance", springJoint.maxDistance);
			writer.WriteProperty<float>("tolerance", springJoint.tolerance);
			writer.WriteProperty<Rigidbody>("connectedBody", springJoint.connectedBody);
			writer.WriteProperty<Vector3>("axis", springJoint.axis);
			writer.WriteProperty<Vector3>("anchor", springJoint.anchor);
			writer.WriteProperty<Vector3>("connectedAnchor", springJoint.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", springJoint.autoConfigureConnectedAnchor);
			writer.WriteProperty<float>("breakForce", springJoint.breakForce);
			writer.WriteProperty<float>("breakTorque", springJoint.breakTorque);
			writer.WriteProperty<bool>("enableCollision", springJoint.enableCollision);
			writer.WriteProperty<bool>("enablePreprocessing", springJoint.enablePreprocessing);
			writer.WriteProperty<float>("massScale", springJoint.massScale);
			writer.WriteProperty<float>("connectedMassScale", springJoint.connectedMassScale);
			writer.WriteProperty<string>("tag", springJoint.tag);
			writer.WriteProperty<string>("name", springJoint.name);
			writer.WriteProperty<HideFlags>("hideFlags", springJoint.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			SpringJoint springJoint = SaveGameType.CreateComponent<SpringJoint>();
			this.ReadInto(springJoint, reader);
			return springJoint;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			SpringJoint springJoint = (SpringJoint)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "spring":
					springJoint.spring = reader.ReadProperty<float>();
					break;
				case "damper":
					springJoint.damper = reader.ReadProperty<float>();
					break;
				case "minDistance":
					springJoint.minDistance = reader.ReadProperty<float>();
					break;
				case "maxDistance":
					springJoint.maxDistance = reader.ReadProperty<float>();
					break;
				case "tolerance":
					springJoint.tolerance = reader.ReadProperty<float>();
					break;
				case "connectedBody":
					if (springJoint.connectedBody == null)
					{
						springJoint.connectedBody = reader.ReadProperty<Rigidbody>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody>(springJoint.connectedBody);
					}
					break;
				case "axis":
					springJoint.axis = reader.ReadProperty<Vector3>();
					break;
				case "anchor":
					springJoint.anchor = reader.ReadProperty<Vector3>();
					break;
				case "connectedAnchor":
					springJoint.connectedAnchor = reader.ReadProperty<Vector3>();
					break;
				case "autoConfigureConnectedAnchor":
					springJoint.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					springJoint.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					springJoint.breakTorque = reader.ReadProperty<float>();
					break;
				case "enableCollision":
					springJoint.enableCollision = reader.ReadProperty<bool>();
					break;
				case "enablePreprocessing":
					springJoint.enablePreprocessing = reader.ReadProperty<bool>();
					break;
				case "massScale":
					springJoint.massScale = reader.ReadProperty<float>();
					break;
				case "connectedMassScale":
					springJoint.connectedMassScale = reader.ReadProperty<float>();
					break;
				case "tag":
					springJoint.tag = reader.ReadProperty<string>();
					break;
				case "name":
					springJoint.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					springJoint.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
