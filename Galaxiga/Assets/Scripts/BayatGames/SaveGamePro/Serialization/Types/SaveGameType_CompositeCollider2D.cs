using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_CompositeCollider2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(CompositeCollider2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			CompositeCollider2D compositeCollider2D = (CompositeCollider2D)value;
			writer.WriteProperty<CompositeCollider2D.GeometryType>("geometryType", compositeCollider2D.geometryType);
			writer.WriteProperty<CompositeCollider2D.GenerationType>("generationType", compositeCollider2D.generationType);
			writer.WriteProperty<float>("vertexDistance", compositeCollider2D.vertexDistance);
			writer.WriteProperty<float>("edgeRadius", compositeCollider2D.edgeRadius);
			writer.WriteProperty<float>("density", compositeCollider2D.density);
			writer.WriteProperty<bool>("isTrigger", compositeCollider2D.isTrigger);
			writer.WriteProperty<bool>("usedByEffector", compositeCollider2D.usedByEffector);
			writer.WriteProperty<bool>("usedByComposite", compositeCollider2D.usedByComposite);
			writer.WriteProperty<Vector2>("offset", compositeCollider2D.offset);
			writer.WriteProperty<PhysicsMaterial2D>("sharedMaterial", compositeCollider2D.sharedMaterial);
			writer.WriteProperty<bool>("enabled", compositeCollider2D.enabled);
			writer.WriteProperty<string>("tag", compositeCollider2D.tag);
			writer.WriteProperty<string>("name", compositeCollider2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", compositeCollider2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			CompositeCollider2D compositeCollider2D = SaveGameType.CreateComponent<CompositeCollider2D>();
			this.ReadInto(compositeCollider2D, reader);
			return compositeCollider2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			CompositeCollider2D compositeCollider2D = (CompositeCollider2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "geometryType":
					compositeCollider2D.geometryType = reader.ReadProperty<CompositeCollider2D.GeometryType>();
					break;
				case "generationType":
					compositeCollider2D.generationType = reader.ReadProperty<CompositeCollider2D.GenerationType>();
					break;
				case "vertexDistance":
					compositeCollider2D.vertexDistance = reader.ReadProperty<float>();
					break;
				case "edgeRadius":
					compositeCollider2D.edgeRadius = reader.ReadProperty<float>();
					break;
				case "density":
					compositeCollider2D.density = reader.ReadProperty<float>();
					break;
				case "isTrigger":
					compositeCollider2D.isTrigger = reader.ReadProperty<bool>();
					break;
				case "usedByEffector":
					compositeCollider2D.usedByEffector = reader.ReadProperty<bool>();
					break;
				case "usedByComposite":
					compositeCollider2D.usedByComposite = reader.ReadProperty<bool>();
					break;
				case "offset":
					compositeCollider2D.offset = reader.ReadProperty<Vector2>();
					break;
				case "sharedMaterial":
					if (compositeCollider2D.sharedMaterial == null)
					{
						compositeCollider2D.sharedMaterial = reader.ReadProperty<PhysicsMaterial2D>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial2D>(compositeCollider2D.sharedMaterial);
					}
					break;
				case "enabled":
					compositeCollider2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					compositeCollider2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					compositeCollider2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					compositeCollider2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
