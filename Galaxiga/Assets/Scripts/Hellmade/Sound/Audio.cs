using System;
using UnityEngine;

namespace Hellmade.Sound
{
	public class Audio
	{
		public Audio(Audio.AudioType audioType, AudioClip clip, bool loop, bool persist, float volume, float fadeInValue, float fadeOutValue, Transform sourceTransform)
		{
			this.AudioID = Audio.audioCounter;
			Audio.audioCounter++;
			this.Type = audioType;
			this.Clip = clip;
			this.SourceTransform = sourceTransform;
			this.Loop = loop;
			this.Persist = persist;
			this.targetVolume = volume;
			this.initTargetVolume = volume;
			this.tempFadeSeconds = -1f;
			this.FadeInSeconds = fadeInValue;
			this.FadeOutSeconds = fadeOutValue;
			this.Volume = 0f;
			this.Pooled = false;
			this.Mute = false;
			this.Priority = 128;
			this.Pitch = 1f;
			this.StereoPan = 0f;
			if (sourceTransform != null && sourceTransform != EazySoundManager.Gameobject.transform)
			{
				this.SpatialBlend = 1f;
			}
			this.ReverbZoneMix = 1f;
			this.DopplerLevel = 1f;
			this.Spread = 0f;
			this.RolloffMode = AudioRolloffMode.Logarithmic;
			this.Min3DDistance = 1f;
			this.Max3DDistance = 500f;
			this.IsPlaying = false;
			this.Paused = false;
			this.Activated = false;
		}

		public int AudioID { get; private set; }

		public Audio.AudioType Type { get; private set; }

		public bool IsPlaying { get; private set; }

		public bool Paused { get; private set; }

		public bool Stopping { get; private set; }

		public bool Activated { get; private set; }

		public bool Pooled { get; set; }

		public float Volume { get; private set; }

		public AudioSource AudioSource { get; private set; }

		public Transform SourceTransform
		{
			get
			{
				return this.sourceTransform;
			}
			set
			{
				if (value == null)
				{
					this.sourceTransform = EazySoundManager.Gameobject.transform;
				}
				else
				{
					this.sourceTransform = value;
				}
			}
		}

		public AudioClip Clip
		{
			get
			{
				return this.clip;
			}
			set
			{
				this.clip = value;
				if (this.AudioSource != null)
				{
					this.AudioSource.clip = this.clip;
				}
			}
		}

		public bool Loop
		{
			get
			{
				return this.loop;
			}
			set
			{
				this.loop = value;
				if (this.AudioSource != null)
				{
					this.AudioSource.loop = this.loop;
				}
			}
		}

		public bool Mute
		{
			get
			{
				return this.mute;
			}
			set
			{
				this.mute = value;
				if (this.AudioSource != null)
				{
					this.AudioSource.mute = this.mute;
				}
			}
		}

