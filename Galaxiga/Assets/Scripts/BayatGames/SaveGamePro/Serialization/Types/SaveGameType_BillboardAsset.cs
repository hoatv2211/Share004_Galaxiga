using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_BillboardAsset : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(BillboardAsset);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			BillboardAsset billboardAsset = (BillboardAsset)value;
			writer.WriteProperty<Vector4[]>("imageTexCoords", billboardAsset.GetImageTexCoords());
			writer.WriteProperty<ushort[]>("indices", billboardAsset.GetIndices());
			writer.WriteProperty<Vector2[]>("vertices", billboardAsset.GetVertices());
			writer.WriteProperty<float>("width", billboardAsset.width);
			writer.WriteProperty<float>("height", billboardAsset.height);
			writer.WriteProperty<float>("bottom", billboardAsset.bottom);
			writer.WriteProperty<Material>("material", billboardAsset.material);
			writer.WriteProperty<string>("name", billboardAsset.name);
			writer.WriteProperty<HideFlags>("hideFlags", billboardAsset.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			BillboardAsset billboardAsset = new BillboardAsset();
			this.ReadInto(billboardAsset, reader);
			return billboardAsset;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			BillboardAsset billboardAsset = (BillboardAsset)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "imageTexCoords":
					billboardAsset.SetImageTexCoords(reader.ReadProperty<Vector4[]>());
					break;
				case "indices":
					billboardAsset.SetIndices(reader.ReadProperty<ushort[]>());
					break;
				case "vertices":
					billboardAsset.SetVertices(reader.ReadProperty<Vector2[]>());
					break;
				case "width":
					billboardAsset.width = reader.ReadProperty<float>();
					break;
				case "height":
					billboardAsset.height = reader.ReadProperty<float>();
					break;
				case "bottom":
					billboardAsset.bottom = reader.ReadProperty<float>();
					break;
				case "material":
					if (billboardAsset.material == null)
					{
						billboardAsset.material = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(billboardAsset.material);
					}
					break;
				case "name":
					billboardAsset.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					billboardAsset.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
