using System;
using UnityEngine;
using UnityEngine.AI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_NavMeshTriangulation : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(NavMeshTriangulation);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			NavMeshTriangulation navMeshTriangulation = (NavMeshTriangulation)value;
			writer.WriteProperty<Vector3[]>("vertices", navMeshTriangulation.vertices);
			writer.WriteProperty<int[]>("indices", navMeshTriangulation.indices);
			writer.WriteProperty<int[]>("areas", navMeshTriangulation.areas);
		}

		public override object Read(ISaveGameReader reader)
		{
			NavMeshTriangulation navMeshTriangulation = default(NavMeshTriangulation);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "vertices"))
					{
						if (!(text == "indices"))
						{
							if (text == "areas")
							{
								navMeshTriangulation.areas = reader.ReadProperty<int[]>();
							}
						}
						else
						{
							navMeshTriangulation.indices = reader.ReadProperty<int[]>();
						}
					}
					else
					{
						navMeshTriangulation.vertices = reader.ReadProperty<Vector3[]>();
					}
				}
			}
			return navMeshTriangulation;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
