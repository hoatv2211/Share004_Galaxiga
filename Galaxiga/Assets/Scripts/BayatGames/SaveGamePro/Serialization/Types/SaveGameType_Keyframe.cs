using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Keyframe : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Keyframe);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Keyframe keyframe = (Keyframe)value;
			writer.WriteProperty<float>("time", keyframe.time);
			writer.WriteProperty<float>("value", keyframe.value);
			writer.WriteProperty<float>("inTangent", keyframe.inTangent);
			writer.WriteProperty<float>("outTangent", keyframe.outTangent);
		}

		public override object Read(ISaveGameReader reader)
		{
			Keyframe keyframe = default(Keyframe);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "time"))
					{
						if (!(text == "value"))
						{
							if (!(text == "inTangent"))
							{
								if (!(text == "outTangent"))
								{
									if (text == "tangentMode")
									{
										reader.ReadProperty<int>();
									}
								}
								else
								{
									keyframe.outTangent = reader.ReadProperty<float>();
								}
							}
							else
							{
								keyframe.inTangent = reader.ReadProperty<float>();
							}
						}
						else
						{
							keyframe.value = reader.ReadProperty<float>();
						}
					}
					else
					{
						keyframe.time = reader.ReadProperty<float>();
					}
				}
			}
			return keyframe;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
