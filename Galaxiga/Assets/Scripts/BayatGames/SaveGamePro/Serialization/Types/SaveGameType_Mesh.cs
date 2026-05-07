using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Mesh : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Mesh);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Mesh mesh = (Mesh)value;
			writer.WriteProperty<Bounds>("bounds", mesh.bounds);
			writer.WriteProperty<int>("subMeshCount", mesh.subMeshCount);
			writer.WriteProperty<BoneWeight[]>("boneWeights", mesh.boneWeights);
			writer.WriteProperty<Matrix4x4[]>("bindposes", mesh.bindposes);
			writer.WriteProperty<Vector3[]>("vertices", mesh.vertices);
			writer.WriteProperty<Vector3[]>("normals", mesh.normals);
			writer.WriteProperty<Vector4[]>("tangents", mesh.tangents);
			writer.WriteProperty<Vector2[]>("uv", mesh.uv);
			writer.WriteProperty<Vector2[]>("uv2", mesh.uv2);
			writer.WriteProperty<Vector2[]>("uv3", mesh.uv3);
			writer.WriteProperty<Vector2[]>("uv4", mesh.uv4);
			writer.WriteProperty<Color[]>("colors", mesh.colors);
			writer.WriteProperty<Color32[]>("colors32", mesh.colors32);
			writer.WriteProperty<int[]>("triangles", mesh.triangles);
			writer.WriteProperty<string>("name", mesh.name);
			writer.WriteProperty<HideFlags>("hideFlags", mesh.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Mesh mesh = new Mesh();
			this.ReadInto(mesh, reader);
			return mesh;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Mesh mesh = (Mesh)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "bounds":
					mesh.bounds = reader.ReadProperty<Bounds>();
					break;
				case "subMeshCount":
					mesh.subMeshCount = reader.ReadProperty<int>();
					break;
				case "boneWeights":
					mesh.boneWeights = reader.ReadProperty<BoneWeight[]>();
					break;
				case "bindposes":
					mesh.bindposes = reader.ReadProperty<Matrix4x4[]>();
					break;
				case "vertices":
					mesh.vertices = reader.ReadProperty<Vector3[]>();
					break;
				case "normals":
					mesh.normals = reader.ReadProperty<Vector3[]>();
					break;
				case "tangents":
					mesh.tangents = reader.ReadProperty<Vector4[]>();
					break;
				case "uv":
					mesh.uv = reader.ReadProperty<Vector2[]>();
					break;
				case "uv2":
					mesh.uv2 = reader.ReadProperty<Vector2[]>();
					break;
				case "uv3":
					mesh.uv3 = reader.ReadProperty<Vector2[]>();
					break;
				case "uv4":
					mesh.uv4 = reader.ReadProperty<Vector2[]>();
					break;
				case "colors":
					mesh.colors = reader.ReadProperty<Color[]>();
					break;
				case "colors32":
					mesh.colors32 = reader.ReadProperty<Color32[]>();
					break;
				case "triangles":
					mesh.triangles = reader.ReadProperty<int[]>();
					break;
				case "name":
					mesh.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					mesh.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
