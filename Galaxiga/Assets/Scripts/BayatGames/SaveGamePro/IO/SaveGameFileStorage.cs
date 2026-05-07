using System;
using System.IO;

namespace BayatGames.SaveGamePro.IO
{
	public class SaveGameFileStorage : SaveGameStorage
	{
		public override bool HasFileIO
		{
			get
			{
				return true;
			}
		}

		public override Stream GetReadStream(SaveGameSettings settings)
		{
			string absolutePath = SaveGameFileStorage.GetAbsolutePath(settings.Identifier, settings.BasePath);
			if (!File.Exists(absolutePath))
			{
				return null;
			}
			return new FileStream(absolutePath, FileMode.Open);
		}

		public override Stream GetWriteStream(SaveGameSettings settings)
		{
			string absolutePath = SaveGameFileStorage.GetAbsolutePath(settings.Identifier, settings.BasePath);
			string directoryName = Path.GetDirectoryName(absolutePath);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			return new FileStream(absolutePath, FileMode.Create);
		}

		public override void Clear(SaveGameSettings settings)
		{
			string text = SaveGameFileStorage.GetAbsolutePath(settings.Identifier, settings.BasePath);
			if (File.Exists(text))
			{
				this.Delete(settings);
				return;
			}
			if (string.IsNullOrEmpty(text) || !Directory.Exists(text))
			{
				text = settings.BasePath;
			}
			if (!Directory.Exists(text))
			{
				return;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			FileInfo[] files = directoryInfo.GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Delete();
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			for (int j = 0; j < directories.Length; j++)
			{
				if (directories[j].Name != "Unity")
				{
					directories[j].Delete(true);
				}
			}
		}

		public override void Copy(string fromIdentifier, string toIdentifier, SaveGameSettings settings)
		{
			string absolutePath = SaveGameFileStorage.GetAbsolutePath(fromIdentifier, settings.BasePath);
			string absolutePath2 = SaveGameFileStorage.GetAbsolutePath(toIdentifier, settings.BasePath);
			if (File.Exists(absolutePath) && !Directory.Exists(absolutePath2))
			{
				File.Copy(absolutePath, absolutePath2);
			}
			else if (File.Exists(absolutePath) && Directory.Exists(absolutePath2))
			{
				File.Copy(absolutePath, Path.Combine(absolutePath2, Path.GetFileName(absolutePath)));
			}
			else if (Directory.Exists(absolutePath) && !File.Exists(absolutePath2))
			{
				Directory.CreateDirectory(absolutePath2);
				DirectoryInfo directoryInfo = new DirectoryInfo(absolutePath);
				FileInfo[] files = directoryInfo.GetFiles();
				for (int i = 0; i < files.Length; i++)
				{
					string destFileName = Path.Combine(absolutePath2, files[i].Name);
					files[i].CopyTo(destFileName, true);
				}
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				for (int j = 0; j < directories.Length; j++)
				{
					string toIdentifier2 = Path.Combine(absolutePath2, directories[j].Name);
					this.Copy(directories[j].FullName, toIdentifier2, settings);
				}
			}
		}

		public override void Delete(SaveGameSettings settings)
		{
			string absolutePath = SaveGameFileStorage.GetAbsolutePath(settings.Identifier, settings.BasePath);
			if (File.Exists(absolutePath))
			{
				File.Delete(absolutePath);
			}
			else if (Directory.Exists(absolutePath))
			{
				Directory.Delete(absolutePath, true);
			}
		}

		public override bool Exists(SaveGameSettings settings)
		{
			string absolutePath = SaveGameFileStorage.GetAbsolutePath(settings.Identifier, settings.BasePath);
			return File.Exists(absolutePath) || Directory.Exists(absolutePath);
		}

		public override DirectoryInfo[] GetDirectories(SaveGameSettings settings)
		{
			string absolutePath = SaveGameFileStorage.GetAbsolutePath(settings.Identifier, settings.BasePath);
			DirectoryInfo[] result = new DirectoryInfo[0];
			if (Directory.Exists(absolutePath))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(absolutePath);
				result = directoryInfo.GetDirectories();
			}
			return result;
		}

		public override FileInfo[] GetFiles(SaveGameSettings settings)
		{
			string absolutePath = SaveGameFileStorage.GetAbsolutePath(settings.Identifier, settings.BasePath);
			FileInfo[] result = new FileInfo[0];
			if (Directory.Exists(absolutePath))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(absolutePath);
				result = directoryInfo.GetFiles();
			}
			return result;
		}

		public override void Move(string fromIdentifier, string toIdentifier, SaveGameSettings settings)
		{
			string absolutePath = SaveGameFileStorage.GetAbsolutePath(fromIdentifier, settings.BasePath);
			string absolutePath2 = SaveGameFileStorage.GetAbsolutePath(toIdentifier, settings.BasePath);
			if (File.Exists(absolutePath) && !Directory.Exists(absolutePath2))
			{
				File.Move(absolutePath, absolutePath2);
			}
			else if (File.Exists(absolutePath) && Directory.Exists(absolutePath2))
			{
				File.Move(absolutePath, Path.Combine(absolutePath2, Path.GetFileName(absolutePath)));
			}
			else if (Directory.Exists(absolutePath) && !File.Exists(absolutePath2))
			{
				Directory.Move(absolutePath, absolutePath2);
			}
		}

		public static string GetAbsolutePath(string path, string basePath)
		{
			if (string.IsNullOrEmpty(path))
			{
				return basePath;
			}
			if (string.IsNullOrEmpty(basePath))
			{
				return path;
			}
			if (Path.IsPathRooted(path))
			{
				return path;
			}
			return Path.Combine(basePath, path);
		}
	}
}
