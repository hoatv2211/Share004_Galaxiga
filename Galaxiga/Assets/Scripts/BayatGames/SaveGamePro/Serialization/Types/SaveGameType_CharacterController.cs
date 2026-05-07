using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_CharacterController : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(CharacterController);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			CharacterController characterController = (CharacterController)value;
			writer.WriteProperty<float>("radius", characterController.radius);
			writer.WriteProperty<float>("height", characterController.height);
			writer.WriteProperty<Vector3>("center", characterController.center);
			writer.WriteProperty<float>("slopeLimit", characterController.slopeLimit);
			writer.WriteProperty<float>("stepOffset", characterController.stepOffset);
			writer.WriteProperty<float>("skinWidth", characterController.skinWidth);
			writer.WriteProperty<float>("minMoveDistance", characterController.minMoveDistance);
			writer.WriteProperty<bool>("detectCollisions", characterController.detectCollisions);
			writer.WriteProperty<bool>("enableOverlapRecovery", characterController.enableOverlapRecovery);
			writer.WriteProperty<bool>("enabled", characterController.enabled);
			writer.WriteProperty<bool>("isTrigger", characterController.isTrigger);
			writer.WriteProperty<float>("contactOffset", characterController.contactOffset);
			writer.WriteProperty<PhysicsMaterial>("material", characterController.material);
			writer.WriteProperty<PhysicsMaterial>("sharedMaterial", characterController.sharedMaterial);
			writer.WriteProperty<string>("tag", characterController.tag);
			writer.WriteProperty<string>("name", characterController.name);
			writer.WriteProperty<HideFlags>("hideFlags", characterController.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			CharacterController characterController = SaveGameType.CreateComponent<CharacterController>();
			this.ReadInto(characterController, reader);
			return characterController;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			CharacterController characterController = (CharacterController)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "radius":
					characterController.radius = reader.ReadProperty<float>();
					break;
				case "height":
					characterController.height = reader.ReadProperty<float>();
					break;
				case "center":
					characterController.center = reader.ReadProperty<Vector3>();
					break;
				case "slopeLimit":
					characterController.slopeLimit = reader.ReadProperty<float>();
					break;
				case "stepOffset":
					characterController.stepOffset = reader.ReadProperty<float>();
					break;
				case "skinWidth":
					characterController.skinWidth = reader.ReadProperty<float>();
					break;
				case "minMoveDistance":
					characterController.minMoveDistance = reader.ReadProperty<float>();
					break;
				case "detectCollisions":
					characterController.detectCollisions = reader.ReadProperty<bool>();
					break;
				case "enableOverlapRecovery":
					characterController.enableOverlapRecovery = reader.ReadProperty<bool>();
					break;
				case "enabled":
					characterController.enabled = reader.ReadProperty<bool>();
					break;
				case "isTrigger":
					characterController.isTrigger = reader.ReadProperty<bool>();
					break;
				case "contactOffset":
					characterController.contactOffset = reader.ReadProperty<float>();
					break;
				case "material":
					if (characterController.material == null)
					{
						characterController.material = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(characterController.material);
					}
					break;
				case "sharedMaterial":
					if (characterController.sharedMaterial == null)
					{
						characterController.sharedMaterial = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(characterController.sharedMaterial);
					}
					break;
				case "tag":
					characterController.tag = reader.ReadProperty<string>();
					break;
				case "name":
					characterController.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					characterController.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
