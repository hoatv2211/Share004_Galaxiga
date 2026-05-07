using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	public class ListItem6 : MonoBehaviour
	{
		public void Init()
		{
			foreach (ListItem5 listItem in this.mItemList)
			{
				listItem.Init();
			}
		}

		public List<ListItem5> mItemList;
	}
}
