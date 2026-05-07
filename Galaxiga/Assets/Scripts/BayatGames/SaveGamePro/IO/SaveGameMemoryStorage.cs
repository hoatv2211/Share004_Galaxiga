using System;
using System.IO;

namespace BayatGames.SaveGamePro.IO
{
	public class SaveGameMemoryStorage : SaveGameStorage
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
			return this.m_TempStream;
		}

		public override Stream GetWriteStream(SaveGameSettings settings)
		{
			this.m_TempStream = new MemoryStream();
			return this.m_TempStream;
		}

		public virtual MemoryStream GetWriteStream(byte[] buffer, SaveGameSettings settings)
		{
			this.m_TempStream = new MemoryStream(buffer);
			return this.m_TempStream;
		}

		public override void OnSaved(SaveGameSettings settings)
		{
			this.m_TempStream.Dispose();
		}

		public override void Clear(SaveGameSettings settings)
		{
			this.m_TempStream = new MemoryStream();
		}

		public override void Copy(string fromIdentifier, string toIdentifier, SaveGameSettings settings)
		{
			throw new InvalidOperationException("Save Game Memory Storage does not support Copy operation");
		}

		public override void Delete(SaveGameSettings settings)
		{
			throw new InvalidOperationException("Save Game Memory Storage does not support Delete operation");
		}

		public override bool Exists(SaveGameSettings settings)
		{
			throw new InvalidOperationException("Save Game Memory Storage does not support Exists operation");
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
			throw new InvalidOperationException("Save Game Memory Storage does not support Move operation");
		}

		protected MemoryStream m_TempStream;
	}
}
