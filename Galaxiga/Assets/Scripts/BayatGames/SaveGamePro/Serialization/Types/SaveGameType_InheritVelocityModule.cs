using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_InheritVelocityModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.InheritVelocityModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.InheritVelocityModule inheritVelocityModule = (ParticleSystem.InheritVelocityModule)value;
			writer.WriteProperty<bool>("enabled", inheritVelocityModule.enabled);
			writer.WriteProperty<ParticleSystemInheritVelocityMode>("mode", inheritVelocityModule.mode);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("curve", inheritVelocityModule.curve);
			writer.WriteProperty<float>("curveMultiplier", inheritVelocityModule.curveMultiplier);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.InheritVelocityModule inheritVelocityModule = default(ParticleSystem.InheritVelocityModule);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "enabled"))
					{
						if (!(text == "mode"))
						{
							if (!(text == "curve"))
							{
								if (text == "curveMultiplier")
								{
									inheritVelocityModule.curveMultiplier = reader.ReadProperty<float>();
								}
							}
							else
							{
								inheritVelocityModule.curve = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
							}
						}
						else
						{
							inheritVelocityModule.mode = reader.ReadProperty<ParticleSystemInheritVelocityMode>();
						}
					}
					else
					{
						inheritVelocityModule.enabled = reader.ReadProperty<bool>();
					}
				}
			}
			return inheritVelocityModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
