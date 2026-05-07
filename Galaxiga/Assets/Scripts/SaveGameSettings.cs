using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using BayatGames.SaveGamePro.IO;
using BayatGames.SaveGamePro.Serialization;
using BayatGames.SaveGamePro.Serialization.Formatters.Binary;
using UnityEngine;

namespace BayatGames.SaveGamePro
{
	[Serializable]
	public struct SaveGameSettings
	{
		public SaveGameSettings(string identifier)
		{
			this = new SaveGameSettings(identifier, SaveGameSettings.DefaultBasePath, SaveGameSettings.DefaultFormatter, SaveGameSettings.DefaultStorage);
		}

		public SaveGameSettings(string identifier, string basePath)
		{
			this = new SaveGameSettings(identifier, basePath, SaveGameSettings.DefaultFormatter, SaveGameSettings.DefaultStorage);
		}

		public SaveGameSettings(string identifier, ISaveGameFormatter formatter)
		{
			this = new SaveGameSettings(identifier, SaveGameSettings.DefaultBasePath, formatter, SaveGameSettings.DefaultStorage);
		}

		public SaveGameSettings(string identifier, SaveGameStorage storage)
		{
			this = new SaveGameSettings(identifier, SaveGameSettings.DefaultBasePath, SaveGameSettings.DefaultFormatter, storage);
		}

		public SaveGameSettings(string identifier, string basePath, ISaveGameFormatter formatter, SaveGameStorage storage)
		{
			this.m_Identifier = identifier.Replace('\\', '/');
			this.m_BasePath = basePath.Replace('\\', '/');
			this.m_Formatter = formatter;
			this.m_Storage = storage;
			this.m_Encoding = SaveGameSettings.DefaultEncoding;
			this.m_Encrypt = SaveGameSettings.DefaultEncrypt;
			this.m_EncryptionPassword = SaveGameSettings.DefaultEncryptionPassword;
			this.m_EncryptionIterations = SaveGameSettings.DefaultEncryptionIterations;
			this.m_EncryptionKeySize = SaveGameSettings.DefaultEncryptionKeySize;
			this.m_EncryptionHash = SaveGameSettings.DefaultEncryptionHash;
			this.m_EncryptionSalt = SaveGameSettings.DefaultEncryptionSalt;
			this.m_EncryptionVector = SaveGameSettings.DefaultEncryptionVector;
		}

		public static string DefaultBasePath
		{
			get
			{
				if (!SaveGame.IsFileIOSupported)
				{
					SaveGameSettings.m_DefaultBasePath = string.Empty;
				}
				else if (string.IsNullOrEmpty(SaveGameSettings.m_DefaultBasePath))
				{
					SaveGameSettings.m_DefaultBasePath = SaveGame.PersistentDataPath.Replace('\\', '/');
				}
				return SaveGameSettings.m_DefaultBasePath;
			}
			set
			{
				if (!File.Exists(value))
				{
					SaveGameSettings.m_DefaultBasePath = value;
				}
			}
		}

		public static bool DefaultEncrypt
		{
			get
			{
				return SaveGameSettings.m_DefaultEncrypt;
			}
			set
			{
				SaveGameSettings.m_DefaultEncrypt = value;
			}
		}

		public static string DefaultEncryptionPassword
		{
			get
			{
				if (string.IsNullOrEmpty(SaveGameSettings.m_DefaultEncryptionPassword))
				{
					SaveGameSettings.m_DefaultEncryptionPassword = "SGPEP";
				}
				return SaveGameSettings.m_DefaultEncryptionPassword;
			}
			set
			{
				SaveGameSettings.m_DefaultEncryptionPassword = value;
			}
		}

		public static int DefaultEncryptionIterations
		{
			get
			{
				return SaveGameSettings.m_DefaultEncryptionIterations;
			}
			set
			{
				SaveGameSettings.m_DefaultEncryptionIterations = value;
			}
		}

		public static int DefaultEncryptionKeySize
		{
			get
			{
				return SaveGameSettings.m_DefaultEncryptionKeySize;
			}
			set
			{
				SaveGameSettings.m_DefaultEncryptionKeySize = value;
			}
		}

