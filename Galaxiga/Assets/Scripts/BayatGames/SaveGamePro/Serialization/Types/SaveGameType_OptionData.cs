using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_OptionData : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Dropdown.OptionData);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Dropdown.OptionData optionData = (Dropdown.OptionData)value;
			writer.WriteProperty<string>("text", optionData.text);
			writer.WriteProperty<Sprite>("image", optionData.image);
		}

		public override object Read(ISaveGameReader reader)
		{
			Dropdown.OptionData optionData = new Dropdown.OptionData();
			this.ReadInto(optionData, reader);
			return optionData;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Dropdown.OptionData optionData = (Dropdown.OptionData)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "text"))
					{
						if (text == "image")
						{
							if (optionData.image == null)
							{
								optionData.image = reader.ReadProperty<Sprite>();
							}
							else
							{
								reader.ReadIntoProperty<Sprite>(optionData.image);
							}
						}
					}
					else
					{
						optionData.text = reader.ReadProperty<string>();
					}
				}
			}
		}
	}
}
