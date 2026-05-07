using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Rigidbody : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Rigidbody);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Rigidbody rigidbody = (Rigidbody)value;
			writer.WriteProperty<Vector3>("velocity", rigidbody.linearVelocity);
			writer.WriteProperty<Vector3>("angularVelocity", rigidbody.angularVelocity);
			writer.WriteProperty<float>("drag", rigidbody.linearDamping);
			writer.WriteProperty<float>("angularDrag", rigidbody.angularDamping);
			writer.WriteProperty<float>("mass", rigidbody.mass);
			writer.WriteProperty<bool>("useGravity", rigidbody.useGravity);
			writer.WriteProperty<float>("maxDepenetrationVelocity", rigidbody.maxDepenetrationVelocity);
			writer.WriteProperty<bool>("isKinematic", rigidbody.isKinematic);
			writer.WriteProperty<bool>("freezeRotation", rigidbody.freezeRotation);
			writer.WriteProperty<RigidbodyConstraints>("constraints", rigidbody.constraints);
			writer.WriteProperty<CollisionDetectionMode>("collisionDetectionMode", rigidbody.collisionDetectionMode);
			writer.WriteProperty<Vector3>("centerOfMass", rigidbody.centerOfMass);
			writer.WriteProperty<Quaternion>("inertiaTensorRotation", rigidbody.inertiaTensorRotation);
			writer.WriteProperty<Vector3>("inertiaTensor", rigidbody.inertiaTensor);
			writer.WriteProperty<bool>("detectCollisions", rigidbody.detectCollisions);
			writer.WriteProperty<Vector3>("position", rigidbody.position);
			writer.WriteProperty<Quaternion>("rotation", rigidbody.rotation);
			writer.WriteProperty<RigidbodyInterpolation>("interpolation", rigidbody.interpolation);
			writer.WriteProperty<int>("solverIterations", rigidbody.solverIterations);
			writer.WriteProperty<int>("solverVelocityIterations", rigidbody.solverVelocityIterations);
			writer.WriteProperty<float>("sleepThreshold", rigidbody.sleepThreshold);
			writer.WriteProperty<float>("maxAngularVelocity", rigidbody.maxAngularVelocity);
			writer.WriteProperty<string>("tag", rigidbody.tag);
			writer.WriteProperty<string>("name", rigidbody.name);
			writer.WriteProperty<HideFlags>("hideFlags", rigidbody.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Rigidbody rigidbody = SaveGameType.CreateComponent<Rigidbody>();
			this.ReadInto(rigidbody, reader);
			return rigidbody;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Rigidbody rigidbody = (Rigidbody)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "velocity":
					rigidbody.linearVelocity = reader.ReadProperty<Vector3>();
					break;
				case "angularVelocity":
					rigidbody.angularVelocity = reader.ReadProperty<Vector3>();
					break;
				case "drag":
					rigidbody.linearDamping = reader.ReadProperty<float>();
					break;
				case "angularDrag":
					rigidbody.angularDamping = reader.ReadProperty<float>();
					break;
				case "mass":
					rigidbody.mass = reader.ReadProperty<float>();
					break;
				case "useGravity":
					rigidbody.useGravity = reader.ReadProperty<bool>();
					break;
				case "maxDepenetrationVelocity":
					rigidbody.maxDepenetrationVelocity = reader.ReadProperty<float>();
					break;
				case "isKinematic":
					rigidbody.isKinematic = reader.ReadProperty<bool>();
					break;
				case "freezeRotation":
					rigidbody.freezeRotation = reader.ReadProperty<bool>();
					break;
				case "constraints":
					rigidbody.constraints = reader.ReadProperty<RigidbodyConstraints>();
					break;
				case "collisionDetectionMode":
					rigidbody.collisionDetectionMode = reader.ReadProperty<CollisionDetectionMode>();
					break;
				case "centerOfMass":
					rigidbody.centerOfMass = reader.ReadProperty<Vector3>();
					break;
				case "inertiaTensorRotation":
					rigidbody.inertiaTensorRotation = reader.ReadProperty<Quaternion>();
					break;
				case "inertiaTensor":
					rigidbody.inertiaTensor = reader.ReadProperty<Vector3>();
					break;
				case "detectCollisions":
					rigidbody.detectCollisions = reader.ReadProperty<bool>();
					break;
				case "position":
					rigidbody.position = reader.ReadProperty<Vector3>();
					break;
				case "rotation":
					rigidbody.rotation = reader.ReadProperty<Quaternion>();
					break;
				case "interpolation":
					rigidbody.interpolation = reader.ReadProperty<RigidbodyInterpolation>();
					break;
				case "solverIterations":
					rigidbody.solverIterations = reader.ReadProperty<int>();
					break;
				case "solverVelocityIterations":
					rigidbody.solverVelocityIterations = reader.ReadProperty<int>();
					break;
				case "sleepThreshold":
					rigidbody.sleepThreshold = reader.ReadProperty<float>();
					break;
				case "maxAngularVelocity":
					rigidbody.maxAngularVelocity = reader.ReadProperty<float>();
					break;
				case "tag":
					rigidbody.tag = reader.ReadProperty<string>();
					break;
				case "name":
					rigidbody.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					rigidbody.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
