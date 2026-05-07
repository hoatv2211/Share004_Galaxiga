using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_RaycastHit2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(RaycastHit2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			RaycastHit2D raycastHit2D = (RaycastHit2D)value;
			writer.WriteProperty<Vector2>("centroid", raycastHit2D.centroid);
			writer.WriteProperty<Vector2>("point", raycastHit2D.point);
			writer.WriteProperty<Vector2>("normal", raycastHit2D.normal);
			writer.WriteProperty<float>("distance", raycastHit2D.distance);
			writer.WriteProperty<float>("fraction", raycastHit2D.fraction);
		}

		public override object Read(ISaveGameReader reader)
		{
			RaycastHit2D raycastHit2D = default(RaycastHit2D);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "centroid"))
					{
						if (!(text == "point"))
						{
							if (!(text == "normal"))
							{
								if (!(text == "distance"))
								{
									if (text == "fraction")
									{
										raycastHit2D.fraction = reader.ReadProperty<float>();
									}
								}
								else
								{
									raycastHit2D.distance = reader.ReadProperty<float>();
								}
							}
							else
							{
								raycastHit2D.normal = reader.ReadProperty<Vector2>();
							}
						}
						else
						{
							raycastHit2D.point = reader.ReadProperty<Vector2>();
						}
					}
					else
					{
						raycastHit2D.centroid = reader.ReadProperty<Vector2>();
					}
				}
			}
			return raycastHit2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
