using System;
using System.Collections.Generic;

namespace UnityEngine.UI.Extensions.Examples
{
	public class TestAddingPoints : MonoBehaviour
	{
		public void AddNewPoint()
		{
			Vector2 item = new Vector2
			{
				x = float.Parse(this.XValue.text),
				y = float.Parse(this.YValue.text)
			};
			List<Vector2> list = new List<Vector2>(this.LineRenderer.Points);
			list.Add(item);
			this.LineRenderer.Points = list.ToArray();
		}

		public void ClearPoints()
		{
			this.LineRenderer.Points = new Vector2[0];
		}

		public UILineRenderer LineRenderer;

		public Text XValue;

		public Text YValue;
	}
}
