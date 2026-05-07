using System;
using UnityEngine;
using UnityEngine.Audio;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AudioSource : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AudioSource);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AudioSource audioSource = (AudioSource)value;
			writer.WriteProperty<float>("volume", audioSource.volume);
			writer.WriteProperty<float>("pitch", audioSource.pitch);
			writer.WriteProperty<float>("time", audioSource.time);
			writer.WriteProperty<int>("timeSamples", audioSource.timeSamples);
			writer.WriteProperty<AudioClip>("clip", audioSource.clip);
			writer.WriteProperty<AudioMixerGroup>("outputAudioMixerGroup", audioSource.outputAudioMixerGroup);
			writer.WriteProperty<bool>("loop", audioSource.loop);
			writer.WriteProperty<bool>("ignoreListenerVolume", audioSource.ignoreListenerVolume);
			writer.WriteProperty<bool>("playOnAwake", audioSource.playOnAwake);
			writer.WriteProperty<bool>("ignoreListenerPause", audioSource.ignoreListenerPause);
			writer.WriteProperty<AudioVelocityUpdateMode>("velocityUpdateMode", audioSource.velocityUpdateMode);
			writer.WriteProperty<float>("panStereo", audioSource.panStereo);
			writer.WriteProperty<float>("spatialBlend", audioSource.spatialBlend);
			writer.WriteProperty<bool>("spatialize", audioSource.spatialize);
			writer.WriteProperty<bool>("spatializePostEffects", audioSource.spatializePostEffects);
			writer.WriteProperty<float>("reverbZoneMix", audioSource.reverbZoneMix);
			writer.WriteProperty<bool>("bypassEffects", audioSource.bypassEffects);
			writer.WriteProperty<bool>("bypassListenerEffects", audioSource.bypassListenerEffects);
			writer.WriteProperty<bool>("bypassReverbZones", audioSource.bypassReverbZones);
			writer.WriteProperty<float>("dopplerLevel", audioSource.dopplerLevel);
			writer.WriteProperty<float>("spread", audioSource.spread);
			writer.WriteProperty<int>("priority", audioSource.priority);
			writer.WriteProperty<bool>("mute", audioSource.mute);
			writer.WriteProperty<float>("minDistance", audioSource.minDistance);
			writer.WriteProperty<float>("maxDistance", audioSource.maxDistance);
			writer.WriteProperty<AudioRolloffMode>("rolloffMode", audioSource.rolloffMode);
			writer.WriteProperty<bool>("enabled", audioSource.enabled);
			writer.WriteProperty<string>("tag", audioSource.tag);
			writer.WriteProperty<string>("name", audioSource.name);
			writer.WriteProperty<HideFlags>("hideFlags", audioSource.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			AudioSource audioSource = SaveGameType.CreateComponent<AudioSource>();
			this.ReadInto(audioSource, reader);
			return audioSource;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AudioSource audioSource = (AudioSource)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "volume":
					audioSource.volume = reader.ReadProperty<float>();
					break;
				case "pitch":
					audioSource.pitch = reader.ReadProperty<float>();
					break;
				case "time":
					audioSource.time = reader.ReadProperty<float>();
					break;
				case "timeSamples":
					audioSource.timeSamples = reader.ReadProperty<int>();
					break;
				case "clip":
					if (audioSource.clip == null)
					{
						audioSource.clip = reader.ReadProperty<AudioClip>();
					}
					else
					{
						reader.ReadIntoProperty<AudioClip>(audioSource.clip);
					}
					break;
				case "outputAudioMixerGroup":
					if (audioSource.outputAudioMixerGroup == null)
					{
						audioSource.outputAudioMixerGroup = reader.ReadProperty<AudioMixerGroup>();
					}
					else
					{
						reader.ReadIntoProperty<AudioMixerGroup>(audioSource.outputAudioMixerGroup);
					}
					break;
				case "loop":
					audioSource.loop = reader.ReadProperty<bool>();
					break;
				case "ignoreListenerVolume":
					audioSource.ignoreListenerVolume = reader.ReadProperty<bool>();
					break;
				case "playOnAwake":
					audioSource.playOnAwake = reader.ReadProperty<bool>();
					break;
				case "ignoreListenerPause":
					audioSource.ignoreListenerPause = reader.ReadProperty<bool>();
					break;
				case "velocityUpdateMode":
					audioSource.velocityUpdateMode = reader.ReadProperty<AudioVelocityUpdateMode>();
					break;
				case "panStereo":
					audioSource.panStereo = reader.ReadProperty<float>();
					break;
				case "spatialBlend":
					audioSource.spatialBlend = reader.ReadProperty<float>();
					break;
				case "spatialize":
					audioSource.spatialize = reader.ReadProperty<bool>();
					break;
				case "spatializePostEffects":
					audioSource.spatializePostEffects = reader.ReadProperty<bool>();
					break;
				case "reverbZoneMix":
					audioSource.reverbZoneMix = reader.ReadProperty<float>();
					break;
				case "bypassEffects":
					audioSource.bypassEffects = reader.ReadProperty<bool>();
					break;
				case "bypassListenerEffects":
					audioSource.bypassListenerEffects = reader.ReadProperty<bool>();
					break;
				case "bypassReverbZones":
					audioSource.bypassReverbZones = reader.ReadProperty<bool>();
					break;
				case "dopplerLevel":
					audioSource.dopplerLevel = reader.ReadProperty<float>();
					break;
				case "spread":
					audioSource.spread = reader.ReadProperty<float>();
					break;
				case "priority":
					audioSource.priority = reader.ReadProperty<int>();
					break;
				case "mute":
					audioSource.mute = reader.ReadProperty<bool>();
					break;
				case "minDistance":
					audioSource.minDistance = reader.ReadProperty<float>();
					break;
				case "maxDistance":
					audioSource.maxDistance = reader.ReadProperty<float>();
					break;
				case "rolloffMode":
					audioSource.rolloffMode = reader.ReadProperty<AudioRolloffMode>();
					break;
				case "enabled":
					audioSource.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					audioSource.tag = reader.ReadProperty<string>();
					break;
				case "name":
					audioSource.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					audioSource.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
