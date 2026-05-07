using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Touch : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Touch);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Touch touch = (Touch)value;
			writer.WriteProperty<int>("fingerId", touch.fingerId);
			writer.WriteProperty<Vector2>("position", touch.position);
			writer.WriteProperty<Vector2>("rawPosition", touch.rawPosition);
			writer.WriteProperty<Vector2>("deltaPosition", touch.deltaPosition);
			writer.WriteProperty<float>("deltaTime", touch.deltaTime);
			writer.WriteProperty<int>("tapCount", touch.tapCount);
			writer.WriteProperty<TouchPhase>("phase", touch.phase);
			writer.WriteProperty<float>("pressure", touch.pressure);
			writer.WriteProperty<float>("maximumPossiblePressure", touch.maximumPossiblePressure);
			writer.WriteProperty<TouchType>("type", touch.type);
			writer.WriteProperty<float>("altitudeAngle", touch.altitudeAngle);
			writer.WriteProperty<float>("azimuthAngle", touch.azimuthAngle);
			writer.WriteProperty<float>("radius", touch.radius);
			writer.WriteProperty<float>("radiusVariance", touch.radiusVariance);
		}

		public override object Read(ISaveGameReader reader)
		{
			Touch touch = default(Touch);
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "fingerId":
					touch.fingerId = reader.ReadProperty<int>();
					break;
				case "position":
					touch.position = reader.ReadProperty<Vector2>();
					break;
				case "rawPosition":
					touch.rawPosition = reader.ReadProperty<Vector2>();
					break;
				case "deltaPosition":
					touch.deltaPosition = reader.ReadProperty<Vector2>();
					break;
				case "deltaTime":
					touch.deltaTime = reader.ReadProperty<float>();
					break;
				case "tapCount":
					touch.tapCount = reader.ReadProperty<int>();
					break;
				case "phase":
					touch.phase = reader.ReadProperty<TouchPhase>();
					break;
				case "pressure":
					touch.pressure = reader.ReadProperty<float>();
					break;
				case "maximumPossiblePressure":
					touch.maximumPossiblePressure = reader.ReadProperty<float>();
					break;
				case "type":
					touch.type = reader.ReadProperty<TouchType>();
					break;
				case "altitudeAngle":
					touch.altitudeAngle = reader.ReadProperty<float>();
					break;
				case "azimuthAngle":
					touch.azimuthAngle = reader.ReadProperty<float>();
					break;
				case "radius":
					touch.radius = reader.ReadProperty<float>();
					break;
				case "radiusVariance":
					touch.radiusVariance = reader.ReadProperty<float>();
					break;
				}
			}
			return touch;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			base.ReadInto(value, reader);
		}
	}
}
