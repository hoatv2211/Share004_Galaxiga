using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Extensions/Segmented Control")]
	[RequireComponent(typeof(RectTransform))]
	public class SegmentedControl : UIBehaviour
	{
		protected SegmentedControl()
		{
		}

		protected float SeparatorWidth
		{
			get
			{
				if (this.m_separatorWidth == 0f && this.separator)
				{
					this.m_separatorWidth = this.separator.rectTransform.rect.width;
					Image component = this.separator.GetComponent<Image>();
					if (component)
					{
						this.m_separatorWidth /= component.pixelsPerUnit;
					}
				}
				return this.m_separatorWidth;
			}
		}

		public Selectable[] segments
		{
			get
			{
				if (this.m_segments == null || this.m_segments.Length == 0)
				{
					this.m_segments = this.GetChildSegments();
				}
				return this.m_segments;
			}
		}

		public Graphic separator
		{
			get
			{
				return this.m_separator;
			}
			set
			{
				this.m_separator = value;
				this.m_separatorWidth = 0f;
				this.LayoutSegments();
			}
		}

		public bool allowSwitchingOff
		{
			get
			{
				return this.m_allowSwitchingOff;
			}
			set
			{
				this.m_allowSwitchingOff = value;
			}
		}

		public int selectedSegmentIndex
		{
			get
			{
				return Array.IndexOf<Selectable>(this.segments, this.selectedSegment);
			}
			set
			{
				value = Math.Max(value, -1);
				value = Math.Min(value, this.segments.Length - 1);
				this.m_selectedSegmentIndex = value;
				if (value == -1)
				{
					if (this.selectedSegment)
					{
						this.selectedSegment.GetComponent<Segment>().selected = false;
						this.selectedSegment = null;
					}
				}
				else
				{
					this.segments[value].GetComponent<Segment>().selected = true;
				}
			}
		}

		public SegmentedControl.SegmentSelectedEvent onValueChanged
		{
			get
			{
				return this.m_onValueChanged;
			}
			set
			{
				this.m_onValueChanged = value;
			}
		}

		protected override void Start()
		{
			base.Start();
			this.LayoutSegments();
			if (this.m_selectedSegmentIndex != -1)
			{
				this.selectedSegmentIndex = this.m_selectedSegmentIndex;
			}
		}

		private Selectable[] GetChildSegments()
		{
			Selectable[] componentsInChildren = base.GetComponentsInChildren<Selectable>();
			if (componentsInChildren.Length < 2)
			{
				throw new InvalidOperationException("A segmented control must have at least two Button children");
			}
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Segment segment = componentsInChildren[i].GetComponent<Segment>();
				if (segment == null)
				{
					segment = componentsInChildren[i].gameObject.AddComponent<Segment>();
				}
				segment.index = i;
			}
			return componentsInChildren;
		}

		public void SetAllSegmentsOff()
		{
			this.selectedSegment = null;
		}

		private void RecreateSprites()
		{
			for (int i = 0; i < this.segments.Length; i++)
			{
				if (!(this.segments[i].image == null))
				{
					Sprite sprite = this.segments[i].image.sprite;
					if (sprite.border.x != 0f && sprite.border.z != 0f)
					{
						Rect rect = sprite.rect;
						Vector4 border = sprite.border;
						if (i > 0)
						{
							rect.xMin = border.x;
							border.x = 0f;
						}
						if (i < this.segments.Length - 1)
						{
							rect.xMax = border.z;
							border.z = 0f;
						}
						this.segments[i].image.sprite = Sprite.Create(sprite.texture, rect, sprite.pivot, sprite.pixelsPerUnit, 0u, SpriteMeshType.FullRect, border);
					}
				}
			}
		}

		public void LayoutSegments()
		{
			this.RecreateSprites();
			RectTransform rectTransform = base.transform as RectTransform;
			float num = rectTransform.rect.width / (float)this.segments.Length - this.SeparatorWidth * (float)(this.segments.Length - 1);
			for (int i = 0; i < this.segments.Length; i++)
			{
				float num2 = (num + this.SeparatorWidth) * (float)i;
				RectTransform component = this.segments[i].GetComponent<RectTransform>();
				component.anchorMin = Vector2.zero;
				component.anchorMax = Vector2.zero;
				component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, num2, num);
				component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, rectTransform.rect.height);
				if (this.separator && i > 0)
				{
					Transform transform = base.gameObject.transform.Find("Separator " + i);
					Graphic graphic = (!(transform != null)) ? Object.Instantiate<GameObject>(this.separator.gameObject).GetComponent<Graphic>() : transform.GetComponent<Graphic>();
					graphic.gameObject.name = "Separator " + i;
					graphic.gameObject.SetActive(true);
					graphic.rectTransform.SetParent(base.transform, false);
					graphic.rectTransform.anchorMin = Vector2.zero;
					graphic.rectTransform.anchorMax = Vector2.zero;
					graphic.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, num2 - this.SeparatorWidth, this.SeparatorWidth);
					graphic.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, rectTransform.rect.height);
				}
			}
		}

		private Selectable[] m_segments;

		[SerializeField]
		[Tooltip("A GameObject with an Image to use as a separator between segments. Size of the RectTransform will determine the size of the separator used.\nNote, make sure to disable the separator GO so that it does not affect the scene")]
		private Graphic m_separator;

		private float m_separatorWidth;

		[SerializeField]
		[Tooltip("When True, it allows each button to be toggled on/off")]
		private bool m_allowSwitchingOff;

		[SerializeField]
		[Tooltip("The selected default for the control (zero indexed array)")]
		private int m_selectedSegmentIndex = -1;

		[SerializeField]
		[Tooltip("Event to fire once the selection has been changed")]
		private SegmentedControl.SegmentSelectedEvent m_onValueChanged = new SegmentedControl.SegmentSelectedEvent();

		protected internal Selectable selectedSegment;

		[SerializeField]
		public Color selectedColor;

		[Serializable]
		public class SegmentSelectedEvent : UnityEvent<int>
		{
		}
	}
}
