using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_CanvasScaler : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(CanvasScaler);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			CanvasScaler canvasScaler = (CanvasScaler)value;
			writer.WriteProperty<CanvasScaler.ScaleMode>("uiScaleMode", canvasScaler.uiScaleMode);
			writer.WriteProperty<float>("referencePixelsPerUnit", canvasScaler.referencePixelsPerUnit);
			writer.WriteProperty<float>("scaleFactor", canvasScaler.scaleFactor);
			writer.WriteProperty<Vector2>("referenceResolution", canvasScaler.referenceResolution);
			writer.WriteProperty<CanvasScaler.ScreenMatchMode>("screenMatchMode", canvasScaler.screenMatchMode);
			writer.WriteProperty<float>("matchWidthOrHeight", canvasScaler.matchWidthOrHeight);
			writer.WriteProperty<CanvasScaler.Unit>("physicalUnit", canvasScaler.physicalUnit);
			writer.WriteProperty<float>("fallbackScreenDPI", canvasScaler.fallbackScreenDPI);
			writer.WriteProperty<float>("defaultSpriteDPI", canvasScaler.defaultSpriteDPI);
			writer.WriteProperty<float>("dynamicPixelsPerUnit", canvasScaler.dynamicPixelsPerUnit);
			writer.WriteProperty<bool>("useGUILayout", canvasScaler.useGUILayout);
			writer.WriteProperty<bool>("enabled", canvasScaler.enabled);
			writer.WriteProperty<string>("tag", canvasScaler.tag);
			writer.WriteProperty<string>("name", canvasScaler.name);
			writer.WriteProperty<HideFlags>("hideFlags", canvasScaler.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			CanvasScaler canvasScaler = SaveGameType.CreateComponent<CanvasScaler>();
			this.ReadInto(canvasScaler, reader);
			return canvasScaler;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			CanvasScaler canvasScaler = (CanvasScaler)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "uiScaleMode":
					canvasScaler.uiScaleMode = reader.ReadProperty<CanvasScaler.ScaleMode>();
					break;
				case "referencePixelsPerUnit":
					canvasScaler.referencePixelsPerUnit = reader.ReadProperty<float>();
					break;
				case "scaleFactor":
					canvasScaler.scaleFactor = reader.ReadProperty<float>();
					break;
				case "referenceResolution":
					canvasScaler.referenceResolution = reader.ReadProperty<Vector2>();
					break;
				case "screenMatchMode":
					canvasScaler.screenMatchMode = reader.ReadProperty<CanvasScaler.ScreenMatchMode>();
					break;
				case "matchWidthOrHeight":
					canvasScaler.matchWidthOrHeight = reader.ReadProperty<float>();
					break;
				case "physicalUnit":
					canvasScaler.physicalUnit = reader.ReadProperty<CanvasScaler.Unit>();
					break;
				case "fallbackScreenDPI":
					canvasScaler.fallbackScreenDPI = reader.ReadProperty<float>();
					break;
				case "defaultSpriteDPI":
					canvasScaler.defaultSpriteDPI = reader.ReadProperty<float>();
					break;
				case "dynamicPixelsPerUnit":
					canvasScaler.dynamicPixelsPerUnit = reader.ReadProperty<float>();
					break;
				case "useGUILayout":
					canvasScaler.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					canvasScaler.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					canvasScaler.tag = reader.ReadProperty<string>();
					break;
				case "name":
					canvasScaler.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					canvasScaler.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
