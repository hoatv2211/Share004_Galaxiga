using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Flare : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Flare);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Flare flare = (Flare)value;
			writer.WriteProperty<string>("name", flare.name);
			writer.WriteProperty<HideFlags>("hideFlags", flare.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Flare flare = new Flare();
			this.ReadInto(flare, reader);
			return flare;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Flare flare = (Flare)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "name"))
					{
						if (text == "hideFlags")
						{
							flare.hideFlags = reader.ReadProperty<HideFlags>();
						}
					}
					else
					{
						flare.name = reader.ReadProperty<string>();
					}
				}
			}
		}
	}
}
