using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRF.UI
{
	[RequireComponent(typeof(CanvasScaler))]
	[AddComponentMenu("SRF/UI/Retina Scaler")]
	public class SRRetinaScaler : SRMonoBehaviour
	{
		public int ThresholdDpi
		{
			get
			{
				return this._thresholdDpi;
			}
		}

		public float RetinaScale
		{
			get
			{
				return this._retinaScale;
			}
		}

		private void Start()
		{
			float dpi = Screen.dpi;
			if (dpi <= 0f)
			{
				return;
			}
			if (dpi > (float)this.ThresholdDpi)
			{
				CanvasScaler component = base.GetComponent<CanvasScaler>();
				component.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
				component.scaleFactor = this.RetinaScale;
				if (this._disablePixelPerfect)
				{
					base.GetComponent<Canvas>().pixelPerfect = false;
				}
			}
		}

		[SerializeField]
		private float _retinaScale = 2f;

		[SerializeField]
		private int _thresholdDpi = 250;

		[SerializeField]
		private bool _disablePixelPerfect;
	}
}
