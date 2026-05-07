using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_GraphicRaycaster : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(GraphicRaycaster);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			GraphicRaycaster graphicRaycaster = (GraphicRaycaster)value;
			writer.WriteProperty<bool>("ignoreReversedGraphics", graphicRaycaster.ignoreReversedGraphics);
			writer.WriteProperty<GraphicRaycaster.BlockingObjects>("blockingObjects", graphicRaycaster.blockingObjects);
			writer.WriteProperty<bool>("useGUILayout", graphicRaycaster.useGUILayout);
			writer.WriteProperty<bool>("enabled", graphicRaycaster.enabled);
			writer.WriteProperty<string>("tag", graphicRaycaster.tag);
			writer.WriteProperty<string>("name", graphicRaycaster.name);
			writer.WriteProperty<HideFlags>("hideFlags", graphicRaycaster.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			GraphicRaycaster graphicRaycaster = SaveGameType.CreateComponent<GraphicRaycaster>();
			this.ReadInto(graphicRaycaster, reader);
			return graphicRaycaster;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			GraphicRaycaster graphicRaycaster = (GraphicRaycaster)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "ignoreReversedGraphics":
					graphicRaycaster.ignoreReversedGraphics = reader.ReadProperty<bool>();
					break;
				case "blockingObjects":
					graphicRaycaster.blockingObjects = reader.ReadProperty<GraphicRaycaster.BlockingObjects>();
					break;
				case "useGUILayout":
					graphicRaycaster.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					graphicRaycaster.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					graphicRaycaster.tag = reader.ReadProperty<string>();
					break;
				case "name":
					graphicRaycaster.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					graphicRaycaster.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
