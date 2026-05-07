using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_DistanceJoint2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(DistanceJoint2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			DistanceJoint2D distanceJoint2D = (DistanceJoint2D)value;
			writer.WriteProperty<bool>("autoConfigureDistance", distanceJoint2D.autoConfigureDistance);
			writer.WriteProperty<float>("distance", distanceJoint2D.distance);
			writer.WriteProperty<bool>("maxDistanceOnly", distanceJoint2D.maxDistanceOnly);
			writer.WriteProperty<Vector2>("anchor", distanceJoint2D.anchor);
			writer.WriteProperty<Vector2>("connectedAnchor", distanceJoint2D.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", distanceJoint2D.autoConfigureConnectedAnchor);
			writer.WriteProperty<Rigidbody2D>("connectedBody", distanceJoint2D.connectedBody);
			writer.WriteProperty<bool>("enableCollision", distanceJoint2D.enableCollision);
			writer.WriteProperty<float>("breakForce", distanceJoint2D.breakForce);
			writer.WriteProperty<float>("breakTorque", distanceJoint2D.breakTorque);
			writer.WriteProperty<bool>("enabled", distanceJoint2D.enabled);
			writer.WriteProperty<string>("tag", distanceJoint2D.tag);
			writer.WriteProperty<string>("name", distanceJoint2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", distanceJoint2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			DistanceJoint2D distanceJoint2D = SaveGameType.CreateComponent<DistanceJoint2D>();
			this.ReadInto(distanceJoint2D, reader);
			return distanceJoint2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			DistanceJoint2D distanceJoint2D = (DistanceJoint2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "autoConfigureDistance":
					distanceJoint2D.autoConfigureDistance = reader.ReadProperty<bool>();
					break;
				case "distance":
					distanceJoint2D.distance = reader.ReadProperty<float>();
					break;
				case "maxDistanceOnly":
					distanceJoint2D.maxDistanceOnly = reader.ReadProperty<bool>();
					break;
				case "anchor":
					distanceJoint2D.anchor = reader.ReadProperty<Vector2>();
					break;
				case "connectedAnchor":
					distanceJoint2D.connectedAnchor = reader.ReadProperty<Vector2>();
					break;
				case "autoConfigureConnectedAnchor":
					distanceJoint2D.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "connectedBody":
					if (distanceJoint2D.connectedBody == null)
					{
						distanceJoint2D.connectedBody = reader.ReadProperty<Rigidbody2D>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody2D>(distanceJoint2D.connectedBody);
					}
					break;
				case "enableCollision":
					distanceJoint2D.enableCollision = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					distanceJoint2D.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					distanceJoint2D.breakTorque = reader.ReadProperty<float>();
					break;
				case "enabled":
					distanceJoint2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					distanceJoint2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					distanceJoint2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					distanceJoint2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
