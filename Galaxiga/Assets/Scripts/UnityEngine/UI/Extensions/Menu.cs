using System;

namespace UnityEngine.UI.Extensions
{
	public abstract class Menu : MonoBehaviour
	{
		public abstract void OnBackPressed();

		[Tooltip("Destroy the Game UnityEngine.Object when menu is closed (reduces memory usage)")]
		public bool DestroyWhenClosed = true;

		[Tooltip("Disable menus that are under this one in the stack")]
		public bool DisableMenusUnderneath = true;
	}
}