		public int Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = Mathf.Clamp(value, 0, 256);
				if (this.AudioSource != null)
				{
					this.AudioSource.priority = this.priority;
				}
			}
		}

		public float Pitch
		{
			get
			{
				return this.pitch;
			}
			set
			{
				this.pitch = Mathf.Clamp(value, -3f, 3f);
				if (this.AudioSource != null)
				{
					this.AudioSource.pitch = this.pitch;
				}
			}
		}

		public float StereoPan
		{
			get
			{
				return this.stereoPan;
			}
			set
			{
				this.stereoPan = Mathf.Clamp(value, -1f, 1f);
				if (this.AudioSource != null)
				{
					this.AudioSource.panStereo = this.stereoPan;
				}
			}
		}

		public float SpatialBlend
		{
			get
			{
				return this.spatialBlend;
			}
			set
			{
				this.spatialBlend = Mathf.Clamp01(value);
				if (this.AudioSource != null)
				{
					this.AudioSource.spatialBlend = this.spatialBlend;
				}
			}
		}

		public float ReverbZoneMix
		{
			get
			{
				return this.reverbZoneMix;
			}
			set
			{
				this.reverbZoneMix = Mathf.Clamp(value, 0f, 1.1f);
				if (this.AudioSource != null)
				{
					this.AudioSource.reverbZoneMix = this.reverbZoneMix;
				}
			}
		}

		public float DopplerLevel
		{
			get
			{
				return this.dopplerLevel;
			}
			set
			{
				this.dopplerLevel = Mathf.Clamp(value, 0f, 5f);
				if (this.AudioSource != null)
				{
					this.AudioSource.dopplerLevel = this.dopplerLevel;
				}
			}
		}

		public float Spread
		{
			get
			{
				return this.spread;
			}
			set
			{
				this.spread = Mathf.Clamp(value, 0f, 360f);
				if (this.AudioSource != null)
				{
					this.AudioSource.spread = this.spread;
				}
			}
		}

		public AudioRolloffMode RolloffMode
		{
			get
			{
				return this.rolloffMode;
			}
			set
			{
				this.rolloffMode = value;
				if (this.AudioSource != null)
				{
					this.AudioSource.rolloffMode = this.rolloffMode;
				}
			}
		}

		public float Max3DDistance
		{
			get
			{
				return this.max3DDistance;
			}
			set
			{
				this.max3DDistance = Mathf.Max(value, 0.01f);
				if (this.AudioSource != null)
				{
					this.AudioSource.maxDistance = this.max3DDistance;
				}
			}
		}

		public float Min3DDistance
		{
			get
			{
				return this.min3DDistance;
			}
			set
			{
				this.min3DDistance = Mathf.Max(value, 0f);
				if (this.AudioSource != null)
				{
					this.AudioSource.minDistance = this.min3DDistance;
				}
			}
		}

		public bool Persist { get; set; }

		public float FadeInSeconds { get; set; }

		public float FadeOutSeconds { get; set; }

		private void CreateAudiosource()
		{
			if (this.sourceTransform)
			{
				this.sourceTransform = EazySoundManager.Gameobject.transform;
			}
			this.AudioSource = this.sourceTransform.gameObject.AddComponent<AudioSource>();
			this.AudioSource.clip = this.Clip;
			this.AudioSource.loop = this.Loop;
			this.AudioSource.mute = this.Mute;
			this.AudioSource.volume = this.Volume;
			this.AudioSource.priority = this.Priority;
			this.AudioSource.pitch = this.Pitch;
			this.AudioSource.panStereo = this.StereoPan;
			this.AudioSource.spatialBlend = this.SpatialBlend;
			this.AudioSource.reverbZoneMix = this.ReverbZoneMix;
			this.AudioSource.dopplerLevel = this.DopplerLevel;
			this.AudioSource.spread = this.Spread;
			this.AudioSource.rolloffMode = this.RolloffMode;
			this.AudioSource.maxDistance = this.Max3DDistance;
			this.AudioSource.minDistance = this.Min3DDistance;
		}

		public void Play()
		{
			this.Play(this.initTargetVolume);
		}

		public void Play(float volume)
		{
			if (this.Pooled)
			{
				if (!EazySoundManager.RestoreAudioFromPool(this.Type, this.AudioID))
				{
					return;
				}
				this.Pooled = true;
			}
			if (this.AudioSource == null)
			{
				this.CreateAudiosource();
			}
			this.AudioSource.Play();
			this.IsPlaying = true;
			this.fadeInterpolater = 0f;
			this.onFadeStartVolume = this.Volume;
			this.targetVolume = volume;
		}

		public void Stop()
		{
			this.fadeInterpolater = 0f;
			this.onFadeStartVolume = this.Volume;
			this.targetVolume = 0f;
			this.Stopping = true;
		}

		public void Pause()
		{
			this.AudioSource.Pause();
			this.Paused = true;
		}

		public void UnPause()
		{
			this.AudioSource.UnPause();
			this.Paused = false;
		}

		public void Resume()
		{
			this.AudioSource.UnPause();
			this.Paused = false;
		}

		public void SetVolume(float volume)
		{
			if (volume > this.targetVolume)
			{
				this.SetVolume(volume, this.FadeOutSeconds);
			}
			else
			{
				this.SetVolume(volume, this.FadeInSeconds);
			}
		}

		public void SetVolume(float volume, float fadeSeconds)
		{
			this.SetVolume(volume, fadeSeconds, this.Volume);
		}

		public void SetVolume(float volume, float fadeSeconds, float startVolume)
		{
			this.targetVolume = Mathf.Clamp01(volume);
			this.fadeInterpolater = 0f;
			this.onFadeStartVolume = startVolume;
			this.tempFadeSeconds = fadeSeconds;
		}

		public void Set3DDistances(float min, float max)
		{
			this.Min3DDistance = min;
			this.Max3DDistance = max;
		}

		public void Update()
		{
			if (this.AudioSource == null)
			{
				return;
			}
			this.Activated = true;
			if (this.Volume != this.targetVolume)
			{
				this.fadeInterpolater += Time.unscaledDeltaTime;
				float num;
				if (this.Volume > this.targetVolume)
				{
					num = ((this.tempFadeSeconds == -1f) ? this.FadeOutSeconds : this.tempFadeSeconds);
				}
				else
				{
					num = ((this.tempFadeSeconds == -1f) ? this.FadeInSeconds : this.tempFadeSeconds);
				}
				this.Volume = Mathf.Lerp(this.onFadeStartVolume, this.targetVolume, this.fadeInterpolater / num);
			}
			else if (this.tempFadeSeconds != -1f)
			{
				this.tempFadeSeconds = -1f;
			}
			Audio.AudioType type = this.Type;
			if (type != Audio.AudioType.Music)
			{
				if (type != Audio.AudioType.Sound)
				{
					if (type == Audio.AudioType.UISound)
					{
						this.AudioSource.volume = this.Volume * EazySoundManager.GlobalUISoundsVolume * EazySoundManager.GlobalVolume;
					}
				}
				else
				{
					this.AudioSource.volume = this.Volume * EazySoundManager.GlobalSoundsVolume * EazySoundManager.GlobalVolume;
				}
			}
			else
			{
				this.AudioSource.volume = this.Volume * EazySoundManager.GlobalMusicVolume * EazySoundManager.GlobalVolume;
			}
			if (this.Volume == 0f && this.Stopping)
			{
				this.AudioSource.Stop();
				this.Stopping = false;
				this.IsPlaying = false;
				this.Paused = false;
			}
			if (this.AudioSource.isPlaying != this.IsPlaying && Application.isFocused)
			{
				this.IsPlaying = this.AudioSource.isPlaying;
			}
		}

		private static int audioCounter;

		private AudioClip clip;

		private bool loop;

		private bool mute;

		private int priority;

		private float pitch;

		private float stereoPan;

		private float spatialBlend;

		private float reverbZoneMix;

		private float dopplerLevel;

		private float spread;

		private AudioRolloffMode rolloffMode;

		private float max3DDistance;

		private float min3DDistance;

		private float targetVolume;

		private float initTargetVolume;

		private float tempFadeSeconds;

		private float fadeInterpolater;

		private float onFadeStartVolume;

		private Transform sourceTransform;

		public enum AudioType
		{
			Music,
			Sound,
			UISound
		}
	}
}
