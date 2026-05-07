using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SubEmittersModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.SubEmittersModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			writer.WriteProperty<bool>("enabled", ((ParticleSystem.SubEmittersModule)value).enabled);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.SubEmittersModule subEmittersModule = default(ParticleSystem.SubEmittersModule);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (text == "enabled")
					{
						subEmittersModule.enabled = reader.ReadProperty<bool>();
					}
				}
			}
			return subEmittersModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
