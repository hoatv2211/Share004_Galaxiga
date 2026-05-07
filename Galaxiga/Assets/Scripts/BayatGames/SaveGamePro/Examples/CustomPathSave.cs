using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Examples
{
	public class CustomPathSave : MonoBehaviour
	{
		private void Awake()
		{
			SaveGame.OnSaved += this.SaveGame_OnSaved;
			SaveGame.OnLoaded += this.SaveGame_OnLoaded;
			SaveGame.OnDeleted += this.SaveGame_OnDeleted;
		}

		private void SaveGame_OnDeleted(string identifier, SaveGameSettings settings)
		{
			UnityEngine.Debug.LogFormat("The identifier '{0}' deleted successfully", new object[]
			{
				identifier
			});
		}

		private void SaveGame_OnLoaded(string identifier, object result, Type type, object defaultValue, SaveGameSettings settings)
		{
			UnityEngine.Debug.LogFormat("The identifier '{0}' loaded successfully", new object[]
			{
				identifier
			});
		}

		private void SaveGame_OnSaved(string identifier, object value, SaveGameSettings settings)
		{
			UnityEngine.Debug.LogFormat("The identifier '{0}' saved successfully", new object[]
			{
				identifier
			});
		}

		private void Start()
		{
			if (string.IsNullOrEmpty(this.identifierInputField.text) || !Path.IsPathRooted(this.identifierInputField.text))
			{
				this.identifierInputField.text = Path.Combine(Application.persistentDataPath, "helloWorld.txt");
			}
		}

		public void Save()
		{
			SaveGame.Save<string>(this.identifierInputField.text, this.dataInputField.text);
		}

		public void Load()
		{
			this.dataInputField.text = SaveGame.Load<string>(this.identifierInputField.text, "Hello World");
		}

		public void Delete()
		{
			SaveGame.Delete(this.identifierInputField.text);
		}

		public InputField identifierInputField;

		public InputField dataInputField;
	}
}
