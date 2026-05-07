using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Shader : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Shader);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Shader shader = (Shader)value;
			writer.WriteProperty<string>("name", shader.name);
			writer.WriteProperty<int>("maximumLOD", shader.maximumLOD);
			writer.WriteProperty<HideFlags>("hideFlags", shader.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Shader shader = Shader.Find(reader.ReadProperty<string>());
			this.ReadInto(shader, reader);
			return shader;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Shader shader = (Shader)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "name"))
					{
						if (!(text == "maximumLOD"))
						{
							if (text == "hideFlags")
							{
								shader.hideFlags = reader.ReadProperty<HideFlags>();
							}
						}
						else
						{
							shader.maximumLOD = reader.ReadProperty<int>();
						}
					}
					else
					{
						shader.name = reader.ReadProperty<string>();
					}
				}
			}
		}
	}
}
