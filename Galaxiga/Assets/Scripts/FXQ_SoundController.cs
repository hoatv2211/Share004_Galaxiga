using System;
using UnityEngine;

public class FXQ_SoundController : MonoBehaviour
{
	public static FXQ_SoundController Instance
	{
		get
		{
			if (FXQ_SoundController.instance == null)
			{
				FXQ_SoundController.instance = UnityEngine.Object.FindObjectOfType<FXQ_SoundController>();
				UnityEngine.Object.DontDestroyOnLoad(FXQ_SoundController.instance.gameObject);
			}
			return FXQ_SoundController.instance;
		}
	}

	private void Awake()
	{
		if (FXQ_SoundController.instance == null)
		{
			FXQ_SoundController.instance = this;
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
		else if (this != FXQ_SoundController.instance)
		{
			this.InitAudioListener();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
		this.InitAudioListener();
	}

	private void Update()
	{
	}

	private void InitAudioListener()
	{
		AudioListener[] array = UnityEngine.Object.FindObjectsOfType<AudioListener>();
		foreach (AudioListener audioListener in array)
		{
			if (audioListener.gameObject.GetComponent<FXQ_SoundController>() == null)
			{
				UnityEngine.Object.Destroy(audioListener);
			}
		}
		AudioListener x = base.gameObject.GetComponent<AudioListener>();
		if (x == null)
		{
			x = base.gameObject.AddComponent<AudioListener>();
		}
	}

	private void PlayMusic(AudioClip pAudioClip)
	{
		if (pAudioClip == null)
		{
			return;
		}
		AudioListener audioListener = UnityEngine.Object.FindObjectOfType<AudioListener>();
		if (audioListener != null)
		{
			bool flag = false;
			AudioSource[] components = audioListener.gameObject.GetComponents<AudioSource>();
			if (components.Length > 0)
			{
				for (int i = 0; i < components.Length; i++)
				{
					if (!components[i].isPlaying)
					{
						components[i].loop = true;
						components[i].clip = pAudioClip;
						components[i].ignoreListenerVolume = true;
						components[i].playOnAwake = false;
						components[i].Play();
						break;
					}
				}
			}
			if (!flag && components.Length < 16)
			{
				AudioSource audioSource = audioListener.gameObject.AddComponent<AudioSource>();
				audioSource.rolloffMode = AudioRolloffMode.Linear;
				audioSource.loop = true;
				audioSource.clip = pAudioClip;
				audioSource.ignoreListenerVolume = true;
				audioSource.playOnAwake = false;
				audioSource.Play();
			}
		}
	}

	private void PlaySoundOneShot(AudioClip pAudioClip)
	{
		if (pAudioClip == null)
		{
			return;
		}
		if (Time.timeSinceLevelLoad < 1.5f)
		{
			return;
		}
		AudioListener audioListener = UnityEngine.Object.FindObjectOfType<AudioListener>();
		if (audioListener != null)
		{
			bool flag = false;
			AudioSource[] components = audioListener.gameObject.GetComponents<AudioSource>();
			if (components.Length > 0)
			{
				for (int i = 0; i < components.Length; i++)
				{
					if (!components[i].isPlaying)
					{
						components[i].PlayOneShot(pAudioClip);
						break;
					}
				}
			}
			if (!flag && components.Length < 16)
			{
				AudioSource audioSource = audioListener.gameObject.AddComponent<AudioSource>();
				audioSource.rolloffMode = AudioRolloffMode.Linear;
				audioSource.playOnAwake = false;
				audioSource.PlayOneShot(pAudioClip);
			}
		}
	}

	public void SetSoundVolume(float volume)
	{
		this.m_SoundVolume = volume;
		AudioListener.volume = volume;
	}

	public void Play_SoundBack()
	{
		this.PlaySoundOneShot(this.m_ButtonBack);
	}

	public void Play_SoundClick()
	{
		this.PlaySoundOneShot(this.m_ButtonClick);
	}

	public void Play_SoundPress()
	{
		this.PlaySoundOneShot(this.m_ButtonPress);
	}

	private static FXQ_SoundController instance;

	public int m_MaxAudioSource = 3;

	public AudioClip m_ButtonBack;

	public AudioClip m_ButtonClick;

	public AudioClip m_ButtonPress;

	public float m_SoundVolume = 1f;
}
