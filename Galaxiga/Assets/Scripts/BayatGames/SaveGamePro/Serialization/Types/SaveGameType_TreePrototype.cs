using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_TreePrototype : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(TreePrototype);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			TreePrototype treePrototype = (TreePrototype)value;
			writer.WriteProperty<GameObject>("prefab", treePrototype.prefab);
			writer.WriteProperty<float>("bendFactor", treePrototype.bendFactor);
		}

		public override object Read(ISaveGameReader reader)
		{
			TreePrototype treePrototype = new TreePrototype();
			this.ReadInto(treePrototype, reader);
			return treePrototype;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			TreePrototype treePrototype = (TreePrototype)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "prefab"))
					{
						if (text == "bendFactor")
						{
							treePrototype.bendFactor = reader.ReadProperty<float>();
						}
					}
					else if (treePrototype.prefab == null)
					{
						treePrototype.prefab = reader.ReadProperty<GameObject>();
					}
					else
					{
						reader.ReadIntoProperty<GameObject>(treePrototype.prefab);
					}
				}
			}
		}
	}
}
