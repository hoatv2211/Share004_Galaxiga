using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Outline : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Outline);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Outline outline = (Outline)value;
			writer.WriteProperty<Color>("effectColor", outline.effectColor);
			writer.WriteProperty<Vector2>("effectDistance", outline.effectDistance);
			writer.WriteProperty<bool>("useGraphicAlpha", outline.useGraphicAlpha);
			writer.WriteProperty<bool>("useGUILayout", outline.useGUILayout);
			writer.WriteProperty<bool>("enabled", outline.enabled);
			writer.WriteProperty<string>("tag", outline.tag);
			writer.WriteProperty<string>("name", outline.name);
			writer.WriteProperty<HideFlags>("hideFlags", outline.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Outline outline = SaveGameType.CreateComponent<Outline>();
			this.ReadInto(outline, reader);
			return outline;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Outline outline = (Outline)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "effectColor":
					outline.effectColor = reader.ReadProperty<Color>();
					break;
				case "effectDistance":
					outline.effectDistance = reader.ReadProperty<Vector2>();
					break;
				case "useGraphicAlpha":
					outline.useGraphicAlpha = reader.ReadProperty<bool>();
					break;
				case "useGUILayout":
					outline.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					outline.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					outline.tag = reader.ReadProperty<string>();
					break;
				case "name":
					outline.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					outline.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
