using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_OcclusionPortal : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(OcclusionPortal);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			OcclusionPortal occlusionPortal = (OcclusionPortal)value;
			writer.WriteProperty<bool>("open", occlusionPortal.open);
			writer.WriteProperty<string>("tag", occlusionPortal.tag);
			writer.WriteProperty<string>("name", occlusionPortal.name);
			writer.WriteProperty<HideFlags>("hideFlags", occlusionPortal.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			OcclusionPortal occlusionPortal = SaveGameType.CreateComponent<OcclusionPortal>();
			this.ReadInto(occlusionPortal, reader);
			return occlusionPortal;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			OcclusionPortal occlusionPortal = (OcclusionPortal)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "open"))
					{
						if (!(text == "tag"))
						{
							if (!(text == "name"))
							{
								if (text == "hideFlags")
								{
									occlusionPortal.hideFlags = reader.ReadProperty<HideFlags>();
								}
							}
							else
							{
								occlusionPortal.name = reader.ReadProperty<string>();
							}
						}
						else
						{
							occlusionPortal.tag = reader.ReadProperty<string>();
						}
					}
					else
					{
						occlusionPortal.open = reader.ReadProperty<bool>();
					}
				}
			}
		}
	}
}
