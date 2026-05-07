using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Examples
{
	public class SaveCustomData : MonoBehaviour
	{
		public void Save()
		{
			SaveGame.Save<SaveCustomData.CustomData>(this.identifier, this.data);
			UnityEngine.Debug.Log("Data Saved");
		}

		public void Load()
		{
			this.data = SaveGame.Load<SaveCustomData.CustomData>(this.identifier);
			UnityEngine.Debug.Log("Data Loaded");
			UnityEngine.Debug.Log(this.data.playerName);
			UnityEngine.Debug.Log(this.data.score);
		}

		public string identifier = "customData.txt";

		public SaveCustomData.CustomData data;

		[Serializable]
		public class CustomData
		{
			public string playerName = "Hello World";

			public int score = 12;

			[NonSavable]
			public bool test;
		}
	}
}
