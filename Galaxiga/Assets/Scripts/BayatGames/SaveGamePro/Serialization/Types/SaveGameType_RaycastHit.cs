using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_RaycastHit : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(RaycastHit);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			RaycastHit raycastHit = (RaycastHit)value;
			writer.WriteProperty<Vector3>("point", raycastHit.point);
			writer.WriteProperty<Vector3>("normal", raycastHit.normal);
			writer.WriteProperty<Vector3>("barycentricCoordinate", raycastHit.barycentricCoordinate);
			writer.WriteProperty<float>("distance", raycastHit.distance);
		}

		public override object Read(ISaveGameReader reader)
		{
			RaycastHit raycastHit = default(RaycastHit);
			foreach (string text in reader.Properties)
			{
				if (text != null)
				{
					if (!(text == "point"))
					{
						if (!(text == "normal"))
						{
							if (!(text == "barycentricCoordinate"))
							{
								if (text == "distance")
								{
									raycastHit.distance = reader.ReadProperty<float>();
								}
							}
							else
							{
								raycastHit.barycentricCoordinate = reader.ReadProperty<Vector3>();
							}
						}
						else
						{
							raycastHit.normal = reader.ReadProperty<Vector3>();
						}
					}
					else
					{
						raycastHit.point = reader.ReadProperty<Vector3>();
					}
				}
			}
			return raycastHit;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
