using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_CustomDataModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.CustomDataModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			writer.WriteProperty<bool>("enabled", ((ParticleSystem.CustomDataModule)value).enabled);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.CustomDataModule customDataModule = default(ParticleSystem.CustomDataModule);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (text == "enabled")
					{
						customDataModule.enabled = reader.ReadProperty<bool>();
					}
				}
			}
			return customDataModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
