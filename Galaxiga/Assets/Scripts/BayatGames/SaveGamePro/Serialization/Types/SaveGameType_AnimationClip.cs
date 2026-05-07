using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AnimationClip : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AnimationClip);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AnimationClip animationClip = (AnimationClip)value;
			writer.WriteProperty<float>("frameRate", animationClip.frameRate);
			writer.WriteProperty<WrapMode>("wrapMode", animationClip.wrapMode);
			writer.WriteProperty<Bounds>("localBounds", animationClip.localBounds);
			writer.WriteProperty<bool>("legacy", animationClip.legacy);
			writer.WriteProperty<AnimationEvent[]>("events", animationClip.events);
			writer.WriteProperty<string>("name", animationClip.name);
			writer.WriteProperty<HideFlags>("hideFlags", animationClip.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AnimationClip animationClip = new AnimationClip();
			this.ReadInto(animationClip, reader);
			return animationClip;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AnimationClip animationClip = (AnimationClip)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "frameRate":
					animationClip.frameRate = reader.ReadProperty<float>();
					break;
				case "wrapMode":
					animationClip.wrapMode = reader.ReadProperty<WrapMode>();
					break;
				case "localBounds":
					animationClip.localBounds = reader.ReadProperty<Bounds>();
					break;
				case "legacy":
					animationClip.legacy = reader.ReadProperty<bool>();
					break;
				case "events":
					animationClip.events = reader.ReadProperty<AnimationEvent[]>();
					break;
				case "name":
					animationClip.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					animationClip.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
