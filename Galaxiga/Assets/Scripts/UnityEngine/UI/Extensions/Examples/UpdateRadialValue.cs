using System;

namespace UnityEngine.UI.Extensions.Examples
{
	public class UpdateRadialValue : MonoBehaviour
	{
		private void Start()
		{
		}

		public void UpdateSliderValue()
		{
			float value;
			float.TryParse(this.input.text, out value);
			this.slider.Value = value;
		}

		public void UpdateSliderAndle()
		{
			int num;
			int.TryParse(this.input.text, out num);
			this.slider.Angle = (float)num;
		}

		public InputField input;

		public RadialSlider slider;
	}
}
