using System;
using System.Collections;
using BayatGames.SaveGamePro.Networking;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Examples
{
	public class WebCloudSave : MonoBehaviour
	{
		public void Save()
		{
			base.StartCoroutine("DoSave");
		}

		private IEnumerator DoSave()
		{
			UnityEngine.Debug.Log("Saving...");
			this.saveButton.interactable = false;
			SaveGameWeb web = new SaveGameWeb(this.url, this.secretKey, this.usernameInputField.text, this.passwordInputField.text);
			yield return base.StartCoroutine(web.Save<string>(this.identifier, this.dataInputField.text));
			this.saveButton.interactable = true;
			if (web.Request.isHttpError || web.Request.isNetworkError)
			{
				UnityEngine.Debug.LogError("Save Failed");
				UnityEngine.Debug.LogError(web.Request.error);
				UnityEngine.Debug.LogError(web.Request.downloadHandler.text);
			}
			else
			{
				UnityEngine.Debug.Log("Save Successful");
				UnityEngine.Debug.Log("Response: " + web.Request.downloadHandler.text);
			}
			yield break;
		}

		public void Load()
		{
			base.StartCoroutine("DoLoad");
		}

		private IEnumerator DoLoad()
		{
			UnityEngine.Debug.Log("Loading...");
			this.loadButton.interactable = false;
			SaveGameWeb web = new SaveGameWeb(this.url, this.secretKey, this.usernameInputField.text, this.passwordInputField.text);
			yield return base.StartCoroutine(web.Download(this.identifier));
			this.loadButton.interactable = true;
			if (web.Request.isHttpError || web.Request.isNetworkError)
			{
				UnityEngine.Debug.LogError("Load Failed");
				UnityEngine.Debug.LogError(web.Request.error);
				UnityEngine.Debug.LogError(web.Request.downloadHandler.text);
			}
			else
			{
				UnityEngine.Debug.Log("Load Successful");
				UnityEngine.Debug.Log("Response: " + web.Request.downloadHandler.text);
				this.dataInputField.text = web.Load<string>(this.defaultValue);
			}
			yield break;
		}

		public void Clear()
		{
			base.StartCoroutine("DoClear");
		}

		private IEnumerator DoClear()
		{
			UnityEngine.Debug.Log("Clearing...");
			this.clearButton.interactable = false;
			SaveGameWeb web = new SaveGameWeb(this.url, this.secretKey, this.usernameInputField.text, this.passwordInputField.text);
			yield return base.StartCoroutine(web.Clear());
			this.clearButton.interactable = true;
			if (web.Request.isHttpError || web.Request.isNetworkError)
			{
				UnityEngine.Debug.LogError("Clear Failed");
				UnityEngine.Debug.LogError(web.Request.error);
			}
			else
			{
				UnityEngine.Debug.Log("Clear Successful");
				UnityEngine.Debug.Log("Response: " + web.Request.downloadHandler.text);
			}
			yield break;
		}

		public string identifier = "helloWorld";

		public string secretKey;

		public string url;

		public string defaultValue = "Hello, World!";

		public Button saveButton;

		public Button loadButton;

		public Button clearButton;

		public InputField usernameInputField;

		public InputField passwordInputField;

		public InputField dataInputField;
	}
}
