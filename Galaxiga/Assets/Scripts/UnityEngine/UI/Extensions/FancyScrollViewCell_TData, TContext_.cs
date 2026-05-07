using System;

namespace UnityEngine.UI.Extensions
{
	public class FancyScrollViewCell<TData, TContext> : MonoBehaviour where TContext : class
	{
		public virtual void SetContext(TContext context)
		{
		}

		public virtual void UpdateContent(TData itemData)
		{
		}

		public virtual void UpdatePosition(float position)
		{
		}

		public virtual void SetVisible(bool visible)
		{
			base.gameObject.SetActive(visible);
		}

		public int DataIndex { get; set; }
	}
}
