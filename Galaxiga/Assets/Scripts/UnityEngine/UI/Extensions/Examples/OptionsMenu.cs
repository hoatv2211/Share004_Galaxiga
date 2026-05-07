using System;

namespace UnityEngine.UI.Extensions.Examples
{
	public class OptionsMenu : SimpleMenu<OptionsMenu>
	{
		public void OnMagicButtonPressed()
		{
			AwesomeMenu.Show(this.Slider.value);
		}

		public Slider Slider;
	}
}
