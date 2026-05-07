using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_TriggerModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.TriggerModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.TriggerModule triggerModule = (ParticleSystem.TriggerModule)value;
			writer.WriteProperty<bool>("enabled", triggerModule.enabled);
			writer.WriteProperty<ParticleSystemOverlapAction>("inside", triggerModule.inside);
			writer.WriteProperty<ParticleSystemOverlapAction>("outside", triggerModule.outside);
			writer.WriteProperty<ParticleSystemOverlapAction>("enter", triggerModule.enter);
			writer.WriteProperty<ParticleSystemOverlapAction>("exit", triggerModule.exit);
			writer.WriteProperty<float>("radiusScale", triggerModule.radiusScale);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.TriggerModule triggerModule = default(ParticleSystem.TriggerModule);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "enabled"))
					{
						if (!(text == "inside"))
						{
							if (!(text == "outside"))
							{
								if (!(text == "enter"))
								{
									if (!(text == "exit"))
									{
										if (text == "radiusScale")
										{
											triggerModule.radiusScale = reader.ReadProperty<float>();
										}
									}
									else
									{
										triggerModule.exit = reader.ReadProperty<ParticleSystemOverlapAction>();
									}
								}
								else
								{
									triggerModule.enter = reader.ReadProperty<ParticleSystemOverlapAction>();
								}
							}
							else
							{
								triggerModule.outside = reader.ReadProperty<ParticleSystemOverlapAction>();
							}
						}
						else
						{
							triggerModule.inside = reader.ReadProperty<ParticleSystemOverlapAction>();
						}
					}
					else
					{
						triggerModule.enabled = reader.ReadProperty<bool>();
					}
				}
			}
			return triggerModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
