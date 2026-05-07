using System;

namespace UnityEngine.UI.Extensions.Examples
{
	public class AwesomeMenu : Menu<AwesomeMenu>
	{
		public static void Show(float awesomeness)
		{
			Menu<AwesomeMenu>.Open();
			Menu<AwesomeMenu>.Instance.Background.color = new Color32((byte)(129f * awesomeness), (byte)(197f * awesomeness), (byte)(34f * awesomeness), byte.MaxValue);
			Menu<AwesomeMenu>.Instance.Title.text = string.Format("This menu is {0:P} awesome", awesomeness);
		}

		public static void Hide()
		{
			Menu<AwesomeMenu>.Close();
		}

		public Image Background;

		public Text Title;
	}
}
