using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_GridLayoutGroup : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(GridLayoutGroup);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			GridLayoutGroup gridLayoutGroup = (GridLayoutGroup)value;
			writer.WriteProperty<GridLayoutGroup.Corner>("startCorner", gridLayoutGroup.startCorner);
			writer.WriteProperty<GridLayoutGroup.Axis>("startAxis", gridLayoutGroup.startAxis);
			writer.WriteProperty<Vector2>("cellSize", gridLayoutGroup.cellSize);
			writer.WriteProperty<Vector2>("spacing", gridLayoutGroup.spacing);
			writer.WriteProperty<GridLayoutGroup.Constraint>("constraint", gridLayoutGroup.constraint);
			writer.WriteProperty<int>("constraintCount", gridLayoutGroup.constraintCount);
			writer.WriteProperty<RectOffset>("padding", gridLayoutGroup.padding);
			writer.WriteProperty<TextAnchor>("childAlignment", gridLayoutGroup.childAlignment);
			writer.WriteProperty<bool>("useGUILayout", gridLayoutGroup.useGUILayout);
			writer.WriteProperty<bool>("enabled", gridLayoutGroup.enabled);
			writer.WriteProperty<string>("tag", gridLayoutGroup.tag);
			writer.WriteProperty<string>("name", gridLayoutGroup.name);
			writer.WriteProperty<HideFlags>("hideFlags", gridLayoutGroup.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			GridLayoutGroup gridLayoutGroup = SaveGameType.CreateComponent<GridLayoutGroup>();
			this.ReadInto(gridLayoutGroup, reader);
			return gridLayoutGroup;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			GridLayoutGroup gridLayoutGroup = (GridLayoutGroup)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "startCorner":
					gridLayoutGroup.startCorner = reader.ReadProperty<GridLayoutGroup.Corner>();
					break;
				case "startAxis":
					gridLayoutGroup.startAxis = reader.ReadProperty<GridLayoutGroup.Axis>();
					break;
				case "cellSize":
					gridLayoutGroup.cellSize = reader.ReadProperty<Vector2>();
					break;
				case "spacing":
					gridLayoutGroup.spacing = reader.ReadProperty<Vector2>();
					break;
				case "constraint":
					gridLayoutGroup.constraint = reader.ReadProperty<GridLayoutGroup.Constraint>();
					break;
				case "constraintCount":
					gridLayoutGroup.constraintCount = reader.ReadProperty<int>();
					break;
				case "padding":
					gridLayoutGroup.padding = reader.ReadProperty<RectOffset>();
					break;
				case "childAlignment":
					gridLayoutGroup.childAlignment = reader.ReadProperty<TextAnchor>();
					break;
				case "useGUILayout":
					gridLayoutGroup.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					gridLayoutGroup.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					gridLayoutGroup.tag = reader.ReadProperty<string>();
					break;
				case "name":
					gridLayoutGroup.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					gridLayoutGroup.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
