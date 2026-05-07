using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SphereCollider : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SphereCollider);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SphereCollider sphereCollider = (SphereCollider)value;
			writer.WriteProperty<Vector3>("center", sphereCollider.center);
			writer.WriteProperty<float>("radius", sphereCollider.radius);
			writer.WriteProperty<bool>("enabled", sphereCollider.enabled);
			writer.WriteProperty<bool>("isTrigger", sphereCollider.isTrigger);
			writer.WriteProperty<float>("contactOffset", sphereCollider.contactOffset);
			writer.WriteProperty<PhysicsMaterial>("material", sphereCollider.material);
			writer.WriteProperty<PhysicsMaterial>("sharedMaterial", sphereCollider.sharedMaterial);
			writer.WriteProperty<string>("tag", sphereCollider.tag);
			writer.WriteProperty<string>("name", sphereCollider.name);
			writer.WriteProperty<HideFlags>("hideFlags", sphereCollider.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			SphereCollider sphereCollider = SaveGameType.CreateComponent<SphereCollider>();
			this.ReadInto(sphereCollider, reader);
			return sphereCollider;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			SphereCollider sphereCollider = (SphereCollider)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "center":
					sphereCollider.center = reader.ReadProperty<Vector3>();
					break;
				case "radius":
					sphereCollider.radius = reader.ReadProperty<float>();
					break;
				case "enabled":
					sphereCollider.enabled = reader.ReadProperty<bool>();
					break;
				case "isTrigger":
					sphereCollider.isTrigger = reader.ReadProperty<bool>();
					break;
				case "contactOffset":
					sphereCollider.contactOffset = reader.ReadProperty<float>();
					break;
				case "material":
					if (sphereCollider.material == null)
					{
						sphereCollider.material = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(sphereCollider.material);
					}
					break;
				case "sharedMaterial":
					if (sphereCollider.sharedMaterial == null)
					{
						sphereCollider.sharedMaterial = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(sphereCollider.sharedMaterial);
					}
					break;
				case "tag":
					sphereCollider.tag = reader.ReadProperty<string>();
					break;
				case "name":
					sphereCollider.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					sphereCollider.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
