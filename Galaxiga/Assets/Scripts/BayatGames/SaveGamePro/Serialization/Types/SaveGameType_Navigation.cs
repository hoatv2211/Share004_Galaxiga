using System;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Navigation : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Navigation);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Navigation navigation = (Navigation)value;
			writer.WriteProperty<Navigation.Mode>("mode", navigation.mode);
			writer.WriteProperty<Selectable>("selectOnUp", navigation.selectOnUp);
			writer.WriteProperty<Selectable>("selectOnDown", navigation.selectOnDown);
			writer.WriteProperty<Selectable>("selectOnLeft", navigation.selectOnLeft);
			writer.WriteProperty<Selectable>("selectOnRight", navigation.selectOnRight);
		}

		public override object Read(ISaveGameReader reader)
		{
			Navigation navigation = default(Navigation);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "mode"))
					{
						if (!(text == "selectOnUp"))
						{
							if (!(text == "selectOnDown"))
							{
								if (!(text == "selectOnLeft"))
								{
									if (text == "selectOnRight")
									{
										if (navigation.selectOnRight == null)
										{
											navigation.selectOnRight = reader.ReadProperty<Selectable>();
										}
										else
										{
											reader.ReadIntoProperty<Selectable>(navigation.selectOnRight);
										}
									}
								}
								else if (navigation.selectOnLeft == null)
								{
									navigation.selectOnLeft = reader.ReadProperty<Selectable>();
								}
								else
								{
									reader.ReadIntoProperty<Selectable>(navigation.selectOnLeft);
								}
							}
							else if (navigation.selectOnDown == null)
							{
								navigation.selectOnDown = reader.ReadProperty<Selectable>();
							}
							else
							{
								reader.ReadIntoProperty<Selectable>(navigation.selectOnDown);
							}
						}
						else if (navigation.selectOnUp == null)
						{
							navigation.selectOnUp = reader.ReadProperty<Selectable>();
						}
						else
						{
							reader.ReadIntoProperty<Selectable>(navigation.selectOnUp);
						}
					}
					else
					{
						navigation.mode = reader.ReadProperty<Navigation.Mode>();
					}
				}
			}
			return navigation;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
