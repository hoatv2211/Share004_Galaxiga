using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_OptionDataList : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Dropdown.OptionDataList);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Dropdown.OptionDataList optionDataList = (Dropdown.OptionDataList)value;
			writer.WriteProperty<List<Dropdown.OptionData>>("options", optionDataList.options);
		}

		public override object Read(ISaveGameReader reader)
		{
			Dropdown.OptionDataList optionDataList = new Dropdown.OptionDataList();
			this.ReadInto(optionDataList, reader);
			return optionDataList;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Dropdown.OptionDataList optionDataList = (Dropdown.OptionDataList)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (text == "options")
					{
						optionDataList.options = reader.ReadProperty<List<Dropdown.OptionData>>();
					}
				}
			}
		}
	}
}
