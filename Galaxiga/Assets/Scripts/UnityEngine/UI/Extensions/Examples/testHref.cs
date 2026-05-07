using System;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions.Examples
{
	public class testHref : MonoBehaviour
	{
		private void Awake()
		{
			this.textPic = base.GetComponent<TextPic>();
		}

		private void OnEnable()
		{
			this.textPic.onHrefClick.AddListener(new UnityAction<string>(this.OnHrefClick));
		}

		private void OnDisable()
		{
			this.textPic.onHrefClick.RemoveListener(new UnityAction<string>(this.OnHrefClick));
		}

		private void OnHrefClick(string hrefName)
		{
			UnityEngine.Debug.Log("Click on the " + hrefName);
		}

		public TextPic textPic;
	}
}
