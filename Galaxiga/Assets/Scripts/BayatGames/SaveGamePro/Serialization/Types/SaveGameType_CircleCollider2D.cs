using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_CircleCollider2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(CircleCollider2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			CircleCollider2D circleCollider2D = (CircleCollider2D)value;
			writer.WriteProperty<float>("radius", circleCollider2D.radius);
			writer.WriteProperty<float>("density", circleCollider2D.density);
			writer.WriteProperty<bool>("isTrigger", circleCollider2D.isTrigger);
			writer.WriteProperty<bool>("usedByEffector", circleCollider2D.usedByEffector);
			writer.WriteProperty<bool>("usedByComposite", circleCollider2D.usedByComposite);
			writer.WriteProperty<Vector2>("offset", circleCollider2D.offset);
			writer.WriteProperty<PhysicsMaterial2D>("sharedMaterial", circleCollider2D.sharedMaterial);
			writer.WriteProperty<bool>("enabled", circleCollider2D.enabled);
			writer.WriteProperty<string>("tag", circleCollider2D.tag);
			writer.WriteProperty<string>("name", circleCollider2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", circleCollider2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			CircleCollider2D circleCollider2D = SaveGameType.CreateComponent<CircleCollider2D>();
			this.ReadInto(circleCollider2D, reader);
			return circleCollider2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			CircleCollider2D circleCollider2D = (CircleCollider2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "radius":
					circleCollider2D.radius = reader.ReadProperty<float>();
					break;
				case "density":
					circleCollider2D.density = reader.ReadProperty<float>();
					break;
				case "isTrigger":
					circleCollider2D.isTrigger = reader.ReadProperty<bool>();
					break;
				case "usedByEffector":
					circleCollider2D.usedByEffector = reader.ReadProperty<bool>();
					break;
				case "usedByComposite":
					circleCollider2D.usedByComposite = reader.ReadProperty<bool>();
					break;
				case "offset":
					circleCollider2D.offset = reader.ReadProperty<Vector2>();
					break;
				case "sharedMaterial":
					if (circleCollider2D.sharedMaterial == null)
					{
						circleCollider2D.sharedMaterial = reader.ReadProperty<PhysicsMaterial2D>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial2D>(circleCollider2D.sharedMaterial);
					}
					break;
				case "enabled":
					circleCollider2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					circleCollider2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					circleCollider2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					circleCollider2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
