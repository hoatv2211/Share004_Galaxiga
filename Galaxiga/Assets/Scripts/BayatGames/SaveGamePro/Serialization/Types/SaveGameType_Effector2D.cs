using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Effector2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Effector2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Effector2D effector2D = (Effector2D)value;
			writer.WriteProperty<bool>("useColliderMask", effector2D.useColliderMask);
			writer.WriteProperty<int>("colliderMask", effector2D.colliderMask);
			writer.WriteProperty<bool>("enabled", effector2D.enabled);
			writer.WriteProperty<string>("tag", effector2D.tag);
			writer.WriteProperty<string>("name", effector2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", effector2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Effector2D effector2D = SaveGameType.CreateComponent<Effector2D>();
			this.ReadInto(effector2D, reader);
			return effector2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Effector2D effector2D = (Effector2D)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "useColliderMask"))
					{
						if (!(text == "colliderMask"))
						{
							if (!(text == "enabled"))
							{
								if (!(text == "tag"))
								{
									if (!(text == "name"))
									{
										if (text == "hideFlags")
										{
											effector2D.hideFlags = reader.ReadProperty<HideFlags>();
										}
									}
									else
									{
										effector2D.name = reader.ReadProperty<string>();
									}
								}
								else
								{
									effector2D.tag = reader.ReadProperty<string>();
								}
							}
							else
							{
								effector2D.enabled = reader.ReadProperty<bool>();
							}
						}
						else
						{
							effector2D.colliderMask = reader.ReadProperty<int>();
						}
					}
					else
					{
						effector2D.useColliderMask = reader.ReadProperty<bool>();
					}
				}
			}
		}
	}
}
