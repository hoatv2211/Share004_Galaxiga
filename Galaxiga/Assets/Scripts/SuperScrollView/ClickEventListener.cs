using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SuperScrollView
{
	public class ClickEventListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		public static ClickEventListener Get(GameObject obj)
		{
			ClickEventListener clickEventListener = obj.GetComponent<ClickEventListener>();
			if (clickEventListener == null)
			{
				clickEventListener = obj.AddComponent<ClickEventListener>();
			}
			return clickEventListener;
		}

		public bool IsPressd
		{
			get
			{
				return this.mIsPressed;
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.clickCount == 2)
			{
				if (this.mDoubleClickedHandler != null)
				{
					this.mDoubleClickedHandler(base.gameObject);
				}
			}
			else if (this.mClickedHandler != null)
			{
				this.mClickedHandler(base.gameObject);
			}
		}

		public void SetClickEventHandler(Action<GameObject> handler)
		{
			this.mClickedHandler = handler;
		}

		public void SetDoubleClickEventHandler(Action<GameObject> handler)
		{
			this.mDoubleClickedHandler = handler;
		}

		public void SetPointerDownHandler(Action<GameObject> handler)
		{
			this.mOnPointerDownHandler = handler;
		}

		public void SetPointerUpHandler(Action<GameObject> handler)
		{
			this.mOnPointerUpHandler = handler;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			this.mIsPressed = true;
			if (this.mOnPointerDownHandler != null)
			{
				this.mOnPointerDownHandler(base.gameObject);
			}
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			this.mIsPressed = false;
			if (this.mOnPointerUpHandler != null)
			{
				this.mOnPointerUpHandler(base.gameObject);
			}
		}

		private Action<GameObject> mClickedHandler;

		private Action<GameObject> mDoubleClickedHandler;

		private Action<GameObject> mOnPointerDownHandler;

		private Action<GameObject> mOnPointerUpHandler;

		private bool mIsPressed;
	}
}
