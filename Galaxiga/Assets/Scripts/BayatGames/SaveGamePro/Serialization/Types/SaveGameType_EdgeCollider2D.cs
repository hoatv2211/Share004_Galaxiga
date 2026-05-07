using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_EdgeCollider2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(EdgeCollider2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			EdgeCollider2D edgeCollider2D = (EdgeCollider2D)value;
			writer.WriteProperty<float>("edgeRadius", edgeCollider2D.edgeRadius);
			writer.WriteProperty<Vector2[]>("points", edgeCollider2D.points);
			writer.WriteProperty<float>("density", edgeCollider2D.density);
			writer.WriteProperty<bool>("isTrigger", edgeCollider2D.isTrigger);
			writer.WriteProperty<bool>("usedByEffector", edgeCollider2D.usedByEffector);
			writer.WriteProperty<bool>("usedByComposite", edgeCollider2D.usedByComposite);
			writer.WriteProperty<Vector2>("offset", edgeCollider2D.offset);
			writer.WriteProperty<PhysicsMaterial2D>("sharedMaterial", edgeCollider2D.sharedMaterial);
			writer.WriteProperty<bool>("enabled", edgeCollider2D.enabled);
			writer.WriteProperty<string>("tag", edgeCollider2D.tag);
			writer.WriteProperty<string>("name", edgeCollider2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", edgeCollider2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			EdgeCollider2D edgeCollider2D = SaveGameType.CreateComponent<EdgeCollider2D>();
			this.ReadInto(edgeCollider2D, reader);
			return edgeCollider2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			EdgeCollider2D edgeCollider2D = (EdgeCollider2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "edgeRadius":
					edgeCollider2D.edgeRadius = reader.ReadProperty<float>();
					break;
				case "points":
					edgeCollider2D.points = reader.ReadProperty<Vector2[]>();
					break;
				case "density":
					edgeCollider2D.density = reader.ReadProperty<float>();
					break;
				case "isTrigger":
					edgeCollider2D.isTrigger = reader.ReadProperty<bool>();
					break;
				case "usedByEffector":
					edgeCollider2D.usedByEffector = reader.ReadProperty<bool>();
					break;
				case "usedByComposite":
					edgeCollider2D.usedByComposite = reader.ReadProperty<bool>();
					break;
				case "offset":
					edgeCollider2D.offset = reader.ReadProperty<Vector2>();
					break;
				case "sharedMaterial":
					if (edgeCollider2D.sharedMaterial == null)
					{
						edgeCollider2D.sharedMaterial = reader.ReadProperty<PhysicsMaterial2D>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial2D>(edgeCollider2D.sharedMaterial);
					}
					break;
				case "enabled":
					edgeCollider2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					edgeCollider2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					edgeCollider2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					edgeCollider2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
