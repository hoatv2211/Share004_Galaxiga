using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Skybox : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Skybox);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Skybox skybox = (Skybox)value;
			writer.WriteProperty<Material>("material", skybox.material);
			writer.WriteProperty<bool>("enabled", skybox.enabled);
			writer.WriteProperty<string>("tag", skybox.tag);
			writer.WriteProperty<string>("name", skybox.name);
			writer.WriteProperty<HideFlags>("hideFlags", skybox.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Skybox skybox = SaveGameType.CreateComponent<Skybox>();
			this.ReadInto(skybox, reader);
			return skybox;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Skybox skybox = (Skybox)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "material"))
					{
						if (!(text == "enabled"))
						{
							if (!(text == "tag"))
							{
								if (!(text == "name"))
								{
									if (text == "hideFlags")
									{
										skybox.hideFlags = reader.ReadProperty<HideFlags>();
									}
								}
								else
								{
									skybox.name = reader.ReadProperty<string>();
								}
							}
							else
							{
								skybox.tag = reader.ReadProperty<string>();
							}
						}
						else
						{
							skybox.enabled = reader.ReadProperty<bool>();
						}
					}
					else if (skybox.material == null)
					{
						skybox.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(skybox.material);
					}
				}
			}
		}
	}
}