		public static string DefaultEncryptionHash
		{
			get
			{
				if (string.IsNullOrEmpty(SaveGameSettings.m_DefaultEncryptionHash))
				{
					SaveGameSettings.m_DefaultEncryptionHash = "SHA1";
				}
				return SaveGameSettings.m_DefaultEncryptionHash;
			}
			set
			{
				SaveGameSettings.m_DefaultEncryptionHash = value;
			}
		}

		public static string DefaultEncryptionSalt
		{
			get
			{
				if (string.IsNullOrEmpty(SaveGameSettings.m_DefaultEncryptionSalt))
				{
					SaveGameSettings.m_DefaultEncryptionSalt = "aselrias38490a32";
				}
				return SaveGameSettings.m_DefaultEncryptionSalt;
			}
			set
			{
				SaveGameSettings.m_DefaultEncryptionSalt = value;
			}
		}

		public static string DefaultEncryptionVector
		{
			get
			{
				if (string.IsNullOrEmpty(SaveGameSettings.m_DefaultEncryptionVector))
				{
					SaveGameSettings.m_DefaultEncryptionVector = "8947az34awl34kjq";
				}
				return SaveGameSettings.m_DefaultEncryptionVector;
			}
			set
			{
				SaveGameSettings.m_DefaultEncryptionVector = value;
			}
		}

		public static ISaveGameFormatter DefaultFormatter
		{
			get
			{
				if (SaveGameSettings.m_DefaultFormatter == null)
				{
					SaveGameSettings.m_DefaultFormatter = new BinaryFormatter(SaveGame.DefaultSettings);
				}
				return SaveGameSettings.m_DefaultFormatter;
			}
			set
			{
				SaveGameSettings.m_DefaultFormatter = value;
			}
		}

		public static SaveGameStorage DefaultStorage
		{
			get
			{
				if (!SaveGameStorage.IsAppropriate(SaveGameSettings.m_DefaultStorage))
				{
					SaveGameSettings.m_DefaultStorage = SaveGameStorage.GetAppropriate();
				}
				return SaveGameSettings.m_DefaultStorage;
			}
			set
			{
				SaveGameSettings.m_DefaultStorage = value;
			}
		}

		public static Encoding DefaultEncoding
		{
			get
			{
				if (SaveGameSettings.m_DefaultEncoding == null)
				{
					SaveGameSettings.m_DefaultEncoding = Encoding.UTF8;
				}
				return SaveGameSettings.m_DefaultEncoding;
			}
			set
			{
				SaveGameSettings.m_DefaultEncoding = value;
			}
		}

		public string Identifier
		{
			get
			{
				return this.m_Identifier;
			}
			set
			{
				this.m_Identifier = value.Replace('\\', '/');
			}
		}

		public string BasePath
		{
			get
			{
				if (!SaveGame.IsFileIOSupported)
				{
					this.m_BasePath = string.Empty;
				}
				else if (string.IsNullOrEmpty(this.m_BasePath))
				{
					this.m_BasePath = SaveGameSettings.DefaultBasePath.Replace('\\', '/');
				}
				return this.m_BasePath;
			}
			set
			{
				if (!File.Exists(value))
				{
					this.m_BasePath = value.Replace('\\', '/');
				}
			}
		}

		public bool Encrypt
		{
			get
			{
				return this.m_Encrypt;
			}
			set
			{
				this.m_Encrypt = value;
			}
		}

