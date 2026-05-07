using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioListener : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioListener);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioListener audioListener = (AudioListener)value;
			writer.WriteProperty<AudioVelocityUpdateMode>("velocityUpdateMode", audioListener.velocityUpdateMode);
			writer.WriteProperty<bool>("enabled", audioListener.enabled);
			writer.WriteProperty<string>("tag", audioListener.tag);
			writer.WriteProperty<string>("name", audioListener.name);
			writer.WriteProperty<HideFlags>("hideFlags", audioListener.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AudioListener audioListener = SaveGameType.CreateComponent<AudioListener>();
			this.ReadInto(audioListener, reader);
			return audioListener;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AudioListener audioListener = (AudioListener)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "velocityUpdateMode"))
					{
						if (!(text == "enabled"))
						{
							if (!(text == "tag"))
							{
								if (!(text == "name"))
								{
									if (text == "hideFlags")
									{
										audioListener.hideFlags = reader.ReadProperty<HideFlags>();
									}
								}
								else
								{
									audioListener.name = reader.ReadProperty<string>();
								}
							}
							else
							{
								audioListener.tag = reader.ReadProperty<string>();
							}
						}
						else
						{
							audioListener.enabled = reader.ReadProperty<bool>();
						}
					}
					else
					{
						audioListener.velocityUpdateMode = reader.ReadProperty<AudioVelocityUpdateMode>();
					}
				}
			}
		}
	}
}
