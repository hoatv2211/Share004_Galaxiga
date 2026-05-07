using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Collider2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Collider2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Collider2D collider2D = (Collider2D)value;
			writer.WriteProperty<float>("density", collider2D.density);
			writer.WriteProperty<bool>("isTrigger", collider2D.isTrigger);
			writer.WriteProperty<bool>("usedByEffector", collider2D.usedByEffector);
			writer.WriteProperty<bool>("usedByComposite", collider2D.usedByComposite);
			writer.WriteProperty<Vector2>("offset", collider2D.offset);
			writer.WriteProperty<PhysicsMaterial2D>("sharedMaterial", collider2D.sharedMaterial);
			writer.WriteProperty<bool>("enabled", collider2D.enabled);
			writer.WriteProperty<string>("tag", collider2D.tag);
			writer.WriteProperty<string>("name", collider2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", collider2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Collider2D collider2D = SaveGameType.CreateComponent<Collider2D>();
			this.ReadInto(collider2D, reader);
			return collider2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Collider2D collider2D = (Collider2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "density":
					collider2D.density = reader.ReadProperty<float>();
					break;
				case "isTrigger":
					collider2D.isTrigger = reader.ReadProperty<bool>();
					break;
				case "usedByEffector":
					collider2D.usedByEffector = reader.ReadProperty<bool>();
					break;
				case "usedByComposite":
					collider2D.usedByComposite = reader.ReadProperty<bool>();
					break;
				case "offset":
					collider2D.offset = reader.ReadProperty<Vector2>();
					break;
				case "sharedMaterial":
					if (collider2D.sharedMaterial == null)
					{
						collider2D.sharedMaterial = reader.ReadProperty<PhysicsMaterial2D>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial2D>(collider2D.sharedMaterial);
					}
					break;
				case "enabled":
					collider2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					collider2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					collider2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					collider2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
