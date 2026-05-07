using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SortingGroup : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SortingGroup);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SortingGroup sortingGroup = (SortingGroup)value;
			writer.WriteProperty<string>("sortingLayerName", sortingGroup.sortingLayerName);
			writer.WriteProperty<int>("sortingLayerID", sortingGroup.sortingLayerID);
			writer.WriteProperty<int>("sortingOrder", sortingGroup.sortingOrder);
			writer.WriteProperty<bool>("enabled", sortingGroup.enabled);
			writer.WriteProperty<string>("tag", sortingGroup.tag);
			writer.WriteProperty<string>("name", sortingGroup.name);
			writer.WriteProperty<HideFlags>("hideFlags", sortingGroup.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			SortingGroup sortingGroup = SaveGameType.CreateComponent<SortingGroup>();
			this.ReadInto(sortingGroup, reader);
			return sortingGroup;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			SortingGroup sortingGroup = (SortingGroup)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "sortingLayerName":
					sortingGroup.sortingLayerName = reader.ReadProperty<string>();
					break;
				case "sortingLayerID":
					sortingGroup.sortingLayerID = reader.ReadProperty<int>();
					break;
				case "sortingOrder":
					sortingGroup.sortingOrder = reader.ReadProperty<int>();
					break;
				case "enabled":
					sortingGroup.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					sortingGroup.tag = reader.ReadProperty<string>();
					break;
				case "name":
					sortingGroup.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					sortingGroup.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
