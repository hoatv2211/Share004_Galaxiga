using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_MinMaxCurve : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.MinMaxCurve);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.MinMaxCurve minMaxCurve = (ParticleSystem.MinMaxCurve)value;
			writer.WriteProperty<ParticleSystemCurveMode>("mode", minMaxCurve.mode);
			writer.WriteProperty<float>("curveMultiplier", minMaxCurve.curveMultiplier);
			writer.WriteProperty<AnimationCurve>("curveMax", minMaxCurve.curveMax);
			writer.WriteProperty<AnimationCurve>("curveMin", minMaxCurve.curveMin);
			writer.WriteProperty<float>("constantMax", minMaxCurve.constantMax);
			writer.WriteProperty<float>("constantMin", minMaxCurve.constantMin);
			writer.WriteProperty<float>("constant", minMaxCurve.constant);
			writer.WriteProperty<AnimationCurve>("curve", minMaxCurve.curve);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.MinMaxCurve minMaxCurve = default(ParticleSystem.MinMaxCurve);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "mode":
					minMaxCurve.mode = reader.ReadProperty<ParticleSystemCurveMode>();
					break;
				case "curveMultiplier":
					minMaxCurve.curveMultiplier = reader.ReadProperty<float>();
					break;
				case "curveMax":
					minMaxCurve.curveMax = reader.ReadProperty<AnimationCurve>();
					break;
				case "curveMin":
					minMaxCurve.curveMin = reader.ReadProperty<AnimationCurve>();
					break;
				case "constantMax":
					minMaxCurve.constantMax = reader.ReadProperty<float>();
					break;
				case "constantMin":
					minMaxCurve.constantMin = reader.ReadProperty<float>();
					break;
				case "constant":
					minMaxCurve.constant = reader.ReadProperty<float>();
					break;
				case "curve":
					minMaxCurve.curve = reader.ReadProperty<AnimationCurve>();
					break;
				}
			}
			return minMaxCurve;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
