using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_RuntimeAnimatorController : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(RuntimeAnimatorController);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			RuntimeAnimatorController runtimeAnimatorController = (RuntimeAnimatorController)value;
			writer.WriteProperty<string>("name", runtimeAnimatorController.name);
			writer.WriteProperty<HideFlags>("hideFlags", runtimeAnimatorController.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			return base.Read(reader);
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			RuntimeAnimatorController runtimeAnimatorController = (RuntimeAnimatorController)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "name"))
					{
						if (text == "hideFlags")
						{
							runtimeAnimatorController.hideFlags = reader.ReadProperty<HideFlags>();
						}
					}
					else
					{
						runtimeAnimatorController.name = reader.ReadProperty<string>();
					}
				}
			}
		}
	}
}
