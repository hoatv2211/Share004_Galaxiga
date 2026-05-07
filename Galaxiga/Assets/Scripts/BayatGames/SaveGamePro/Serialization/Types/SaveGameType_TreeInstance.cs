using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_TreeInstance : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(TreeInstance);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			TreeInstance treeInstance = (TreeInstance)value;
			writer.WriteProperty<Vector3>("position", treeInstance.position);
			writer.WriteProperty<float>("widthScale", treeInstance.widthScale);
			writer.WriteProperty<float>("heightScale", treeInstance.heightScale);
			writer.WriteProperty<float>("rotation", treeInstance.rotation);
			writer.WriteProperty<Color32>("color", treeInstance.color);
			writer.WriteProperty<Color32>("lightmapColor", treeInstance.lightmapColor);
			writer.WriteProperty<int>("prototypeIndex", treeInstance.prototypeIndex);
		}

		public override object Read(ISaveGameReader reader)
		{
			TreeInstance treeInstance = default(TreeInstance);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "position":
					treeInstance.position = reader.ReadProperty<Vector3>();
					break;
				case "widthScale":
					treeInstance.widthScale = reader.ReadProperty<float>();
					break;
				case "heightScale":
					treeInstance.heightScale = reader.ReadProperty<float>();
					break;
				case "rotation":
					treeInstance.rotation = reader.ReadProperty<float>();
					break;
				case "color":
					treeInstance.color = reader.ReadProperty<Color32>();
					break;
				case "lightmapColor":
					treeInstance.lightmapColor = reader.ReadProperty<Color32>();
					break;
				case "prototypeIndex":
					treeInstance.prototypeIndex = reader.ReadProperty<int>();
					break;
				}
			}
			return treeInstance;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
