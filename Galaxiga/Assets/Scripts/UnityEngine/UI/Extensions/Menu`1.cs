using System;

namespace UnityEngine.UI.Extensions
{
	public abstract class Menu<T> : Menu where T : Menu<T>
	{
		public static T Instance { get; private set; }

		protected virtual void Awake()
		{
			Menu<T>.Instance = (T)((object)this);
		}

		protected virtual void OnDestroy()
		{
			Menu<T>.Instance = (T)((object)null);
		}

		protected static void Open()
		{
			if (Menu<T>.Instance == null)
			{
				MenuManager.Instance.CreateInstance(typeof(T).Name);
			}
			else
			{
				T instance = Menu<T>.Instance;
				instance.gameObject.SetActive(true);
			}
			MenuManager.Instance.OpenMenu(Menu<T>.Instance);
		}

		protected static void Close()
		{
			if (Menu<T>.Instance == null)
			{
				UnityEngine.Debug.LogErrorFormat("Trying to close menu {0} but Instance is null", new object[]
				{
					typeof(T)
				});
				return;
			}
			MenuManager.Instance.CloseMenu(Menu<T>.Instance);
		}

		public override void OnBackPressed()
		{
			Menu<T>.Close();
		}
	}
}
