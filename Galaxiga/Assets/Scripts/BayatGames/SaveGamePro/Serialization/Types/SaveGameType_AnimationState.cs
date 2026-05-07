using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AnimationState : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AnimationState);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AnimationState animationState = (AnimationState)value;
			writer.WriteProperty<bool>("enabled", animationState.enabled);
			writer.WriteProperty<float>("weight", animationState.weight);
			writer.WriteProperty<WrapMode>("wrapMode", animationState.wrapMode);
			writer.WriteProperty<float>("time", animationState.time);
			writer.WriteProperty<float>("normalizedTime", animationState.normalizedTime);
			writer.WriteProperty<float>("speed", animationState.speed);
			writer.WriteProperty<float>("normalizedSpeed", animationState.normalizedSpeed);
			writer.WriteProperty<int>("layer", animationState.layer);
			writer.WriteProperty<string>("name", animationState.name);
			writer.WriteProperty<AnimationBlendMode>("blendMode", animationState.blendMode);
		}

		public override object Read(ISaveGameReader reader)
		{
			AnimationState animationState = new AnimationState();
			this.ReadInto(animationState, reader);
			return animationState;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AnimationState animationState = (AnimationState)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "enabled":
					animationState.enabled = reader.ReadProperty<bool>();
					break;
				case "weight":
					animationState.weight = reader.ReadProperty<float>();
					break;
				case "wrapMode":
					animationState.wrapMode = reader.ReadProperty<WrapMode>();
					break;
				case "time":
					animationState.time = reader.ReadProperty<float>();
					break;
				case "normalizedTime":
					animationState.normalizedTime = reader.ReadProperty<float>();
					break;
				case "speed":
					animationState.speed = reader.ReadProperty<float>();
					break;
				case "normalizedSpeed":
					animationState.normalizedSpeed = reader.ReadProperty<float>();
					break;
				case "layer":
					animationState.layer = reader.ReadProperty<int>();
					break;
				case "name":
					animationState.name = reader.ReadProperty<string>();
					break;
				case "blendMode":
					animationState.blendMode = reader.ReadProperty<AnimationBlendMode>();
					break;
				}
			}
		}
	}
}
