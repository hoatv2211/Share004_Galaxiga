using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	[RequireComponent(typeof(RectTransform))]
	public class ReorderableListElement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IEventSystemHandler
	{
		public void OnBeginDrag(PointerEventData eventData)
		{
			this.isValid = true;
			if (this._reorderableList == null)
			{
				return;
			}
			if (!this._reorderableList.IsDraggable || !this.IsGrabbable)
			{
				this._draggingObject = null;
				return;
			}
			if (!this._reorderableList.CloneDraggedObject)
			{
				this._draggingObject = this._rect;
				this._fromIndex = this._rect.GetSiblingIndex();
				if (this._reorderableList.OnElementRemoved != null)
				{
					this._reorderableList.OnElementRemoved.Invoke(new ReorderableList.ReorderableListEventStruct
					{
						DroppedObject = this._draggingObject.gameObject,
						IsAClone = this._reorderableList.CloneDraggedObject,
						SourceObject = ((!this._reorderableList.CloneDraggedObject) ? this._draggingObject.gameObject : base.gameObject),
						FromList = this._reorderableList,
						FromIndex = this._fromIndex
					});
				}
				if (!this.isValid)
				{
					this._draggingObject = null;
					return;
				}
			}
			else
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(base.gameObject);
				this._draggingObject = gameObject.GetComponent<RectTransform>();
			}
			this._draggingObjectOriginalSize = base.gameObject.GetComponent<RectTransform>().rect.size;
			this._draggingObjectLE = this._draggingObject.GetComponent<LayoutElement>();
			this._draggingObject.SetParent(this._reorderableList.DraggableArea, true);
			this._draggingObject.SetAsLastSibling();
			this._fakeElement = new GameObject("Fake").AddComponent<RectTransform>();
			this._fakeElementLE = this._fakeElement.gameObject.AddComponent<LayoutElement>();
			this.RefreshSizes();
			if (this._reorderableList.OnElementGrabbed != null)
			{
				this._reorderableList.OnElementGrabbed.Invoke(new ReorderableList.ReorderableListEventStruct
				{
					DroppedObject = this._draggingObject.gameObject,
					IsAClone = this._reorderableList.CloneDraggedObject,
					SourceObject = ((!this._reorderableList.CloneDraggedObject) ? this._draggingObject.gameObject : base.gameObject),
					FromList = this._reorderableList,
					FromIndex = this._fromIndex
				});
				if (!this.isValid)
				{
					this.CancelDrag();
					return;
				}
			}
			this._isDragging = true;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (!this._isDragging)
			{
				return;
			}
			if (!this.isValid)
			{
				this.CancelDrag();
				return;
			}
			Canvas componentInParent = this._draggingObject.GetComponentInParent<Canvas>();
			Vector3 position;
			RectTransformUtility.ScreenPointToWorldPointInRectangle(componentInParent.GetComponent<RectTransform>(), eventData.position, componentInParent.worldCamera, out position);
			this._draggingObject.position = position;
			EventSystem.current.RaycastAll(eventData, this._raycastResults);
			for (int i = 0; i < this._raycastResults.Count; i++)
			{
				this._currentReorderableListRaycasted = this._raycastResults[i].gameObject.GetComponent<ReorderableList>();
				if (this._currentReorderableListRaycasted != null)
				{
					break;
				}
			}
			if (this._currentReorderableListRaycasted == null || !this._currentReorderableListRaycasted.IsDropable)
			{
				this.RefreshSizes();
				this._fakeElement.transform.SetParent(this._reorderableList.DraggableArea, false);
			}
			else
			{
				if (this._fakeElement.parent != this._currentReorderableListRaycasted)
				{
					this._fakeElement.SetParent(this._currentReorderableListRaycasted.Content, false);
				}
				float num = float.PositiveInfinity;
				int siblingIndex = 0;
				float num2 = 0f;
				for (int j = 0; j < this._currentReorderableListRaycasted.Content.childCount; j++)
				{
					RectTransform component = this._currentReorderableListRaycasted.Content.GetChild(j).GetComponent<RectTransform>();
					if (this._currentReorderableListRaycasted.ContentLayout is VerticalLayoutGroup)
					{
						num2 = Mathf.Abs(component.position.y - position.y);
					}
					else if (this._currentReorderableListRaycasted.ContentLayout is HorizontalLayoutGroup)
					{
						num2 = Mathf.Abs(component.position.x - position.x);
					}
					else if (this._currentReorderableListRaycasted.ContentLayout is GridLayoutGroup)
					{
						num2 = Mathf.Abs(component.position.x - position.x) + Mathf.Abs(component.position.y - position.y);
					}
					if (num2 < num)
					{
						num = num2;
						siblingIndex = j;
					}
				}
				this.RefreshSizes();
				this._fakeElement.SetSiblingIndex(siblingIndex);
				this._fakeElement.gameObject.SetActive(true);
			}
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			this._isDragging = false;
			if (this._draggingObject != null)
			{
				if (this._currentReorderableListRaycasted != null && this._currentReorderableListRaycasted.IsDropable && (this.IsTransferable || this._currentReorderableListRaycasted == this._reorderableList))
				{
					ReorderableList.ReorderableListEventStruct reorderableListEventStruct = new ReorderableList.ReorderableListEventStruct
					{
						DroppedObject = this._draggingObject.gameObject,
						IsAClone = this._reorderableList.CloneDraggedObject,
						SourceObject = ((!this._reorderableList.CloneDraggedObject) ? this._draggingObject.gameObject : base.gameObject),
						FromList = this._reorderableList,
						FromIndex = this._fromIndex,
						ToList = this._currentReorderableListRaycasted,
						ToIndex = this._fakeElement.GetSiblingIndex()
					};
					ReorderableList.ReorderableListEventStruct arg = reorderableListEventStruct;
					if (this._reorderableList && this._reorderableList.OnElementDropped != null)
					{
						this._reorderableList.OnElementDropped.Invoke(arg);
					}
					if (!this.isValid)
					{
						this.CancelDrag();
						return;
					}
					this.RefreshSizes();
					this._draggingObject.SetParent(this._currentReorderableListRaycasted.Content, false);
					this._draggingObject.rotation = this._currentReorderableListRaycasted.transform.rotation;
					this._draggingObject.SetSiblingIndex(this._fakeElement.GetSiblingIndex());
					this._reorderableList.OnElementAdded.Invoke(arg);
					if (!this.isValid)
					{
						throw new Exception("It's too late to cancel the Transfer! Do so in OnElementDropped!");
					}
				}
				else if (this.isDroppableInSpace)
				{
					UnityEvent<ReorderableList.ReorderableListEventStruct> onElementDropped = this._reorderableList.OnElementDropped;
					ReorderableList.ReorderableListEventStruct reorderableListEventStruct = new ReorderableList.ReorderableListEventStruct
					{
						DroppedObject = this._draggingObject.gameObject,
						IsAClone = this._reorderableList.CloneDraggedObject,
						SourceObject = ((!this._reorderableList.CloneDraggedObject) ? this._draggingObject.gameObject : base.gameObject),
						FromList = this._reorderableList,
						FromIndex = this._fromIndex
					};
					onElementDropped.Invoke(reorderableListEventStruct);
				}
				else
				{
					this.CancelDrag();
				}
			}
			if (this._fakeElement != null)
			{
				UnityEngine.Object.Destroy(this._fakeElement.gameObject);
			}
		}

		private void CancelDrag()
		{
			this._isDragging = false;
			if (this._reorderableList.CloneDraggedObject)
			{
				UnityEngine.Object.Destroy(this._draggingObject.gameObject);
			}
			else
			{
				this.RefreshSizes();
				this._draggingObject.SetParent(this._reorderableList.Content, false);
				this._draggingObject.rotation = this._reorderableList.Content.transform.rotation;
				this._draggingObject.SetSiblingIndex(this._fromIndex);
				ReorderableList.ReorderableListEventStruct arg = new ReorderableList.ReorderableListEventStruct
				{
					DroppedObject = this._draggingObject.gameObject,
					IsAClone = this._reorderableList.CloneDraggedObject,
					SourceObject = ((!this._reorderableList.CloneDraggedObject) ? this._draggingObject.gameObject : base.gameObject),
					FromList = this._reorderableList,
					FromIndex = this._fromIndex,
					ToList = this._reorderableList,
					ToIndex = this._fromIndex
				};
				this._reorderableList.OnElementAdded.Invoke(arg);
				if (!this.isValid)
				{
					throw new Exception("Transfer is already Cancelled.");
				}
			}
			if (this._fakeElement != null)
			{
				UnityEngine.Object.Destroy(this._fakeElement.gameObject);
			}
		}

		private void RefreshSizes()
		{
			Vector2 sizeDelta = this._draggingObjectOriginalSize;
			if (this._currentReorderableListRaycasted != null && this._currentReorderableListRaycasted.IsDropable && this._currentReorderableListRaycasted.Content.childCount > 0)
			{
				Transform child = this._currentReorderableListRaycasted.Content.GetChild(0);
				if (child != null)
				{
					sizeDelta = child.GetComponent<RectTransform>().rect.size;
				}
			}
			this._draggingObject.sizeDelta = sizeDelta;
			LayoutElement fakeElementLE = this._fakeElementLE;
			float num = sizeDelta.y;
			this._draggingObjectLE.preferredHeight = num;
			fakeElementLE.preferredHeight = num;
			LayoutElement fakeElementLE2 = this._fakeElementLE;
			num = sizeDelta.x;
			this._draggingObjectLE.preferredWidth = num;
			fakeElementLE2.preferredWidth = num;
		}

		public void Init(ReorderableList reorderableList)
		{
			this._reorderableList = reorderableList;
			this._rect = base.GetComponent<RectTransform>();
		}

		[Tooltip("Can this element be dragged?")]
		public bool IsGrabbable = true;

		[Tooltip("Can this element be transfered to another list")]
		public bool IsTransferable = true;

		[Tooltip("Can this element be dropped in space?")]
		public bool isDroppableInSpace;

		private readonly List<RaycastResult> _raycastResults = new List<RaycastResult>();

		private ReorderableList _currentReorderableListRaycasted;

		private RectTransform _draggingObject;

		private LayoutElement _draggingObjectLE;

		private Vector2 _draggingObjectOriginalSize;

		private RectTransform _fakeElement;

		private LayoutElement _fakeElementLE;

		private int _fromIndex;

		private bool _isDragging;

		private RectTransform _rect;

		private ReorderableList _reorderableList;

		internal bool isValid;
	}
}
