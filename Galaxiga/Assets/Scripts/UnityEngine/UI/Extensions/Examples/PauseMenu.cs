using System;

namespace UnityEngine.UI.Extensions.Examples
{
	public class PauseMenu : SimpleMenu<PauseMenu>
	{
		public void OnQuitPressed()
		{
			SimpleMenu<PauseMenu>.Hide();
			UnityEngine.Object.Destroy(base.gameObject);
			SimpleMenu<GameMenu>.Hide();
		}
	}
}
