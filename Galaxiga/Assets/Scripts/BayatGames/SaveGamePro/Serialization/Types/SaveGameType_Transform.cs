using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Transform : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Transform);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Transform transform = (Transform)value;
			writer.WriteProperty<Vector3>("position", transform.position);
			writer.WriteProperty<Vector3>("localPosition", transform.localPosition);
			writer.WriteProperty<Quaternion>("rotation", transform.rotation);
			writer.WriteProperty<Quaternion>("localRotation", transform.localRotation);
			writer.WriteProperty<Vector3>("localScale", transform.localScale);
			writer.WriteProperty<Transform>("parent", transform.parent);
			writer.WriteProperty<string>("tag", transform.tag);
			writer.WriteProperty<string>("name", transform.name);
			writer.WriteProperty<HideFlags>("hideFlags", transform.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Transform transform = SaveGameType.CreateComponent<Transform>();
			this.ReadInto(transform, reader);
			return transform;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Transform transform = (Transform)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "position":
					transform.position = reader.ReadProperty<Vector3>();
					break;
				case "localPosition":
					transform.localPosition = reader.ReadProperty<Vector3>();
					break;
				case "eulerAngles":
					reader.ReadProperty<Vector3>();
					break;
				case "localEulerAngles":
					reader.ReadProperty<Vector3>();
					break;
				case "right":
					reader.ReadProperty<Vector3>();
					break;
				case "up":
					reader.ReadProperty<Vector3>();
					break;
				case "forward":
					reader.ReadProperty<Vector3>();
					break;
				case "rotation":
					transform.rotation = reader.ReadProperty<Quaternion>();
					break;
				case "localRotation":
					transform.localRotation = reader.ReadProperty<Quaternion>();
					break;
				case "localScale":
					transform.localScale = reader.ReadProperty<Vector3>();
					break;
				case "parent":
					if (transform.parent == null)
					{
						transform.parent = reader.ReadProperty<Transform>();
					}
					else
					{
						reader.ReadIntoProperty<Transform>(transform.parent);
					}
					break;
				case "hasChanged":
					reader.ReadProperty<bool>();
					break;
				case "hierarchyCapacity":
					reader.ReadProperty<int>();
					break;
				case "tag":
					transform.tag = reader.ReadProperty<string>();
					break;
				case "name":
					transform.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					transform.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
