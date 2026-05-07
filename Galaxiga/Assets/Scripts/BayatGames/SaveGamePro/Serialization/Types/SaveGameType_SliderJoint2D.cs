using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_SliderJoint2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(SliderJoint2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			SliderJoint2D sliderJoint2D = (SliderJoint2D)value;
			writer.WriteProperty<bool>("autoConfigureAngle", sliderJoint2D.autoConfigureAngle);
			writer.WriteProperty<float>("angle", sliderJoint2D.angle);
			writer.WriteProperty<bool>("useMotor", sliderJoint2D.useMotor);
			writer.WriteProperty<bool>("useLimits", sliderJoint2D.useLimits);
			writer.WriteProperty<JointMotor2D>("motor", sliderJoint2D.motor);
			writer.WriteProperty<JointTranslationLimits2D>("limits", sliderJoint2D.limits);
			writer.WriteProperty<Vector2>("anchor", sliderJoint2D.anchor);
			writer.WriteProperty<Vector2>("connectedAnchor", sliderJoint2D.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", sliderJoint2D.autoConfigureConnectedAnchor);
			writer.WriteProperty<Rigidbody2D>("connectedBody", sliderJoint2D.connectedBody);
			writer.WriteProperty<bool>("enableCollision", sliderJoint2D.enableCollision);
			writer.WriteProperty<float>("breakForce", sliderJoint2D.breakForce);
			writer.WriteProperty<float>("breakTorque", sliderJoint2D.breakTorque);
			writer.WriteProperty<bool>("enabled", sliderJoint2D.enabled);
			writer.WriteProperty<string>("tag", sliderJoint2D.tag);
			writer.WriteProperty<string>("name", sliderJoint2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", sliderJoint2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			SliderJoint2D sliderJoint2D = SaveGameType.CreateComponent<SliderJoint2D>();
			this.ReadInto(sliderJoint2D, reader);
			return sliderJoint2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			SliderJoint2D sliderJoint2D = (SliderJoint2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "autoConfigureAngle":
					sliderJoint2D.autoConfigureAngle = reader.ReadProperty<bool>();
					break;
				case "angle":
					sliderJoint2D.angle = reader.ReadProperty<float>();
					break;
				case "useMotor":
					sliderJoint2D.useMotor = reader.ReadProperty<bool>();
					break;
				case "useLimits":
					sliderJoint2D.useLimits = reader.ReadProperty<bool>();
					break;
				case "motor":
					sliderJoint2D.motor = reader.ReadProperty<JointMotor2D>();
					break;
				case "limits":
					sliderJoint2D.limits = reader.ReadProperty<JointTranslationLimits2D>();
					break;
				case "anchor":
					sliderJoint2D.anchor = reader.ReadProperty<Vector2>();
					break;
				case "connectedAnchor":
					sliderJoint2D.connectedAnchor = reader.ReadProperty<Vector2>();
					break;
				case "autoConfigureConnectedAnchor":
					sliderJoint2D.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "connectedBody":
					if (sliderJoint2D.connectedBody == null)
					{
						sliderJoint2D.connectedBody = reader.ReadProperty<Rigidbody2D>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody2D>(sliderJoint2D.connectedBody);
					}
					break;
				case "enableCollision":
					sliderJoint2D.enableCollision = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					sliderJoint2D.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					sliderJoint2D.breakTorque = reader.ReadProperty<float>();
					break;
				case "enabled":
					sliderJoint2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					sliderJoint2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					sliderJoint2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					sliderJoint2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
