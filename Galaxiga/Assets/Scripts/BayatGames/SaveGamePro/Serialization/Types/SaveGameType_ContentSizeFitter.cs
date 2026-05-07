using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ContentSizeFitter : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ContentSizeFitter);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ContentSizeFitter contentSizeFitter = (ContentSizeFitter)value;
			writer.WriteProperty<ContentSizeFitter.FitMode>("horizontalFit", contentSizeFitter.horizontalFit);
			writer.WriteProperty<ContentSizeFitter.FitMode>("verticalFit", contentSizeFitter.verticalFit);
			writer.WriteProperty<bool>("useGUILayout", contentSizeFitter.useGUILayout);
			writer.WriteProperty<bool>("enabled", contentSizeFitter.enabled);
			writer.WriteProperty<string>("tag", contentSizeFitter.tag);
			writer.WriteProperty<string>("name", contentSizeFitter.name);
			writer.WriteProperty<HideFlags>("hideFlags", contentSizeFitter.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			ContentSizeFitter contentSizeFitter = SaveGameType.CreateComponent<ContentSizeFitter>();
			this.ReadInto(contentSizeFitter, reader);
			return contentSizeFitter;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			ContentSizeFitter contentSizeFitter = (ContentSizeFitter)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "horizontalFit":
					contentSizeFitter.horizontalFit = reader.ReadProperty<ContentSizeFitter.FitMode>();
					break;
				case "verticalFit":
					contentSizeFitter.verticalFit = reader.ReadProperty<ContentSizeFitter.FitMode>();
					break;
				case "useGUILayout":
					contentSizeFitter.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					contentSizeFitter.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					contentSizeFitter.tag = reader.ReadProperty<string>();
					break;
				case "name":
					contentSizeFitter.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					contentSizeFitter.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
