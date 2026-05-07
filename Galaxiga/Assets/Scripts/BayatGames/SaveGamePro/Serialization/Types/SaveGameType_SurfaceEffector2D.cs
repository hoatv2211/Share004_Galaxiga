using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SurfaceEffector2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SurfaceEffector2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SurfaceEffector2D surfaceEffector2D = (SurfaceEffector2D)value;
			writer.WriteProperty<float>("speed", surfaceEffector2D.speed);
			writer.WriteProperty<float>("speedVariation", surfaceEffector2D.speedVariation);
			writer.WriteProperty<float>("forceScale", surfaceEffector2D.forceScale);
			writer.WriteProperty<bool>("useContactForce", surfaceEffector2D.useContactForce);
			writer.WriteProperty<bool>("useFriction", surfaceEffector2D.useFriction);
			writer.WriteProperty<bool>("useBounce", surfaceEffector2D.useBounce);
			writer.WriteProperty<bool>("useColliderMask", surfaceEffector2D.useColliderMask);
			writer.WriteProperty<int>("colliderMask", surfaceEffector2D.colliderMask);
			writer.WriteProperty<bool>("enabled", surfaceEffector2D.enabled);
			writer.WriteProperty<string>("tag", surfaceEffector2D.tag);
			writer.WriteProperty<string>("name", surfaceEffector2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", surfaceEffector2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			SurfaceEffector2D surfaceEffector2D = SaveGameType.CreateComponent<SurfaceEffector2D>();
			this.ReadInto(surfaceEffector2D, reader);
			return surfaceEffector2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			SurfaceEffector2D surfaceEffector2D = (SurfaceEffector2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "speed":
					surfaceEffector2D.speed = reader.ReadProperty<float>();
					break;
				case "speedVariation":
					surfaceEffector2D.speedVariation = reader.ReadProperty<float>();
					break;
				case "forceScale":
					surfaceEffector2D.forceScale = reader.ReadProperty<float>();
					break;
				case "useContactForce":
					surfaceEffector2D.useContactForce = reader.ReadProperty<bool>();
					break;
				case "useFriction":
					surfaceEffector2D.useFriction = reader.ReadProperty<bool>();
					break;
				case "useBounce":
					surfaceEffector2D.useBounce = reader.ReadProperty<bool>();
					break;
				case "useColliderMask":
					surfaceEffector2D.useColliderMask = reader.ReadProperty<bool>();
					break;
				case "colliderMask":
					surfaceEffector2D.colliderMask = reader.ReadProperty<int>();
					break;
				case "enabled":
					surfaceEffector2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					surfaceEffector2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					surfaceEffector2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					surfaceEffector2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
