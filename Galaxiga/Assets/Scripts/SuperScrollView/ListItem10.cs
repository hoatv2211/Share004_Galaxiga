using System;
using UnityEngine;

namespace SuperScrollView
{
	public class ListItem10 : MonoBehaviour
	{
		public void Init()
		{
			foreach (ListItem9 listItem in this.mItemList)
			{
				listItem.Init();
			}
		}

		public ListItem9[] mItemList;
	}
}
