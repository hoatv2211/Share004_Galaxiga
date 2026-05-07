using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SuperScrollView
{
	public class DragChangSizeScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		public RectTransform CachedRectTransform
		{
			get
			{
				if (this.mCachedRectTransform == null)
				{
					this.mCachedRectTransform = base.gameObject.GetComponent<RectTransform>();
				}
				return this.mCachedRectTransform;
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			this.SetCursor(this.mCursorTexture, this.mCursorHotSpot, CursorMode.Auto);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			this.SetCursor(null, this.mCursorHotSpot, CursorMode.Auto);
		}

		private void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode)
		{
			if (Input.mousePresent)
			{
				Cursor.SetCursor(texture, hotspot, cursorMode);
			}
		}

		private void LateUpdate()
		{
			if (this.mCursorTexture == null)
			{
				return;
			}
			if (this.mIsDraging)
			{
				this.SetCursor(this.mCursorTexture, this.mCursorHotSpot, CursorMode.Auto);
				return;
			}
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.CachedRectTransform, UnityEngine.Input.mousePosition, this.mCamera, out vector))
			{
				this.SetCursor(null, this.mCursorHotSpot, CursorMode.Auto);
				return;
			}
			float num = this.CachedRectTransform.rect.width - vector.x;
			if (num < 0f)
			{
				this.SetCursor(null, this.mCursorHotSpot, CursorMode.Auto);
			}
			else if (num <= this.mBorderSize)
			{
				this.SetCursor(this.mCursorTexture, this.mCursorHotSpot, CursorMode.Auto);
			}
			else
			{
				this.SetCursor(null, this.mCursorHotSpot, CursorMode.Auto);
			}
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			this.mIsDraging = true;
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			this.mIsDraging = false;
			if (this.mOnDragEndAction != null)
			{
				this.mOnDragEndAction();
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.CachedRectTransform, eventData.position, this.mCamera, out vector);
			if (vector.x <= 0f)
			{
				return;
			}
			this.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, vector.x);
		}

		private bool mIsDraging;

		public Camera mCamera;

		public float mBorderSize = 10f;

		public Texture2D mCursorTexture;

		public Vector2 mCursorHotSpot = new Vector2(16f, 16f);

		private RectTransform mCachedRectTransform;

		public Action mOnDragEndAction;
	}
}
