using System;

namespace UnityEngine.UI.Extensions.Examples
{
	public class ComboBoxChanged : MonoBehaviour
	{
		public void ComboBoxChangedEvent(string text)
		{
			UnityEngine.Debug.Log("ComboBox changed [" + text + "]");
		}

		public void AutoCompleteComboBoxChangedEvent(string text)
		{
			UnityEngine.Debug.Log("AutoCompleteComboBox changed [" + text + "]");
		}

		public void AutoCompleteComboBoxSelectionChangedEvent(string text, bool valid)
		{
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"AutoCompleteComboBox selection changed [",
				text,
				"] and its validity was [",
				valid,
				"]"
			}));
		}

		public void DropDownChangedEvent(int newValue)
		{
			UnityEngine.Debug.Log("DropDown changed [" + newValue + "]");
		}
	}
}
