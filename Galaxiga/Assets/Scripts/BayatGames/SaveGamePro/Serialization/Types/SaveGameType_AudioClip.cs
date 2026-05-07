using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioClip : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioClip);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioClip audioClip = (AudioClip)value;
			float[] array = new float[audioClip.samples];
			audioClip.GetData(array, 0);
			writer.WriteProperty<float[]>("data", array);
			writer.WriteProperty<int>("channels", audioClip.channels);
			writer.WriteProperty<int>("frequency", audioClip.frequency);
			writer.WriteProperty<string>("name", audioClip.name);
			writer.WriteProperty<HideFlags>("hideFlags", audioClip.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			string name = string.Empty;
			float[] array = new float[0];
			int channels = 0;
			int frequency = 0;
			HideFlags hideFlags = HideFlags.None;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "data"))
					{
						if (!(text == "channels"))
						{
							if (!(text == "frequency"))
							{
								if (!(text == "name"))
								{
									if (text == "hideFlags")
									{
										hideFlags = reader.ReadProperty<HideFlags>();
									}
								}
								else
								{
									name = reader.ReadProperty<string>();
								}
							}
							else
							{
								frequency = reader.ReadProperty<int>();
							}
						}
						else
						{
							channels = reader.ReadProperty<int>();
						}
					}
					else
					{
						array = reader.ReadProperty<float[]>();
					}
				}
			}
			AudioClip audioClip = AudioClip.Create(name, array.Length, channels, frequency, false);
			audioClip.SetData(array, 0);
			audioClip.hideFlags = hideFlags;
			return audioClip;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AudioClip audioClip = (AudioClip)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "data"))
					{
						if (!(text == "channels"))
						{
							if (!(text == "frequency"))
							{
								if (!(text == "name"))
								{
									if (text == "hideFlags")
									{
										audioClip.hideFlags = reader.ReadProperty<HideFlags>();
									}
								}
								else
								{
									audioClip.name = reader.ReadProperty<string>();
								}
							}
							else
							{
								reader.ReadProperty<int>();
							}
						}
						else
						{
							reader.ReadProperty<int>();
						}
					}
					else
					{
						audioClip.SetData(reader.ReadProperty<float[]>(), 0);
					}
				}
			}
		}
	}
}
