using System;
using System.IO;
using BayatGames.SaveGamePro.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Examples
{
	public class ListSavedFiles : MonoBehaviour
	{
		private void Start()
		{
			this.UpdateList();
		}

		public void Save()
		{
			SaveGame.Save<string>(this.identifierInputField.text, "Hello World");
			this.UpdateList();
		}

		public void UpdateList()
		{
			this.listContainer.DestroyChilds();
			FileInfo[] files = SaveGame.GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				ListItem listItem = UnityEngine.Object.Instantiate<ListItem>(this.listItemPrefab, this.listContainer);
				listItem.file = files[i];
			}
		}

		public Transform listContainer;

		public ListItem listItemPrefab;

		public InputField identifierInputField;
	}
}
