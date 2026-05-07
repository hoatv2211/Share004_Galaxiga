using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_EmissionModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.EmissionModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.EmissionModule emissionModule = (ParticleSystem.EmissionModule)value;
			writer.WriteProperty<bool>("enabled", emissionModule.enabled);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("rateOverTime", emissionModule.rateOverTime);
			writer.WriteProperty<float>("rateOverTimeMultiplier", emissionModule.rateOverTimeMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("rateOverDistance", emissionModule.rateOverDistance);
			writer.WriteProperty<float>("rateOverDistanceMultiplier", emissionModule.rateOverDistanceMultiplier);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.EmissionModule emissionModule = default(ParticleSystem.EmissionModule);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "enabled"))
					{
						if (!(text == "rateOverTime"))
						{
							if (!(text == "rateOverTimeMultiplier"))
							{
								if (!(text == "rateOverDistance"))
								{
									if (text == "rateOverDistanceMultiplier")
									{
										emissionModule.rateOverDistanceMultiplier = reader.ReadProperty<float>();
									}
								}
								else
								{
									emissionModule.rateOverDistance = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
								}
							}
							else
							{
								emissionModule.rateOverTimeMultiplier = reader.ReadProperty<float>();
							}
						}
						else
						{
							emissionModule.rateOverTime = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
						}
					}
					else
					{
						emissionModule.enabled = reader.ReadProperty<bool>();
					}
				}
			}
			return emissionModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
