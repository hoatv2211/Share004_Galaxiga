using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Examples
{
	public class SaveSimpleData : MonoBehaviour
	{
		private void Awake()
		{
			SaveGame.OnSaved += this.OnSaved;
			SaveGame.OnLoaded += this.OnLoaded;
		}

		public void Save()
		{
			SaveGame.Save<string>(this.textIdentifier, this.textInputField.text);
			SaveGame.Save<int>(this.numberIdentifier, (!string.IsNullOrEmpty(this.numberInputField.text)) ? int.Parse(this.numberInputField.text) : 0);
		}

		public void Load()
		{
			this.textInputField.text = SaveGame.Load<string>(this.textIdentifier, "Hello World");
			this.numberInputField.text = SaveGame.Load<int>(this.numberIdentifier, 0).ToString();
		}

		private void OnSaved(string identifier, object value, SaveGameSettings settings)
		{
			UnityEngine.Debug.LogFormat("Data Saved Successfully by Identifier: {0}", new object[]
			{
				identifier
			});
		}

		private void OnLoaded(string identifier, object result, Type type, object defaultValue, SaveGameSettings settings)
		{
			UnityEngine.Debug.LogFormat("Data Loaded Successfully by Identifier: {0}", new object[]
			{
				identifier
			});
		}

		public string textIdentifier = "saveSimpleData-text.txt";

		public string numberIdentifier = "saveSimpleData-number.txt";

		public InputField textInputField;

		public InputField numberInputField;
	}
}
