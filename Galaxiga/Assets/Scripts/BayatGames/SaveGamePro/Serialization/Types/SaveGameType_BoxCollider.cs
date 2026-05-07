using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_BoxCollider : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(BoxCollider);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			BoxCollider boxCollider = (BoxCollider)value;
			writer.WriteProperty<Vector3>("center", boxCollider.center);
			writer.WriteProperty<Vector3>("size", boxCollider.size);
			writer.WriteProperty<bool>("enabled", boxCollider.enabled);
			writer.WriteProperty<bool>("isTrigger", boxCollider.isTrigger);
			writer.WriteProperty<float>("contactOffset", boxCollider.contactOffset);
			writer.WriteProperty<PhysicsMaterial>("material", boxCollider.material);
			writer.WriteProperty<PhysicsMaterial>("sharedMaterial", boxCollider.sharedMaterial);
			writer.WriteProperty<string>("tag", boxCollider.tag);
			writer.WriteProperty<string>("name", boxCollider.name);
			writer.WriteProperty<HideFlags>("hideFlags", boxCollider.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			BoxCollider boxCollider = SaveGameType.CreateComponent<BoxCollider>();
			this.ReadInto(boxCollider, reader);
			return boxCollider;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			BoxCollider boxCollider = (BoxCollider)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "center":
					boxCollider.center = reader.ReadProperty<Vector3>();
					break;
				case "size":
					boxCollider.size = reader.ReadProperty<Vector3>();
					break;
				case "enabled":
					boxCollider.enabled = reader.ReadProperty<bool>();
					break;
				case "isTrigger":
					boxCollider.isTrigger = reader.ReadProperty<bool>();
					break;
				case "contactOffset":
					boxCollider.contactOffset = reader.ReadProperty<float>();
					break;
				case "material":
					if (boxCollider.material == null)
					{
						boxCollider.material = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(boxCollider.material);
					}
					break;
				case "sharedMaterial":
					if (boxCollider.sharedMaterial == null)
					{
						boxCollider.sharedMaterial = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(boxCollider.sharedMaterial);
					}
					break;
				case "tag":
					boxCollider.tag = reader.ReadProperty<string>();
					break;
				case "name":
					boxCollider.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					boxCollider.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
