using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Tree : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Tree);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Tree tree = (Tree)value;
			writer.WriteProperty<string>("dataType", tree.data.GetType().AssemblyQualifiedName);
			writer.WriteProperty<ScriptableObject>("data", tree.data);
			writer.WriteProperty<string>("tag", tree.tag);
			writer.WriteProperty<string>("name", tree.name);
			writer.WriteProperty<HideFlags>("hideFlags", tree.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Tree tree = SaveGameType.CreateComponent<Tree>();
			this.ReadInto(tree, reader);
			return tree;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Tree tree = (Tree)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "data"))
					{
						if (!(text == "tag"))
						{
							if (!(text == "name"))
							{
								if (text == "hideFlags")
								{
									tree.hideFlags = reader.ReadProperty<HideFlags>();
								}
							}
							else
							{
								tree.name = reader.ReadProperty<string>();
							}
						}
						else
						{
							tree.tag = reader.ReadProperty<string>();
						}
					}
					else
					{
						Type type = Type.GetType(reader.ReadProperty<string>());
						if (tree.data == null)
						{
							tree.data = (ScriptableObject)reader.ReadProperty(type);
						}
						else
						{
							reader.ReadIntoProperty<ScriptableObject>(tree.data);
						}
					}
				}
			}
		}
	}
}
