using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions
{
	public class FancyScrollView<TData, TContext> : MonoBehaviour where TContext : class
	{
		protected void Awake()
		{
			this.cellBase.SetActive(false);
		}

		protected void SetContext(TContext context)
		{
			this.context = context;
			for (int i = 0; i < this.cells.Count; i++)
			{
				this.cells[i].SetContext(context);
			}
		}

		private FancyScrollViewCell<TData, TContext> CreateCell()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.cellBase);
			gameObject.SetActive(true);
			FancyScrollViewCell<TData, TContext> component = gameObject.GetComponent<FancyScrollViewCell<TData, TContext>>();
			RectTransform rectTransform = component.transform as RectTransform;
			Vector3 localScale = component.transform.localScale;
			Vector2 sizeDelta = Vector2.zero;
			Vector2 offsetMin = Vector2.zero;
			Vector2 offsetMax = Vector2.zero;
			if (rectTransform)
			{
				sizeDelta = rectTransform.sizeDelta;
				offsetMin = rectTransform.offsetMin;
				offsetMax = rectTransform.offsetMax;
			}
			component.transform.SetParent(this.cellBase.transform.parent);
			component.transform.localScale = localScale;
			if (rectTransform)
			{
				rectTransform.sizeDelta = sizeDelta;
				rectTransform.offsetMin = offsetMin;
				rectTransform.offsetMax = offsetMax;
			}
			component.SetContext(this.context);
			component.SetVisible(false);
			return component;
		}

		private void UpdateCellForIndex(FancyScrollViewCell<TData, TContext> cell, int dataIndex)
		{
			if (this.loop)
			{
				dataIndex = this.GetLoopIndex(dataIndex, this.cellData.Count);
			}
			else if (dataIndex < 0 || dataIndex > this.cellData.Count - 1)
			{
				cell.SetVisible(false);
				return;
			}
			cell.SetVisible(true);
			cell.DataIndex = dataIndex;
			cell.UpdateContent(this.cellData[dataIndex]);
		}

		private int GetLoopIndex(int index, int length)
		{
			if (index < 0)
			{
				index = length - 1 + (index + 1) % length;
			}
			else if (index > length - 1)
			{
				index %= length;
			}
			return index;
		}

		protected void UpdateContents()
		{
			this.UpdatePosition(this.currentPosition);
		}

		protected void UpdatePosition(float position)
		{
			this.currentPosition = position;
			float num = position - this.cellOffset / this.cellInterval;
			float num2 = (Mathf.Ceil(num) - num) * this.cellInterval;
			int num3 = Mathf.CeilToInt(num);
			int i = 0;
			float num4 = num2;
			while (num4 <= 1f)
			{
				if (i >= this.cells.Count)
				{
					this.cells.Add(this.CreateCell());
				}
				num4 += this.cellInterval;
				i++;
			}
			i = 0;
			int loopIndex;
			for (float num5 = num2; num5 <= 1f; num5 += this.cellInterval)
			{
				int num6 = num3 + i;
				loopIndex = this.GetLoopIndex(num6, this.cells.Count);
				if (this.cells[loopIndex].gameObject.activeSelf)
				{
					this.cells[loopIndex].UpdatePosition(num5);
				}
				this.UpdateCellForIndex(this.cells[loopIndex], num6);
				i++;
			}
			loopIndex = this.GetLoopIndex(num3 + i, this.cells.Count);
			while (i < this.cells.Count)
			{
				this.cells[loopIndex].SetVisible(false);
				i++;
				loopIndex = this.GetLoopIndex(num3 + i, this.cells.Count);
			}
		}

		[SerializeField]
		[Range(1.401298E-45f, 1f)]
		private float cellInterval;

		[SerializeField]
		[Range(0f, 1f)]
		private float cellOffset;

		[SerializeField]
		private bool loop;

		[SerializeField]
		private GameObject cellBase;

		private float currentPosition;

		private readonly List<FancyScrollViewCell<TData, TContext>> cells = new List<FancyScrollViewCell<TData, TContext>>();

		protected TContext context;

		protected List<TData> cellData = new List<TData>();
	}
}
