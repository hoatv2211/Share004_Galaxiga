using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Light : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Light);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Light light = (Light)value;
			writer.WriteProperty<LightType>("type", light.type);
			writer.WriteProperty<Color>("color", light.color);
			writer.WriteProperty<float>("colorTemperature", light.colorTemperature);
			writer.WriteProperty<float>("intensity", light.intensity);
			writer.WriteProperty<float>("bounceIntensity", light.bounceIntensity);
			writer.WriteProperty<LightShadows>("shadows", light.shadows);
			writer.WriteProperty<float>("shadowStrength", light.shadowStrength);
			writer.WriteProperty<LightShadowResolution>("shadowResolution", light.shadowResolution);
			writer.WriteProperty<int>("shadowCustomResolution", light.shadowCustomResolution);
			writer.WriteProperty<float>("shadowBias", light.shadowBias);
			writer.WriteProperty<float>("shadowNormalBias", light.shadowNormalBias);
			writer.WriteProperty<float>("shadowNearPlane", light.shadowNearPlane);
			writer.WriteProperty<float>("range", light.range);
			writer.WriteProperty<float>("spotAngle", light.spotAngle);
			writer.WriteProperty<float>("cookieSize", light.cookieSize);
			writer.WriteProperty<Texture>("cookie", light.cookie);
			writer.WriteProperty<Flare>("flare", light.flare);
			writer.WriteProperty<LightRenderMode>("renderMode", light.renderMode);
			writer.WriteProperty<bool>("alreadyLightmapped", light.bakingOutput.isBaked);
			writer.WriteProperty<int>("cullingMask", light.cullingMask);
			writer.WriteProperty<bool>("enabled", light.enabled);
			writer.WriteProperty<string>("tag", light.tag);
			writer.WriteProperty<string>("name", light.name);
			writer.WriteProperty<HideFlags>("hideFlags", light.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Light light = SaveGameType.CreateComponent<Light>();
			this.ReadInto(light, reader);
			return light;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Light light = (Light)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "type":
					light.type = reader.ReadProperty<LightType>();
					break;
				case "color":
					light.color = reader.ReadProperty<Color>();
					break;
				case "colorTemperature":
					light.colorTemperature = reader.ReadProperty<float>();
					break;
				case "intensity":
					light.intensity = reader.ReadProperty<float>();
					break;
				case "bounceIntensity":
					light.bounceIntensity = reader.ReadProperty<float>();
					break;
				case "shadows":
					light.shadows = reader.ReadProperty<LightShadows>();
					break;
				case "shadowStrength":
					light.shadowStrength = reader.ReadProperty<float>();
					break;
				case "shadowResolution":
					light.shadowResolution = reader.ReadProperty<LightShadowResolution>();
					break;
				case "shadowCustomResolution":
					light.shadowCustomResolution = reader.ReadProperty<int>();
					break;
				case "shadowBias":
					light.shadowBias = reader.ReadProperty<float>();
					break;
				case "shadowNormalBias":
					light.shadowNormalBias = reader.ReadProperty<float>();
					break;
				case "shadowNearPlane":
					light.shadowNearPlane = reader.ReadProperty<float>();
					break;
				case "range":
					light.range = reader.ReadProperty<float>();
					break;
				case "spotAngle":
					light.spotAngle = reader.ReadProperty<float>();
					break;
				case "cookieSize":
					light.cookieSize = reader.ReadProperty<float>();
					break;
				case "cookie":
					if (light.cookie == null)
					{
						light.cookie = reader.ReadProperty<Texture>();
					}
					else
					{
						reader.ReadIntoProperty<Texture>(light.cookie);
					}
					break;
				case "flare":
					if (light.flare == null)
					{
						light.flare = reader.ReadProperty<Flare>();
					}
					else
					{
						reader.ReadIntoProperty<Flare>(light.flare);
					}
					break;
				case "renderMode":
					light.renderMode = reader.ReadProperty<LightRenderMode>();
					break;
				case "alreadyLightmapped":
				{
					LightBakingOutput bakingOutput = light.bakingOutput;
					bakingOutput.isBaked = reader.ReadProperty<bool>();
					light.bakingOutput = bakingOutput;
					break;
				}
				case "cullingMask":
					light.cullingMask = reader.ReadProperty<int>();
					break;
				case "enabled":
					light.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					light.tag = reader.ReadProperty<string>();
					break;
				case "name":
					light.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					light.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
