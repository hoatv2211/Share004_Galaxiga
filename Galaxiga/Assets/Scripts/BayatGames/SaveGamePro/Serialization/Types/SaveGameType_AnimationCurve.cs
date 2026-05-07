using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AnimationCurve : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AnimationCurve);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AnimationCurve animationCurve = (AnimationCurve)value;
			writer.WriteProperty<Keyframe[]>("keys", animationCurve.keys);
			writer.WriteProperty<WrapMode>("preWrapMode", animationCurve.preWrapMode);
			writer.WriteProperty<WrapMode>("postWrapMode", animationCurve.postWrapMode);
		}

		public override object Read(ISaveGameReader reader)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			this.ReadInto(animationCurve, reader);
			return animationCurve;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AnimationCurve animationCurve = (AnimationCurve)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "keys"))
					{
						if (!(text == "preWrapMode"))
						{
							if (text == "postWrapMode")
							{
								animationCurve.postWrapMode = reader.ReadProperty<WrapMode>();
							}
						}
						else
						{
							animationCurve.preWrapMode = reader.ReadProperty<WrapMode>();
						}
					}
					else
					{
						animationCurve.keys = reader.ReadProperty<Keyframe[]>();
					}
				}
			}
		}
	}
}
