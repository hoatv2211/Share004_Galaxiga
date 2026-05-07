using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AnimatorOverrideController : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AnimatorOverrideController);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AnimatorOverrideController animatorOverrideController = (AnimatorOverrideController)value;
			writer.WriteProperty<RuntimeAnimatorController>("runtimeAnimatorController", animatorOverrideController.runtimeAnimatorController);
			writer.WriteProperty<string>("name", animatorOverrideController.name);
			writer.WriteProperty<HideFlags>("hideFlags", animatorOverrideController.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AnimatorOverrideController animatorOverrideController = new AnimatorOverrideController();
			this.ReadInto(animatorOverrideController, reader);
			return animatorOverrideController;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AnimatorOverrideController animatorOverrideController = (AnimatorOverrideController)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "runtimeAnimatorController"))
					{
						if (!(text == "name"))
						{
							if (text == "hideFlags")
							{
								animatorOverrideController.hideFlags = reader.ReadProperty<HideFlags>();
							}
						}
						else
						{
							animatorOverrideController.name = reader.ReadProperty<string>();
						}
					}
					else if (animatorOverrideController.runtimeAnimatorController == null)
					{
						animatorOverrideController.runtimeAnimatorController = reader.ReadProperty<RuntimeAnimatorController>();
					}
					else
					{
						reader.ReadIntoProperty<RuntimeAnimatorController>(animatorOverrideController.runtimeAnimatorController);
					}
				}
			}
		}
	}
}
