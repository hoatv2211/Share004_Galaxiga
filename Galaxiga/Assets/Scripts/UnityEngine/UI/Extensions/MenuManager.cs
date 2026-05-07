using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Extensions/Menu Manager")]
	[DisallowMultipleComponent]
	public class MenuManager : MonoBehaviour
	{
		public static MenuManager Instance { get; set; }

		private void Awake()
		{
			MenuManager.Instance = this;
			if (this.MenuScreens.Length > this.StartScreen)
			{
				this.CreateInstance(this.MenuScreens[this.StartScreen].name);
				this.OpenMenu(this.MenuScreens[this.StartScreen]);
			}
			else
			{
				UnityEngine.Debug.LogError("Not enough Menu Screens configured");
			}
		}

		private void OnDestroy()
		{
			MenuManager.Instance = null;
		}

		public void CreateInstance<T>() where T : Menu
		{
			T prefab = this.GetPrefab<T>();
			Object.Instantiate<T>(prefab, base.transform);
		}

		public void CreateInstance(string MenuName)
		{
			GameObject prefab = this.GetPrefab(MenuName);
			Object.Instantiate<GameObject>(prefab, base.transform);
		}

		public void OpenMenu(Menu instance)
		{
			if (this.menuStack.Count > 0)
			{
				if (instance.DisableMenusUnderneath)
				{
					foreach (Menu menu in this.menuStack)
					{
						menu.gameObject.SetActive(false);
						if (menu.DisableMenusUnderneath)
						{
							break;
						}
					}
				}
				Canvas component = instance.GetComponent<Canvas>();
				Canvas component2 = this.menuStack.Peek().GetComponent<Canvas>();
				component.sortingOrder = component2.sortingOrder + 1;
			}
			this.menuStack.Push(instance);
		}

		private GameObject GetPrefab(string PrefabName)
		{
			for (int i = 0; i < this.MenuScreens.Length; i++)
			{
				if (this.MenuScreens[i].name == PrefabName)
				{
					return this.MenuScreens[i].gameObject;
				}
			}
			throw new MissingReferenceException("Prefab not found for " + PrefabName);
		}

		private T GetPrefab<T>() where T : Menu
		{
			FieldInfo[] fields = base.GetType().GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			foreach (FieldInfo fieldInfo in fields)
			{
				T t = fieldInfo.GetValue(this) as T;
				if (t != null)
				{
					return t;
				}
			}
			throw new MissingReferenceException("Prefab not found for type " + typeof(T));
		}

		public void CloseMenu(Menu menu)
		{
			if (this.menuStack.Count == 0)
			{
				UnityEngine.Debug.LogErrorFormat(menu, "{0} cannot be closed because menu stack is empty", new object[]
				{
					menu.GetType()
				});
				return;
			}
			if (this.menuStack.Peek() != menu)
			{
				UnityEngine.Debug.LogErrorFormat(menu, "{0} cannot be closed because it is not on top of stack", new object[]
				{
					menu.GetType()
				});
				return;
			}
			this.CloseTopMenu();
		}

		public void CloseTopMenu()
		{
			Menu menu = this.menuStack.Pop();
			if (menu.DestroyWhenClosed)
			{
				UnityEngine.Object.Destroy(menu.gameObject);
			}
			else
			{
				menu.gameObject.SetActive(false);
			}
			foreach (Menu menu2 in this.menuStack)
			{
				menu2.gameObject.SetActive(true);
				if (menu2.DisableMenusUnderneath)
				{
					break;
				}
			}
		}

		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && this.menuStack.Count > 0)
			{
				this.menuStack.Peek().OnBackPressed();
			}
		}

		public Menu[] MenuScreens;

		public int StartScreen;

		private Stack<Menu> menuStack = new Stack<Menu>();
	}
}
