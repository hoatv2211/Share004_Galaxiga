using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_MinMaxGradient : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.MinMaxGradient);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.MinMaxGradient minMaxGradient = (ParticleSystem.MinMaxGradient)value;
			writer.WriteProperty<ParticleSystemGradientMode>("mode", minMaxGradient.mode);
			writer.WriteProperty<Gradient>("gradientMax", minMaxGradient.gradientMax);
			writer.WriteProperty<Gradient>("gradientMin", minMaxGradient.gradientMin);
			writer.WriteProperty<Color>("colorMax", minMaxGradient.colorMax);
			writer.WriteProperty<Color>("colorMin", minMaxGradient.colorMin);
			writer.WriteProperty<Color>("color", minMaxGradient.color);
			writer.WriteProperty<Gradient>("gradient", minMaxGradient.gradient);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.MinMaxGradient minMaxGradient = default(ParticleSystem.MinMaxGradient);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "mode":
					minMaxGradient.mode = reader.ReadProperty<ParticleSystemGradientMode>();
					break;
				case "gradientMax":
					minMaxGradient.gradientMax = reader.ReadProperty<Gradient>();
					break;
				case "gradientMin":
					minMaxGradient.gradientMin = reader.ReadProperty<Gradient>();
					break;
				case "colorMax":
					minMaxGradient.colorMax = reader.ReadProperty<Color>();
					break;
				case "colorMin":
					minMaxGradient.colorMin = reader.ReadProperty<Color>();
					break;
				case "color":
					minMaxGradient.color = reader.ReadProperty<Color>();
					break;
				case "gradient":
					minMaxGradient.gradient = reader.ReadProperty<Gradient>();
					break;
				}
			}
			return minMaxGradient;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
