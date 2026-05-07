using System;

namespace UnityEngine.UI.Extensions
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Text))]
	[AddComponentMenu("UI/Effects/Extensions/Curly UI Text")]
	public class CUIText : CUIGraphic
	{
		public override void ReportSet()
		{
			if (this.uiGraphic == null)
			{
				this.uiGraphic = base.GetComponent<Text>();
			}
			base.ReportSet();
		}
	}
}
