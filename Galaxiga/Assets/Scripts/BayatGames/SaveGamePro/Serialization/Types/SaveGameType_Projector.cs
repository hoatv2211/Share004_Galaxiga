using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Projector : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Projector);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Projector projector = (Projector)value;
			writer.WriteProperty<float>("nearClipPlane", projector.nearClipPlane);
			writer.WriteProperty<float>("farClipPlane", projector.farClipPlane);
			writer.WriteProperty<float>("fieldOfView", projector.fieldOfView);
			writer.WriteProperty<float>("aspectRatio", projector.aspectRatio);
			writer.WriteProperty<bool>("orthographic", projector.orthographic);
			writer.WriteProperty<float>("orthographicSize", projector.orthographicSize);
			writer.WriteProperty<int>("ignoreLayers", projector.ignoreLayers);
			writer.WriteProperty<Material>("material", projector.material);
			writer.WriteProperty<bool>("enabled", projector.enabled);
			writer.WriteProperty<string>("tag", projector.tag);
			writer.WriteProperty<string>("name", projector.name);
			writer.WriteProperty<HideFlags>("hideFlags", projector.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Projector projector = SaveGameType.CreateComponent<Projector>();
			this.ReadInto(projector, reader);
			return projector;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Projector projector = (Projector)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "nearClipPlane":
					projector.nearClipPlane = reader.ReadProperty<float>();
					break;
				case "farClipPlane":
					projector.farClipPlane = reader.ReadProperty<float>();
					break;
				case "fieldOfView":
					projector.fieldOfView = reader.ReadProperty<float>();
					break;
				case "aspectRatio":
					projector.aspectRatio = reader.ReadProperty<float>();
					break;
				case "orthographic":
					projector.orthographic = reader.ReadProperty<bool>();
					break;
				case "orthographicSize":
					projector.orthographicSize = reader.ReadProperty<float>();
					break;
				case "ignoreLayers":
					projector.ignoreLayers = reader.ReadProperty<int>();
					break;
				case "material":
					if (projector.material == null)
					{
						projector.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(projector.material);
					}
					break;
				case "enabled":
					projector.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					projector.tag = reader.ReadProperty<string>();
					break;
				case "name":
					projector.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					projector.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
