using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AnchoredJoint2D : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AnchoredJoint2D);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AnchoredJoint2D anchoredJoint2D = (AnchoredJoint2D)value;
			writer.WriteProperty<Vector2>("anchor", anchoredJoint2D.anchor);
			writer.WriteProperty<Vector2>("connectedAnchor", anchoredJoint2D.connectedAnchor);
			writer.WriteProperty<bool>("autoConfigureConnectedAnchor", anchoredJoint2D.autoConfigureConnectedAnchor);
			writer.WriteProperty<Rigidbody2D>("connectedBody", anchoredJoint2D.connectedBody);
			writer.WriteProperty<bool>("enableCollision", anchoredJoint2D.enableCollision);
			writer.WriteProperty<float>("breakForce", anchoredJoint2D.breakForce);
			writer.WriteProperty<float>("breakTorque", anchoredJoint2D.breakTorque);
			writer.WriteProperty<bool>("enabled", anchoredJoint2D.enabled);
			writer.WriteProperty<string>("tag", anchoredJoint2D.tag);
			writer.WriteProperty<string>("name", anchoredJoint2D.name);
			writer.WriteProperty<HideFlags>("hideFlags", anchoredJoint2D.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AnchoredJoint2D anchoredJoint2D = SaveGameType.CreateComponent<AnchoredJoint2D>();
			this.ReadInto(anchoredJoint2D, reader);
			return anchoredJoint2D;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AnchoredJoint2D anchoredJoint2D = (AnchoredJoint2D)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "anchor":
					anchoredJoint2D.anchor = reader.ReadProperty<Vector2>();
					break;
				case "connectedAnchor":
					anchoredJoint2D.connectedAnchor = reader.ReadProperty<Vector2>();
					break;
				case "autoConfigureConnectedAnchor":
					anchoredJoint2D.autoConfigureConnectedAnchor = reader.ReadProperty<bool>();
					break;
				case "connectedBody":
					if (anchoredJoint2D.connectedBody == null)
					{
						anchoredJoint2D.connectedBody = reader.ReadProperty<Rigidbody2D>();
					}
					else
					{
						reader.ReadIntoProperty<Rigidbody2D>(anchoredJoint2D.connectedBody);
					}
					break;
				case "enableCollision":
					anchoredJoint2D.enableCollision = reader.ReadProperty<bool>();
					break;
				case "breakForce":
					anchoredJoint2D.breakForce = reader.ReadProperty<float>();
					break;
				case "breakTorque":
					anchoredJoint2D.breakTorque = reader.ReadProperty<float>();
					break;
				case "enabled":
					anchoredJoint2D.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					anchoredJoint2D.tag = reader.ReadProperty<string>();
					break;
				case "name":
					anchoredJoint2D.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					anchoredJoint2D.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
