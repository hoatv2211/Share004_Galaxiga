using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_RotationBySpeedModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.RotationBySpeedModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.RotationBySpeedModule rotationBySpeedModule = (ParticleSystem.RotationBySpeedModule)value;
			writer.WriteProperty<bool>("enabled", rotationBySpeedModule.enabled);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("x", rotationBySpeedModule.x);
			writer.WriteProperty<float>("xMultiplier", rotationBySpeedModule.xMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("y", rotationBySpeedModule.y);
			writer.WriteProperty<float>("yMultiplier", rotationBySpeedModule.yMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("z", rotationBySpeedModule.z);
			writer.WriteProperty<float>("zMultiplier", rotationBySpeedModule.zMultiplier);
			writer.WriteProperty<bool>("separateAxes", rotationBySpeedModule.separateAxes);
			writer.WriteProperty<Vector2>("range", rotationBySpeedModule.range);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.RotationBySpeedModule rotationBySpeedModule = default(ParticleSystem.RotationBySpeedModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					rotationBySpeedModule.enabled = reader.ReadProperty<bool>();
					break;
				case "x":
					rotationBySpeedModule.x = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "xMultiplier":
					rotationBySpeedModule.xMultiplier = reader.ReadProperty<float>();
					break;
				case "y":
					rotationBySpeedModule.y = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "yMultiplier":
					rotationBySpeedModule.yMultiplier = reader.ReadProperty<float>();
					break;
				case "z":
					rotationBySpeedModule.z = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "zMultiplier":
					rotationBySpeedModule.zMultiplier = reader.ReadProperty<float>();
					break;
				case "separateAxes":
					rotationBySpeedModule.separateAxes = reader.ReadProperty<bool>();
					break;
				case "range":
					rotationBySpeedModule.range = reader.ReadProperty<Vector2>();
					break;
				}
			}
			return rotationBySpeedModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
