using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	[RequireComponent(typeof(Selectable))]
	public class StepperSide : UIBehaviour, IPointerClickHandler, ISubmitHandler, IEventSystemHandler
	{
		protected StepperSide()
		{
		}

		private Selectable button
		{
			get
			{
				return base.GetComponent<Selectable>();
			}
		}

		private Stepper stepper
		{
			get
			{
				return base.GetComponentInParent<Stepper>();
			}
		}

		private bool leftmost
		{
			get
			{
				return this.button == this.stepper.sides[0];
			}
		}

		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.Press();
		}

		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Press();
		}

		private void Press()
		{
			if (!this.button.IsActive() || !this.button.IsInteractable())
			{
				return;
			}
			if (this.leftmost)
			{
				this.stepper.StepDown();
			}
			else
			{
				this.stepper.StepUp();
			}
		}
	}
}
