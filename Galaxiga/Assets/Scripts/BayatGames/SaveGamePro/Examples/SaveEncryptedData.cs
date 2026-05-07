using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Examples
{
	public class SaveEncryptedData : MonoBehaviour
	{
		private void Awake()
		{
			this.saveSettings = default(SaveGameSettings);
			this.saveSettings.Encrypt = true;
		}

		public void Save()
		{
			this.saveSettings.EncryptionPassword = this.password.text;
			SaveGame.Save<string>(this.identifier, this.dataField.text, this.saveSettings);
			UnityEngine.Debug.Log("Encrypted Data has been saved successfully");
		}

		public void Load()
		{
			this.saveSettings.EncryptionPassword = this.password.text;
			this.dataField.text = SaveGame.Load<string>(this.identifier, "Default Data", this.saveSettings);
			UnityEngine.Debug.Log("Encrypted Data has been loaded successfully");
			UnityEngine.Debug.Log(this.dataField.text);
		}

		public string identifier = "encrypted.dat";

		public InputField password;

		public InputField dataField;

		protected SaveGameSettings saveSettings;
	}
}
