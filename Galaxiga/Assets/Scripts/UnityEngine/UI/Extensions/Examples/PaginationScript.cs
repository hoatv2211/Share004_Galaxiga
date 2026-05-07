using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions.Examples
{
	public class PaginationScript : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.hss != null)
			{
				this.hss.GoToScreen(this.Page);
			}
		}

		public HorizontalScrollSnap hss;

		public int Page;
	}
}
