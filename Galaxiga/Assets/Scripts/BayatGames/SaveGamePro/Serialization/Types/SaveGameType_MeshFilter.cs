using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_MeshFilter : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(MeshFilter);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			MeshFilter meshFilter = (MeshFilter)value;
			writer.WriteProperty<Mesh>("mesh", meshFilter.mesh);
			writer.WriteProperty<Mesh>("sharedMesh", meshFilter.sharedMesh);
			writer.WriteProperty<string>("tag", meshFilter.tag);
			writer.WriteProperty<string>("name", meshFilter.name);
			writer.WriteProperty<HideFlags>("hideFlags", meshFilter.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			MeshFilter meshFilter = SaveGameType.CreateComponent<MeshFilter>();
			this.ReadInto(meshFilter, reader);
			return meshFilter;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			MeshFilter meshFilter = (MeshFilter)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "mesh"))
					{
						if (!(text == "sharedMesh"))
						{
							if (!(text == "tag"))
							{
								if (!(text == "name"))
								{
									if (text == "hideFlags")
									{
										meshFilter.hideFlags = reader.ReadProperty<HideFlags>();
									}
								}
								else
								{
									meshFilter.name = reader.ReadProperty<string>();
								}
							}
							else
							{
								meshFilter.tag = reader.ReadProperty<string>();
							}
						}
						else if (meshFilter.sharedMesh == null)
						{
							meshFilter.sharedMesh = reader.ReadProperty<Mesh>();
						}
						else
						{
							reader.ReadIntoProperty<Mesh>(meshFilter.sharedMesh);
						}
					}
					else if (meshFilter.mesh == null)
					{
						meshFilter.mesh = reader.ReadProperty<Mesh>();
					}
					else
					{
						reader.ReadIntoProperty<Mesh>(meshFilter.mesh);
					}
				}
			}
		}
	}
}
