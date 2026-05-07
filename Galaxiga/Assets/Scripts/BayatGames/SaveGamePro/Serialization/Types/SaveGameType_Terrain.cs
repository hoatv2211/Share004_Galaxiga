using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Terrain : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Terrain);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Terrain terrain = (Terrain)value;
			writer.WriteProperty<TerrainData>("terrainData", terrain.terrainData);
			writer.WriteProperty<float>("treeDistance", terrain.treeDistance);
			writer.WriteProperty<float>("treeBillboardDistance", terrain.treeBillboardDistance);
			writer.WriteProperty<float>("treeCrossFadeLength", terrain.treeCrossFadeLength);
			writer.WriteProperty<int>("treeMaximumFullLODCount", terrain.treeMaximumFullLODCount);
			writer.WriteProperty<float>("detailObjectDistance", terrain.detailObjectDistance);
			writer.WriteProperty<float>("detailObjectDensity", terrain.detailObjectDensity);
			writer.WriteProperty<float>("heightmapPixelError", terrain.heightmapPixelError);
			writer.WriteProperty<int>("heightmapMaximumLOD", terrain.heightmapMaximumLOD);
			writer.WriteProperty<float>("basemapDistance", terrain.basemapDistance);
			writer.WriteProperty<int>("lightmapIndex", terrain.lightmapIndex);
			writer.WriteProperty<int>("realtimeLightmapIndex", terrain.realtimeLightmapIndex);
			writer.WriteProperty<Vector4>("lightmapScaleOffset", terrain.lightmapScaleOffset);
			writer.WriteProperty<Vector4>("realtimeLightmapScaleOffset", terrain.realtimeLightmapScaleOffset);
			writer.WriteProperty<bool>("castShadows", terrain.castShadows);
			writer.WriteProperty<ReflectionProbeUsage>("reflectionProbeUsage", terrain.reflectionProbeUsage);
			writer.WriteProperty<Terrain.MaterialType>("materialType", terrain.materialType);
			writer.WriteProperty<Material>("materialTemplate", terrain.materialTemplate);
			writer.WriteProperty<Color>("legacySpecular", terrain.legacySpecular);
			writer.WriteProperty<float>("legacyShininess", terrain.legacyShininess);
			writer.WriteProperty<bool>("drawHeightmap", terrain.drawHeightmap);
			writer.WriteProperty<bool>("drawTreesAndFoliage", terrain.drawTreesAndFoliage);
			writer.WriteProperty<Vector3>("patchBoundsMultiplier", terrain.patchBoundsMultiplier);
			writer.WriteProperty<float>("treeLODBiasMultiplier", terrain.treeLODBiasMultiplier);
			writer.WriteProperty<bool>("collectDetailPatches", terrain.collectDetailPatches);
			writer.WriteProperty<TerrainRenderFlags>("editorRenderFlags", terrain.editorRenderFlags);
			writer.WriteProperty<bool>("enabled", terrain.enabled);
			writer.WriteProperty<string>("tag", terrain.tag);
			writer.WriteProperty<string>("name", terrain.name);
			writer.WriteProperty<HideFlags>("hideFlags", terrain.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Terrain terrain = SaveGameType.CreateComponent<Terrain>();
			this.ReadInto(terrain, reader);
			return terrain;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Terrain terrain = (Terrain)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "terrainData":
					if (terrain.terrainData == null)
					{
						terrain.terrainData = reader.ReadProperty<TerrainData>();
					}
					else
					{
						reader.ReadIntoProperty<TerrainData>(terrain.terrainData);
					}
					break;
				case "treeDistance":
					terrain.treeDistance = reader.ReadProperty<float>();
					break;
				case "treeBillboardDistance":
					terrain.treeBillboardDistance = reader.ReadProperty<float>();
					break;
				case "treeCrossFadeLength":
					terrain.treeCrossFadeLength = reader.ReadProperty<float>();
					break;
				case "treeMaximumFullLODCount":
					terrain.treeMaximumFullLODCount = reader.ReadProperty<int>();
					break;
				case "detailObjectDistance":
					terrain.detailObjectDistance = reader.ReadProperty<float>();
					break;
				case "detailObjectDensity":
					terrain.detailObjectDensity = reader.ReadProperty<float>();
					break;
				case "heightmapPixelError":
					terrain.heightmapPixelError = reader.ReadProperty<float>();
					break;
				case "heightmapMaximumLOD":
					terrain.heightmapMaximumLOD = reader.ReadProperty<int>();
					break;
				case "basemapDistance":
					terrain.basemapDistance = reader.ReadProperty<float>();
					break;
				case "lightmapIndex":
					terrain.lightmapIndex = reader.ReadProperty<int>();
					break;
				case "realtimeLightmapIndex":
					terrain.realtimeLightmapIndex = reader.ReadProperty<int>();
					break;
				case "lightmapScaleOffset":
					terrain.lightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "realtimeLightmapScaleOffset":
					terrain.realtimeLightmapScaleOffset = reader.ReadProperty<Vector4>();
					break;
				case "castShadows":
					terrain.castShadows = reader.ReadProperty<bool>();
					break;
				case "reflectionProbeUsage":
					terrain.reflectionProbeUsage = reader.ReadProperty<ReflectionProbeUsage>();
					break;
				case "materialType":
					terrain.materialType = reader.ReadProperty<Terrain.MaterialType>();
					break;
				case "materialTemplate":
					if (terrain.materialTemplate == null)
					{
						terrain.materialTemplate = reader.ReadProperty<Material>();
					}
					else
					{
						reader.ReadIntoProperty<Material>(terrain.materialTemplate);
					}
					break;
				case "legacySpecular":
					terrain.legacySpecular = reader.ReadProperty<Color>();
					break;
				case "legacyShininess":
					terrain.legacyShininess = reader.ReadProperty<float>();
					break;
				case "drawHeightmap":
					terrain.drawHeightmap = reader.ReadProperty<bool>();
					break;
				case "drawTreesAndFoliage":
					terrain.drawTreesAndFoliage = reader.ReadProperty<bool>();
					break;
				case "patchBoundsMultiplier":
					terrain.patchBoundsMultiplier = reader.ReadProperty<Vector3>();
					break;
				case "treeLODBiasMultiplier":
					terrain.treeLODBiasMultiplier = reader.ReadProperty<float>();
					break;
				case "collectDetailPatches":
					terrain.collectDetailPatches = reader.ReadProperty<bool>();
					break;
				case "editorRenderFlags":
					terrain.editorRenderFlags = reader.ReadProperty<TerrainRenderFlags>();
					break;
				case "enabled":
					terrain.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					terrain.tag = reader.ReadProperty<string>();
					break;
				case "name":
					terrain.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					terrain.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
