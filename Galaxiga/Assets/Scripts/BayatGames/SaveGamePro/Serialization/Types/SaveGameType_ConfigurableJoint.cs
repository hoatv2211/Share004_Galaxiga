using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ConfigurableJoint : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ConfigurableJoint);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ConfigurableJoint configurableJoint = (ConfigurableJoint)value;
			writer.WriteProperty<Vector3>("secondaryAxis", configurableJoint.secondaryAxis);
			writer.WriteProperty<ConfigurableJointMotion>("xMotion", configurableJoint.xMotion);
			writer.WriteProperty<ConfigurableJointMotion>("yMotion", configurableJoint.yMotion);
			writer.WriteProperty<ConfigurableJointMotion>("zMotion", configurableJoint.zMotion);
			writer.WriteProperty<ConfigurableJointMotion>("angularXMotion", configurableJoint.angularXMotion);
			writer.WriteProperty<ConfigurableJointMotion>("angularYMotion", configurableJoint.angularYMotion);
			writer.WriteProperty<ConfigurableJointMotion>("angularZMotion", configurableJoint.angularZMotion);
			writer.WriteProperty<SoftJointLimitSpring>("linearLimitSpring", configurableJoint.linearLimitSpring);
			writer.WriteProperty<SoftJointLimitSpring>("angularXLimitSpring", configurableJoint.angularXLimitSpring);
			writer.WriteProperty<SoftJointLimitSpring>("angularYZLimitSpring", configurableJoint.angularYZLimitSpring);
			writer.WriteProperty<SoftJointLimit>("linearLimit", configurableJoint.linearLimit);
			writer.WriteProperty<SoftJointLimit>("lowAngularXLimit", configurableJoint.lowAngularXLimit);
			writer.WriteProperty<SoftJointLimit>("highAngularXLimit", configurableJoint.highAngularXLimit);
			writer.WriteProperty<SoftJointLimit>("angularYLimit", configurableJoint.angularYLimit);
			writer.WriteProperty<SoftJointLimit>("angularZLimit", configurableJoint.angularZLimit);
			writer.WriteProperty<Vector3>("targetPosition", configurableJoint.targetPosition);
			writer.WriteProperty<Vector3>("targetVelocity", configurableJoint.targetVelocity);
			writer.WriteProperty<JointDrive>("xDrive", configurableJoint.xDrive);
			writer.WriteProperty<JointDrive>("yDrive", configurableJoint.yDrive);
			writer.WriteProperty<JointDrive>("zDrive", configurableJoint.zDrive);
			writer.WriteProperty<Quaternion>("targetRotation", configurableJoint.targetRotation);
			writer.WriteProperty<Vector3>("targetAngularVelocity", configurableJoint.targetAngularVelocity);
			writer.WriteProperty<RotationDriveMode>("rotationDriveMode", configurableJoint.rotationDriveMode);
			writer.WriteProperty<JointDrive>("angularXDrive", configurableJoint.angularXDrive);
			writer.WriteProperty<JointDrive>("angularYZDrive", configurableJoint.angularYZDrive);
			writer.WriteProperty<JointDrive>("slerpDrive", configurableJoint.slerpDrive);
			writer.WriteProperty<JointProjectionMode>("projectionMode", configurableJoint.projectionMode);
			writer.WriteProperty<float>("projectionDistance", configurableJoint.projectionDistance);
			writer.WriteProperty<float>("projectionAngle", configurableJoint.projectionAngle);
			writer.WriteProperty<bool>("configuredInWorldSpace", configurableJoint.configuredInWorldSpace);
			writer.WriteProperty<bool>("swapBodies", configurableJoint.swapBodies);
			writer.WriteProperty<Rigidbody>("connectedBody", configurableJoint.connectedBody);
			writer.WriteProperty<Vector3>("axis", configurableJoint.axis);
			writer.WriteProperty<Vector3>("anchor", configurableJoint.anchor);
			writer.WriteProperty<Vector3>("connectedAnchor", configurableJoint.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", configurableJoint.autoConfigureConnectedAnchor);
			writer.WriteProperty<float>("breakForce", configurableJoint.breakForce);
			writer.WriteProperty<float>("breakTorque", configurableJoint.breakTorque);
			writer.WriteProperty<bool>("enableCollision", configurableJoint.enableCollision);
			writer.WriteProperty<bool>("enablePreprocessing", configurableJoint.enablePreprocessing);
			writer.WriteProperty<float>("massScale", configurableJoint.massScale);
			writer.WriteProperty<float>("connectedMassScale", configurableJoint.connectedMassScale);
			writer.WriteProperty<string>("tag", configurableJoint.tag);
			writer.WriteProperty<string>("name", configurableJoint.name);
			writer.WriteProperty<HideFlags>("hideFlags", configurableJoint.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			ConfigurableJoint configurableJoint = SaveGameType.CreateComponent<ConfigurableJoint>();
			this.ReadInto(configurableJoint, reader);
			return configurableJoint;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			ConfigurableJoint configurableJoint = (ConfigurableJoint)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "secondaryAxis":
					configurableJoint.secondaryAxis = reader.ReadProperty<Vector3>();
					break;
				case "xMotion":
					configurableJoint.xMotion = reader.ReadProperty<ConfigurableJointMotion>();
					break;
				case "yMotion":
					configurableJoint.yMotion = reader.ReadProperty<ConfigurableJointMotion>();
					break;
				case "zMotion":
					configurableJoint.zMotion = reader.ReadProperty<ConfigurableJointMotion>();
					break;
				case "angularXMotion":
					configurableJoint.angularXMotion = reader.ReadProperty<ConfigurableJointMotion>();
					break;
				case "angularYMotion":
					configurableJoint.angularYMotion = reader.ReadProperty<ConfigurableJointMotion>();
					break;
				case "angularZMotion":
					configurableJoint.angularZMotion = reader.ReadProperty<ConfigurableJointMotion>();
					break;
				case "linearLimitSpring":
					configurableJoint.linearLimitSpring = reader.ReadProperty<SoftJointLimitSpring>();
					break;
				case "angularXLimitSpring":
					configurableJoint.angularXLimitSpring = reader.ReadProperty<SoftJointLimitSpring>();
					break;
				case "angularYZLimitSpring":
					configurableJoint.angularYZLimitSpring = reader.ReadProperty<SoftJointLimitSpring>();
					break;
				case "linearLimit":
					configurableJoint.linearLimit = reader.ReadProperty<SoftJointLimit>();
					break;
				case "lowAngularXLimit":
					configurableJoint.lowAngularXLimit = reader.ReadProperty<SoftJointLimit>();
					break;
				case "highAngularXLimit":
					configurableJoint.highAngularXLimit = reader.ReadProperty<SoftJointLimit>();
					break;
				case "angularYLimit":
					configurableJoint.angularYLimit = reader.ReadProperty<SoftJointLimit>();
					break;
				case "angularZLimit":
					configurableJoint.angularZLimit = reader.ReadProperty<SoftJointLimit>();
					break;
				case "targetPosition":
					configurableJoint.targetPosition = reader.ReadProperty<Vector3>();
					break;
				case "targetVelocity":
					configurableJoint.targetVelocity = reader.ReadProperty<Vector3>();
					break;
				case "xDrive":
					configurableJoint.xDrive = reader.ReadProperty<JointDrive>();
					break;
				case "yDrive":
					configurableJoint.yDrive = reader.ReadProperty<JointDrive>();
					break;
				case "zDrive":
					configurableJoint.zDrive = reader.ReadProperty<JointDrive>();
					break;
				case "targetRotation":
					configurableJoint.targetRotation = reader.ReadProperty<Quaternion>();
					break;
				case "targetAngularVelocity":
					configurableJoint.targetAngularVelocity = reader.ReadProperty<Vector3>();
					break;
				case "rotationDriveMode":
					configurableJoint.rotationDriveMode = reader.ReadProperty<RotationDriveMode>();
					break;
				case "angularXDrive":
					configurableJoint.angularXDrive = reader.ReadProperty<JointDrive>();
					break;
				case "angularYZDrive":
					configurableJoint.angularYZDrive = reader.ReadProperty<JointDrive>();
					break;
				case "slerpDrive":
					configurableJoint.slerpDrive = reader.ReadProperty<JointDrive>();
					break;
				case "projectionMode":
					configurableJoint.projectionMode = reader.ReadProperty<JointProjectionMode>();
					break;
				case "projectionDistance":
					configurableJoint.projectionDistance = reader.ReadProperty<float>();
					break;
				case "projectionAngle":
					configurableJoint.projectionAngle = reader.ReadProperty<float>();
					break;
				case "configuredInWorldSpace":
					configurableJoint.configuredInWorldSpace = reader.ReadProperty<bool>();
					break;
				case "swapBodies":
					configurableJoint.swapBodies = reader.ReadProperty<bool>();
					break;
				case "connectedBody":
					if (configurableJoint.connectedBody == null)
					{
						configurableJoint.connectedBody = reader.ReadProperty<Rigidbody>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody>(configurableJoint.connectedBody);
					}
					break;
				case "axis":
					configurableJoint.axis = reader.ReadProperty<Vector3>();
					break;
				case "anchor":
					configurableJoint.anchor = reader.ReadProperty<Vector3>();
					break;
				case "connectedAnchor":
					configurableJoint.connectedAnchor = reader.ReadProperty<Vector3>();
					break;
				case "autoConfigureConnectedAnchor":
					configurableJoint.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					configurableJoint.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					configurableJoint.breakTorque = reader.ReadProperty<float>();
					break;
				case "enableCollision":
					configurableJoint.enableCollision = reader.ReadProperty<bool>();
					break;
				case "enablePreprocessing":
					configurableJoint.enablePreprocessing = reader.ReadProperty<bool>();
					break;
				case "massScale":
					configurableJoint.massScale = reader.ReadProperty<float>();
					break;
				case "connectedMassScale":
					configurableJoint.connectedMassScale = reader.ReadProperty<float>();
					break;
				case "tag":
					configurableJoint.tag = reader.ReadProperty<string>();
					break;
				case "name":
					configurableJoint.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					configurableJoint.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
