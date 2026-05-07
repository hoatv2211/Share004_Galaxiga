using System;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	public class ReorderableListDebug : MonoBehaviour
	{
		private void Awake()
		{
			foreach (ReorderableList reorderableList in UnityEngine.Object.FindObjectsOfType<ReorderableList>())
			{
				reorderableList.OnElementDropped.AddListener(new UnityAction<ReorderableList.ReorderableListEventStruct>(this.ElementDropped));
			}
		}

		private void ElementDropped(ReorderableList.ReorderableListEventStruct droppedStruct)
		{
			this.DebugLabel.text = string.Empty;
			Text debugLabel = this.DebugLabel;
			debugLabel.text = debugLabel.text + "Dropped Object: " + droppedStruct.DroppedObject.name + "\n";
			Text debugLabel2 = this.DebugLabel;
			string text = debugLabel2.text;
			debugLabel2.text = string.Concat(new object[]
			{
				text,
				"Is Clone ?: ",
				droppedStruct.IsAClone,
				"\n"
			});
			if (droppedStruct.IsAClone)
			{
				Text debugLabel3 = this.DebugLabel;
				debugLabel3.text = debugLabel3.text + "Source Object: " + droppedStruct.SourceObject.name + "\n";
			}
			Text debugLabel4 = this.DebugLabel;
			debugLabel4.text += string.Format("From {0} at Index {1} \n", droppedStruct.FromList.name, droppedStruct.FromIndex);
			Text debugLabel5 = this.DebugLabel;
			debugLabel5.text += string.Format("To {0} at Index {1} \n", droppedStruct.ToList.name, droppedStruct.ToIndex);
		}

		public Text DebugLabel;
	}
}
