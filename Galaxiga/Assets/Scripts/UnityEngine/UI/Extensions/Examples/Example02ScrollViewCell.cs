using System;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions.Examples
{
	public class Example02ScrollViewCell : FancyScrollViewCell<Example02CellDto, Example02ScrollViewContext>
	{
		private void Start()
		{
			RectTransform rectTransform = base.transform as RectTransform;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchoredPosition3D = Vector3.zero;
			this.UpdatePosition(0f);
			this.button.onClick.AddListener(new UnityAction(this.OnPressedCell));
		}

		public override void SetContext(Example02ScrollViewContext context)
		{
			this.context = context;
		}

		public override void UpdateContent(Example02CellDto itemData)
		{
			this.message.text = itemData.Message;
			if (this.context != null)
			{
				bool flag = this.context.SelectedIndex == base.DataIndex;
				this.image.color = ((!flag) ? new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 77) : new Color32(0, byte.MaxValue, byte.MaxValue, 100));
			}
		}

		public override void UpdatePosition(float position)
		{
			this.animator.Play(this.scrollTriggerHash, -1, position);
			this.animator.speed = 0f;
		}

		public void OnPressedCell()
		{
			if (this.context != null)
			{
				this.context.OnPressedCell(this);
			}
		}

		[SerializeField]
		private Animator animator;

		[SerializeField]
		private Text message;

		[SerializeField]
		private Image image;

		[SerializeField]
		private Button button;

		private readonly int scrollTriggerHash = Animator.StringToHash("scroll");

		private Example02ScrollViewContext context;
	}
}
