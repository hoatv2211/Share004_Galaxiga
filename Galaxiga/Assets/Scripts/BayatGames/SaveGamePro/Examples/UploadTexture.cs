using System;
using System.Collections;
using BayatGames.SaveGamePro.Networking;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Examples
{
	public class UploadTexture : MonoBehaviour
	{
		public void Upload()
		{
			base.StartCoroutine("DoUpload");
		}

		private IEnumerator DoUpload()
		{
			UnityEngine.Debug.Log("Uploading...");
			this.uploadButton.interactable = false;
			SaveGameWeb web = new SaveGameWeb(this.url, this.secretKey, this.usernameInputField.text, this.passwordInputField.text);
			yield return base.StartCoroutine(web.UploadFile(this.identifier, this.identifier));
			this.uploadButton.interactable = true;
			if (web.Request.isHttpError || web.Request.isNetworkError)
			{
				UnityEngine.Debug.LogError("Upload Failed");
				UnityEngine.Debug.LogError(web.Request.error);
				UnityEngine.Debug.LogError(web.Request.downloadHandler.text);
			}
			else
			{
				UnityEngine.Debug.Log("Upload Successful");
				UnityEngine.Debug.Log("Response: " + web.Request.downloadHandler.text);
			}
			yield break;
		}

		public void Download()
		{
			base.StartCoroutine("DoDownload");
		}

		private IEnumerator DoDownload()
		{
			UnityEngine.Debug.Log("Downloading...");
			this.downloadButton.interactable = false;
			SaveGameWeb web = new SaveGameWeb(this.url, this.secretKey, this.usernameInputField.text, this.passwordInputField.text);
			yield return base.StartCoroutine(web.DownloadFile(this.identifier, this.identifier));
			this.downloadButton.interactable = true;
			if (web.Request.isHttpError || web.Request.isNetworkError)
			{
				UnityEngine.Debug.LogError("Download Failed");
				UnityEngine.Debug.LogError(web.Request.error);
				UnityEngine.Debug.LogError(web.Request.downloadHandler.text);
			}
			else
			{
				UnityEngine.Debug.Log("Download Successful");
				UnityEngine.Debug.Log("Response: " + web.Request.downloadHandler.text);
			}
			yield break;
		}

		public void GetFileUrl()
		{
			base.StartCoroutine("DoGetFileUrl");
		}

		private IEnumerator DoGetFileUrl()
		{
			UnityEngine.Debug.Log("Getting File Url...");
			this.downloadButton.interactable = false;
			SaveGameWeb web = new SaveGameWeb(this.url, this.secretKey, this.usernameInputField.text, this.passwordInputField.text);
			yield return base.StartCoroutine(web.GetFileUrl(this.identifier));
			this.downloadButton.interactable = true;
			if (web.Request.isHttpError || web.Request.isNetworkError)
			{
				UnityEngine.Debug.LogError("Getting File Url Failed");
				UnityEngine.Debug.LogError(web.Request.error);
				UnityEngine.Debug.LogError(web.Request.downloadHandler.text);
			}
			else
			{
				UnityEngine.Debug.Log("Getting File Url Successful");
				UnityEngine.Debug.Log("File Url: " + web.Request.downloadHandler.text);
			}
			yield break;
		}

		public void Save()
		{
			SaveGame.SaveImage(this.identifier, this.image.sprite.texture);
		}

		public void Load()
		{
			Texture2D texture2D = SaveGame.LoadImage(this.identifier);
			this.image.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 100f);
		}

		public void Clear()
		{
			this.image.sprite = null;
		}

		public string identifier = "texture.png";

		public string secretKey;

		public string url;

		public Button uploadButton;

		public Button downloadButton;

		public Button saveButton;

		public Button loadButton;

		public Button clearButton;

		public InputField usernameInputField;

		public InputField passwordInputField;

		public Image image;
	}
}
