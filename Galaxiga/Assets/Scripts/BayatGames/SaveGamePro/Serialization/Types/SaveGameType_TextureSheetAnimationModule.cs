using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_TextureSheetAnimationModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ParticleSystem.TextureSheetAnimationModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ParticleSystem.TextureSheetAnimationModule textureSheetAnimationModule = (ParticleSystem.TextureSheetAnimationModule)value;
			writer.WriteProperty<bool>("enabled", textureSheetAnimationModule.enabled);
			writer.WriteProperty<ParticleSystemAnimationMode>("mode", textureSheetAnimationModule.mode);
			writer.WriteProperty<int>("numTilesX", textureSheetAnimationModule.numTilesX);
			writer.WriteProperty<int>("numTilesY", textureSheetAnimationModule.numTilesY);
			writer.WriteProperty<ParticleSystemAnimationType>("animation", textureSheetAnimationModule.animation);
			writer.WriteProperty<bool>("useRandomRow", textureSheetAnimationModule.useRandomRow);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("frameOverTime", textureSheetAnimationModule.frameOverTime);
			writer.WriteProperty<float>("frameOverTimeMultiplier", textureSheetAnimationModule.frameOverTimeMultiplier);
			writer.WriteProperty<ParticleSystem.MinMaxCurve>("startFrame", textureSheetAnimationModule.startFrame);
			writer.WriteProperty<float>("startFrameMultiplier", textureSheetAnimationModule.startFrameMultiplier);
			writer.WriteProperty<int>("cycleCount", textureSheetAnimationModule.cycleCount);
			writer.WriteProperty<int>("rowIndex", textureSheetAnimationModule.rowIndex);
			writer.WriteProperty<UVChannelFlags>("uvChannelMask", textureSheetAnimationModule.uvChannelMask);
			writer.WriteProperty<float>("flipU", textureSheetAnimationModule.flipU);
			writer.WriteProperty<float>("flipV", textureSheetAnimationModule.flipV);
		}

		public override object Read(ISaveGameReader reader)
		{
			ParticleSystem.TextureSheetAnimationModule textureSheetAnimationModule = default(ParticleSystem.TextureSheetAnimationModule);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					textureSheetAnimationModule.enabled = reader.ReadProperty<bool>();
					break;
				case "mode":
					textureSheetAnimationModule.mode = reader.ReadProperty<ParticleSystemAnimationMode>();
					break;
				case "numTilesX":
					textureSheetAnimationModule.numTilesX = reader.ReadProperty<int>();
					break;
				case "numTilesY":
					textureSheetAnimationModule.numTilesY = reader.ReadProperty<int>();
					break;
				case "animation":
					textureSheetAnimationModule.animation = reader.ReadProperty<ParticleSystemAnimationType>();
					break;
				case "useRandomRow":
					textureSheetAnimationModule.useRandomRow = reader.ReadProperty<bool>();
					break;
				case "frameOverTime":
					textureSheetAnimationModule.frameOverTime = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "frameOverTimeMultiplier":
					textureSheetAnimationModule.frameOverTimeMultiplier = reader.ReadProperty<float>();
					break;
				case "startFrame":
					textureSheetAnimationModule.startFrame = reader.ReadProperty<ParticleSystem.MinMaxCurve>();
					break;
				case "startFrameMultiplier":
					textureSheetAnimationModule.startFrameMultiplier = reader.ReadProperty<float>();
					break;
				case "cycleCount":
					textureSheetAnimationModule.cycleCount = reader.ReadProperty<int>();
					break;
				case "rowIndex":
					textureSheetAnimationModule.rowIndex = reader.ReadProperty<int>();
					break;
				case "uvChannelMask":
					textureSheetAnimationModule.uvChannelMask = reader.ReadProperty<UVChannelFlags>();
					break;
				case "flipU":
					textureSheetAnimationModule.flipU = reader.ReadProperty<float>();
					break;
				case "flipV":
					textureSheetAnimationModule.flipV = reader.ReadProperty<float>();
					break;
				}
			}
			return textureSheetAnimationModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
