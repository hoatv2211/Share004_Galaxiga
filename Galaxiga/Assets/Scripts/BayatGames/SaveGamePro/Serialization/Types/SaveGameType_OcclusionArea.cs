using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_OcclusionArea : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(OcclusionArea);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			OcclusionArea occlusionArea = (OcclusionArea)value;
			writer.WriteProperty<Vector3>("center", occlusionArea.center);
			writer.WriteProperty<Vector3>("size", occlusionArea.size);
			writer.WriteProperty<string>("tag", occlusionArea.tag);
			writer.WriteProperty<string>("name", occlusionArea.name);
			writer.WriteProperty<HideFlags>("hideFlags", occlusionArea.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			OcclusionArea occlusionArea = SaveGameType.CreateComponent<OcclusionArea>();
			this.ReadInto(occlusionArea, reader);
			return occlusionArea;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			OcclusionArea occlusionArea = (OcclusionArea)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "center"))
					{
						if (!(text == "size"))
						{
							if (!(text == "tag"))
							{
								if (!(text == "name"))
								{
									if (text == "hideFlags")
									{
										occlusionArea.hideFlags = reader.ReadProperty<HideFlags>();
									}
								}
								else
								{
									occlusionArea.name = reader.ReadProperty<string>();
								}
							}
							else
							{
								occlusionArea.tag = reader.ReadProperty<string>();
							}
						}
						else
						{
							occlusionArea.size = reader.ReadProperty<Vector3>();
						}
					}
					else
					{
						occlusionArea.center = reader.ReadProperty<Vector3>();
					}
				}
			}
		}
	}
}
