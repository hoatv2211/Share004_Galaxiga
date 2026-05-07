using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	[RequireComponent(typeof(Selectable))]
	public class Segment : UIBehaviour, IPointerClickHandler, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler, IEventSystemHandler
	{
		protected Segment()
		{
		}

		internal bool leftmost
		{
			get
			{
				return this.index == 0;
			}
		}

		internal bool rightmost
		{
			get
			{
				return this.index == this.segmentControl.segments.Length - 1;
			}
		}

		public bool selected
		{
			get
			{
				return this.segmentControl.selectedSegment == this.button;
			}
			set
			{
				this.SetSelected(value);
			}
		}

		internal SegmentedControl segmentControl
		{
			get
			{
				return base.GetComponentInParent<SegmentedControl>();
			}
		}

		internal Selectable button
		{
			get
			{
				return base.GetComponent<Selectable>();
			}
		}

		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.selected = true;
		}

		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.MaintainSelection();
		}

		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.MaintainSelection();
		}

		public virtual void OnPointerDown(PointerEventData eventData)
		{
			this.MaintainSelection();
		}

		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.MaintainSelection();
		}

		public virtual void OnSelect(BaseEventData eventData)
		{
			this.MaintainSelection();
		}

		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.MaintainSelection();
		}

		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.selected = true;
		}

		private void SetSelected(bool value)
		{
			if (value && this.button.IsActive() && this.button.IsInteractable())
			{
				if (this.segmentControl.selectedSegment == this.button)
				{
					if (this.segmentControl.allowSwitchingOff)
					{
						this.Deselect();
					}
					else
					{
						this.MaintainSelection();
					}
				}
				else
				{
					if (this.segmentControl.selectedSegment)
					{
						Segment component = this.segmentControl.selectedSegment.GetComponent<Segment>();
						this.segmentControl.selectedSegment = null;
						component.TransitionButton();
					}
					this.segmentControl.selectedSegment = this.button;
					this.StoreTextColor();
					this.TransitionButton();
					this.segmentControl.onValueChanged.Invoke(this.index);
				}
			}
			else if (this.segmentControl.selectedSegment == this.button)
			{
				this.Deselect();
			}
		}

		private void Deselect()
		{
			this.segmentControl.selectedSegment = null;
			this.TransitionButton();
			this.segmentControl.onValueChanged.Invoke(-1);
		}

		private void MaintainSelection()
		{
			if (this.button != this.segmentControl.selectedSegment)
			{
				return;
			}
			this.TransitionButton(true);
		}

		internal void TransitionButton()
		{
			this.TransitionButton(false);
		}

		internal void TransitionButton(bool instant)
		{
			Color a = (!this.selected) ? this.button.colors.normalColor : this.segmentControl.selectedColor;
			Color a2 = (!this.selected) ? this.textColor : this.button.colors.normalColor;
			Sprite newSprite = (!this.selected) ? null : this.button.spriteState.pressedSprite;
			string triggername = (!this.selected) ? this.button.animationTriggers.normalTrigger : this.button.animationTriggers.pressedTrigger;
			Selectable.Transition transition = this.button.transition;
			if (transition != Selectable.Transition.ColorTint)
			{
				if (transition != Selectable.Transition.SpriteSwap)
				{
					if (transition == Selectable.Transition.Animation)
					{
						this.TriggerAnimation(triggername);
					}
				}
				else
				{
					this.DoSpriteSwap(newSprite);
				}
			}
			else
			{
				this.StartColorTween(a * this.button.colors.colorMultiplier, instant);
				this.ChangeTextColor(a2 * this.button.colors.colorMultiplier);
			}
		}

		private void StartColorTween(Color targetColor, bool instant)
		{
			if (this.button.targetGraphic == null)
			{
				return;
			}
			this.button.targetGraphic.CrossFadeColor(targetColor, (!instant) ? this.button.colors.fadeDuration : 0f, true, true);
		}

		internal void StoreTextColor()
		{
			Text componentInChildren = base.GetComponentInChildren<Text>();
			if (!componentInChildren)
			{
				return;
			}
			this.textColor = componentInChildren.color;
		}

		private void ChangeTextColor(Color targetColor)
		{
			Text componentInChildren = base.GetComponentInChildren<Text>();
			if (!componentInChildren)
			{
				return;
			}
			componentInChildren.color = targetColor;
		}

		private void DoSpriteSwap(Sprite newSprite)
		{
			if (this.button.image == null)
			{
				return;
			}
			this.button.image.overrideSprite = newSprite;
		}

		private void TriggerAnimation(string triggername)
		{
			if (this.button.animator == null || !this.button.animator.isActiveAndEnabled || !this.button.animator.hasBoundPlayables || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			this.button.animator.ResetTrigger(this.button.animationTriggers.normalTrigger);
			this.button.animator.ResetTrigger(this.button.animationTriggers.pressedTrigger);
			this.button.animator.ResetTrigger(this.button.animationTriggers.highlightedTrigger);
			this.button.animator.ResetTrigger(this.button.animationTriggers.disabledTrigger);
			this.button.animator.SetTrigger(triggername);
		}

		internal int index;

		[SerializeField]
		private Color textColor;
	}
}
