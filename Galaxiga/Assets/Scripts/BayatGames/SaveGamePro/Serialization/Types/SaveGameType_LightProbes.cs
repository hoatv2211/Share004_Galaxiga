using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_LightProbes : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(LightProbes);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			LightProbes lightProbes = (LightProbes)value;
			writer.WriteProperty<SphericalHarmonicsL2[]>("bakedProbes", lightProbes.bakedProbes);
			writer.WriteProperty<string>("name", lightProbes.name);
			writer.WriteProperty<HideFlags>("hideFlags", lightProbes.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			return base.Read(reader);
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			LightProbes lightProbes = (LightProbes)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "bakedProbes"))
					{
						if (!(text == "name"))
						{
							if (text == "hideFlags")
							{
								lightProbes.hideFlags = reader.ReadProperty<HideFlags>();
							}
						}
						else
						{
							lightProbes.name = reader.ReadProperty<string>();
						}
					}
					else
					{
						lightProbes.bakedProbes = reader.ReadProperty<SphericalHarmonicsL2[]>();
					}
				}
			}
		}
	}
}
