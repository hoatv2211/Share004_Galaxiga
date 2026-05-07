using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_CullingGroup : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(CullingGroup);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			CullingGroup cullingGroup = (CullingGroup)value;
			writer.WriteProperty<bool>("enabled", cullingGroup.enabled);
			writer.WriteProperty<Camera>("targetCamera", cullingGroup.targetCamera);
		}

		public override object Read(ISaveGameReader reader)
		{
			CullingGroup cullingGroup = new CullingGroup();
			this.ReadInto(cullingGroup, reader);
			return cullingGroup;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			CullingGroup cullingGroup = (CullingGroup)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "enabled"))
					{
						if (text == "targetCamera")
						{
							if (cullingGroup.targetCamera == null)
							{
								cullingGroup.targetCamera = reader.ReadProperty<Camera>();
							}
							else
							{
								reader.ReadIntoProperty<Camera>(cullingGroup.targetCamera);
							}
						}
					}
					else
					{
						cullingGroup.enabled = reader.ReadProperty<bool>();
					}
				}
			}
		}
	}
}
