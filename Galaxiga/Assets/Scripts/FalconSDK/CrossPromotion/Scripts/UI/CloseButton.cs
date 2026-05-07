using System;
using UnityEngine;
using UnityEngine.UI;

namespace FalconSDK.CrossPromotion.Scripts.UI
{
	[RequireComponent(typeof(Button))]
	public class CloseButton : MonoBehaviour
	{
		private void Start()
		{
			base.GetComponent<Button>().onClick.AddListener(delegate()
			{
				UnityEngine.Object.Destroy(this.parent);
			});
		}

		public GameObject parent;
	}
}
