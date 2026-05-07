using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AnimatorControllerParameter : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AnimatorControllerParameter);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AnimatorControllerParameter animatorControllerParameter = (AnimatorControllerParameter)value;
			writer.WriteProperty<AnimatorControllerParameterType>("type", animatorControllerParameter.type);
			writer.WriteProperty<float>("defaultFloat", animatorControllerParameter.defaultFloat);
			writer.WriteProperty<int>("defaultInt", animatorControllerParameter.defaultInt);
			writer.WriteProperty<bool>("defaultBool", animatorControllerParameter.defaultBool);
		}

		public override object Read(ISaveGameReader reader)
		{
			AnimatorControllerParameter animatorControllerParameter = new AnimatorControllerParameter();
			this.ReadInto(animatorControllerParameter, reader);
			return animatorControllerParameter;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AnimatorControllerParameter animatorControllerParameter = (AnimatorControllerParameter)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "type"))
					{
						if (!(text == "defaultFloat"))
						{
							if (!(text == "defaultInt"))
							{
								if (text == "defaultBool")
								{
									animatorControllerParameter.defaultBool = reader.ReadProperty<bool>();
								}
							}
							else
							{
								animatorControllerParameter.defaultInt = reader.ReadProperty<int>();
							}
						}
						else
						{
							animatorControllerParameter.defaultFloat = reader.ReadProperty<float>();
						}
					}
					else
					{
						animatorControllerParameter.type = reader.ReadProperty<AnimatorControllerParameterType>();
					}
				}
			}
		}
	}
}
