using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	public class ListItem7 : MonoBehaviour
	{
		public void Init()
		{
		}

		public int Value
		{
			get
			{
				return this.mValue;
			}
			set
			{
				this.mValue = value;
			}
		}

		public Text mText;

		public int mValue;
	}
}
