using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AspectRatioFitter : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AspectRatioFitter);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AspectRatioFitter aspectRatioFitter = (AspectRatioFitter)value;
			writer.WriteProperty<AspectRatioFitter.AspectMode>("aspectMode", aspectRatioFitter.aspectMode);
			writer.WriteProperty<float>("aspectRatio", aspectRatioFitter.aspectRatio);
			writer.WriteProperty<bool>("useGUILayout", aspectRatioFitter.useGUILayout);
			writer.WriteProperty<bool>("enabled", aspectRatioFitter.enabled);
			writer.WriteProperty<string>("tag", aspectRatioFitter.tag);
			writer.WriteProperty<string>("name", aspectRatioFitter.name);
			writer.WriteProperty<HideFlags>("hideFlags", aspectRatioFitter.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AspectRatioFitter aspectRatioFitter = SaveGameType.CreateComponent<AspectRatioFitter>();
			this.ReadInto(aspectRatioFitter, reader);
			return aspectRatioFitter;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AspectRatioFitter aspectRatioFitter = (AspectRatioFitter)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "aspectMode":
					aspectRatioFitter.aspectMode = reader.ReadProperty<AspectRatioFitter.AspectMode>();
					break;
				case "aspectRatio":
					aspectRatioFitter.aspectRatio = reader.ReadProperty<float>();
					break;
				case "useGUILayout":
					aspectRatioFitter.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					aspectRatioFitter.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					aspectRatioFitter.tag = reader.ReadProperty<string>();
					break;
				case "name":
					aspectRatioFitter.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					aspectRatioFitter.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
