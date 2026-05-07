using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_RotationOverLifetimeModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.RotationOverLifetimeModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.RotationOverLifetimeModule rotationOverLifetimeModule = (ParticleSystem.RotationOverLifetimeModule)value;
			writer.WriteProperty<bool>("enabled", rotationOverLifetimeModule.enabled);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("x", rotationOverLifetimeModule.x);
			writer.WriteProperty<float>("xMultiplier", rotationOverLifetimeModule.xMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("y", rotationOverLifetimeModule.y);
			writer.WriteProperty<float>("yMultiplier", rotationOverLifetimeModule.yMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("z", rotationOverLifetimeModule.z);
			writer.WriteProperty<float>("zMultiplier", rotationOverLifetimeModule.zMultiplier);
			writer.WriteProperty<bool>("separateAxes", rotationOverLifetimeModule.separateAxes);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.RotationOverLifetimeModule rotationOverLifetimeModule = default(ParticleSystem.RotationOverLifetimeModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					rotationOverLifetimeModule.enabled = reader.ReadProperty<bool>();
					break;
				case "x":
					rotationOverLifetimeModule.x = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "xMultiplier":
					rotationOverLifetimeModule.xMultiplier = reader.ReadProperty<float>();
					break;
				case "y":
					rotationOverLifetimeModule.y = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "yMultiplier":
					rotationOverLifetimeModule.yMultiplier = reader.ReadProperty<float>();
					break;
				case "z":
					rotationOverLifetimeModule.z = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "zMultiplier":
					rotationOverLifetimeModule.zMultiplier = reader.ReadProperty<float>();
					break;
				case "separateAxes":
					rotationOverLifetimeModule.separateAxes = reader.ReadProperty<bool>();
					break;
				}
			}
			return rotationOverLifetimeModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
