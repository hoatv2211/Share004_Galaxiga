using System;
using System.IO;
using UnityEngine;

namespace BayatGames.SaveGamePro.IO
{
	public class SaveGamePlayerPrefsStorage : SaveGameStorage
	{
		public override bool HasFileIO
		{
			get
			{
				return false;
			}
		}

		public override Stream GetReadStream(SaveGameSettings settings)
		{
			return new MemoryStream(Convert.FromBase64String(PlayerPrefs.GetString(settings.Identifier)));
		}

		public override Stream GetWriteStream(SaveGameSettings settings)
		{
			this.m_TempStream = new MemoryStream();
			return this.m_TempStream;
		}

		public override void OnSaved(SaveGameSettings settings)
		{
			PlayerPrefs.SetString(settings.Identifier, Convert.ToBase64String(this.m_TempStream.ToArray()));
			PlayerPrefs.Save();
			this.m_TempStream.Dispose();
		}

		public override void Clear(SaveGameSettings settings)
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.Save();
		}

		public override void Copy(string fromIdentifier, string toIdentifier, SaveGameSettings settings)
		{
			PlayerPrefs.SetString(toIdentifier, PlayerPrefs.GetString(fromIdentifier));
			PlayerPrefs.Save();
		}

		public override void Delete(SaveGameSettings settings)
		{
			PlayerPrefs.DeleteKey(settings.Identifier);
			PlayerPrefs.Save();
		}

		public override bool Exists(SaveGameSettings settings)
		{
			return PlayerPrefs.HasKey(settings.Identifier);
		}

		public override FileInfo[] GetFiles(SaveGameSettings settings)
		{
			return new FileInfo[0];
		}

		public override DirectoryInfo[] GetDirectories(SaveGameSettings settings)
		{
			return new DirectoryInfo[0];
		}

		public override void Move(string fromIdentifier, string toIdentifier, SaveGameSettings settings)
		{
			this.Copy(fromIdentifier, toIdentifier, settings);
			settings.Identifier = fromIdentifier;
			this.Delete(settings);
		}

		protected MemoryStream m_TempStream;
	}
}
