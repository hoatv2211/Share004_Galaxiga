using System;

namespace UnityEngine.UI.Extensions.Examples
{
	public class UpdateScrollSnap : MonoBehaviour
	{
		public void AddButton()
		{
			if (this.HSS)
			{
				GameObject go = UnityEngine.Object.Instantiate<GameObject>(this.HorizontalPagePrefab);
				this.HSS.AddChild(go);
			}
			if (this.VSS)
			{
				GameObject go2 = UnityEngine.Object.Instantiate<GameObject>(this.VerticalPagePrefab);
				this.VSS.AddChild(go2);
			}
		}

		public void RemoveButton()
		{
			if (this.HSS)
			{
				GameObject gameObject;
				this.HSS.RemoveChild(this.HSS.CurrentPage, out gameObject);
				gameObject.SetActive(false);
			}
			if (this.VSS)
			{
				GameObject gameObject2;
				this.VSS.RemoveChild(this.VSS.CurrentPage, out gameObject2);
				gameObject2.SetActive(false);
			}
		}

		public void JumpToPage()
		{
			int screenIndex = int.Parse(this.JumpPage.text);
			if (this.HSS)
			{
				this.HSS.GoToScreen(screenIndex);
			}
			if (this.VSS)
			{
				this.VSS.GoToScreen(screenIndex);
			}
		}

		public void SelectionStartChange()
		{
			UnityEngine.Debug.Log("Scroll Snap change started");
		}

		public void SelectionEndChange()
		{
			UnityEngine.Debug.Log("Scroll Snap change finished");
		}

		public void PageChange(int page)
		{
			UnityEngine.Debug.Log(string.Format("Scroll Snap page changed to {0}", page));
		}

		public void RemoveAll()
		{
			GameObject[] array;
			this.HSS.RemoveAllChildren(out array);
			this.VSS.RemoveAllChildren(out array);
		}

		public void JumpToSelectedToggle(int page)
		{
			this.HSS.GoToScreen(page);
		}

		public HorizontalScrollSnap HSS;

		public VerticalScrollSnap VSS;

		public GameObject HorizontalPagePrefab;

		public GameObject VerticalPagePrefab;

		public InputField JumpPage;
	}
}
