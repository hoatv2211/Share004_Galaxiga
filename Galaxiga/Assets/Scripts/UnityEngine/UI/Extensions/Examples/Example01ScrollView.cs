using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions.Examples
{
	public class Example01ScrollView : FancyScrollView<Example01CellDto>
	{
		private new void Awake()
		{
			base.Awake();
			this.scrollPositionController.OnUpdatePosition.AddListener(new UnityAction<float>(base.UpdatePosition));
		}

		public void UpdateData(List<Example01CellDto> data)
		{
			this.cellData = data;
			this.scrollPositionController.SetDataCount(this.cellData.Count);
			base.UpdateContents();
		}

		[SerializeField]
		private ScrollPositionController scrollPositionController;
	}
}
