using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UI.Extensions.Examples
{
	public class Example01Scene : MonoBehaviour
	{
		private void Start()
		{
			List<Example01CellDto> data = (from i in Enumerable.Range(0, 20)
			select new Example01CellDto
			{
				Message = "Cell " + i
			}).ToList<Example01CellDto>();
			this.scrollView.UpdateData(data);
		}

		[SerializeField]
		private Example01ScrollView scrollView;
	}
}
