using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_MeshCollider : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(MeshCollider);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			MeshCollider meshCollider = (MeshCollider)value;
			writer.WriteProperty<Mesh>("sharedMesh", meshCollider.sharedMesh);
			writer.WriteProperty<bool>("convex", meshCollider.convex);
			//writer.WriteProperty<bool>("inflateMesh", meshCollider.inflateMesh);
			//writer.WriteProperty<float>("skinWidth", meshCollider.skinWidth);
			writer.WriteProperty<bool>("enabled", meshCollider.enabled);
			writer.WriteProperty<bool>("isTrigger", meshCollider.isTrigger);
			writer.WriteProperty<float>("contactOffset", meshCollider.contactOffset);
			writer.WriteProperty<PhysicsMaterial>("material", meshCollider.material);
			writer.WriteProperty<PhysicsMaterial>("sharedMaterial", meshCollider.sharedMaterial);
			writer.WriteProperty<string>("tag", meshCollider.tag);
			writer.WriteProperty<string>("name", meshCollider.name);
			writer.WriteProperty<HideFlags>("hideFlags", meshCollider.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			MeshCollider meshCollider = SaveGameType.CreateComponent<MeshCollider>();
			this.ReadInto(meshCollider, reader);
			return meshCollider;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			MeshCollider meshCollider = (MeshCollider)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "sharedMesh":
					if (meshCollider.sharedMesh == null)
					{
						meshCollider.sharedMesh = reader.ReadProperty<Mesh>();
					}
					else
					{
						reader.ReadIntoProperty<Mesh>(meshCollider.sharedMesh);
					}
					break;
				case "convex":
					meshCollider.convex = reader.ReadProperty<bool>();
					break;
				case "inflateMesh":
					//meshCollider.inflateMesh = reader.ReadProperty<bool>();
					break;
				case "skinWidth":
					//meshCollider.skinWidth = reader.ReadProperty<float>();
					break;
				case "enabled":
					meshCollider.enabled = reader.ReadProperty<bool>();
					break;
				case "isTrigger":
					meshCollider.isTrigger = reader.ReadProperty<bool>();
					break;
				case "contactOffset":
					meshCollider.contactOffset = reader.ReadProperty<float>();
					break;
				case "material":
					if (meshCollider.material == null)
					{
						meshCollider.material = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(meshCollider.material);
					}
					break;
				case "sharedMaterial":
					if (meshCollider.sharedMaterial == null)
					{
						meshCollider.sharedMaterial = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(meshCollider.sharedMaterial);
					}
					break;
				case "tag":
					meshCollider.tag = reader.ReadProperty<string>();
					break;
				case "name":
					meshCollider.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					meshCollider.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
