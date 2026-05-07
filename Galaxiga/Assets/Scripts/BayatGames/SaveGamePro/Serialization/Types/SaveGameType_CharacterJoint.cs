using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_CharacterJoint : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(CharacterJoint);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			CharacterJoint characterJoint = (CharacterJoint)value;
			writer.WriteProperty<Vector3>("swingAxis", characterJoint.swingAxis);
			writer.WriteProperty<SoftJointLimitSpring>("twistLimitSpring", characterJoint.twistLimitSpring);
			writer.WriteProperty<SoftJointLimitSpring>("swingLimitSpring", characterJoint.swingLimitSpring);
			writer.WriteProperty<SoftJointLimit>("lowTwistLimit", characterJoint.lowTwistLimit);
			writer.WriteProperty<SoftJointLimit>("highTwistLimit", characterJoint.highTwistLimit);
			writer.WriteProperty<SoftJointLimit>("swing1Limit", characterJoint.swing1Limit);
			writer.WriteProperty<SoftJointLimit>("swing2Limit", characterJoint.swing2Limit);
			writer.WriteProperty<bool>("enableProjection", characterJoint.enableProjection);
			writer.WriteProperty<float>("projectionDistance", characterJoint.projectionDistance);
			writer.WriteProperty<float>("projectionAngle", characterJoint.projectionAngle);
			writer.WriteProperty<Rigidbody>("connectedBody", characterJoint.connectedBody);
			writer.WriteProperty<Vector3>("axis", characterJoint.axis);
			writer.WriteProperty<Vector3>("anchor", characterJoint.anchor);
			writer.WriteProperty<Vector3>("connectedAnchor", characterJoint.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", characterJoint.autoConfigureConnectedAnchor);
			writer.WriteProperty<float>("breakForce", characterJoint.breakForce);
			writer.WriteProperty<float>("breakTorque", characterJoint.breakTorque);
			writer.WriteProperty<bool>("enableCollision", characterJoint.enableCollision);
			writer.WriteProperty<bool>("enablePreprocessing", characterJoint.enablePreprocessing);
			writer.WriteProperty<float>("massScale", characterJoint.massScale);
			writer.WriteProperty<float>("connectedMassScale", characterJoint.connectedMassScale);
			writer.WriteProperty<string>("tag", characterJoint.tag);
			writer.WriteProperty<string>("name", characterJoint.name);
			writer.WriteProperty<HideFlags>("hideFlags", characterJoint.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			CharacterJoint characterJoint = SaveGameType.CreateComponent<CharacterJoint>();
			this.ReadInto(characterJoint, reader);
			return characterJoint;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			CharacterJoint characterJoint = (CharacterJoint)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "swingAxis":
					characterJoint.swingAxis = reader.ReadProperty<Vector3>();
					break;
				case "twistLimitSpring":
					characterJoint.twistLimitSpring = reader.ReadProperty<SoftJointLimitSpring>();
					break;
				case "swingLimitSpring":
					characterJoint.swingLimitSpring = reader.ReadProperty<SoftJointLimitSpring>();
					break;
				case "lowTwistLimit":
					characterJoint.lowTwistLimit = reader.ReadProperty<SoftJointLimit>();
					break;
				case "highTwistLimit":
					characterJoint.highTwistLimit = reader.ReadProperty<SoftJointLimit>();
					break;
				case "swing1Limit":
					characterJoint.swing1Limit = reader.ReadProperty<SoftJointLimit>();
					break;
				case "swing2Limit":
					characterJoint.swing2Limit = reader.ReadProperty<SoftJointLimit>();
					break;
				case "enableProjection":
					characterJoint.enableProjection = reader.ReadProperty<bool>();
					break;
				case "projectionDistance":
					characterJoint.projectionDistance = reader.ReadProperty<float>();
					break;
				case "projectionAngle":
					characterJoint.projectionAngle = reader.ReadProperty<float>();
					break;
				case "connectedBody":
					if (characterJoint.connectedBody == null)
					{
						characterJoint.connectedBody = reader.ReadProperty<Rigidbody>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody>(characterJoint.connectedBody);
					}
					break;
				case "axis":
					characterJoint.axis = reader.ReadProperty<Vector3>();
					break;
				case "anchor":
					characterJoint.anchor = reader.ReadProperty<Vector3>();
					break;
				case "connectedAnchor":
					characterJoint.connectedAnchor = reader.ReadProperty<Vector3>();
					break;
				case "autoConfigureConnectedAnchor":
					characterJoint.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					characterJoint.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					characterJoint.breakTorque = reader.ReadProperty<float>();
					break;
				case "enableCollision":
					characterJoint.enableCollision = reader.ReadProperty<bool>();
					break;
				case "enablePreprocessing":
					characterJoint.enablePreprocessing = reader.ReadProperty<bool>();
					break;
				case "massScale":
					characterJoint.massScale = reader.ReadProperty<float>();
					break;
				case "connectedMassScale":
					characterJoint.connectedMassScale = reader.ReadProperty<float>();
					break;
				case "tag":
					characterJoint.tag = reader.ReadProperty<string>();
					break;
				case "name":
					characterJoint.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					characterJoint.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
