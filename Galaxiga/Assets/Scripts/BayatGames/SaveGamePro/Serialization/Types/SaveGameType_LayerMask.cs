using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_LayerMask : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(LayerMask);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			writer.WriteProperty<int>("value", ((LayerMask)value).value);
		}

		public override object Read(ISaveGameReader reader)
		{
			LayerMask layerMask = default(LayerMask);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (text == "value")
					{
						layerMask.value = reader.ReadProperty<int>();
					}
				}
			}
			return layerMask;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
