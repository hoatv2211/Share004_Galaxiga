using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class ListItem12 : MonoBehaviour
	{
		public int TreeItemIndex
		{
			get
			{
				return this.mTreeItemIndex;
			}
		}

		public void Init()
		{
			this.mButton.onClick.AddListener(new UnityAction(this.OnButtonClicked));
		}

		public void SetClickCallBack(Action<int> clickHandler)
		{
			this.mClickHandler = clickHandler;
		}

		private void OnButtonClicked()
		{
			if (this.mClickHandler != null)
			{
				this.mClickHandler(this.mTreeItemIndex);
			}
		}

		public void SetExpand(bool expand)
		{
			if (expand)
			{
				this.mArrow.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
			}
			else
			{
				this.mArrow.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
			}
		}

		public void SetItemData(int treeItemIndex, bool expand)
		{
			this.mTreeItemIndex = treeItemIndex;
			this.SetExpand(expand);
		}

		public Text mText;

		public GameObject mArrow;

		public Button mButton;

		private int mTreeItemIndex = -1;

		private Action<int> mClickHandler;
	}
}
