using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	[RequireComponent(typeof(EventSystem))]
	[AddComponentMenu("Event/Extensions/Tab Navigation Helper")]
	public class TabNavigationHelper : MonoBehaviour
	{
		private void Start()
		{
			this._system = base.GetComponent<EventSystem>();
			if (this._system == null)
			{
				UnityEngine.Debug.LogError("Needs to be attached to the Event System component in the scene");
			}
			if (this.NavigationMode == NavigationMode.Manual && this.NavigationPath.Length > 0)
			{
				this.StartingObject = this.NavigationPath[0].gameObject.GetComponent<Selectable>();
			}
			if (this.StartingObject == null && this.CircularNavigation)
			{
				this.SelectDefaultObject(out this.StartingObject);
			}
		}

		public void Update()
		{
			Selectable selectable = null;
			if (this.LastObject == null && this._system.currentSelectedGameObject != null)
			{
				selectable = this._system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
				while (selectable != null)
				{
					this.LastObject = selectable;
					selectable = selectable.FindSelectableOnDown();
				}
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Tab) && UnityEngine.Input.GetKey(KeyCode.LeftShift))
			{
				if (this.NavigationMode == NavigationMode.Manual && this.NavigationPath.Length > 0)
				{
					for (int i = this.NavigationPath.Length - 1; i >= 0; i--)
					{
						if (!(this._system.currentSelectedGameObject != this.NavigationPath[i].gameObject))
						{
							selectable = ((i != 0) ? this.NavigationPath[i - 1] : this.NavigationPath[this.NavigationPath.Length - 1]);
							break;
						}
					}
				}
				else if (this._system.currentSelectedGameObject != null)
				{
					selectable = this._system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
					if (selectable == null && this.CircularNavigation)
					{
						selectable = this.LastObject;
					}
				}
				else
				{
					this.SelectDefaultObject(out selectable);
				}
			}
			else if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
			{
				if (this.NavigationMode == NavigationMode.Manual && this.NavigationPath.Length > 0)
				{
					for (int j = 0; j < this.NavigationPath.Length; j++)
					{
						if (!(this._system.currentSelectedGameObject != this.NavigationPath[j].gameObject))
						{
							selectable = ((j != this.NavigationPath.Length - 1) ? this.NavigationPath[j + 1] : this.NavigationPath[0]);
							break;
						}
					}
				}
				else if (this._system.currentSelectedGameObject != null)
				{
					selectable = this._system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
					if (selectable == null && this.CircularNavigation)
					{
						selectable = this.StartingObject;
					}
				}
				else
				{
					this.SelectDefaultObject(out selectable);
				}
			}
			else if (this._system.currentSelectedGameObject == null)
			{
				this.SelectDefaultObject(out selectable);
			}
			if (this.CircularNavigation && this.StartingObject == null)
			{
				this.StartingObject = selectable;
			}
			this.selectGameObject(selectable);
		}

		private void SelectDefaultObject(out Selectable next)
		{
			if (this._system.firstSelectedGameObject)
			{
				next = this._system.firstSelectedGameObject.GetComponent<Selectable>();
			}
			else
			{
				next = null;
			}
		}

		private void selectGameObject(Selectable selectable)
		{
			if (selectable != null)
			{
				InputField component = selectable.GetComponent<InputField>();
				if (component != null)
				{
					component.OnPointerClick(new PointerEventData(this._system));
				}
				this._system.SetSelectedGameObject(selectable.gameObject, new BaseEventData(this._system));
			}
		}

		private EventSystem _system;

		private Selectable StartingObject;

		private Selectable LastObject;

		[Tooltip("The path to take when user is tabbing through ui components.")]
		public Selectable[] NavigationPath;

		[Tooltip("Use the default Unity navigation system or a manual fixed order using Navigation Path")]
		public NavigationMode NavigationMode;

		[Tooltip("If True, this will loop the tab order from last to first automatically")]
		public bool CircularNavigation;
	}
}
