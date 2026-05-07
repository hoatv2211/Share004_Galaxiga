using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Extensions/Stepper")]
	[RequireComponent(typeof(RectTransform))]
	public class Stepper : UIBehaviour
	{
		protected Stepper()
		{
		}

		private float separatorWidth
		{
			get
			{
				if (this._separatorWidth == 0f && this.separator)
				{
					this._separatorWidth = this.separator.rectTransform.rect.width;
					Image component = this.separator.GetComponent<Image>();
					if (component)
					{
						this._separatorWidth /= component.pixelsPerUnit;
					}
				}
				return this._separatorWidth;
			}
		}

		public Selectable[] sides
		{
			get
			{
				if (this._sides == null || this._sides.Length == 0)
				{
					this._sides = this.GetSides();
				}
				return this._sides;
			}
		}

		public int value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		public int minimum
		{
			get
			{
				return this._minimum;
			}
			set
			{
				this._minimum = value;
			}
		}

		public int maximum
		{
			get
			{
				return this._maximum;
			}
			set
			{
				this._maximum = value;
			}
		}

		public int step
		{
			get
			{
				return this._step;
			}
			set
			{
				this._step = value;
			}
		}

		public bool wrap
		{
			get
			{
				return this._wrap;
			}
			set
			{
				this._wrap = value;
			}
		}

		public Graphic separator
		{
			get
			{
				return this._separator;
			}
			set
			{
				this._separator = value;
				this._separatorWidth = 0f;
				this.LayoutSides(this.sides);
			}
		}

		public Stepper.StepperValueChangedEvent onValueChanged
		{
			get
			{
				return this._onValueChanged;
			}
			set
			{
				this._onValueChanged = value;
			}
		}

		private Selectable[] GetSides()
		{
			Selectable[] componentsInChildren = base.GetComponentsInChildren<Selectable>();
			if (componentsInChildren.Length != 2)
			{
				throw new InvalidOperationException("A stepper must have two Button children");
			}
			for (int i = 0; i < 2; i++)
			{
				StepperSide x = componentsInChildren[i].GetComponent<StepperSide>();
				if (x == null)
				{
					x = componentsInChildren[i].gameObject.AddComponent<StepperSide>();
				}
			}
			if (!this.wrap)
			{
				this.DisableAtExtremes(componentsInChildren);
			}
			this.LayoutSides(componentsInChildren);
			return componentsInChildren;
		}

		public void StepUp()
		{
			this.Step(this.step);
		}

		public void StepDown()
		{
			this.Step(-this.step);
		}

		private void Step(int amount)
		{
			this.value += amount;
			if (this.wrap)
			{
				if (this.value > this.maximum)
				{
					this.value = this.minimum;
				}
				if (this.value < this.minimum)
				{
					this.value = this.maximum;
				}
			}
			else
			{
				this.value = Math.Max(this.minimum, this.value);
				this.value = Math.Min(this.maximum, this.value);
				this.DisableAtExtremes(this.sides);
			}
			this._onValueChanged.Invoke(this.value);
		}

		private void DisableAtExtremes(Selectable[] sides)
		{
			sides[0].interactable = (this.wrap || this.value > this.minimum);
			sides[1].interactable = (this.wrap || this.value < this.maximum);
		}

		private void RecreateSprites(Selectable[] sides)
		{
			for (int i = 0; i < 2; i++)
			{
				if (!(sides[i].image == null))
				{
					Sprite sprite = sides[i].image.sprite;
					if (sprite.border.x != 0f && sprite.border.z != 0f)
					{
						Rect rect = sprite.rect;
						Vector4 border = sprite.border;
						if (i == 0)
						{
							rect.xMax = border.z;
							border.z = 0f;
						}
						else
						{
							rect.xMin = border.x;
							border.x = 0f;
						}
						sides[i].image.sprite = Sprite.Create(sprite.texture, rect, sprite.pivot, sprite.pixelsPerUnit, 0u, SpriteMeshType.FullRect, border);
					}
				}
			}
		}

		public void LayoutSides(Selectable[] sides = null)
		{
			sides = (sides ?? this.sides);
			this.RecreateSprites(sides);
			RectTransform rectTransform = base.transform as RectTransform;
			float num = rectTransform.rect.width / 2f - this.separatorWidth;
			for (int i = 0; i < 2; i++)
			{
				float inset = (i != 0) ? (num + this.separatorWidth) : 0f;
				RectTransform component = sides[i].GetComponent<RectTransform>();
				component.anchorMin = Vector2.zero;
				component.anchorMax = Vector2.zero;
				component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, inset, num);
				component.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, rectTransform.rect.height);
			}
			if (this.separator)
			{
				Transform transform = base.gameObject.transform.Find("Separator");
				Graphic graphic = (!(transform != null)) ? Object.Instantiate<GameObject>(this.separator.gameObject).GetComponent<Graphic>() : transform.GetComponent<Graphic>();
				graphic.gameObject.name = "Separator";
				graphic.gameObject.SetActive(true);
				graphic.rectTransform.SetParent(base.transform, false);
				graphic.rectTransform.anchorMin = Vector2.zero;
				graphic.rectTransform.anchorMax = Vector2.zero;
				graphic.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, num, this.separatorWidth);
				graphic.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0f, rectTransform.rect.height);
			}
		}

		private Selectable[] _sides;

		[SerializeField]
		[Tooltip("The current step value of the control")]
		private int _value;

		[SerializeField]
		[Tooltip("The minimum step value allowed by the control. When reached it will disable the '-' button")]
		private int _minimum;

		[SerializeField]
		[Tooltip("The maximum step value allowed by the control. When reached it will disable the '+' button")]
		private int _maximum = 100;

		[SerializeField]
		[Tooltip("The step increment used to increment / decrement the step value")]
		private int _step = 1;

		[SerializeField]
		[Tooltip("Does the step value loop around from end to end")]
		private bool _wrap;

		[SerializeField]
		[Tooltip("A GameObject with an Image to use as a separator between segments. Size of the RectTransform will determine the size of the separator used.\nNote, make sure to disable the separator GO so that it does not affect the scene")]
		private Graphic _separator;

		private float _separatorWidth;

		[SerializeField]
		private Stepper.StepperValueChangedEvent _onValueChanged = new Stepper.StepperValueChangedEvent();

		[Serializable]
		public class StepperValueChangedEvent : UnityEvent<int>
		{
		}
	}
}
