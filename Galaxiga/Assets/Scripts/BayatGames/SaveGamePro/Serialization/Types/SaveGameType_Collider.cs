using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Collider : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Collider);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Collider collider = (Collider)value;
			writer.WriteProperty<bool>("enabled", collider.enabled);
			writer.WriteProperty<bool>("isTrigger", collider.isTrigger);
			writer.WriteProperty<float>("contactOffset", collider.contactOffset);
			writer.WriteProperty<PhysicsMaterial>("material", collider.material);
			writer.WriteProperty<PhysicsMaterial>("sharedMaterial", collider.sharedMaterial);
			writer.WriteProperty<string>("tag", collider.tag);
			writer.WriteProperty<string>("name", collider.name);
			writer.WriteProperty<HideFlags>("hideFlags", collider.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Collider collider = SaveGameType.CreateComponent<Collider>();
			this.ReadInto(collider, reader);
			return collider;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Collider collider = (Collider)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					collider.enabled = reader.ReadProperty<bool>();
					break;
				case "isTrigger":
					collider.isTrigger = reader.ReadProperty<bool>();
					break;
				case "contactOffset":
					collider.contactOffset = reader.ReadProperty<float>();
					break;
				case "material":
					if (collider.material == null)
					{
						collider.material = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(collider.material);
					}
					break;
				case "sharedMaterial":
					if (collider.sharedMaterial == null)
					{
						collider.sharedMaterial = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(collider.sharedMaterial);
					}
					break;
				case "tag":
					collider.tag = reader.ReadProperty<string>();
					break;
				case "name":
					collider.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					collider.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
