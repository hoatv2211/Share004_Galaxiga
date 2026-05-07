using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hellmade.Sound
{
	public class EazySoundManager : MonoBehaviour
	{
		static EazySoundManager()
		{
			EazySoundManager.Instance.Init();
		}

		public static GameObject Gameobject
		{
			get
			{
				return EazySoundManager.Instance.gameObject;
			}
		}

		public static bool IgnoreDuplicateMusic { get; set; }

		public static bool IgnoreDuplicateSounds { get; set; }

		public static bool IgnoreDuplicateUISounds { get; set; }

		public static float GlobalVolume { get; set; }

		public static float GlobalMusicVolume { get; set; }

		public static float GlobalSoundsVolume { get; set; }

		public static float GlobalUISoundsVolume { get; set; }

		private static EazySoundManager Instance
		{
			get
			{
				if (EazySoundManager.instance == null)
				{
					EazySoundManager.instance = (EazySoundManager)UnityEngine.Object.FindObjectOfType(typeof(EazySoundManager));
					if (EazySoundManager.instance == null)
					{
						EazySoundManager.instance = new GameObject("EazySoundManager").AddComponent<EazySoundManager>();
					}
				}
				return EazySoundManager.instance;
			}
		}

		private void Init()
		{
			if (!EazySoundManager.initialized)
			{
				EazySoundManager.musicAudio = new Dictionary<int, Audio>();
				EazySoundManager.soundsAudio = new Dictionary<int, Audio>();
				EazySoundManager.UISoundsAudio = new Dictionary<int, Audio>();
				EazySoundManager.audioPool = new Dictionary<int, Audio>();
				EazySoundManager.GlobalVolume = 1f;
				EazySoundManager.GlobalMusicVolume = 1f;
				EazySoundManager.GlobalSoundsVolume = 1f;
				EazySoundManager.GlobalUISoundsVolume = 1f;
				EazySoundManager.IgnoreDuplicateMusic = false;
				EazySoundManager.IgnoreDuplicateSounds = false;
				EazySoundManager.IgnoreDuplicateUISounds = false;
				EazySoundManager.initialized = true;
				UnityEngine.Object.DontDestroyOnLoad(this);
			}
		}

		private void OnEnable()
		{
			SceneManager.sceneLoaded += this.OnSceneLoaded;
		}

		private void OnDisable()
		{
			SceneManager.sceneLoaded -= this.OnSceneLoaded;
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			EazySoundManager.RemoveNonPersistAudio(EazySoundManager.musicAudio);
			EazySoundManager.RemoveNonPersistAudio(EazySoundManager.soundsAudio);
			EazySoundManager.RemoveNonPersistAudio(EazySoundManager.UISoundsAudio);
		}

		private void Update()
		{
			EazySoundManager.UpdateAllAudio(EazySoundManager.musicAudio);
			EazySoundManager.UpdateAllAudio(EazySoundManager.soundsAudio);
			EazySoundManager.UpdateAllAudio(EazySoundManager.UISoundsAudio);
		}

		private static Dictionary<int, Audio> GetAudioTypeDictionary(Audio.AudioType audioType)
		{
			Dictionary<int, Audio> result = new Dictionary<int, Audio>();
			if (audioType != Audio.AudioType.Music)
			{
				if (audioType != Audio.AudioType.Sound)
				{
					if (audioType == Audio.AudioType.UISound)
					{
						result = EazySoundManager.UISoundsAudio;
					}
				}
				else
				{
					result = EazySoundManager.soundsAudio;
				}
			}
			else
			{
				result = EazySoundManager.musicAudio;
			}
			return result;
		}

		private static bool GetAudioTypeIgnoreDuplicateSetting(Audio.AudioType audioType)
		{
			switch (audioType)
			{
			case Audio.AudioType.Music:
				return EazySoundManager.IgnoreDuplicateMusic;
			case Audio.AudioType.Sound:
				return EazySoundManager.IgnoreDuplicateSounds;
			case Audio.AudioType.UISound:
				return EazySoundManager.IgnoreDuplicateUISounds;
			default:
				return false;
			}
		}

		private static void UpdateAllAudio(Dictionary<int, Audio> audioDict)
		{
			List<int> list = new List<int>(audioDict.Keys);
			foreach (int key in list)
			{
				Audio audio = audioDict[key];
				audio.Update();
				if (!audio.IsPlaying && !audio.Paused)
				{
					UnityEngine.Object.Destroy(audio.AudioSource);
					EazySoundManager.audioPool.Add(key, audio);
					audio.Pooled = true;
					audioDict.Remove(key);
				}
			}
		}

		private static void RemoveNonPersistAudio(Dictionary<int, Audio> audioDict)
		{
			List<int> list = new List<int>(audioDict.Keys);
			foreach (int key in list)
			{
				Audio audio = audioDict[key];
				if (!audio.Persist && audio.Activated)
				{
					UnityEngine.Object.Destroy(audio.AudioSource);
					audioDict.Remove(key);
				}
			}
			list = new List<int>(EazySoundManager.audioPool.Keys);
			foreach (int key2 in list)
			{
				Audio audio2 = EazySoundManager.audioPool[key2];
				if (!audio2.Persist && audio2.Activated)
				{
					EazySoundManager.audioPool.Remove(key2);
				}
			}
		}

		public static bool RestoreAudioFromPool(Audio.AudioType audioType, int audioID)
		{
			if (EazySoundManager.audioPool.ContainsKey(audioID))
			{
				Dictionary<int, Audio> audioTypeDictionary = EazySoundManager.GetAudioTypeDictionary(audioType);
				audioTypeDictionary.Add(audioID, EazySoundManager.audioPool[audioID]);
				EazySoundManager.audioPool.Remove(audioID);
				return true;
			}
			return false;
		}

		public static Audio GetAudio(int audioID)
		{
			Audio audio = EazySoundManager.GetMusicAudio(audioID);
			if (audio != null)
			{
				return audio;
			}
			audio = EazySoundManager.GetSoundAudio(audioID);
			if (audio != null)
			{
				return audio;
			}
			audio = EazySoundManager.GetUISoundAudio(audioID);
			if (audio != null)
			{
				return audio;
			}
			return null;
		}

		public static Audio GetAudio(AudioClip audioClip)
		{
			Audio audio = EazySoundManager.GetMusicAudio(audioClip);
			if (audio != null)
			{
				return audio;
			}
			audio = EazySoundManager.GetSoundAudio(audioClip);
			if (audio != null)
			{
				return audio;
			}
			audio = EazySoundManager.GetUISoundAudio(audioClip);
			if (audio != null)
			{
				return audio;
			}
			return null;
		}

		public static Audio GetMusicAudio(int audioID)
		{
			return EazySoundManager.GetAudio(Audio.AudioType.Music, true, audioID);
		}

		public static Audio GetMusicAudio(AudioClip audioClip)
		{
			return EazySoundManager.GetAudio(Audio.AudioType.Music, true, audioClip);
		}

		public static Audio GetSoundAudio(int audioID)
		{
			return EazySoundManager.GetAudio(Audio.AudioType.Sound, true, audioID);
		}

		public static Audio GetSoundAudio(AudioClip audioClip)
		{
			return EazySoundManager.GetAudio(Audio.AudioType.Sound, true, audioClip);
		}

		public static Audio GetUISoundAudio(int audioID)
		{
			return EazySoundManager.GetAudio(Audio.AudioType.UISound, true, audioID);
		}

		public static Audio GetUISoundAudio(AudioClip audioClip)
		{
			return EazySoundManager.GetAudio(Audio.AudioType.UISound, true, audioClip);
		}

		private static Audio GetAudio(Audio.AudioType audioType, bool usePool, int audioID)
		{
			Dictionary<int, Audio> audioTypeDictionary = EazySoundManager.GetAudioTypeDictionary(audioType);
			if (audioTypeDictionary.ContainsKey(audioID))
			{
				return audioTypeDictionary[audioID];
			}
			if (usePool && EazySoundManager.audioPool.ContainsKey(audioID) && EazySoundManager.audioPool[audioID].Type == audioType)
			{
				return EazySoundManager.audioPool[audioID];
			}
			return null;
		}

		private static Audio GetAudio(Audio.AudioType audioType, bool usePool, AudioClip audioClip)
		{
			Dictionary<int, Audio> audioTypeDictionary = EazySoundManager.GetAudioTypeDictionary(audioType);
			List<int> list = new List<int>(audioTypeDictionary.Keys);
			List<int> second = new List<int>(EazySoundManager.audioPool.Keys);
			List<int> list2 = (!usePool) ? list : list.Concat(second).ToList<int>();
			foreach (int key in list2)
			{
				Audio audio = audioTypeDictionary[key];
				if (audio.Clip == audioClip && audio.Type == audioType)
				{
					return audio;
				}
			}
			return null;
		}

		public static int PrepareMusic(AudioClip clip)
		{
			return EazySoundManager.PrepareAudio(Audio.AudioType.Music, clip, 1f, false, false, 1f, 1f, -1f, null);
		}

		public static int PrepareMusic(AudioClip clip, float volume)
		{
			return EazySoundManager.PrepareAudio(Audio.AudioType.Music, clip, volume, false, false, 1f, 1f, -1f, null);
		}

		public static int PrepareMusic(AudioClip clip, float volume, bool loop, bool persist)
		{
			return EazySoundManager.PrepareAudio(Audio.AudioType.Music, clip, volume, loop, persist, 1f, 1f, -1f, null);
		}

		public static int PrepareMusic(AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds)
		{
			return EazySoundManager.PrepareAudio(Audio.AudioType.Music, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, -1f, null);
		}

		public static int PrepareMusic(AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds, Transform sourceTransform)
		{
			return EazySoundManager.PrepareAudio(Audio.AudioType.Music, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, currentMusicfadeOutSeconds, sourceTransform);
		}

		public static int PrepareSound(AudioClip clip)
		{
			return EazySoundManager.PrepareAudio(Audio.AudioType.Sound, clip, 1f, false, false, 0f, 0f, -1f, null);
		}

		public static int PrepareSound(AudioClip clip, float volume)
		{
			return EazySoundManager.PrepareAudio(Audio.AudioType.Sound, clip, volume, false, false, 0f, 0f, -1f, null);
		}

		public static int PrepareSound(AudioClip clip, bool loop)
		{
			return EazySoundManager.PrepareAudio(Audio.AudioType.Sound, clip, 1f, loop, false, 0f, 0f, -1f, null);
		}

		public static int PrepareSound(AudioClip clip, float volume, bool loop, Transform sourceTransform)
		{
			return EazySoundManager.PrepareAudio(Audio.AudioType.Sound, clip, volume, loop, false, 0f, 0f, -1f, sourceTransform);
		}

		public static int PrepareUISound(AudioClip clip)
		{
			return EazySoundManager.PrepareAudio(Audio.AudioType.UISound, clip, 1f, false, false, 0f, 0f, -1f, null);
		}

		public static int PrepareUISound(AudioClip clip, float volume)
		{
			return EazySoundManager.PrepareAudio(Audio.AudioType.UISound, clip, volume, false, false, 0f, 0f, -1f, null);
		}

		private static int PrepareAudio(Audio.AudioType audioType, AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds, Transform sourceTransform)
		{
			if (clip == null)
			{
				UnityEngine.Debug.LogError("[Eazy Sound Manager] Audio clip is null", clip);
			}
			Dictionary<int, Audio> audioTypeDictionary = EazySoundManager.GetAudioTypeDictionary(audioType);
			bool audioTypeIgnoreDuplicateSetting = EazySoundManager.GetAudioTypeIgnoreDuplicateSetting(audioType);
			if (audioTypeIgnoreDuplicateSetting)
			{
				Audio audio = EazySoundManager.GetAudio(audioType, true, clip);
				if (audio != null)
				{
					return audio.AudioID;
				}
			}
			Audio audio2 = new Audio(audioType, clip, loop, persist, volume, fadeInSeconds, fadeOutSeconds, sourceTransform);
			audioTypeDictionary.Add(audio2.AudioID, audio2);
			return audio2.AudioID;
		}

		public static int PlayMusic(AudioClip clip)
		{
			return EazySoundManager.PlayAudio(Audio.AudioType.Music, clip, 1f, false, false, 1f, 1f, -1f, null);
		}

		public static int PlayMusic(AudioClip clip, float volume)
		{
			return EazySoundManager.PlayAudio(Audio.AudioType.Music, clip, volume, false, false, 1f, 1f, -1f, null);
		}

		public static int PlayMusic(AudioClip clip, float volume, bool loop, bool persist)
		{
			return EazySoundManager.PlayAudio(Audio.AudioType.Music, clip, volume, loop, persist, 1f, 1f, -1f, null);
		}

		public static int PlayMusic(AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds)
		{
			return EazySoundManager.PlayAudio(Audio.AudioType.Music, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, -1f, null);
		}

		public static int PlayMusic(AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds, Transform sourceTransform)
		{
			return EazySoundManager.PlayAudio(Audio.AudioType.Music, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, currentMusicfadeOutSeconds, sourceTransform);
		}

		public static int PlaySound(AudioClip clip)
		{
			return EazySoundManager.PlayAudio(Audio.AudioType.Sound, clip, 1f, false, false, 0f, 0f, -1f, null);
		}

		public static int PlaySound(AudioClip clip, float volume)
		{
			return EazySoundManager.PlayAudio(Audio.AudioType.Sound, clip, volume, false, false, 0f, 0f, -1f, null);
		}

		public static int PlaySound(AudioClip clip, bool loop)
		{
			return EazySoundManager.PlayAudio(Audio.AudioType.Sound, clip, 1f, loop, false, 0f, 0f, -1f, null);
		}

		public static int PlaySound(AudioClip clip, float volume, bool loop, Transform sourceTransform)
		{
			return EazySoundManager.PlayAudio(Audio.AudioType.Sound, clip, volume, loop, false, 0f, 0f, -1f, sourceTransform);
		}

		public static int PlayUISound(AudioClip clip)
		{
			return EazySoundManager.PlayAudio(Audio.AudioType.UISound, clip, 1f, false, false, 0f, 0f, -1f, null);
		}

		public static int PlayUISound(AudioClip clip, float volume)
		{
			return EazySoundManager.PlayAudio(Audio.AudioType.UISound, clip, volume, false, false, 0f, 0f, -1f, null);
		}

		private static int PlayAudio(Audio.AudioType audioType, AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds, Transform sourceTransform)
		{
			int num = EazySoundManager.PrepareAudio(audioType, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, currentMusicfadeOutSeconds, sourceTransform);
			if (audioType == Audio.AudioType.Music)
			{
				EazySoundManager.StopAllMusic(currentMusicfadeOutSeconds);
			}
			EazySoundManager.GetAudio(audioType, false, num).Play();
			return num;
		}

		public static void StopAll()
		{
			EazySoundManager.StopAll(-1f);
		}

		public static void StopAll(float musicFadeOutSeconds)
		{
			EazySoundManager.StopAllMusic(musicFadeOutSeconds);
			EazySoundManager.StopAllSounds();
			EazySoundManager.StopAllUISounds();
		}

		public static void StopAllMusic()
		{
			EazySoundManager.StopAllAudio(Audio.AudioType.Music, -1f);
		}

		public static void StopAllMusic(float fadeOutSeconds)
		{
			EazySoundManager.StopAllAudio(Audio.AudioType.Music, fadeOutSeconds);
		}

		public static void StopAllSounds()
		{
			EazySoundManager.StopAllAudio(Audio.AudioType.Sound, -1f);
		}

		public static void StopAllUISounds()
		{
			EazySoundManager.StopAllAudio(Audio.AudioType.UISound, -1f);
		}

		private static void StopAllAudio(Audio.AudioType audioType, float fadeOutSeconds)
		{
			Dictionary<int, Audio> audioTypeDictionary = EazySoundManager.GetAudioTypeDictionary(audioType);
			List<int> list = new List<int>(audioTypeDictionary.Keys);
			foreach (int key in list)
			{
				Audio audio = audioTypeDictionary[key];
				if (fadeOutSeconds > 0f)
				{
					audio.FadeOutSeconds = fadeOutSeconds;
				}
				audio.Stop();
			}
		}

		public static void PauseAll()
		{
			EazySoundManager.PauseAllMusic();
			EazySoundManager.PauseAllSounds();
			EazySoundManager.PauseAllUISounds();
		}

		public static void PauseAllMusic()
		{
			EazySoundManager.PauseAllAudio(Audio.AudioType.Music);
		}

		public static void PauseAllSounds()
		{
			EazySoundManager.PauseAllAudio(Audio.AudioType.Sound);
		}

		public static void PauseAllUISounds()
		{
			EazySoundManager.PauseAllAudio(Audio.AudioType.UISound);
		}

		private static void PauseAllAudio(Audio.AudioType audioType)
		{
			Dictionary<int, Audio> audioTypeDictionary = EazySoundManager.GetAudioTypeDictionary(audioType);
			List<int> list = new List<int>(audioTypeDictionary.Keys);
			foreach (int key in list)
			{
				Audio audio = audioTypeDictionary[key];
				audio.Pause();
			}
		}

		public static void ResumeAll()
		{
			EazySoundManager.ResumeAllMusic();
			EazySoundManager.ResumeAllSounds();
			EazySoundManager.ResumeAllUISounds();
		}

		public static void ResumeAllMusic()
		{
			EazySoundManager.ResumeAllAudio(Audio.AudioType.Music);
		}

		public static void ResumeAllSounds()
		{
			EazySoundManager.ResumeAllAudio(Audio.AudioType.Sound);
		}

		public static void ResumeAllUISounds()
		{
			EazySoundManager.ResumeAllAudio(Audio.AudioType.UISound);
		}

		private static void ResumeAllAudio(Audio.AudioType audioType)
		{
			Dictionary<int, Audio> audioTypeDictionary = EazySoundManager.GetAudioTypeDictionary(audioType);
			List<int> list = new List<int>(audioTypeDictionary.Keys);
			foreach (int key in list)
			{
				Audio audio = audioTypeDictionary[key];
				audio.Resume();
			}
		}

		private static EazySoundManager instance;

		private static Dictionary<int, Audio> musicAudio;

		private static Dictionary<int, Audio> soundsAudio;

		private static Dictionary<int, Audio> UISoundsAudio;

		private static Dictionary<int, Audio> audioPool;

		private static bool initialized;
	}
}
