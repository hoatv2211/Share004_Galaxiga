using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_WheelHit : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(WheelHit);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			WheelHit wheelHit = (WheelHit)value;
			writer.WriteProperty<Collider>("collider", wheelHit.collider);
			writer.WriteProperty<Vector3>("point", wheelHit.point);
			writer.WriteProperty<Vector3>("normal", wheelHit.normal);
			writer.WriteProperty<Vector3>("forwardDir", wheelHit.forwardDir);
			writer.WriteProperty<Vector3>("sidewaysDir", wheelHit.sidewaysDir);
			writer.WriteProperty<float>("force", wheelHit.force);
			writer.WriteProperty<float>("forwardSlip", wheelHit.forwardSlip);
			writer.WriteProperty<float>("sidewaysSlip", wheelHit.sidewaysSlip);
		}

		public override object Read(ISaveGameReader reader)
		{
			WheelHit wheelHit = default(WheelHit);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "collider":
					if (wheelHit.collider == null)
					{
						wheelHit.collider = reader.ReadProperty<Collider>();
					}
					else
					{
						reader.ReadIntoProperty<Collider>(wheelHit.collider);
					}
					break;
				case "point":
					wheelHit.point = reader.ReadProperty<Vector3>();
					break;
				case "normal":
					wheelHit.normal = reader.ReadProperty<Vector3>();
					break;
				case "forwardDir":
					wheelHit.forwardDir = reader.ReadProperty<Vector3>();
					break;
				case "sidewaysDir":
					wheelHit.sidewaysDir = reader.ReadProperty<Vector3>();
					break;
				case "force":
					wheelHit.force = reader.ReadProperty<float>();
					break;
				case "forwardSlip":
					wheelHit.forwardSlip = reader.ReadProperty<float>();
					break;
				case "sidewaysSlip":
					wheelHit.sidewaysSlip = reader.ReadProperty<float>();
					break;
				}
			}
			return wheelHit;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
