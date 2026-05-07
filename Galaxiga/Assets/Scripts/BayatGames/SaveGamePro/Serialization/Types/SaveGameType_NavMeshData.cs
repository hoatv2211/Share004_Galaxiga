using System;
using UnityEngine;
using UnityEngine.AI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_NavMeshData : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(NavMeshData);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			NavMeshData navMeshData = (NavMeshData)value;
			writer.WriteProperty<Vector3>("position", navMeshData.position);
			writer.WriteProperty<Quaternion>("rotation", navMeshData.rotation);
			writer.WriteProperty<string>("name", navMeshData.name);
			writer.WriteProperty<HideFlags>("hideFlags", navMeshData.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			NavMeshData navMeshData = new NavMeshData();
			this.ReadInto(navMeshData, reader);
			return navMeshData;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			NavMeshData navMeshData = (NavMeshData)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "position"))
					{
						if (!(text == "rotation"))
						{
							if (!(text == "name"))
							{
								if (text == "hideFlags")
								{
									navMeshData.hideFlags = reader.ReadProperty<HideFlags>();
								}
							}
							else
							{
								navMeshData.name = reader.ReadProperty<string>();
							}
						}
						else
						{
							navMeshData.rotation = reader.ReadProperty<Quaternion>();
						}
					}
					else
					{
						navMeshData.position = reader.ReadProperty<Vector3>();
					}
				}
			}
		}
	}
}
