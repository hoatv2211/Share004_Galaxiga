using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Extensions/UI Highlightable Extension")]
	[RequireComponent(typeof(RectTransform), typeof(Graphic))]
	public class UIHighlightable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		public bool Interactable
		{
			get
			{
				return this.m_Interactable;
			}
			set
			{
				this.m_Interactable = value;
				this.HighlightInteractable(this.m_Graphic);
				this.OnInteractableChanged.Invoke(this.m_Interactable);
			}
		}

		public bool ClickToHold
		{
			get
			{
				return this.m_ClickToHold;
			}
			set
			{
				this.m_ClickToHold = value;
			}
		}

		private void Awake()
		{
			this.m_Graphic = base.GetComponent<Graphic>();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.Interactable && !this.m_Pressed)
			{
				this.m_Highlighted = true;
				this.m_Graphic.color = this.HighlightedColor;
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.Interactable && !this.m_Pressed)
			{
				this.m_Highlighted = false;
				this.m_Graphic.color = this.NormalColor;
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.Interactable)
			{
				this.m_Graphic.color = this.PressedColor;
				if (this.ClickToHold)
				{
					this.m_Pressed = !this.m_Pressed;
				}
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (!this.m_Pressed)
			{
				this.HighlightInteractable(this.m_Graphic);
			}
		}

		private void HighlightInteractable(Graphic graphic)
		{
			if (this.m_Interactable)
			{
				if (this.m_Highlighted)
				{
					graphic.color = this.HighlightedColor;
				}
				else
				{
					graphic.color = this.NormalColor;
				}
			}
			else
			{
				graphic.color = this.DisabledColor;
			}
		}

		private Graphic m_Graphic;

		private bool m_Highlighted;

		private bool m_Pressed;

		[SerializeField]
		[Tooltip("Can this panel be interacted with or is it disabled? (does not affect child components)")]
		private bool m_Interactable = true;

		[SerializeField]
		[Tooltip("Does the panel remain in the pressed state when clicked? (default false)")]
		private bool m_ClickToHold;

		[Tooltip("The default color for the panel")]
		public Color NormalColor = Color.grey;

		[Tooltip("The color for the panel when a mouse is over it or it is in focus")]
		public Color HighlightedColor = Color.yellow;

		[Tooltip("The color for the panel when it is clicked/held")]
		public Color PressedColor = Color.green;

		[Tooltip("The color for the panel when it is not interactable (see Interactable)")]
		public Color DisabledColor = Color.gray;

		[Tooltip("Event for when the panel is enabled / disabled, to enable disabling / enabling of child or other gameobjects")]
		public UIHighlightable.InteractableChangedEvent OnInteractableChanged;

		[Serializable]
		public class InteractableChangedEvent : UnityEvent<bool>
		{
		}
	}
}
