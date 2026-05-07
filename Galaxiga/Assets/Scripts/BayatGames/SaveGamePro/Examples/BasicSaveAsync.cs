using System;
using System.Threading.Tasks;
using UnityEngine;

namespace BayatGames.SaveGamePro.Examples
{
	public class BasicSaveAsync : MonoBehaviour
	{
		public string Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		public void Save()
		{
			UnityEngine.Debug.Log("Saving ...");
			SaveGame.SaveAsync<string>(this.identifier, this.data).ContinueWith(delegate(Task task)
			{
				if (task.IsCanceled)
				{
					UnityEngine.Debug.LogError("Save Task - Cancelled");
				}
				else if (task.IsFaulted)
				{
					UnityEngine.Debug.LogError("Save Tsak - Faulted");
					UnityEngine.Debug.LogException(task.Exception);
				}
				else
				{
					UnityEngine.Debug.Log("Saved");
				}
			});
		}

		public void Load()
		{
			UnityEngine.Debug.Log("Loading ...");
			SaveGame.LoadAsync<string>(this.identifier).ContinueWith(delegate(Task<string> task)
			{
				if (task.IsCanceled)
				{
					UnityEngine.Debug.LogError("Load Task - Cancelled");
				}
				else if (task.IsFaulted)
				{
					UnityEngine.Debug.LogError("Load Tsak - Faulted");
					UnityEngine.Debug.LogException(task.Exception);
				}
				else
				{
					UnityEngine.Debug.Log("Loaded");
					UnityEngine.Debug.Log(task.Result);
					this.data = task.Result;
				}
			});
		}

		public string identifier = "saveAsync.txt";

		public string data = "This is the data.";
	}
}
