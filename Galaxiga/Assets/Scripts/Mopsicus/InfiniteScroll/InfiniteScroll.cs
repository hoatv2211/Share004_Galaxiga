using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mopsicus.InfiniteScroll
{
	public class InfiniteScroll : MonoBehaviour, IDropHandler, IEventSystemHandler
	{
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event InfiniteScroll.HeightItem OnHeight;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event InfiniteScroll.HeightItem OnWidth;

		private void Awake()
		{
			this._container = base.GetComponent<RectTransform>().rect;
			this._scroll = base.GetComponent<ScrollRect>();
			this._scroll.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScrollChange));
			this._content = this._scroll.viewport.transform.GetChild(0).GetComponent<RectTransform>();
			this._heights = new Dictionary<int, int>();
			this._widths = new Dictionary<int, int>();
			this._positions = new Dictionary<int, float>();
			this.CreateLabels();
		}

		private void Update()
		{
			if (this.Type == 0)
			{
				this.UpdateVertical();
			}
			else
			{
				this.UpdateHorizontal();
			}
		}

		private void UpdateVertical()
		{
			if (this._count == 0)
			{
				return;
			}
			float num = this._content.anchoredPosition.y - (float)this.ItemSpacing;
			if (num <= 0f && this._rects[0].anchoredPosition.y < (float)(-(float)this.TopPadding) - 10f)
			{
				this.InitData(this._count);
				return;
			}
			if (num < 0f)
			{
				return;
			}
			float num2 = Mathf.Abs(this._positions[this._previousPosition]) + (float)this._heights[this._previousPosition];
			int num3 = (num <= num2) ? (this._previousPosition - 1) : (this._previousPosition + 1);
			if (num3 < 0 || this._previousPosition == num3 || this._scroll.velocity.y == 0f)
			{
				return;
			}
			if (num3 > this._previousPosition)
			{
				if (num3 - this._previousPosition > 1)
				{
					num3 = this._previousPosition + 1;
				}
				int num4 = num3 % this._views.Length;
				num4--;
				if (num4 < 0)
				{
					num4 = this._views.Length - 1;
				}
				int num5 = num3 + this._views.Length - 1;
				if (num5 < this._count)
				{
					Vector2 anchoredPosition = this._rects[num4].anchoredPosition;
					anchoredPosition.y = this._positions[num5];
					this._rects[num4].anchoredPosition = anchoredPosition;
					Vector2 sizeDelta = this._rects[num4].sizeDelta;
					sizeDelta.y = (float)this._heights[num5];
					this._rects[num4].sizeDelta = sizeDelta;
					this._views[num4].name = num5.ToString();
					this.OnFill(num5, this._views[num4]);
				}
			}
			else
			{
				if (this._previousPosition - num3 > 1)
				{
					num3 = this._previousPosition - 1;
				}
				int num6 = num3 % this._views.Length;
				Vector2 anchoredPosition2 = this._rects[num6].anchoredPosition;
				anchoredPosition2.y = this._positions[num3];
				this._rects[num6].anchoredPosition = anchoredPosition2;
				Vector2 sizeDelta2 = this._rects[num6].sizeDelta;
				sizeDelta2.y = (float)this._heights[num3];
				this._rects[num6].sizeDelta = sizeDelta2;
				this._views[num6].name = num3.ToString();
				this.OnFill(num3, this._views[num6]);
			}
			this._previousPosition = num3;
		}

		private void UpdateHorizontal()
		{
			if (this._count == 0)
			{
				return;
			}
			float num = this._content.anchoredPosition.x * -1f - (float)this.ItemSpacing;
			if (num <= 0f && this._rects[0].anchoredPosition.x < (float)(-(float)this.LeftPadding) - 10f)
			{
				this.InitData(this._count);
				return;
			}
			if (num < 0f)
			{
				return;
			}
			float num2 = Mathf.Abs(this._positions[this._previousPosition]) + (float)this._widths[this._previousPosition];
			int num3 = (num <= num2) ? (this._previousPosition - 1) : (this._previousPosition + 1);
			if (num3 < 0 || this._previousPosition == num3 || this._scroll.velocity.x == 0f)
			{
				return;
			}
			if (num3 > this._previousPosition)
			{
				if (num3 - this._previousPosition > 1)
				{
					num3 = this._previousPosition + 1;
				}
				int num4 = num3 % this._views.Length;
				num4--;
				if (num4 < 0)
				{
					num4 = this._views.Length - 1;
				}
				int num5 = num3 + this._views.Length - 1;
				if (num5 < this._count)
				{
					Vector2 anchoredPosition = this._rects[num4].anchoredPosition;
					anchoredPosition.x = this._positions[num5];
					this._rects[num4].anchoredPosition = anchoredPosition;
					Vector2 sizeDelta = this._rects[num4].sizeDelta;
					sizeDelta.x = (float)this._widths[num5];
					this._rects[num4].sizeDelta = sizeDelta;
					this._views[num4].name = num5.ToString();
					this.OnFill(num5, this._views[num4]);
				}
			}
			else
			{
				if (this._previousPosition - num3 > 1)
				{
					num3 = this._previousPosition - 1;
				}
				int num6 = num3 % this._views.Length;
				Vector2 anchoredPosition2 = this._rects[num6].anchoredPosition;
				anchoredPosition2.x = this._positions[num3];
				this._rects[num6].anchoredPosition = anchoredPosition2;
				Vector2 sizeDelta2 = this._rects[num6].sizeDelta;
				sizeDelta2.x = (float)this._widths[num3];
				this._rects[num6].sizeDelta = sizeDelta2;
				this._views[num6].name = num3.ToString();
				this.OnFill(num3, this._views[num6]);
			}
			this._previousPosition = num3;
		}

		private void OnScrollChange(Vector2 vector)
		{
			if (this.Type == 0)
			{
				this.ScrollChangeVertical(vector);
			}
			else
			{
				this.ScrollChangeHorizontal(vector);
			}
		}

		private void ScrollChangeVertical(Vector2 vector)
		{
			this._isCanLoadUp = false;
			this._isCanLoadDown = false;
			if (this._views == null)
			{
				return;
			}
			float num = 0f;
			float num2 = 0f;
			float num3 = (float)(this._count / this._views.Length);
			if (num3 >= 1f)
			{
				if (vector.y > 1f)
				{
					num = (vector.y - 1f) * num3;
				}
				else if (vector.y < 0f)
				{
					num = vector.y * num3;
				}
			}
			else
			{
				num2 = this._content.anchoredPosition.y;
			}
			if ((num > 0.05f || num2 < -27.5f) && this.IsPullTop)
			{
				this.TopLabel.gameObject.SetActive(true);
				this.TopLabel.text = this.TopPullLabel;
				if (num > 0.1f || num2 < -55f)
				{
					this.TopLabel.text = this.TopReleaseLabel;
					this._isCanLoadUp = true;
				}
			}
			else
			{
				this.TopLabel.gameObject.SetActive(false);
			}
			if ((num < -0.05f || num2 > 27.5f) && this.IsPullBottom)
			{
				this.BottomLabel.gameObject.SetActive(true);
				this.BottomLabel.text = this.BottomPullLabel;
				if (num < -0.1f || num2 > 55f)
				{
					this.BottomLabel.text = this.BottomReleaseLabel;
					this._isCanLoadDown = true;
				}
			}
			else
			{
				this.BottomLabel.gameObject.SetActive(false);
			}
		}

		private void ScrollChangeHorizontal(Vector2 vector)
		{
			this._isCanLoadLeft = false;
			this._isCanLoadRight = false;
			if (this._views == null)
			{
				return;
			}
			float num = 0f;
			float num2 = 0f;
			float num3 = (float)(this._count / this._views.Length);
			if (num3 >= 1f)
			{
				if (vector.x > 1f)
				{
					num = (vector.x - 1f) * num3;
				}
				else if (vector.x < 0f)
				{
					num = vector.x * num3;
				}
			}
			else
			{
				num2 = this._content.anchoredPosition.x;
			}
			if ((num > 0.05f || num2 < -27.5f) && this.IsPullRight)
			{
				this.RightLabel.gameObject.SetActive(true);
				this.RightLabel.text = this.RightPullLabel;
				if (num > 0.1f || num2 < -55f)
				{
					this.RightLabel.text = this.RightReleaseLabel;
					this._isCanLoadRight = true;
				}
			}
			else
			{
				this.RightLabel.gameObject.SetActive(false);
			}
			if ((num < -0.05f || num2 > 27.5f) && this.IsPullLeft)
			{
				this.LeftLabel.gameObject.SetActive(true);
				this.LeftLabel.text = this.LeftPullLabel;
				if (num < -0.1f || num2 > 55f)
				{
					this.LeftLabel.text = this.LeftReleaseLabel;
					this._isCanLoadLeft = true;
				}
			}
			else
			{
				this.LeftLabel.gameObject.SetActive(false);
			}
		}

		public void OnDrop(PointerEventData eventData)
		{
			if (this.Type == 0)
			{
				this.DropVertical();
			}
			else
			{
				this.DropHorizontal();
			}
		}

		private void DropVertical()
		{
			if (this._isCanLoadUp)
			{
				this.OnPull(InfiniteScroll.Direction.Top);
			}
			else if (this._isCanLoadDown)
			{
				this.OnPull(InfiniteScroll.Direction.Bottom);
			}
			this._isCanLoadUp = false;
			this._isCanLoadDown = false;
		}

		private void DropHorizontal()
		{
			if (this._isCanLoadLeft)
			{
				this.OnPull(InfiniteScroll.Direction.Left);
			}
			else if (this._isCanLoadRight)
			{
				this.OnPull(InfiniteScroll.Direction.Right);
			}
			this._isCanLoadLeft = false;
			this._isCanLoadRight = false;
		}

		public void InitData(int count)
		{
			if (this.Type == 0)
			{
				this.InitVertical(count);
			}
			else
			{
				this.InitHorizontal(count);
			}
		}

		private void InitVertical(int count)
		{
			float y = this.CalcSizesPositions(count);
			this.CreateViews();
			this._previousPosition = 0;
			this._count = count;
			this._content.sizeDelta = new Vector2(this._content.sizeDelta.x, y);
			Vector2 anchoredPosition = this._content.anchoredPosition;
			Vector2 sizeDelta = Vector2.zero;
			anchoredPosition.y = 0f;
			this._content.anchoredPosition = anchoredPosition;
			int num = this.TopPadding;
			for (int i = 0; i < this._views.Length; i++)
			{
				bool active = i < count;
				this._views[i].gameObject.SetActive(active);
				if (i + 1 <= this._count)
				{
					anchoredPosition = this._rects[i].anchoredPosition;
					anchoredPosition.y = this._positions[i];
					anchoredPosition.x = 0f;
					this._rects[i].anchoredPosition = anchoredPosition;
					sizeDelta = this._rects[i].sizeDelta;
					sizeDelta.y = (float)this._heights[i];
					this._rects[i].sizeDelta = sizeDelta;
					num += this.ItemSpacing + this._heights[i];
					this._views[i].name = i.ToString();
					this.OnFill(i, this._views[i]);
				}
			}
		}

		private void InitHorizontal(int count)
		{
			float x = this.CalcSizesPositions(count);
			this.CreateViews();
			this._previousPosition = 0;
			this._count = count;
			this._content.sizeDelta = new Vector2(x, this._content.sizeDelta.y);
			Vector2 anchoredPosition = this._content.anchoredPosition;
			Vector2 sizeDelta = Vector2.zero;
			anchoredPosition.x = 0f;
			this._content.anchoredPosition = anchoredPosition;
			int num = this.LeftPadding;
			for (int i = 0; i < this._views.Length; i++)
			{
				bool active = i < count;
				this._views[i].gameObject.SetActive(active);
				if (i + 1 <= this._count)
				{
					anchoredPosition = this._rects[i].anchoredPosition;
					anchoredPosition.x = this._positions[i];
					anchoredPosition.y = 0f;
					this._rects[i].anchoredPosition = anchoredPosition;
					sizeDelta = this._rects[i].sizeDelta;
					sizeDelta.x = (float)this._widths[i];
					this._rects[i].sizeDelta = sizeDelta;
					num += this.ItemSpacing + this._widths[i];
					this._views[i].name = i.ToString();
					this.OnFill(i, this._views[i]);
				}
			}
		}

		private float CalcSizesPositions(int count)
		{
			return (this.Type != 0) ? this.CalcSizesPositionsHorizontal(count) : this.CalcSizesPositionsVertical(count);
		}

		private float CalcSizesPositionsVertical(int count)
		{
			this._heights.Clear();
			this._positions.Clear();
			float num = 0f;
			for (int i = 0; i < count; i++)
			{
				this._heights[i] = this.OnHeight(i);
				this._positions[i] = -((float)(this.TopPadding + i * this.ItemSpacing) + num);
				num += (float)this._heights[i];
			}
			return num + (float)(this.TopPadding + this.BottomPadding + ((count != 0) ? ((count - 1) * this.ItemSpacing) : 0));
		}

		private float CalcSizesPositionsHorizontal(int count)
		{
			this._widths.Clear();
			this._positions.Clear();
			float num = 0f;
			for (int i = 0; i < count; i++)
			{
				this._widths[i] = this.OnWidth(i);
				this._positions[i] = (float)(this.LeftPadding + i * this.ItemSpacing) + num;
				num += (float)this._widths[i];
			}
			return num + (float)(this.LeftPadding + this.RightPadding + ((count != 0) ? ((count - 1) * this.ItemSpacing) : 0));
		}

		public void ApplyDataTo(int count, int newCount, InfiniteScroll.Direction direction)
		{
			if (this.Type == 0)
			{
				this.ApplyDataToVertical(count, newCount, direction);
			}
			else
			{
				this.ApplyDataToHorizontal(count, newCount, direction);
			}
		}

		private void ApplyDataToVertical(int count, int newCount, InfiniteScroll.Direction direction)
		{
			this._count = count;
			if (this._count <= this._views.Length)
			{
				this.InitData(count);
				return;
			}
			float num = this.CalcSizesPositions(count);
			this._content.sizeDelta = new Vector2(this._content.sizeDelta.x, num);
			Vector2 anchoredPosition = this._content.anchoredPosition;
			if (direction == InfiniteScroll.Direction.Top)
			{
				float num2 = 0f;
				for (int i = 0; i < newCount; i++)
				{
					num2 += (float)(this._heights[i] + this.ItemSpacing);
				}
				anchoredPosition.y = num2;
				this._previousPosition = newCount;
			}
			else
			{
				float num3 = 0f;
				for (int j = this._heights.Count - 1; j >= this._heights.Count - newCount; j--)
				{
					num3 += (float)(this._heights[j] + this.ItemSpacing);
				}
				anchoredPosition.y = num - num3 - this._container.height;
			}
			this._content.anchoredPosition = anchoredPosition;
			float num4 = this._content.anchoredPosition.y - (float)this.ItemSpacing;
			float num5 = Mathf.Abs(this._positions[this._previousPosition]) + (float)this._heights[this._previousPosition];
			int num6 = (num4 <= num5) ? (this._previousPosition - 1) : (this._previousPosition + 1);
			for (int k = 0; k < this._views.Length; k++)
			{
				int num7 = num6 % this._views.Length;
				this._views[num7].name = num6.ToString();
				this.OnFill(num6, this._views[num7]);
				anchoredPosition = this._rects[num7].anchoredPosition;
				anchoredPosition.y = this._positions[num6];
				this._rects[num7].anchoredPosition = anchoredPosition;
				Vector2 sizeDelta = this._rects[num7].sizeDelta;
				sizeDelta.y = (float)this._heights[num6];
				this._rects[num7].sizeDelta = sizeDelta;
				num6++;
				if (num6 == this._count)
				{
					break;
				}
			}
		}

		private void ApplyDataToHorizontal(int count, int newCount, InfiniteScroll.Direction direction)
		{
			this._count = count;
			if (this._count <= this._views.Length)
			{
				this.InitData(count);
				return;
			}
			float num = this.CalcSizesPositions(count);
			this._content.sizeDelta = new Vector2(num, this._content.sizeDelta.y);
			Vector2 anchoredPosition = this._content.anchoredPosition;
			if (direction == InfiniteScroll.Direction.Left)
			{
				float num2 = 0f;
				for (int i = 0; i < newCount; i++)
				{
					num2 -= (float)(this._widths[i] + this.ItemSpacing);
				}
				anchoredPosition.x = num2;
				this._previousPosition = newCount;
			}
			else
			{
				float num3 = 0f;
				for (int j = this._widths.Count - 1; j >= this._widths.Count - newCount; j--)
				{
					num3 += (float)(this._widths[j] + this.ItemSpacing);
				}
				anchoredPosition.x = -num + num3 + this._container.width;
			}
			this._content.anchoredPosition = anchoredPosition;
			float num4 = this._content.anchoredPosition.x - (float)this.ItemSpacing;
			float num5 = Mathf.Abs(this._positions[this._previousPosition]) + (float)this._widths[this._previousPosition];
			int num6 = (num4 <= num5) ? (this._previousPosition - 1) : (this._previousPosition + 1);
			for (int k = 0; k < this._views.Length; k++)
			{
				int num7 = num6 % this._views.Length;
				this._views[num7].name = num6.ToString();
				this.OnFill(num6, this._views[num7]);
				anchoredPosition = this._rects[num7].anchoredPosition;
				anchoredPosition.x = this._positions[num6];
				this._rects[num7].anchoredPosition = anchoredPosition;
				Vector2 sizeDelta = this._rects[num7].sizeDelta;
				sizeDelta.x = (float)this._widths[num6];
				this._rects[num7].sizeDelta = sizeDelta;
				num6++;
				if (num6 == this._count)
				{
					break;
				}
			}
		}

		private void MoveDataTo(int index, float height)
		{
			if (this.Type == 0)
			{
				this.MoveDataToVertical(index, height);
			}
			else
			{
				this.MoveDataToHorizontal(index, height);
			}
		}

		private void MoveDataToVertical(int index, float height)
		{
			this._content.sizeDelta = new Vector2(this._content.sizeDelta.x, height);
			Vector2 anchoredPosition = this._content.anchoredPosition;
			for (int i = 0; i < this._views.Length; i++)
			{
				int num = index % this._views.Length;
				this._views[num].name = index.ToString();
				if (index >= this._count)
				{
					this._views[num].gameObject.SetActive(false);
				}
				else
				{
					this._views[num].gameObject.SetActive(true);
					this.OnFill(index, this._views[num]);
					anchoredPosition = this._rects[num].anchoredPosition;
					anchoredPosition.y = this._positions[index];
					this._rects[num].anchoredPosition = anchoredPosition;
					Vector2 sizeDelta = this._rects[num].sizeDelta;
					sizeDelta.y = (float)this._heights[index];
					this._rects[num].sizeDelta = sizeDelta;
					index++;
				}
			}
		}

		private void MoveDataToHorizontal(int index, float width)
		{
			this._content.sizeDelta = new Vector2(width, this._content.sizeDelta.y);
			Vector2 anchoredPosition = this._content.anchoredPosition;
			for (int i = 0; i < this._views.Length; i++)
			{
				int num = index % this._views.Length;
				this._views[num].name = index.ToString();
				if (index >= this._count)
				{
					this._views[num].gameObject.SetActive(false);
				}
				else
				{
					this._views[num].gameObject.SetActive(true);
					this.OnFill(index, this._views[num]);
					anchoredPosition = this._rects[num].anchoredPosition;
					anchoredPosition.x = this._positions[index];
					this._rects[num].anchoredPosition = anchoredPosition;
					Vector2 sizeDelta = this._rects[num].sizeDelta;
					sizeDelta.x = (float)this._widths[index];
					this._rects[num].sizeDelta = sizeDelta;
					index++;
				}
			}
		}

		public void RecycleAll()
		{
			this._count = 0;
			if (this._views == null)
			{
				return;
			}
			for (int i = 0; i < this._views.Length; i++)
			{
				this._views[i].gameObject.SetActive(false);
			}
		}

		public void Recycle(int index)
		{
			this._count--;
			string strB = index.ToString();
			float height = this.CalcSizesPositions(this._count);
			for (int i = 0; i < this._views.Length; i++)
			{
				if (string.CompareOrdinal(this._views[i].name, strB) == 0)
				{
					this._views[i].gameObject.SetActive(false);
					this.MoveDataTo(i, height);
					break;
				}
			}
		}

		public void UpdateVisible()
		{
			for (int i = 0; i < this._views.Length; i++)
			{
				bool active = i < this._count;
				this._views[i].gameObject.SetActive(active);
				if (i + 1 <= this._count)
				{
					int arg = int.Parse(this._views[i].name);
					this.OnFill(arg, this._views[i]);
				}
			}
		}

		public void RefreshViews()
		{
			if (this._views == null)
			{
				return;
			}
			for (int i = 0; i < this._views.Length; i++)
			{
				UnityEngine.Object.Destroy(this._views[i].gameObject);
			}
			this._rects = null;
			this._views = null;
			this.CreateViews();
		}

		private void CreateViews()
		{
			if (this.Type == 0)
			{
				this.CreateViewsVertical();
			}
			else
			{
				this.CreateViewsHorizontal();
			}
		}

		private void CreateViewsVertical()
		{
			if (this._views != null)
			{
				return;
			}
			int num = 0;
			foreach (int num2 in this._heights.Values)
			{
				num += num2;
			}
			num /= this._heights.Count;
			int num3 = Mathf.RoundToInt(this._container.height / (float)num) + 4;
			this._views = new GameObject[num3];
			for (int i = 0; i < num3; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Prefab, Vector3.zero, Quaternion.identity);
				gameObject.transform.SetParent(this._content);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				RectTransform component = gameObject.GetComponent<RectTransform>();
				component.pivot = new Vector2(0.5f, 1f);
				component.anchorMin = new Vector2(0f, 1f);
				component.anchorMax = Vector2.one;
				component.offsetMax = Vector2.zero;
				component.offsetMin = Vector2.zero;
				this._views[i] = gameObject;
			}
			this._rects = new RectTransform[this._views.Length];
			for (int j = 0; j < this._views.Length; j++)
			{
				this._rects[j] = this._views[j].gameObject.GetComponent<RectTransform>();
			}
		}

		private void CreateViewsHorizontal()
		{
			if (this._views != null)
			{
				return;
			}
			int num = 0;
			foreach (int num2 in this._widths.Values)
			{
				num += num2;
			}
			num /= this._widths.Count;
			int num3 = Mathf.RoundToInt(this._container.width / (float)num) + 4;
			this._views = new GameObject[num3];
			for (int i = 0; i < num3; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.Prefab, Vector3.zero, Quaternion.identity);
				gameObject.transform.SetParent(this._content);
				gameObject.transform.localScale = Vector3.one;
				gameObject.transform.localPosition = Vector3.zero;
				RectTransform component = gameObject.GetComponent<RectTransform>();
				component.pivot = new Vector2(0f, 0.5f);
				component.anchorMin = Vector2.zero;
				component.anchorMax = new Vector2(0f, 1f);
				component.offsetMax = Vector2.zero;
				component.offsetMin = Vector2.zero;
				this._views[i] = gameObject;
			}
			this._rects = new RectTransform[this._views.Length];
			for (int j = 0; j < this._views.Length; j++)
			{
				this._rects[j] = this._views[j].gameObject.GetComponent<RectTransform>();
			}
		}

		private void CreateLabels()
		{
			if (this.Type == 0)
			{
				this.CreateLabelsVertical();
			}
			else
			{
				this.CreateLabelsHorizontal();
			}
		}

		private void CreateLabelsVertical()
		{
			GameObject gameObject = new GameObject("TopLabel");
			gameObject.transform.SetParent(this._scroll.viewport.transform);
			this.TopLabel = gameObject.AddComponent<Text>();
			this.TopLabel.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			this.TopLabel.fontSize = 24;
			this.TopLabel.transform.localScale = Vector3.one;
			this.TopLabel.alignment = TextAnchor.MiddleCenter;
			this.TopLabel.text = this.TopPullLabel;
			RectTransform component = this.TopLabel.GetComponent<RectTransform>();
			component.pivot = new Vector2(0.5f, 1f);
			component.anchorMin = new Vector2(0f, 1f);
			component.anchorMax = Vector2.one;
			component.offsetMax = Vector2.zero;
			component.offsetMin = new Vector2(0f, -55f);
			component.anchoredPosition3D = Vector3.zero;
			gameObject.SetActive(false);
			GameObject gameObject2 = new GameObject("BottomLabel");
			gameObject2.transform.SetParent(this._scroll.viewport.transform);
			this.BottomLabel = gameObject2.AddComponent<Text>();
			this.BottomLabel.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			this.BottomLabel.fontSize = 24;
			this.BottomLabel.transform.localScale = Vector3.one;
			this.BottomLabel.alignment = TextAnchor.MiddleCenter;
			this.BottomLabel.text = this.BottomPullLabel;
			this.BottomLabel.transform.position = Vector3.zero;
			component = this.BottomLabel.GetComponent<RectTransform>();
			component.pivot = new Vector2(0.5f, 0f);
			component.anchorMin = Vector2.zero;
			component.anchorMax = new Vector2(1f, 0f);
			component.offsetMax = new Vector2(0f, 55f);
			component.offsetMin = Vector2.zero;
			component.anchoredPosition3D = Vector3.zero;
			gameObject2.SetActive(false);
		}

		private void CreateLabelsHorizontal()
		{
			GameObject gameObject = new GameObject("LeftLabel");
			gameObject.transform.SetParent(this._scroll.viewport.transform);
			this.LeftLabel = gameObject.AddComponent<Text>();
			this.LeftLabel.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			this.LeftLabel.fontSize = 24;
			this.LeftLabel.transform.localScale = Vector3.one;
			this.LeftLabel.alignment = TextAnchor.MiddleCenter;
			this.LeftLabel.text = this.LeftPullLabel;
			RectTransform component = this.LeftLabel.GetComponent<RectTransform>();
			component.pivot = new Vector2(0f, 0.5f);
			component.anchorMin = Vector2.zero;
			component.anchorMax = new Vector2(0f, 1f);
			component.offsetMax = Vector2.zero;
			component.offsetMin = new Vector2(-110f, 0f);
			component.anchoredPosition3D = Vector3.zero;
			gameObject.SetActive(false);
			GameObject gameObject2 = new GameObject("RightLabel");
			gameObject2.transform.SetParent(this._scroll.viewport.transform);
			this.RightLabel = gameObject2.AddComponent<Text>();
			this.RightLabel.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
			this.RightLabel.fontSize = 24;
			this.RightLabel.transform.localScale = Vector3.one;
			this.RightLabel.alignment = TextAnchor.MiddleCenter;
			this.RightLabel.text = this.RightPullLabel;
			this.RightLabel.transform.position = Vector3.zero;
			component = this.RightLabel.GetComponent<RectTransform>();
			component.pivot = new Vector2(1f, 0.5f);
			component.anchorMin = new Vector2(1f, 0f);
			component.anchorMax = Vector3.one;
			component.offsetMax = new Vector2(110f, 0f);
			component.offsetMin = Vector2.zero;
			component.anchoredPosition3D = Vector3.zero;
			gameObject2.SetActive(false);
		}

		private const float PULL_VALUE = 0.05f;

		private const float LABEL_OFFSET = 55f;

		public Action<int, GameObject> OnFill = delegate(int A_0, GameObject A_1)
		{
		};

		public Action<InfiniteScroll.Direction> OnPull = delegate(InfiniteScroll.Direction A_0)
		{
		};

		[Header("Item settings")]
		public GameObject Prefab;

		[Header("Padding")]
		public int TopPadding = 10;

		public int BottomPadding = 10;

		[Header("Padding")]
		public int LeftPadding = 10;

		public int RightPadding = 10;

		public int ItemSpacing = 2;

		[Header("Labels")]
		public string TopPullLabel = "Pull to refresh";

		public string TopReleaseLabel = "Release to load";

		public string BottomPullLabel = "Pull to refresh";

		public string BottomReleaseLabel = "Release to load";

		public string LeftPullLabel = "Pull to refresh";

		public string LeftReleaseLabel = "Release to load";

		public string RightPullLabel = "Pull to refresh";

		public string RightReleaseLabel = "Release to load";

		[Header("Directions")]
		public bool IsPullTop = true;

		public bool IsPullBottom = true;

		[Header("Directions")]
		public bool IsPullLeft = true;

		public bool IsPullRight = true;

		[HideInInspector]
		public Text TopLabel;

		[HideInInspector]
		public Text BottomLabel;

		[HideInInspector]
		public Text LeftLabel;

		[HideInInspector]
		public Text RightLabel;

		[HideInInspector]
		public int Type;

		private ScrollRect _scroll;

		private RectTransform _content;

		private Rect _container;

		private RectTransform[] _rects;

		private GameObject[] _views;

		private bool _isCanLoadUp;

		private bool _isCanLoadDown;

		private bool _isCanLoadLeft;

		private bool _isCanLoadRight;

		private int _previousPosition = -1;

		private int _count;

		private Dictionary<int, int> _heights;

		private Dictionary<int, int> _widths;

		private Dictionary<int, float> _positions;

		public enum Direction
		{
			Top,
			Bottom,
			Left,
			Right
		}

		public delegate int HeightItem(int index);

		public delegate int WidthtItem(int index);
	}
}
