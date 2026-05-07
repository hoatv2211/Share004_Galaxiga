using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Extensions/MultiTouchScrollRect")]
	public class MultiTouchScrollRect : ScrollRect
	{
		public override void OnBeginDrag(PointerEventData eventData)
		{
			this.pid = eventData.pointerId;
			base.OnBeginDrag(eventData);
		}

		public override void OnDrag(PointerEventData eventData)
		{
			if (this.pid == eventData.pointerId)
			{
				base.OnDrag(eventData);
			}
		}

		public override void OnEndDrag(PointerEventData eventData)
		{
			this.pid = -100;
			base.OnEndDrag(eventData);
		}

		private int pid = -100;
	}
}
