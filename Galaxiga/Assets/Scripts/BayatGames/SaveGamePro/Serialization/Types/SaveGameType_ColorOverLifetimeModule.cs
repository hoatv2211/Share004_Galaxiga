using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ColorOverLifetimeModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.ColorOverLifetimeModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = (ParticleSystem.ColorOverLifetimeModule)value;
			writer.WriteProperty<bool>("enabled", colorOverLifetimeModule.enabled);
			writer.WriteProperty<ParticleSystem.MinMaxGradient>("color", colorOverLifetimeModule.color);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = default(ParticleSystem.ColorOverLifetimeModule);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "enabled"))
					{
						if (text == "color")
						{
							colorOverLifetimeModule.color = reader.ReadProperty<ParticleSystem.MinMaxGradient>();
						}
					}
					else
					{
						colorOverLifetimeModule.enabled = reader.ReadProperty<bool>();
					}
				}
			}
			return colorOverLifetimeModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
