using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_BoxCollider2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(BoxCollider2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			BoxCollider2D boxCollider2D = (BoxCollider2D)value;
			writer.WriteProperty<Vector2>("size", boxCollider2D.size);
			writer.WriteProperty<float>("edgeRadius", boxCollider2D.edgeRadius);
			writer.WriteProperty<bool>("autoTiling", boxCollider2D.autoTiling);
			writer.WriteProperty<float>("density", boxCollider2D.density);
			writer.WriteProperty<bool>("isTrigger", boxCollider2D.isTrigger);
			writer.WriteProperty<bool>("usedByEffector", boxCollider2D.usedByEffector);
			writer.WriteProperty<bool>("usedByComposite", boxCollider2D.usedByComposite);
			writer.WriteProperty<Vector2>("offset", boxCollider2D.offset);
			writer.WriteProperty<PhysicsMaterial2D>("sharedMaterial", boxCollider2D.sharedMaterial);
			writer.WriteProperty<bool>("enabled", boxCollider2D.enabled);
			writer.WriteProperty<string>("tag", boxCollider2D.tag);
			writer.WriteProperty<string>("name", boxCollider2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", boxCollider2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			BoxCollider2D boxCollider2D = SaveGameType.CreateComponent<BoxCollider2D>();
			this.ReadInto(boxCollider2D, reader);
			return boxCollider2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			BoxCollider2D boxCollider2D = (BoxCollider2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "size":
					boxCollider2D.size = reader.ReadProperty<Vector2>();
					break;
				case "edgeRadius":
					boxCollider2D.edgeRadius = reader.ReadProperty<float>();
					break;
				case "autoTiling":
					boxCollider2D.autoTiling = reader.ReadProperty<bool>();
					break;
				case "density":
					boxCollider2D.density = reader.ReadProperty<float>();
					break;
				case "isTrigger":
					boxCollider2D.isTrigger = reader.ReadProperty<bool>();
					break;
				case "usedByEffector":
					boxCollider2D.usedByEffector = reader.ReadProperty<bool>();
					break;
				case "usedByComposite":
					boxCollider2D.usedByComposite = reader.ReadProperty<bool>();
					break;
				case "offset":
					boxCollider2D.offset = reader.ReadProperty<Vector2>();
					break;
				case "sharedMaterial":
					if (boxCollider2D.sharedMaterial == null)
					{
						boxCollider2D.sharedMaterial = reader.ReadProperty<PhysicsMaterial2D>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial2D>(boxCollider2D.sharedMaterial);
					}
					break;
				case "enabled":
					boxCollider2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					boxCollider2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					boxCollider2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					boxCollider2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
