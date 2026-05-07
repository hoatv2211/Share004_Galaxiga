using System;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AnimationTriggers : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AnimationTriggers);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AnimationTriggers animationTriggers = (AnimationTriggers)value;
			writer.WriteProperty<string>("normalTrigger", animationTriggers.normalTrigger);
			writer.WriteProperty<string>("highlightedTrigger", animationTriggers.highlightedTrigger);
			writer.WriteProperty<string>("pressedTrigger", animationTriggers.pressedTrigger);
			writer.WriteProperty<string>("disabledTrigger", animationTriggers.disabledTrigger);
		}

		public override object Read(ISaveGameReader reader)
		{
			AnimationTriggers animationTriggers = new AnimationTriggers();
			this.ReadInto(animationTriggers, reader);
			return animationTriggers;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AnimationTriggers animationTriggers = (AnimationTriggers)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "normalTrigger"))
					{
						if (!(text == "highlightedTrigger"))
						{
							if (!(text == "pressedTrigger"))
							{
								if (text == "disabledTrigger")
								{
									animationTriggers.disabledTrigger = reader.ReadProperty<string>();
								}
							}
							else
							{
								animationTriggers.pressedTrigger = reader.ReadProperty<string>();
							}
						}
						else
						{
							animationTriggers.highlightedTrigger = reader.ReadProperty<string>();
						}
					}
					else
					{
						animationTriggers.normalTrigger = reader.ReadProperty<string>();
					}
				}
			}
		}
	}
}
