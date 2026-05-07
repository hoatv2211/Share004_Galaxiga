using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_ScrollRect : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(ScrollRect);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			ScrollRect scrollRect = (ScrollRect)value;
			writer.WriteProperty<RectTransform>("content", scrollRect.content);
			writer.WriteProperty<bool>("horizontal", scrollRect.horizontal);
			writer.WriteProperty<bool>("vertical", scrollRect.vertical);
			writer.WriteProperty<ScrollRect.MovementType>("movementType", scrollRect.movementType);
			writer.WriteProperty<float>("elasticity", scrollRect.elasticity);
			writer.WriteProperty<bool>("inertia", scrollRect.inertia);
			writer.WriteProperty<float>("decelerationRate", scrollRect.decelerationRate);
			writer.WriteProperty<float>("scrollSensitivity", scrollRect.scrollSensitivity);
			writer.WriteProperty<RectTransform>("viewport", scrollRect.viewport);
			writer.WriteProperty<Scrollbar>("horizontalScrollbar", scrollRect.horizontalScrollbar);
			writer.WriteProperty<Scrollbar>("verticalScrollbar", scrollRect.verticalScrollbar);
			writer.WriteProperty<ScrollRect.ScrollbarVisibility>("horizontalScrollbarVisibility", scrollRect.horizontalScrollbarVisibility);
			writer.WriteProperty<ScrollRect.ScrollbarVisibility>("verticalScrollbarVisibility", scrollRect.verticalScrollbarVisibility);
			writer.WriteProperty<float>("horizontalScrollbarSpacing", scrollRect.horizontalScrollbarSpacing);
			writer.WriteProperty<float>("verticalScrollbarSpacing", scrollRect.verticalScrollbarSpacing);
			writer.WriteProperty<Vector2>("velocity", scrollRect.velocity);
			writer.WriteProperty<Vector2>("normalizedPosition", scrollRect.normalizedPosition);
			writer.WriteProperty<float>("horizontalNormalizedPosition", scrollRect.horizontalNormalizedPosition);
			writer.WriteProperty<float>("verticalNormalizedPosition", scrollRect.verticalNormalizedPosition);
			writer.WriteProperty<bool>("useGUILayout", scrollRect.useGUILayout);
			writer.WriteProperty<bool>("enabled", scrollRect.enabled);
			writer.WriteProperty<string>("tag", scrollRect.tag);
			writer.WriteProperty<string>("name", scrollRect.name);
			writer.WriteProperty<HideFlags>("hideFlags", scrollRect.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			ScrollRect scrollRect = SaveGameType.CreateComponent<ScrollRect>();
			this.ReadInto(scrollRect, reader);
			return scrollRect;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			ScrollRect scrollRect = (ScrollRect)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "content":
					if (scrollRect.content == null)
					{
						scrollRect.content = reader.ReadProperty<RectTransform>();
					}
					else
					{
						reader.ReadIntoProperty<RectTransform>(scrollRect.content);
					}
					break;
				case "horizontal":
					scrollRect.horizontal = reader.ReadProperty<bool>();
					break;
				case "vertical":
					scrollRect.vertical = reader.ReadProperty<bool>();
					break;
				case "movementType":
					scrollRect.movementType = reader.ReadProperty<ScrollRect.MovementType>();
					break;
				case "elasticity":
					scrollRect.elasticity = reader.ReadProperty<float>();
					break;
				case "inertia":
					scrollRect.inertia = reader.ReadProperty<bool>();
					break;
				case "decelerationRate":
					scrollRect.decelerationRate = reader.ReadProperty<float>();
					break;
				case "scrollSensitivity":
					scrollRect.scrollSensitivity = reader.ReadProperty<float>();
					break;
				case "viewport":
					if (scrollRect.viewport == null)
					{
						scrollRect.viewport = reader.ReadProperty<RectTransform>();
					}
					else
					{
						reader.ReadIntoProperty<RectTransform>(scrollRect.viewport);
					}
					break;
				case "horizontalScrollbar":
					if (scrollRect.horizontalScrollbar == null)
					{
						scrollRect.horizontalScrollbar = reader.ReadProperty<Scrollbar>();
					}
					else
					{
						reader.ReadIntoProperty<Scrollbar>(scrollRect.horizontalScrollbar);
					}
					break;
				case "verticalScrollbar":
					if (scrollRect.verticalScrollbar == null)
					{
						scrollRect.verticalScrollbar = reader.ReadProperty<Scrollbar>();
					}
					else
					{
						reader.ReadIntoProperty<Scrollbar>(scrollRect.verticalScrollbar);
					}
					break;
				case "horizontalScrollbarVisibility":
					scrollRect.horizontalScrollbarVisibility = reader.ReadProperty<ScrollRect.ScrollbarVisibility>();
					break;
				case "verticalScrollbarVisibility":
					scrollRect.verticalScrollbarVisibility = reader.ReadProperty<ScrollRect.ScrollbarVisibility>();
					break;
				case "horizontalScrollbarSpacing":
					scrollRect.horizontalScrollbarSpacing = reader.ReadProperty<float>();
					break;
				case "verticalScrollbarSpacing":
					scrollRect.verticalScrollbarSpacing = reader.ReadProperty<float>();
					break;
				case "velocity":
					scrollRect.velocity = reader.ReadProperty<Vector2>();
					break;
				case "normalizedPosition":
					scrollRect.normalizedPosition = reader.ReadProperty<Vector2>();
					break;
				case "horizontalNormalizedPosition":
					scrollRect.horizontalNormalizedPosition = reader.ReadProperty<float>();
					break;
				case "verticalNormalizedPosition":
					scrollRect.verticalNormalizedPosition = reader.ReadProperty<float>();
					break;
				case "useGUILayout":
					scrollRect.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					scrollRect.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					scrollRect.tag = reader.ReadProperty<string>();
					break;
				case "name":
					scrollRect.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					scrollRect.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
