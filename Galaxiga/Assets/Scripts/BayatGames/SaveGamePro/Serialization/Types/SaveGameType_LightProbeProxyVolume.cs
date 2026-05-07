using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_LightProbeProxyVolume : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(LightProbeProxyVolume);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			LightProbeProxyVolume lightProbeProxyVolume = (LightProbeProxyVolume)value;
			writer.WriteProperty<Vector3>("sizeCustom", lightProbeProxyVolume.sizeCustom);
			writer.WriteProperty<Vector3>("originCustom", lightProbeProxyVolume.originCustom);
			writer.WriteProperty<LightProbeProxyVolume.BoundingBoxMode>("boundingBoxMode", lightProbeProxyVolume.boundingBoxMode);
			writer.WriteProperty<LightProbeProxyVolume.ResolutionMode>("resolutionMode", lightProbeProxyVolume.resolutionMode);
			writer.WriteProperty<LightProbeProxyVolume.ProbePositionMode>("probePositionMode", lightProbeProxyVolume.probePositionMode);
			writer.WriteProperty<LightProbeProxyVolume.RefreshMode>("refreshMode", lightProbeProxyVolume.refreshMode);
			writer.WriteProperty<float>("probeDensity", lightProbeProxyVolume.probeDensity);
			writer.WriteProperty<int>("gridResolutionX", lightProbeProxyVolume.gridResolutionX);
			writer.WriteProperty<int>("gridResolutionY", lightProbeProxyVolume.gridResolutionY);
			writer.WriteProperty<int>("gridResolutionZ", lightProbeProxyVolume.gridResolutionZ);
			writer.WriteProperty<bool>("enabled", lightProbeProxyVolume.enabled);
			writer.WriteProperty<string>("tag", lightProbeProxyVolume.tag);
			writer.WriteProperty<string>("name", lightProbeProxyVolume.name);
			writer.WriteProperty<HideFlags>("hideFlags", lightProbeProxyVolume.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			LightProbeProxyVolume lightProbeProxyVolume = SaveGameType.CreateComponent<LightProbeProxyVolume>();
			this.ReadInto(lightProbeProxyVolume, reader);
			return lightProbeProxyVolume;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			LightProbeProxyVolume lightProbeProxyVolume = (LightProbeProxyVolume)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "sizeCustom":
					lightProbeProxyVolume.sizeCustom = reader.ReadProperty<Vector3>();
					break;
				case "originCustom":
					lightProbeProxyVolume.originCustom = reader.ReadProperty<Vector3>();
					break;
				case "boundingBoxMode":
					lightProbeProxyVolume.boundingBoxMode = reader.ReadProperty<LightProbeProxyVolume.BoundingBoxMode>();
					break;
				case "resolutionMode":
					lightProbeProxyVolume.resolutionMode = reader.ReadProperty<LightProbeProxyVolume.ResolutionMode>();
					break;
				case "probePositionMode":
					lightProbeProxyVolume.probePositionMode = reader.ReadProperty<LightProbeProxyVolume.ProbePositionMode>();
					break;
				case "refreshMode":
					lightProbeProxyVolume.refreshMode = reader.ReadProperty<LightProbeProxyVolume.RefreshMode>();
					break;
				case "probeDensity":
					lightProbeProxyVolume.probeDensity = reader.ReadProperty<float>();
					break;
				case "gridResolutionX":
					lightProbeProxyVolume.gridResolutionX = reader.ReadProperty<int>();
					break;
				case "gridResolutionY":
					lightProbeProxyVolume.gridResolutionY = reader.ReadProperty<int>();
					break;
				case "gridResolutionZ":
					lightProbeProxyVolume.gridResolutionZ = reader.ReadProperty<int>();
					break;
				case "enabled":
					lightProbeProxyVolume.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					lightProbeProxyVolume.tag = reader.ReadProperty<string>();
					break;
				case "name":
					lightProbeProxyVolume.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					lightProbeProxyVolume.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
