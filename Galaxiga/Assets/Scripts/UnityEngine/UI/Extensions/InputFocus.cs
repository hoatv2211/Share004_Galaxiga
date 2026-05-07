using System;

namespace UnityEngine.UI.Extensions
{
	[RequireComponent(typeof(InputField))]
	[AddComponentMenu("UI/Extensions/InputFocus")]
	public class InputFocus : MonoBehaviour
	{
		private void Start()
		{
			this._inputField = base.GetComponent<InputField>();
		}

		private void Update()
		{
			if (UnityEngine.Input.GetKeyUp(KeyCode.Return) && !this._inputField.isFocused)
			{
				if (this._ignoreNextActivation)
				{
					this._ignoreNextActivation = false;
				}
				else
				{
					this._inputField.Select();
					this._inputField.ActivateInputField();
				}
			}
		}

		public void buttonPressed()
		{
			bool flag = this._inputField.text == string.Empty;
			this._inputField.text = string.Empty;
			if (!flag)
			{
				this._inputField.Select();
				this._inputField.ActivateInputField();
			}
		}

		public void OnEndEdit(string textString)
		{
			if (!Input.GetKeyDown(KeyCode.Return))
			{
				return;
			}
			bool flag = this._inputField.text == string.Empty;
			this._inputField.text = string.Empty;
			if (flag)
			{
				this._ignoreNextActivation = true;
			}
		}

		protected InputField _inputField;

		public bool _ignoreNextActivation;
	}
}
