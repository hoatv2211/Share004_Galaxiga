using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SuperScrollView
{
	public class DragEventHelper : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IEventSystemHandler
	{
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this.mOnBeginDragHandler != null)
			{
				this.mOnBeginDragHandler(eventData);
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (this.mOnDragHandler != null)
			{
				this.mOnDragHandler(eventData);
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (this.mOnEndDragHandler != null)
			{
				this.mOnEndDragHandler(eventData);
			}
		}

		public DragEventHelper.OnDragEventHandler mOnBeginDragHandler;

		public DragEventHelper.OnDragEventHandler mOnDragHandler;

		public DragEventHelper.OnDragEventHandler mOnEndDragHandler;

		public delegate void OnDragEventHandler(PointerEventData eventData);
	}
}
