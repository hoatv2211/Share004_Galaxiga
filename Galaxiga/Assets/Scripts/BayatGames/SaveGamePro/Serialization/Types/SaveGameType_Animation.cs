using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Animation : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Animation);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Animation animation = (Animation)value;
			writer.WriteProperty<AnimationClip>("clip", animation.clip);
			writer.WriteProperty<bool>("playAutomatically", animation.playAutomatically);
			writer.WriteProperty<WrapMode>("wrapMode", animation.wrapMode);
			writer.WriteProperty<bool>("animatePhysics", animation.animatePhysics);
			writer.WriteProperty<AnimationCullingType>("cullingType", animation.cullingType);
			writer.WriteProperty<Bounds>("localBounds", animation.localBounds);
			writer.WriteProperty<bool>("enabled", animation.enabled);
			writer.WriteProperty<string>("tag", animation.tag);
			writer.WriteProperty<string>("name", animation.name);
			writer.WriteProperty<HideFlags>("hideFlags", animation.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Animation animation = SaveGameType.CreateComponent<Animation>();
			this.ReadInto(animation, reader);
			return animation;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Animation animation = (Animation)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "clip":
					if (animation.clip == null)
					{
						animation.clip = reader.ReadProperty<AnimationClip>();
					}
					else
					{
						reader.ReadIntoProperty<AnimationClip>(animation.clip);
					}
					break;
				case "playAutomatically":
					animation.playAutomatically = reader.ReadProperty<bool>();
					break;
				case "wrapMode":
					animation.wrapMode = reader.ReadProperty<WrapMode>();
					break;
				case "animatePhysics":
					animation.animatePhysics = reader.ReadProperty<bool>();
					break;
				case "cullingType":
					animation.cullingType = reader.ReadProperty<AnimationCullingType>();
					break;
				case "localBounds":
					animation.localBounds = reader.ReadProperty<Bounds>();
					break;
				case "enabled":
					animation.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					animation.tag = reader.ReadProperty<string>();
					break;
				case "name":
					animation.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					animation.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
