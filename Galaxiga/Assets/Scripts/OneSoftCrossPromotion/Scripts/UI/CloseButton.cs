using System;
using OneSoftCrossPromotion.Scripts.Singleton;
using UnityEngine;
using UnityEngine.UI;

namespace OneSoftCrossPromotion.Scripts.UI
{
	public class CloseButton : Singleton<CloseButton>
	{
		protected override void Awake()
		{
			base.GetComponent<Button>().onClick.AddListener(delegate()
			{
				UnityEngine.Object.Destroy(base.transform.parent.parent.gameObject);
			});
		}
	}
}
