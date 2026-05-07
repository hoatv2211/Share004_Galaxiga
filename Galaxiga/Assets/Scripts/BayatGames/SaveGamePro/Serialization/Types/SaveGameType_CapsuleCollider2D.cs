using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_CapsuleCollider2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(CapsuleCollider2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			CapsuleCollider2D capsuleCollider2D = (CapsuleCollider2D)value;
			writer.WriteProperty<Vector2>("size", capsuleCollider2D.size);
			writer.WriteProperty<CapsuleDirection2D>("direction", capsuleCollider2D.direction);
			writer.WriteProperty<float>("density", capsuleCollider2D.density);
			writer.WriteProperty<bool>("isTrigger", capsuleCollider2D.isTrigger);
			writer.WriteProperty<bool>("usedByEffector", capsuleCollider2D.usedByEffector);
			writer.WriteProperty<bool>("usedByComposite", capsuleCollider2D.usedByComposite);
			writer.WriteProperty<Vector2>("offset", capsuleCollider2D.offset);
			writer.WriteProperty<PhysicsMaterial2D>("sharedMaterial", capsuleCollider2D.sharedMaterial);
			writer.WriteProperty<bool>("enabled", capsuleCollider2D.enabled);
			writer.WriteProperty<string>("tag", capsuleCollider2D.tag);
			writer.WriteProperty<string>("name", capsuleCollider2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", capsuleCollider2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			CapsuleCollider2D capsuleCollider2D = SaveGameType.CreateComponent<CapsuleCollider2D>();
			this.ReadInto(capsuleCollider2D, reader);
			return capsuleCollider2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			CapsuleCollider2D capsuleCollider2D = (CapsuleCollider2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "size":
					capsuleCollider2D.size = reader.ReadProperty<Vector2>();
					break;
				case "direction":
					capsuleCollider2D.direction = reader.ReadProperty<CapsuleDirection2D>();
					break;
				case "density":
					capsuleCollider2D.density = reader.ReadProperty<float>();
					break;
				case "isTrigger":
					capsuleCollider2D.isTrigger = reader.ReadProperty<bool>();
					break;
				case "usedByEffector":
					capsuleCollider2D.usedByEffector = reader.ReadProperty<bool>();
					break;
				case "usedByComposite":
					capsuleCollider2D.usedByComposite = reader.ReadProperty<bool>();
					break;
				case "offset":
					capsuleCollider2D.offset = reader.ReadProperty<Vector2>();
					break;
				case "sharedMaterial":
					if (capsuleCollider2D.sharedMaterial == null)
					{
						capsuleCollider2D.sharedMaterial = reader.ReadProperty<PhysicsMaterial2D>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial2D>(capsuleCollider2D.sharedMaterial);
					}
					break;
				case "enabled":
					capsuleCollider2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					capsuleCollider2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					capsuleCollider2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					capsuleCollider2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
