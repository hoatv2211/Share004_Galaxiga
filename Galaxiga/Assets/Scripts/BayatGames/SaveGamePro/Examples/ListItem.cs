using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Examples
{
	public class ListItem : MonoBehaviour
	{
		private void Start()
		{
			this.nameText.text = this.file.Name;
			this.dateText.text = this.file.LastWriteTime.ToString();
		}

		public void Delete()
		{
			SaveGame.Delete(this.file.Name);
			UnityEngine.Object.Destroy(base.gameObject);
		}

		public Text nameText;

		public Text dateText;

		public FileInfo file;
	}
}
