using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_PolygonCollider2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(PolygonCollider2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			PolygonCollider2D polygonCollider2D = (PolygonCollider2D)value;
			writer.WriteProperty<Vector2[]>("points", polygonCollider2D.points);
			writer.WriteProperty<int>("pathCount", polygonCollider2D.pathCount);
			writer.WriteProperty<bool>("autoTiling", polygonCollider2D.autoTiling);
			writer.WriteProperty<float>("density", polygonCollider2D.density);
			writer.WriteProperty<bool>("isTrigger", polygonCollider2D.isTrigger);
			writer.WriteProperty<bool>("usedByEffector", polygonCollider2D.usedByEffector);
			writer.WriteProperty<bool>("usedByComposite", polygonCollider2D.usedByComposite);
			writer.WriteProperty<Vector2>("offset", polygonCollider2D.offset);
			writer.WriteProperty<PhysicsMaterial2D>("sharedMaterial", polygonCollider2D.sharedMaterial);
			writer.WriteProperty<bool>("enabled", polygonCollider2D.enabled);
			writer.WriteProperty<string>("tag", polygonCollider2D.tag);
			writer.WriteProperty<string>("name", polygonCollider2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", polygonCollider2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			PolygonCollider2D polygonCollider2D = SaveGameType.CreateComponent<PolygonCollider2D>();
			this.ReadInto(polygonCollider2D, reader);
			return polygonCollider2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			PolygonCollider2D polygonCollider2D = (PolygonCollider2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "points":
					polygonCollider2D.points = reader.ReadProperty<Vector2[]>();
					break;
				case "pathCount":
					polygonCollider2D.pathCount = reader.ReadProperty<int>();
					break;
				case "autoTiling":
					polygonCollider2D.autoTiling = reader.ReadProperty<bool>();
					break;
				case "density":
					polygonCollider2D.density = reader.ReadProperty<float>();
					break;
				case "isTrigger":
					polygonCollider2D.isTrigger = reader.ReadProperty<bool>();
					break;
				case "usedByEffector":
					polygonCollider2D.usedByEffector = reader.ReadProperty<bool>();
					break;
				case "usedByComposite":
					polygonCollider2D.usedByComposite = reader.ReadProperty<bool>();
					break;
				case "offset":
					polygonCollider2D.offset = reader.ReadProperty<Vector2>();
					break;
				case "sharedMaterial":
					if (polygonCollider2D.sharedMaterial == null)
					{
						polygonCollider2D.sharedMaterial = reader.ReadProperty<PhysicsMaterial2D>();
					}
					else
					{
						reader.ReadIntoProperty<PhysicsMaterial2D>(polygonCollider2D.sharedMaterial);
					}
					break;
				case "enabled":
					polygonCollider2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					polygonCollider2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					polygonCollider2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					polygonCollider2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
