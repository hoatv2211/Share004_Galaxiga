using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_RectTransform : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(RectTransform);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			RectTransform rectTransform = (RectTransform)value;
			writer.WriteProperty<Vector2>("anchorMin", rectTransform.anchorMin);
			writer.WriteProperty<Vector2>("anchorMax", rectTransform.anchorMax);
			writer.WriteProperty<Vector3>("anchoredPosition3D", rectTransform.anchoredPosition3D);
			writer.WriteProperty<Vector2>("anchoredPosition", rectTransform.anchoredPosition);
			writer.WriteProperty<Vector2>("sizeDelta", rectTransform.sizeDelta);
			writer.WriteProperty<Vector2>("pivot", rectTransform.pivot);
			writer.WriteProperty<Vector2>("offsetMin", rectTransform.offsetMin);
			writer.WriteProperty<Vector2>("offsetMax", rectTransform.offsetMax);
			writer.WriteProperty<Vector3>("position", rectTransform.position);
			writer.WriteProperty<Vector3>("localPosition", rectTransform.localPosition);
			writer.WriteProperty<Vector3>("eulerAngles", rectTransform.eulerAngles);
			writer.WriteProperty<Vector3>("localEulerAngles", rectTransform.localEulerAngles);
			writer.WriteProperty<Vector3>("right", rectTransform.right);
			writer.WriteProperty<Vector3>("up", rectTransform.up);
			writer.WriteProperty<Vector3>("forward", rectTransform.forward);
			writer.WriteProperty<Quaternion>("rotation", rectTransform.rotation);
			writer.WriteProperty<Quaternion>("localRotation", rectTransform.localRotation);
			writer.WriteProperty<Vector3>("localScale", rectTransform.localScale);
			writer.WriteProperty<Transform>("parent", rectTransform.parent);
			writer.WriteProperty<bool>("hasChanged", rectTransform.hasChanged);
			writer.WriteProperty<int>("hierarchyCapacity", rectTransform.hierarchyCapacity);
			writer.WriteProperty<string>("tag", rectTransform.tag);
			writer.WriteProperty<string>("name", rectTransform.name);
			writer.WriteProperty<HideFlags>("hideFlags", rectTransform.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			RectTransform rectTransform = SaveGameType.CreateComponent<RectTransform>();
			this.ReadInto(rectTransform, reader);
			return rectTransform;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			RectTransform rectTransform = (RectTransform)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "anchorMin":
					rectTransform.anchorMin = reader.ReadProperty<Vector2>();
					break;
				case "anchorMax":
					rectTransform.anchorMax = reader.ReadProperty<Vector2>();
					break;
				case "anchoredPosition3D":
					rectTransform.anchoredPosition3D = reader.ReadProperty<Vector3>();
					break;
				case "anchoredPosition":
					rectTransform.anchoredPosition = reader.ReadProperty<Vector2>();
					break;
				case "sizeDelta":
					rectTransform.sizeDelta = reader.ReadProperty<Vector2>();
					break;
				case "pivot":
					rectTransform.pivot = reader.ReadProperty<Vector2>();
					break;
				case "offsetMin":
					rectTransform.offsetMin = reader.ReadProperty<Vector2>();
					break;
				case "offsetMax":
					rectTransform.offsetMax = reader.ReadProperty<Vector2>();
					break;
				case "position":
					rectTransform.position = reader.ReadProperty<Vector3>();
					break;
				case "localPosition":
					rectTransform.localPosition = reader.ReadProperty<Vector3>();
					break;
				case "eulerAngles":
					rectTransform.eulerAngles = reader.ReadProperty<Vector3>();
					break;
				case "localEulerAngles":
					rectTransform.localEulerAngles = reader.ReadProperty<Vector3>();
					break;
				case "right":
					rectTransform.right = reader.ReadProperty<Vector3>();
					break;
				case "up":
					rectTransform.up = reader.ReadProperty<Vector3>();
					break;
				case "forward":
					rectTransform.forward = reader.ReadProperty<Vector3>();
					break;
				case "rotation":
					rectTransform.rotation = reader.ReadProperty<Quaternion>();
					break;
				case "localRotation":
					rectTransform.localRotation = reader.ReadProperty<Quaternion>();
					break;
				case "localScale":
					rectTransform.localScale = reader.ReadProperty<Vector3>();
					break;
				case "parent":
					if (rectTransform.parent == null)
					{
						rectTransform.SetParent(reader.ReadProperty<Transform>(), false);
					}
					else
					{
						reader.ReadIntoProperty<Transform>(rectTransform.parent);
					}
					break;
				case "hasChanged":
					rectTransform.hasChanged = reader.ReadProperty<bool>();
					break;
				case "hierarchyCapacity":
					rectTransform.hierarchyCapacity = reader.ReadProperty<int>();
					break;
				case "tag":
					rectTransform.tag = reader.ReadProperty<string>();
					break;
				case "name":
					rectTransform.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					rectTransform.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
