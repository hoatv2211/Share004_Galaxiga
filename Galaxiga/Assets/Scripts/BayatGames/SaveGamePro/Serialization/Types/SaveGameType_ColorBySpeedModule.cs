using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ColorBySpeedModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.ColorBySpeedModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.ColorBySpeedModule colorBySpeedModule = (ParticleSystem.ColorBySpeedModule)value;
			writer.WriteProperty<bool>("enabled", colorBySpeedModule.enabled);
			writer.WriteProperty<ParticleSystem.MinMaxGradient>("color", colorBySpeedModule.color);
			writer.WriteProperty<Vector2>("range", colorBySpeedModule.range);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.ColorBySpeedModule colorBySpeedModule = default(ParticleSystem.ColorBySpeedModule);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "enabled"))
					{
						if (!(text == "color"))
						{
							if (text == "range")
							{
								colorBySpeedModule.range = reader.ReadProperty<Vector2>();
							}
						}
						else
						{
							colorBySpeedModule.color = reader.ReadProperty<ParticleSystem.MinMaxGradient>();
						}
					}
					else
					{
						colorBySpeedModule.enabled = reader.ReadProperty<bool>();
					}
				}
			}
			return colorBySpeedModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
