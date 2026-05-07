using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_PointEffector2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(PointEffector2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			PointEffector2D pointEffector2D = (PointEffector2D)value;
			writer.WriteProperty<float>("forceMagnitude", pointEffector2D.forceMagnitude);
			writer.WriteProperty<float>("forceVariation", pointEffector2D.forceVariation);
			writer.WriteProperty<float>("distanceScale", pointEffector2D.distanceScale);
			writer.WriteProperty<float>("drag", pointEffector2D.linearDamping);
			writer.WriteProperty<float>("angularDrag", pointEffector2D.angularDamping);
			writer.WriteProperty<EffectorSelection2D>("forceSource", pointEffector2D.forceSource);
			writer.WriteProperty<EffectorSelection2D>("forceTarget", pointEffector2D.forceTarget);
			writer.WriteProperty<EffectorForceMode2D>("forceMode", pointEffector2D.forceMode);
			writer.WriteProperty<bool>("useColliderMask", pointEffector2D.useColliderMask);
			writer.WriteProperty<int>("colliderMask", pointEffector2D.colliderMask);
			writer.WriteProperty<bool>("enabled", pointEffector2D.enabled);
			writer.WriteProperty<string>("tag", pointEffector2D.tag);
			writer.WriteProperty<string>("name", pointEffector2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", pointEffector2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			PointEffector2D pointEffector2D = SaveGameType.CreateComponent<PointEffector2D>();
			this.ReadInto(pointEffector2D, reader);
			return pointEffector2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			PointEffector2D pointEffector2D = (PointEffector2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "forceMagnitude":
					pointEffector2D.forceMagnitude = reader.ReadProperty<float>();
					break;
				case "forceVariation":
					pointEffector2D.forceVariation = reader.ReadProperty<float>();
					break;
				case "distanceScale":
					pointEffector2D.distanceScale = reader.ReadProperty<float>();
					break;
				case "drag":
					pointEffector2D.linearDamping = reader.ReadProperty<float>();
					break;
				case "angularDrag":
					pointEffector2D.angularDamping = reader.ReadProperty<float>();
					break;
				case "forceSource":
					pointEffector2D.forceSource = reader.ReadProperty<EffectorSelection2D>();
					break;
				case "forceTarget":
					pointEffector2D.forceTarget = reader.ReadProperty<EffectorSelection2D>();
					break;
				case "forceMode":
					pointEffector2D.forceMode = reader.ReadProperty<EffectorForceMode2D>();
					break;
				case "useColliderMask":
					pointEffector2D.useColliderMask = reader.ReadProperty<bool>();
					break;
				case "colliderMask":
					pointEffector2D.colliderMask = reader.ReadProperty<int>();
					break;
				case "enabled":
					pointEffector2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					pointEffector2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					pointEffector2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					pointEffector2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
