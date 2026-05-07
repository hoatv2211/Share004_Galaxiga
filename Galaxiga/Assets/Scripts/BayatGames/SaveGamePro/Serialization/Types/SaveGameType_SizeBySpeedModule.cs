using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SizeBySpeedModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.SizeBySpeedModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.SizeBySpeedModule sizeBySpeedModule = (ParticleSystem.SizeBySpeedModule)value;
			writer.WriteProperty<bool>("enabled", sizeBySpeedModule.enabled);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("size", sizeBySpeedModule.size);
			writer.WriteProperty<float>("sizeMultiplier", sizeBySpeedModule.sizeMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("x", sizeBySpeedModule.x);
			writer.WriteProperty<float>("xMultiplier", sizeBySpeedModule.xMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("y", sizeBySpeedModule.y);
			writer.WriteProperty<float>("yMultiplier", sizeBySpeedModule.yMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("z", sizeBySpeedModule.z);
			writer.WriteProperty<float>("zMultiplier", sizeBySpeedModule.zMultiplier);
			writer.WriteProperty<bool>("separateAxes", sizeBySpeedModule.separateAxes);
			writer.WriteProperty<Vector2>("range", sizeBySpeedModule.range);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.SizeBySpeedModule sizeBySpeedModule = default(ParticleSystem.SizeBySpeedModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					sizeBySpeedModule.enabled = reader.ReadProperty<bool>();
					break;
				case "size":
					sizeBySpeedModule.size = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "sizeMultiplier":
					sizeBySpeedModule.sizeMultiplier = reader.ReadProperty<float>();
					break;
				case "x":
					sizeBySpeedModule.x = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "xMultiplier":
					sizeBySpeedModule.xMultiplier = reader.ReadProperty<float>();
					break;
				case "y":
					sizeBySpeedModule.y = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "yMultiplier":
					sizeBySpeedModule.yMultiplier = reader.ReadProperty<float>();
					break;
				case "z":
					sizeBySpeedModule.z = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "zMultiplier":
					sizeBySpeedModule.zMultiplier = reader.ReadProperty<float>();
					break;
				case "separateAxes":
					sizeBySpeedModule.separateAxes = reader.ReadProperty<bool>();
					break;
				case "range":
					sizeBySpeedModule.range = reader.ReadProperty<Vector2>();
					break;
				}
			}
			return sizeBySpeedModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
