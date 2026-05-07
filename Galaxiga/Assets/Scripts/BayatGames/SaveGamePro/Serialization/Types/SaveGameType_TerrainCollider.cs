using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_TerrainCollider : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(TerrainCollider);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			TerrainCollider terrainCollider = (TerrainCollider)value;
			writer.WriteProperty<TerrainData>("terrainData", terrainCollider.terrainData);
			writer.WriteProperty<bool>("enabled", terrainCollider.enabled);
			writer.WriteProperty<bool>("isTrigger", terrainCollider.isTrigger);
			writer.WriteProperty<float>("contactOffset", terrainCollider.contactOffset);
			writer.WriteProperty<PhysicsMaterial>("material", terrainCollider.material);
			writer.WriteProperty<PhysicsMaterial>("sharedMaterial", terrainCollider.sharedMaterial);
			writer.WriteProperty<string>("tag", terrainCollider.tag);
			writer.WriteProperty<string>("name", terrainCollider.name);
			writer.WriteProperty<HideFlags>("hideFlags", terrainCollider.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			TerrainCollider terrainCollider = SaveGameType.CreateComponent<TerrainCollider>();
			this.ReadInto(terrainCollider, reader);
			return terrainCollider;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			TerrainCollider terrainCollider = (TerrainCollider)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "terrainData":
					if (terrainCollider.terrainData == null)
					{
						terrainCollider.terrainData = reader.ReadProperty<TerrainData>();
					}
					else
					{
						reader.ReadIntoProperty<TerrainData>(terrainCollider.terrainData);
					}
					break;
				case "enabled":
					terrainCollider.enabled = reader.ReadProperty<bool>();
					break;
				case "isTrigger":
					terrainCollider.isTrigger = reader.ReadProperty<bool>();
					break;
				case "contactOffset":
					terrainCollider.contactOffset = reader.ReadProperty<float>();
					break;
				case "material":
					if (terrainCollider.material == null)
					{
						terrainCollider.material = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(terrainCollider.material);
					}
					break;
				case "sharedMaterial":
					if (terrainCollider.sharedMaterial == null)
					{
						terrainCollider.sharedMaterial = reader.ReadProperty<PhysicsMaterial>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial>(terrainCollider.sharedMaterial);
					}
					break;
				case "tag":
					terrainCollider.tag = reader.ReadProperty<string>();
					break;
				case "name":
					terrainCollider.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					terrainCollider.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
