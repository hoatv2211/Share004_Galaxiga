using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_PhysicsMaterial2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(PhysicsMaterial2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			PhysicsMaterial2D physicsMaterial2D = (PhysicsMaterial2D)value;
			writer.WriteProperty<float>("bounciness", physicsMaterial2D.bounciness);
			writer.WriteProperty<float>("friction", physicsMaterial2D.friction);
			writer.WriteProperty<string>("name", physicsMaterial2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", physicsMaterial2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			PhysicsMaterial2D physicsMaterial2D = new PhysicsMaterial2D();
			this.ReadInto(physicsMaterial2D, reader);
			return physicsMaterial2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			PhysicsMaterial2D physicsMaterial2D = (PhysicsMaterial2D)value;
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "bounciness"))
					{
						if (!(text == "friction"))
						{
							if (!(text == "name"))
							{
								if (text == "hideFlags")
								{
									physicsMaterial2D.hideFlags = reader.ReadProperty<HideFlags>();
								}
							}
							else
							{
								physicsMaterial2D.name = reader.ReadProperty<string>();
							}
						}
						else
						{
							physicsMaterial2D.friction = reader.ReadProperty<float>();
						}
					}
					else
					{
						physicsMaterial2D.bounciness = reader.ReadProperty<float>();
					}
				}
			}
		}
	}
}
