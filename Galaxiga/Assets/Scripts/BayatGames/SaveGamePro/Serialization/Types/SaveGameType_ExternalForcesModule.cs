using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ExternalForcesModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.ExternalForcesModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.ExternalForcesModule externalForcesModule = (ParticleSystem.ExternalForcesModule)value;
			writer.WriteProperty<bool>("enabled", externalForcesModule.enabled);
			writer.WriteProperty<float>("multiplier", externalForcesModule.multiplier);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.ExternalForcesModule externalForcesModule = default(ParticleSystem.ExternalForcesModule);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "enabled"))
					{
						if (text == "multiplier")
						{
							externalForcesModule.multiplier = reader.ReadProperty<float>();
						}
					}
					else
					{
						externalForcesModule.enabled = reader.ReadProperty<bool>();
					}
				}
			}
			return externalForcesModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
