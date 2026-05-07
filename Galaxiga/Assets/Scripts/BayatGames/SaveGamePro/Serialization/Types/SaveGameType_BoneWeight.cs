using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_BoneWeight : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(BoneWeight);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			BoneWeight boneWeight = (BoneWeight)value;
			writer.WriteProperty<float>("weight0", boneWeight.weight0);
			writer.WriteProperty<float>("weight1", boneWeight.weight1);
			writer.WriteProperty<float>("weight2", boneWeight.weight2);
			writer.WriteProperty<float>("weight3", boneWeight.weight3);
			writer.WriteProperty<int>("boneIndex0", boneWeight.boneIndex0);
			writer.WriteProperty<int>("boneIndex1", boneWeight.boneIndex1);
			writer.WriteProperty<int>("boneIndex2", boneWeight.boneIndex2);
			writer.WriteProperty<int>("boneIndex3", boneWeight.boneIndex3);
		}

		public override object Read(ISaveGameReader reader)
		{
			BoneWeight boneWeight = default(BoneWeight);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "weight0":
					boneWeight.weight0 = reader.ReadProperty<float>();
					break;
				case "weight1":
					boneWeight.weight1 = reader.ReadProperty<float>();
					break;
				case "weight2":
					boneWeight.weight2 = reader.ReadProperty<float>();
					break;
				case "weight3":
					boneWeight.weight3 = reader.ReadProperty<float>();
					break;
				case "boneIndex0":
					boneWeight.boneIndex0 = reader.ReadProperty<int>();
					break;
				case "boneIndex1":
					boneWeight.boneIndex1 = reader.ReadProperty<int>();
					break;
				case "boneIndex2":
					boneWeight.boneIndex2 = reader.ReadProperty<int>();
					break;
				case "boneIndex3":
					boneWeight.boneIndex3 = reader.ReadProperty<int>();
					break;
				}
			}
			return boneWeight;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
