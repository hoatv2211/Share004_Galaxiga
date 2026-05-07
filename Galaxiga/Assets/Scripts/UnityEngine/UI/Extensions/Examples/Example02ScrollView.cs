using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions.Examples
{
	public class Example02ScrollView : FancyScrollView<Example02CellDto, Example02ScrollViewContext>
	{
		private new void Awake()
		{
			this.scrollPositionController.OnUpdatePosition.AddListener(new UnityAction<float>(base.UpdatePosition));
			this.scrollPositionController.OnItemSelected.AddListener(new UnityAction<int>(this.CellSelected));
			base.SetContext(new Example02ScrollViewContext
			{
				OnPressedCell = new Action<Example02ScrollViewCell>(this.OnPressedCell)
			});
			base.Awake();
		}

		public void UpdateData(List<Example02CellDto> data)
		{
			this.cellData = data;
			this.scrollPositionController.SetDataCount(this.cellData.Count);
			base.UpdateContents();
		}

		private void OnPressedCell(Example02ScrollViewCell cell)
		{
			this.scrollPositionController.ScrollTo(cell.DataIndex, 0.4f);
			this.context.SelectedIndex = cell.DataIndex;
			base.UpdateContents();
		}

		private void CellSelected(int cellIndex)
		{
			this.context.SelectedIndex = cellIndex;
			base.UpdateContents();
		}

		[SerializeField]
		private ScrollPositionController scrollPositionController;
	}
}
