using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Extensions/Pagination Manager")]
	public class PaginationManager : ToggleGroup
	{
		protected PaginationManager()
		{
		}

		public int CurrentPage
		{
			get
			{
				return this.scrollSnap.CurrentPage;
			}
		}

		protected override void Start()
		{
			base.Start();
			if (this.scrollSnap == null)
			{
				UnityEngine.Debug.LogError("A ScrollSnap script must be attached");
				return;
			}
			if (this.scrollSnap.Pagination)
			{
				this.scrollSnap.Pagination = null;
			}
			this.scrollSnap.OnSelectionPageChangedEvent.AddListener(new UnityAction<int>(this.SetToggleGraphics));
			this.scrollSnap.OnSelectionChangeEndEvent.AddListener(new UnityAction<int>(this.OnPageChangeEnd));
			this.m_PaginationChildren = base.GetComponentsInChildren<Toggle>().ToList<Toggle>();
			for (int i = 0; i < this.m_PaginationChildren.Count; i++)
			{
				this.m_PaginationChildren[i].onValueChanged.AddListener(new UnityAction<bool>(this.ToggleClick));
				this.m_PaginationChildren[i].group = this;
				this.m_PaginationChildren[i].isOn = false;
			}
			this.SetToggleGraphics(this.CurrentPage);
			if (this.m_PaginationChildren.Count != this.scrollSnap._scroll_rect.content.childCount)
			{
				UnityEngine.Debug.LogWarning("Uneven pagination icon to page count");
			}
		}

		public void GoToScreen(int pageNo)
		{
			this.scrollSnap.GoToScreen(pageNo);
		}

		private void ToggleClick(Toggle target)
		{
			if (!target.isOn)
			{
				this.isAClick = true;
				this.GoToScreen(this.m_PaginationChildren.IndexOf(target));
			}
		}

		private void ToggleClick(bool toggle)
		{
			if (toggle)
			{
				for (int i = 0; i < this.m_PaginationChildren.Count; i++)
				{
					if (this.m_PaginationChildren[i].isOn)
					{
						this.GoToScreen(i);
						break;
					}
				}
			}
		}

		private void ToggleClick(int target)
		{
			this.isAClick = true;
			this.GoToScreen(target);
		}

		private void SetToggleGraphics(int pageNo)
		{
			if (!this.isAClick)
			{
				this.m_PaginationChildren[pageNo].isOn = true;
			}
		}

		private void OnPageChangeEnd(int pageNo)
		{
			this.isAClick = false;
		}

		private List<Toggle> m_PaginationChildren;

		[SerializeField]
		private ScrollSnapBase scrollSnap;

		private bool isAClick;
	}
}
