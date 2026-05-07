using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BayatGames.SaveGamePro.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace BayatGames.SaveGamePro.Networking
{
	public class SaveGameWeb : SaveGameCloud
	{
		public SaveGameWeb(string url, string secretKey, string username, string password) : this(url, secretKey, username, password, SaveGame.DefaultSettings)
		{
		}

		public SaveGameWeb(string url, string secretKey, string username, string password, SaveGameSettings settings) : base(settings)
		{
			this.m_Url = url;
			this.m_SecretKey = secretKey;
			this.m_Username = username;
			this.m_Password = password;
		}

		public virtual string SecretKey
		{
			get
			{
				return this.m_SecretKey;
			}
			set
			{
				this.m_SecretKey = value;
			}
		}

		public virtual string Username
		{
			get
			{
				return this.m_Username;
			}
			set
			{
				this.m_Username = value;
			}
		}

		public virtual string Password
		{
			get
			{
				return this.m_Password;
			}
			set
			{
				this.m_Password = value;
			}
		}

		public virtual bool CreateAccount
		{
			get
			{
				return this.m_CreateAccount;
			}
			set
			{
				this.m_CreateAccount = value;
			}
		}

		public virtual string Url
		{
			get
			{
				return this.m_Url;
			}
			set
			{
				this.m_Url = value;
			}
		}

		public virtual UnityWebRequest Request
		{
			get
			{
				return this.m_Request;
			}
		}

		public virtual IEnumerator GetFileUrl(string identifier)
		{
			return this.GetFileUrl(identifier, this.Settings);
		}

		public virtual IEnumerator GetFileUrl(string identifier, SaveGameSettings settings)
		{
			Dictionary<string, string> form = this.CreateRequestForm("getfileurl", identifier, settings);
			form.Add("file-name", identifier);
			this.m_Request = UnityWebRequest.Post(this.m_Url, form);
			yield return this.m_Request.SendWebRequest();
			yield break;
		}

		public virtual IEnumerator UploadFile(string identifier, string uploadIdentifier)
		{
			return this.UploadFile(identifier, uploadIdentifier, this.Settings);
		}

		public virtual IEnumerator UploadFile(string identifier, string uploadIdentifier, SaveGameSettings settings)
		{
			settings.Identifier = identifier;
			byte[] data = File.ReadAllBytes(SaveGameFileStorage.GetAbsolutePath(settings.Identifier, settings.BasePath));
			WWWForm form = new WWWForm();
			form.AddField("action", "uploadfile");
			form.AddField("secret-key", this.m_SecretKey);
			form.AddField("username", this.m_Username);
			form.AddField("password", this.m_Password);
			form.AddField("file-name", uploadIdentifier);
			if (this.m_CreateAccount)
			{
				form.AddField("create-account", string.Empty);
			}
			form.AddBinaryData("file", data, uploadIdentifier);
			this.m_Request = UnityWebRequest.Post(this.m_Url, form);
			yield return this.m_Request.SendWebRequest();
			yield break;
		}

		public virtual IEnumerator DownloadFile(string identifier, string downloadIdentifier)
		{
			return this.DownloadFile(identifier, downloadIdentifier, this.Settings);
		}

		public virtual IEnumerator DownloadFile(string identifier, string downloadIdentifier, SaveGameSettings settings)
		{
			settings.Identifier = identifier;
			Dictionary<string, string> form = this.CreateRequestForm("downloadfile", settings);
			form.Add("file-name", identifier);
			this.m_Request = UnityWebRequest.Post(this.m_Url, form);
			yield return this.m_Request.SendWebRequest();
			if (this.m_Request.isHttpError || this.m_Request.isNetworkError)
			{
				UnityEngine.Debug.LogError("Download Failed");
				UnityEngine.Debug.LogError(this.m_Request.error);
				UnityEngine.Debug.LogError(this.m_Request.downloadHandler.text);
			}
			else
			{
				File.WriteAllBytes(SaveGameFileStorage.GetAbsolutePath(downloadIdentifier, settings.BasePath), this.m_Request.downloadHandler.data);
			}
			yield break;
		}

		public override IEnumerator Save(string identifier, object value, SaveGameSettings settings)
		{
			byte[] data;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				settings.Formatter.Serialize(memoryStream, value, settings);
				data = memoryStream.ToArray();
			}
			Dictionary<string, string> form = this.CreateRequestForm("save", identifier, data, settings);
			this.m_Request = UnityWebRequest.Post(this.m_Url, form);
			yield return this.m_Request.SendWebRequest();
			yield break;
		}

		public override IEnumerator Download(string identifier, SaveGameSettings settings)
		{
			Dictionary<string, string> form = this.CreateRequestForm("load", identifier, settings);
			this.m_Request = UnityWebRequest.Post(this.m_Url, form);
			yield return this.m_Request.SendWebRequest();
			yield break;
		}

		public override object Load(Type type, object defaultValue, SaveGameSettings settings)
		{
			object obj = null;
			using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(this.m_Request.downloadHandler.text)))
			{
				obj = settings.Formatter.Deserialize(memoryStream, type, settings);
			}
			if (obj == null)
			{
				obj = defaultValue;
			}
			return obj;
		}

		public override void LoadInto(object value, SaveGameSettings settings)
		{
			using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(this.m_Request.downloadHandler.text)))
			{
				settings.Formatter.DeserializeInto(memoryStream, value, settings);
			}
		}

		public override IEnumerator Clear(SaveGameSettings settings)
		{
			Dictionary<string, string> form = this.CreateRequestForm("clear", settings);
			this.m_Request = UnityWebRequest.Post(this.m_Url, form);
			yield return this.m_Request.SendWebRequest();
			yield break;
		}

		public override IEnumerator Delete(string identifier, SaveGameSettings settings)
		{
			Dictionary<string, string> form = this.CreateRequestForm("delete", settings);
			this.m_Request = UnityWebRequest.Post(this.m_Url, form);
			yield return this.m_Request.SendWebRequest();
			yield break;
		}

		public virtual Dictionary<string, string> CreateRequestForm(string action, SaveGameSettings settings)
		{
			return this.CreateRequestForm(action, null, null, settings);
		}

		public virtual Dictionary<string, string> CreateRequestForm(string action, string identifier, SaveGameSettings settings)
		{
			return this.CreateRequestForm(action, identifier, null, settings);
		}

		public virtual Dictionary<string, string> CreateRequestForm(string action, byte[] data, SaveGameSettings settings)
		{
			return this.CreateRequestForm(action, null, data, settings);
		}

		public virtual Dictionary<string, string> CreateRequestForm(string action, string identifier, byte[] data, SaveGameSettings settings)
		{
			if (string.IsNullOrEmpty(action))
			{
				throw new ArgumentNullException("action");
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("secret-key", this.m_SecretKey);
			dictionary.Add("username", this.m_Username);
			dictionary.Add("password", this.m_Password);
			if (this.m_CreateAccount)
			{
				dictionary.Add("create-account", string.Empty);
			}
			dictionary.Add("action", action);
			if (!string.IsNullOrEmpty(identifier))
			{
				dictionary.Add("data-key", identifier);
			}
			if (data != null)
			{
				dictionary.Add("data-value", Convert.ToBase64String(data));
			}
			return dictionary;
		}

		protected string m_SecretKey;

		protected string m_Username;

		protected string m_Password;

		protected string m_Url;

		protected bool m_CreateAccount = true;

		protected UnityWebRequest m_Request;
	}
}
