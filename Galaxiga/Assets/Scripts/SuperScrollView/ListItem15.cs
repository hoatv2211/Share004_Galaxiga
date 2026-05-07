using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	public class ListItem15 : MonoBehaviour
	{
		public void Init()
		{
			foreach (ListItem16 listItem in this.mItemList)
			{
				listItem.Init();
			}
		}

		public List<ListItem16> mItemList;
	}
}
