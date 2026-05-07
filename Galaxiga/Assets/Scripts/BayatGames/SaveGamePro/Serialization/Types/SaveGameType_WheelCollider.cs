using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_WheelCollider : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(WheelCollider);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			WheelCollider wheelCollider = (WheelCollider)value;
			writer.WriteProperty<Vector3>("center", wheelCollider.center);
			writer.WriteProperty<float>("radius", wheelCollider.radius);
			writer.WriteProperty<float>("suspensionDistance", wheelCollider.suspensionDistance);
			writer.WriteProperty<JointSpring>("suspensionSpring", wheelCollider.suspensionSpring);
			writer.WriteProperty<float>("forceAppPointDistance", wheelCollider.forceAppPointDistance);
			writer.WriteProperty<float>("mass", wheelCollider.mass);
			writer.WriteProperty<float>("wheelDampingRate", wheelCollider.wheelDampingRate);
			writer.WriteProperty<WheelFrictionCurve>("forwardFriction", wheelCollider.forwardFriction);
			writer.WriteProperty<WheelFrictionCurve>("sidewaysFriction", wheelCollider.sidewaysFriction);
			writer.WriteProperty<float>("motorTorque", wheelCollider.motorTorque);
			writer.WriteProperty<float>("brakeTorque", wheelCollider.brakeTorque);
			writer.WriteProperty<float>("steerAngle", wheelCollider.steerAngle);
			writer.WriteProperty<bool>("enabled", wheelCollider.enabled);
			writer.WriteProperty<bool>("isTrigger", wheelCollider.isTrigger);
			writer.WriteProperty<float>("contactOffset", wheelCollider.contactOffset);
			writer.WriteProperty<PhysicsMaterial>("material", wheelCollider.material);
			writer.WriteProperty<PhysicsMaterial>("sharedMaterial", wheelCollider.sharedMaterial);
			writer.WriteProperty<string>("tag", wheelCollider.tag);
			writer.WriteProperty<string>("name", wheelCollider.name);
			writer.WriteProperty<HideFlags>("hideFlags", wheelCollider.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			WheelCollider wheelCollider = SaveGameType.CreateComponent<WheelCollider>();
			this.ReadInto(wheelCollider, reader);
			return wheelCollider;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			WheelCollider wheelCollider = (WheelCollider)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "center":
					wheelCollider.center = reader.ReadProperty<Vector3>();
					break;
				case "radius":
					wheelCollider.radius = reader.ReadProperty<float>();
					break;
				case "suspensionDistance":
					wheelCollider.suspensionDistance = reader.ReadProperty<float>();
					break;
				case "suspensionSpring":
					wheelCollider.suspensionSpring = reader.ReadProperty<JointSpring>();
					break;
				case "forceAppPointDistance":
					wheelCollider.forceAppPointDistance = reader.ReadProperty<float>();
					break;
				case "mass":
					wheelCollider.mass = reader.ReadProperty<float>();
					break;
				case "wheelDampingRate":
					wheelCollider.wheelDampingRate = reader.ReadProperty<float>();
					break;
				case "forwardFriction":
					wheelCollider.forwardFriction = reader.ReadProperty<WheelFrictionCurve>();
					break;
				case "sidewaysFriction":
					wheelCollider.sidewaysFriction = reader.ReadProperty<WheelFrictionCurve>();
					break;
				case "motorTorque":
					wheelCollider.motorTorque = reader.ReadProperty<float>();
					break;
				case "brakeTorque":
					wheelCollider.brakeTorque = reader.ReadProperty<float>();
					break;
				case "steerAngle":
					wheelCollider.steerAngle = reader.ReadProperty<float>();
					break;
				case "enabled":
					wheelCollider.enabled = reader.ReadProperty<bool>();
					break;
				case "isTrigger":
					wheelCollider.isTrigger = reader.ReadProperty<bool>();
					break;
				case "contactOffset":
					wheelCollider.contactOffset = reader.ReadProperty<float>();
					break;
				case "material":
					if (wheelCollider.material == null)
					{
						wheelCollider.material = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(wheelCollider.material);
					}
					break;
				case "sharedMaterial":
					if (wheelCollider.sharedMaterial == null)
					{
						wheelCollider.sharedMaterial = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(wheelCollider.sharedMaterial);
					}
					break;
				case "tag":
					wheelCollider.tag = reader.ReadProperty<string>();
					break;
				case "name":
					wheelCollider.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					wheelCollider.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
