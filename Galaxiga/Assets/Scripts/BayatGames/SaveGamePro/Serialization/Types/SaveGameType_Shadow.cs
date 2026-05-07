using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Shadow : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Shadow);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Shadow shadow = (Shadow)value;
			writer.WriteProperty<Color>("effectColor", shadow.effectColor);
			writer.WriteProperty<Vector2>("effectDistance", shadow.effectDistance);
			writer.WriteProperty<bool>("useGraphicAlpha", shadow.useGraphicAlpha);
			writer.WriteProperty<bool>("useGUILayout", shadow.useGUILayout);
			writer.WriteProperty<bool>("enabled", shadow.enabled);
			writer.WriteProperty<string>("tag", shadow.tag);
			writer.WriteProperty<string>("name", shadow.name);
			writer.WriteProperty<HideFlags>("hideFlags", shadow.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Shadow shadow = SaveGameType.CreateComponent<Shadow>();
			this.ReadInto(shadow, reader);
			return shadow;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Shadow shadow = (Shadow)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "effectColor":
					shadow.effectColor = reader.ReadProperty<Color>();
					break;
				case "effectDistance":
					shadow.effectDistance = reader.ReadProperty<Vector2>();
					break;
				case "useGraphicAlpha":
					shadow.useGraphicAlpha = reader.ReadProperty<bool>();
					break;
				case "useGUILayout":
					shadow.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					shadow.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					shadow.tag = reader.ReadProperty<string>();
					break;
				case "name":
					shadow.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					shadow.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
