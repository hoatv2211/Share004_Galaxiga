using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_LightProbeGroup : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(LightProbeGroup);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			LightProbeGroup lightProbeGroup = (LightProbeGroup)value;
			writer.WriteProperty<bool>("enabled", lightProbeGroup.enabled);
			writer.WriteProperty<string>("tag", lightProbeGroup.tag);
			writer.WriteProperty<string>("name", lightProbeGroup.name);
			writer.WriteProperty<HideFlags>("hideFlags", lightProbeGroup.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			LightProbeGroup lightProbeGroup = SaveGameType.CreateComponent<LightProbeGroup>();
			this.ReadInto(lightProbeGroup, reader);
			return lightProbeGroup;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			LightProbeGroup lightProbeGroup = (LightProbeGroup)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "probePositions"))
					{
						if (!(text == "enabled"))
						{
							if (!(text == "tag"))
							{
								if (!(text == "name"))
								{
									if (text == "hideFlags")
									{
										lightProbeGroup.hideFlags = reader.ReadProperty<HideFlags>();
									}
								}
								else
								{
									lightProbeGroup.name = reader.ReadProperty<string>();
								}
							}
							else
							{
								lightProbeGroup.tag = reader.ReadProperty<string>();
							}
						}
						else
						{
							lightProbeGroup.enabled = reader.ReadProperty<bool>();
						}
					}
					else
					{
						reader.ReadProperty<Vector3[]>();
					}
				}
			}
		}
	}
}
