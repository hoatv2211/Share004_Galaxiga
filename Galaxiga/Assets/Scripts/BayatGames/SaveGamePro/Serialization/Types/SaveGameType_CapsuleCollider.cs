using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_CapsuleCollider : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(CapsuleCollider);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			CapsuleCollider capsuleCollider = (CapsuleCollider)value;
			writer.WriteProperty<Vector3>("center", capsuleCollider.center);
			writer.WriteProperty<float>("radius", capsuleCollider.radius);
			writer.WriteProperty<float>("height", capsuleCollider.height);
			writer.WriteProperty<int>("direction", capsuleCollider.direction);
			writer.WriteProperty<bool>("enabled", capsuleCollider.enabled);
			writer.WriteProperty<bool>("isTrigger", capsuleCollider.isTrigger);
			writer.WriteProperty<float>("contactOffset", capsuleCollider.contactOffset);
			writer.WriteProperty<PhysicsMaterial>("material", capsuleCollider.material);
			writer.WriteProperty<PhysicsMaterial>("sharedMaterial", capsuleCollider.sharedMaterial);
			writer.WriteProperty<string>("tag", capsuleCollider.tag);
			writer.WriteProperty<string>("name", capsuleCollider.name);
			writer.WriteProperty<HideFlags>("hideFlags", capsuleCollider.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			CapsuleCollider capsuleCollider = SaveGameType.CreateComponent<CapsuleCollider>();
			this.ReadInto(capsuleCollider, reader);
			return capsuleCollider;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			CapsuleCollider capsuleCollider = (CapsuleCollider)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "center":
					capsuleCollider.center = reader.ReadProperty<Vector3>();
					break;
				case "radius":
					capsuleCollider.radius = reader.ReadProperty<float>();
					break;
				case "height":
					capsuleCollider.height = reader.ReadProperty<float>();
					break;
				case "direction":
					capsuleCollider.direction = reader.ReadProperty<int>();
					break;
				case "enabled":
					capsuleCollider.enabled = reader.ReadProperty<bool>();
					break;
				case "isTrigger":
					capsuleCollider.isTrigger = reader.ReadProperty<bool>();
					break;
				case "contactOffset":
					capsuleCollider.contactOffset = reader.ReadProperty<float>();
					break;
				case "material":
					if (capsuleCollider.material == null)
					{
						capsuleCollider.material = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(capsuleCollider.material);
					}
					break;
				case "sharedMaterial":
					if (capsuleCollider.sharedMaterial == null)
					{
						capsuleCollider.sharedMaterial = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(capsuleCollider.sharedMaterial);
					}
					break;
				case "tag":
					capsuleCollider.tag = reader.ReadProperty<string>();
					break;
				case "name":
					capsuleCollider.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					capsuleCollider.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