		public string EncryptionPassword
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_EncryptionPassword))
				{
					this.m_EncryptionPassword = SaveGameSettings.DefaultEncryptionPassword;
				}
				return this.m_EncryptionPassword;
			}
			set
			{
				this.m_EncryptionPassword = value;
			}
		}

		public int EncryptionIterations
		{
			get
			{
				if (this.m_EncryptionIterations <= 0)
				{
					this.m_EncryptionIterations = SaveGameSettings.DefaultEncryptionIterations;
				}
				return this.m_EncryptionIterations;
			}
			set
			{
				this.m_EncryptionIterations = value;
			}
		}

		public int EncryptionKeySize
		{
			get
			{
				if (this.m_EncryptionKeySize <= 0)
				{
					this.m_EncryptionKeySize = SaveGameSettings.DefaultEncryptionKeySize;
				}
				return this.m_EncryptionKeySize;
			}
			set
			{
				this.m_EncryptionKeySize = value;
			}
		}

		public string EncryptionHash
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_EncryptionHash))
				{
					this.m_EncryptionHash = SaveGameSettings.DefaultEncryptionHash;
				}
				return this.m_EncryptionHash;
			}
			set
			{
				this.m_EncryptionHash = value;
			}
		}

		public string EncryptionSalt
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_EncryptionSalt))
				{
					this.m_EncryptionSalt = SaveGameSettings.DefaultEncryptionSalt;
				}
				return this.m_EncryptionSalt;
			}
			set
			{
				this.m_EncryptionSalt = value;
			}
		}

		public string EncryptionVector
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_EncryptionVector))
				{
					this.m_EncryptionVector = SaveGameSettings.DefaultEncryptionVector;
				}
				return this.m_EncryptionVector;
			}
			set
			{
				this.m_EncryptionVector = value;
			}
		}

		public ISaveGameFormatter Formatter
		{
			get
			{
				if (this.m_Formatter == null)
				{
					this.m_Formatter = SaveGameSettings.DefaultFormatter;
				}
				return this.m_Formatter;
			}
			set
			{
				this.m_Formatter = value;
			}
		}

		public SaveGameStorage Storage
		{
			get
			{
				if (!SaveGameStorage.IsAppropriate(this.m_Storage))
				{
					this.m_Storage = SaveGameSettings.DefaultStorage;
				}
				return this.m_Storage;
			}
			set
			{
				this.m_Storage = value;
			}
		}

		public Encoding Encoding
		{
			get
			{
				if (this.m_Encoding == null)
				{
					this.m_Encoding = SaveGameSettings.DefaultEncoding;
				}
				return this.m_Encoding;
			}
			set
			{
				this.m_Encoding = value;
			}
		}

		public ICryptoTransform Encryptor
		{
			get
			{
				AesManaged aesManaged = new AesManaged();
				byte[] bytes = Encoding.ASCII.GetBytes(this.EncryptionVector);
				byte[] bytes2 = Encoding.ASCII.GetBytes(this.EncryptionSalt);
				PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(this.EncryptionPassword, bytes2, this.EncryptionHash, this.EncryptionIterations);
				byte[] bytes3 = passwordDeriveBytes.GetBytes(this.EncryptionKeySize / 8);
				aesManaged.Mode = CipherMode.CBC;
				aesManaged.Padding = PaddingMode.PKCS7;
				return aesManaged.CreateEncryptor(bytes3, bytes);
			}
		}

		public ICryptoTransform Decryptor
		{
			get
			{
				AesManaged aesManaged = new AesManaged();
				byte[] bytes = Encoding.ASCII.GetBytes(this.EncryptionVector);
				byte[] bytes2 = Encoding.ASCII.GetBytes(this.EncryptionSalt);
				PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(this.EncryptionPassword, bytes2, this.EncryptionHash, this.EncryptionIterations);
				byte[] bytes3 = passwordDeriveBytes.GetBytes(this.EncryptionKeySize / 8);
				aesManaged.Mode = CipherMode.CBC;
				aesManaged.Padding = PaddingMode.PKCS7;
				return aesManaged.CreateDecryptor(bytes3, bytes);
			}
		}

		private static string m_DefaultBasePath;

		private static ISaveGameFormatter m_DefaultFormatter;

		private static SaveGameStorage m_DefaultStorage;

		private static Encoding m_DefaultEncoding;

		[SerializeField]
		private string m_Identifier;

		[SerializeField]
		private string m_BasePath;

		private ISaveGameFormatter m_Formatter;

		private SaveGameStorage m_Storage;

		private Encoding m_Encoding;

		private static bool m_DefaultEncrypt = true;

		private static string m_DefaultEncryptionPassword = "SGPEP";

		private static int m_DefaultEncryptionIterations = 2;

		private static int m_DefaultEncryptionKeySize = 256;

		private static string m_DefaultEncryptionHash = "SHA1";

		private static string m_DefaultEncryptionSalt = "aselrias38490a32";

		private static string m_DefaultEncryptionVector = "8947az34awl34kjq";

		[SerializeField]
		private bool m_Encrypt;

		[SerializeField]
		private string m_EncryptionPassword;

		[SerializeField]
		private int m_EncryptionIterations;

		[SerializeField]
		private int m_EncryptionKeySize;

		[SerializeField]
		private string m_EncryptionHash;

		[SerializeField]
		private string m_EncryptionSalt;

		[SerializeField]
		private string m_EncryptionVector;
	}
}
