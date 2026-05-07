using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_TerrainData : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(TerrainData);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			TerrainData terrainData = (TerrainData)value;
			float[,,] alphamaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
			float[,] heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
			writer.WriteProperty<float[,,]>("alphamaps", alphamaps);
			writer.WriteProperty<float[,]>("heights", heights);
			writer.WriteProperty<int>("heightmapResolution", terrainData.heightmapResolution);
			writer.WriteProperty<Vector3>("size", terrainData.size);
			writer.WriteProperty<float>("thickness", terrainData.thickness);
			writer.WriteProperty<float>("wavingGrassStrength", terrainData.wavingGrassStrength);
			writer.WriteProperty<float>("wavingGrassAmount", terrainData.wavingGrassAmount);
			writer.WriteProperty<float>("wavingGrassSpeed", terrainData.wavingGrassSpeed);
			writer.WriteProperty<Color>("wavingGrassTint", terrainData.wavingGrassTint);
			writer.WriteProperty<DetailPrototype[]>("detailPrototypes", terrainData.detailPrototypes);
			writer.WriteProperty<TreeInstance[]>("treeInstances", terrainData.treeInstances);
			writer.WriteProperty<TreePrototype[]>("treePrototypes", terrainData.treePrototypes);
			writer.WriteProperty<int>("alphamapResolution", terrainData.alphamapResolution);
			writer.WriteProperty<int>("baseMapResolution", terrainData.baseMapResolution);
			writer.WriteProperty<SplatPrototype[]>("splatPrototypes", terrainData.splatPrototypes);
			writer.WriteProperty<string>("name", terrainData.name);
			writer.WriteProperty<HideFlags>("hideFlags", terrainData.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			TerrainData terrainData = new TerrainData();
			this.ReadInto(terrainData, reader);
			return terrainData;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			TerrainData terrainData = (TerrainData)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "alphamaps":
					terrainData.SetAlphamaps(0, 0, reader.ReadProperty<float[,,]>());
					break;
				case "heights":
					terrainData.SetHeights(0, 0, reader.ReadProperty<float[,]>());
					break;
				case "heightmapResolution":
					terrainData.heightmapResolution = reader.ReadProperty<int>();
					break;
				case "size":
					terrainData.size = reader.ReadProperty<Vector3>();
					break;
				case "thickness":
					terrainData.thickness = reader.ReadProperty<float>();
					break;
				case "wavingGrassStrength":
					terrainData.wavingGrassStrength = reader.ReadProperty<float>();
					break;
				case "wavingGrassAmount":
					terrainData.wavingGrassAmount = reader.ReadProperty<float>();
					break;
				case "wavingGrassSpeed":
					terrainData.wavingGrassSpeed = reader.ReadProperty<float>();
					break;
				case "wavingGrassTint":
					terrainData.wavingGrassTint = reader.ReadProperty<Color>();
					break;
				case "detailPrototypes":
					terrainData.detailPrototypes = reader.ReadProperty<DetailPrototype[]>();
					break;
				case "treeInstances":
					terrainData.treeInstances = reader.ReadProperty<TreeInstance[]>();
					break;
				case "treePrototypes":
					terrainData.treePrototypes = reader.ReadProperty<TreePrototype[]>();
					break;
				case "alphamapResolution":
					terrainData.alphamapResolution = reader.ReadProperty<int>();
					break;
				case "baseMapResolution":
					terrainData.baseMapResolution = reader.ReadProperty<int>();
					break;
				case "splatPrototypes":
					terrainData.splatPrototypes = reader.ReadProperty<SplatPrototype[]>();
					break;
				case "name":
					terrainData.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					terrainData.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
