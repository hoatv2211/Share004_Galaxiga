using System;
using System.Threading.Tasks;
using UnityEngine;

namespace BayatGames.SaveGamePro.Examples
{
	public class AudioClipSaveAsync : MonoBehaviour
	{
		public void Save()
		{
			UnityEngine.Debug.Log("Saving ...");
			AudioClipSaveAsync.ClipData value = default(AudioClipSaveAsync.ClipData);
			value.name = this.audioClip.name;
			value.samples = new float[this.audioClip.samples];
			this.audioClip.GetData(value.samples, 0);
			value.channels = this.audioClip.channels;
			value.frequency = this.audioClip.frequency;
			SaveGame.SaveAsync<AudioClipSaveAsync.ClipData>(this.identifier, value).ContinueWith(delegate(Task task)
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
			Task<AudioClipSaveAsync.ClipData> task = SaveGame.LoadAsync<AudioClipSaveAsync.ClipData>(this.identifier);
			task.ContinueWith(delegate(Task<AudioClipSaveAsync.ClipData> result)
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
				}
			});
			task.Wait();
			AudioClipSaveAsync.ClipData result2 = task.Result;
			this.audioClip = AudioClip.Create(result2.name, result2.samples.Length, result2.channels, result2.frequency, false);
			this.audioClip.SetData(result2.samples, 0);
		}

		public string identifier = "audioClipSaveSync.txt";

		public AudioClip audioClip;

		public struct ClipData
		{
			public string name;

			public float[] samples;

			public int channels;

			public int frequency;
		}
	}
}
