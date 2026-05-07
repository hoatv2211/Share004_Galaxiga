using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_HingeJoint : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(HingeJoint);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			HingeJoint hingeJoint = (HingeJoint)value;
			writer.WriteProperty<JointMotor>("motor", hingeJoint.motor);
			writer.WriteProperty<JointLimits>("limits", hingeJoint.limits);
			writer.WriteProperty<JointSpring>("spring", hingeJoint.spring);
			writer.WriteProperty<bool>("useMotor", hingeJoint.useMotor);
			writer.WriteProperty<bool>("useLimits", hingeJoint.useLimits);
			writer.WriteProperty<bool>("useSpring", hingeJoint.useSpring);
			writer.WriteProperty<Rigidbody>("connectedBody", hingeJoint.connectedBody);
			writer.WriteProperty<Vector3>("axis", hingeJoint.axis);
			writer.WriteProperty<Vector3>("anchor", hingeJoint.anchor);
			writer.WriteProperty<Vector3>("connectedAnchor", hingeJoint.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", hingeJoint.autoConfigureConnectedAnchor);
			writer.WriteProperty<float>("breakForce", hingeJoint.breakForce);
			writer.WriteProperty<float>("breakTorque", hingeJoint.breakTorque);
			writer.WriteProperty<bool>("enableCollision", hingeJoint.enableCollision);
			writer.WriteProperty<bool>("enablePreprocessing", hingeJoint.enablePreprocessing);
			writer.WriteProperty<float>("massScale", hingeJoint.massScale);
			writer.WriteProperty<float>("connectedMassScale", hingeJoint.connectedMassScale);
			writer.WriteProperty<string>("tag", hingeJoint.tag);
			writer.WriteProperty<string>("name", hingeJoint.name);
			writer.WriteProperty<HideFlags>("hideFlags", hingeJoint.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			HingeJoint hingeJoint = SaveGameType.CreateComponent<HingeJoint>();
			this.ReadInto(hingeJoint, reader);
			return hingeJoint;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			HingeJoint hingeJoint = (HingeJoint)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "motor":
					hingeJoint.motor = reader.ReadProperty<JointMotor>();
					break;
				case "limits":
					hingeJoint.limits = reader.ReadProperty<JointLimits>();
					break;
				case "spring":
					hingeJoint.spring = reader.ReadProperty<JointSpring>();
					break;
				case "useMotor":
					hingeJoint.useMotor = reader.ReadProperty<bool>();
					break;
				case "useLimits":
					hingeJoint.useLimits = reader.ReadProperty<bool>();
					break;
				case "useSpring":
					hingeJoint.useSpring = reader.ReadProperty<bool>();
					break;
				case "connectedBody":
					if (hingeJoint.connectedBody == null)
					{
						hingeJoint.connectedBody = reader.ReadProperty<Rigidbody>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody>(hingeJoint.connectedBody);
					}
					break;
				case "axis":
					hingeJoint.axis = reader.ReadProperty<Vector3>();
					break;
				case "anchor":
					hingeJoint.anchor = reader.ReadProperty<Vector3>();
					break;
				case "connectedAnchor":
					hingeJoint.connectedAnchor = reader.ReadProperty<Vector3>();
					break;
				case "autoConfigureConnectedAnchor":
					hingeJoint.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					hingeJoint.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					hingeJoint.breakTorque = reader.ReadProperty<float>();
					break;
				case "enableCollision":
					hingeJoint.enableCollision = reader.ReadProperty<bool>();
					break;
				case "enablePreprocessing":
					hingeJoint.enablePreprocessing = reader.ReadProperty<bool>();
					break;
				case "massScale":
					hingeJoint.massScale = reader.ReadProperty<float>();
					break;
				case "connectedMassScale":
					hingeJoint.connectedMassScale = reader.ReadProperty<float>();
					break;
				case "tag":
					hingeJoint.tag = reader.ReadProperty<string>();
					break;
				case "name":
					hingeJoint.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					hingeJoint.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
