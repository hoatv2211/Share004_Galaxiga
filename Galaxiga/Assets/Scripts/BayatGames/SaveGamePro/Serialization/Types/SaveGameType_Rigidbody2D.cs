using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Rigidbody2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Rigidbody2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Rigidbody2D rigidbody2D = (Rigidbody2D)value;
			writer.WriteProperty<Vector2>("position", rigidbody2D.position);
			writer.WriteProperty<float>("rotation", rigidbody2D.rotation);
			writer.WriteProperty<Vector2>("velocity", rigidbody2D.linearVelocity);
			writer.WriteProperty<float>("angularVelocity", rigidbody2D.angularVelocity);
			writer.WriteProperty<bool>("useAutoMass", rigidbody2D.useAutoMass);
			writer.WriteProperty<float>("mass", rigidbody2D.mass);
			writer.WriteProperty<PhysicsMaterial2D>("sharedMaterial", rigidbody2D.sharedMaterial);
			writer.WriteProperty<Vector2>("centerOfMass", rigidbody2D.centerOfMass);
			writer.WriteProperty<float>("inertia", rigidbody2D.inertia);
			writer.WriteProperty<float>("drag", rigidbody2D.linearDamping);
			writer.WriteProperty<float>("angularDrag", rigidbody2D.angularDamping);
			writer.WriteProperty<float>("gravityScale", rigidbody2D.gravityScale);
			writer.WriteProperty<RigidbodyType2D>("bodyType", rigidbody2D.bodyType);
			writer.WriteProperty<bool>("useFullKinematicContacts", rigidbody2D.useFullKinematicContacts);
			writer.WriteProperty<bool>("isKinematic", rigidbody2D.isKinematic);
			writer.WriteProperty<bool>("freezeRotation", rigidbody2D.freezeRotation);
			writer.WriteProperty<RigidbodyConstraints2D>("constraints", rigidbody2D.constraints);
			writer.WriteProperty<bool>("simulated", rigidbody2D.simulated);
			writer.WriteProperty<RigidbodyInterpolation2D>("interpolation", rigidbody2D.interpolation);
			writer.WriteProperty<RigidbodySleepMode2D>("sleepMode", rigidbody2D.sleepMode);
			writer.WriteProperty<CollisionDetectionMode2D>("collisionDetectionMode", rigidbody2D.collisionDetectionMode);
			writer.WriteProperty<string>("tag", rigidbody2D.tag);
			writer.WriteProperty<string>("name", rigidbody2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", rigidbody2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Rigidbody2D rigidbody2D = SaveGameType.CreateComponent<Rigidbody2D>();
			this.ReadInto(rigidbody2D, reader);
			return rigidbody2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Rigidbody2D rigidbody2D = (Rigidbody2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "position":
					rigidbody2D.position = reader.ReadProperty<Vector2>();
					break;
				case "rotation":
					rigidbody2D.rotation = reader.ReadProperty<float>();
					break;
				case "velocity":
					rigidbody2D.linearVelocity = reader.ReadProperty<Vector2>();
					break;
				case "angularVelocity":
					rigidbody2D.angularVelocity = reader.ReadProperty<float>();
					break;
				case "useAutoMass":
					rigidbody2D.useAutoMass = reader.ReadProperty<bool>();
					break;
				case "mass":
					rigidbody2D.mass = reader.ReadProperty<float>();
					break;
				case "sharedMaterial":
					if (rigidbody2D.sharedMaterial == null)
					{
						rigidbody2D.sharedMaterial = reader.ReadProperty<PhysicsMaterial2D>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial2D>(rigidbody2D.sharedMaterial);
					}
					break;
				case "centerOfMass":
					rigidbody2D.centerOfMass = reader.ReadProperty<Vector2>();
					break;
				case "inertia":
					rigidbody2D.inertia = reader.ReadProperty<float>();
					break;
				case "drag":
					rigidbody2D.linearDamping = reader.ReadProperty<float>();
					break;
				case "angularDrag":
					rigidbody2D.angularDamping = reader.ReadProperty<float>();
					break;
				case "gravityScale":
					rigidbody2D.gravityScale = reader.ReadProperty<float>();
					break;
				case "bodyType":
					rigidbody2D.bodyType = reader.ReadProperty<RigidbodyType2D>();
					break;
				case "useFullKinematicContacts":
					rigidbody2D.useFullKinematicContacts = reader.ReadProperty<bool>();
					break;
				case "isKinematic":
					rigidbody2D.isKinematic = reader.ReadProperty<bool>();
					break;
				case "freezeRotation":
					rigidbody2D.freezeRotation = reader.ReadProperty<bool>();
					break;
				case "constraints":
					rigidbody2D.constraints = reader.ReadProperty<RigidbodyConstraints2D>();
					break;
				case "simulated":
					rigidbody2D.simulated = reader.ReadProperty<bool>();
					break;
				case "interpolation":
					rigidbody2D.interpolation = reader.ReadProperty<RigidbodyInterpolation2D>();
					break;
				case "sleepMode":
					rigidbody2D.sleepMode = reader.ReadProperty<RigidbodySleepMode2D>();
					break;
				case "collisionDetectionMode":
					rigidbody2D.collisionDetectionMode = reader.ReadProperty<CollisionDetectionMode2D>();
					break;
				case "tag":
					rigidbody2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					rigidbody2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					rigidbody2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
