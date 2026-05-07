using System;
using UnityEngine;
using UnityEngine.Video;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_VideoPlayer : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(VideoPlayer);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			VideoPlayer videoPlayer = (VideoPlayer)value;
			writer.WriteProperty<VideoSource>("source", videoPlayer.source);
			writer.WriteProperty<string>("url", videoPlayer.url);
			writer.WriteProperty<VideoClip>("clip", videoPlayer.clip);
			writer.WriteProperty<VideoRenderMode>("renderMode", videoPlayer.renderMode);
			writer.WriteProperty<Camera>("targetCamera", videoPlayer.targetCamera);
			writer.WriteProperty<RenderTexture>("targetTexture", videoPlayer.targetTexture);
			writer.WriteProperty<Renderer>("targetMaterialRenderer", videoPlayer.targetMaterialRenderer);
			writer.WriteProperty<string>("targetMaterialProperty", videoPlayer.targetMaterialProperty);
			writer.WriteProperty<VideoAspectRatio>("aspectRatio", videoPlayer.aspectRatio);
			writer.WriteProperty<float>("targetCameraAlpha", videoPlayer.targetCameraAlpha);
			writer.WriteProperty<bool>("waitForFirstFrame", videoPlayer.waitForFirstFrame);
			writer.WriteProperty<bool>("playOnAwake", videoPlayer.playOnAwake);
			writer.WriteProperty<double>("time", videoPlayer.time);
			writer.WriteProperty<long>("frame", videoPlayer.frame);
			writer.WriteProperty<float>("playbackSpeed", videoPlayer.playbackSpeed);
			writer.WriteProperty<bool>("isLooping", videoPlayer.isLooping);
			writer.WriteProperty<VideoTimeUpdateMode>("timeSource", videoPlayer.timeUpdateMode);
			writer.WriteProperty<VideoTimeReference>("timeReference", videoPlayer.timeReference);
			writer.WriteProperty<double>("externalReferenceTime", videoPlayer.externalReferenceTime);
			writer.WriteProperty<bool>("skipOnDrop", videoPlayer.skipOnDrop);
			writer.WriteProperty<ushort>("controlledAudioTrackCount", videoPlayer.controlledAudioTrackCount);
			writer.WriteProperty<VideoAudioOutputMode>("audioOutputMode", videoPlayer.audioOutputMode);
			writer.WriteProperty<bool>("sendFrameReadyEvents", videoPlayer.sendFrameReadyEvents);
			writer.WriteProperty<bool>("enabled", videoPlayer.enabled);
			writer.WriteProperty<string>("tag", videoPlayer.tag);
			writer.WriteProperty<string>("name", videoPlayer.name);
			writer.WriteProperty<HideFlags>("hideFlags", videoPlayer.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			VideoPlayer videoPlayer = SaveGameType.CreateComponent<VideoPlayer>();
			this.ReadInto(videoPlayer, reader);
			return videoPlayer;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			VideoPlayer videoPlayer = (VideoPlayer)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "source":
					videoPlayer.source = reader.ReadProperty<VideoSource>();
					break;
				case "url":
					videoPlayer.url = reader.ReadProperty<string>();
					break;
				case "clip":
					if (videoPlayer.clip == null)
					{
						videoPlayer.clip = reader.ReadProperty<VideoClip>();
					}
					else
					{
						reader.ReadIntoProperty<VideoClip>(videoPlayer.clip);
					}
					break;
				case "renderMode":
					videoPlayer.renderMode = reader.ReadProperty<VideoRenderMode>();
					break;
				case "targetCamera":
					if (videoPlayer.targetCamera == null)
					{
						videoPlayer.targetCamera = reader.ReadProperty<Camera>();
					}
					else
					{
						reader.ReadIntoProperty<Camera>(videoPlayer.targetCamera);
					}
					break;
				case "targetTexture":
					if (videoPlayer.targetTexture == null)
					{
						videoPlayer.targetTexture = reader.ReadProperty<RenderTexture>();
					}
					else
					{
						reader.ReadIntoProperty<RenderTexture>(videoPlayer.targetTexture);
					}
					break;
				case "targetMaterialRenderer":
					if (videoPlayer.targetMaterialRenderer == null)
					{
						videoPlayer.targetMaterialRenderer = reader.ReadProperty<Renderer>();
					}
					else
					{
						reader.ReadIntoProperty<Renderer>(videoPlayer.targetMaterialRenderer);
					}
					break;
				case "targetMaterialProperty":
					videoPlayer.targetMaterialProperty = reader.ReadProperty<string>();
					break;
				case "aspectRatio":
					videoPlayer.aspectRatio = reader.ReadProperty<VideoAspectRatio>();
					break;
				case "targetCameraAlpha":
					videoPlayer.targetCameraAlpha = reader.ReadProperty<float>();
					break;
				case "waitForFirstFrame":
					videoPlayer.waitForFirstFrame = reader.ReadProperty<bool>();
					break;
				case "playOnAwake":
					videoPlayer.playOnAwake = reader.ReadProperty<bool>();
					break;
				case "time":
					videoPlayer.time = reader.ReadProperty<double>();
					break;
				case "frame":
					videoPlayer.frame = reader.ReadProperty<long>();
					break;
				case "playbackSpeed":
					videoPlayer.playbackSpeed = reader.ReadProperty<float>();
					break;
				case "isLooping":
					videoPlayer.isLooping = reader.ReadProperty<bool>();
					break;
				case "timeSource":
					videoPlayer.timeUpdateMode = reader.ReadProperty<VideoTimeUpdateMode>();
					break;
				case "timeReference":
					videoPlayer.timeReference = reader.ReadProperty<VideoTimeReference>();
					break;
				case "externalReferenceTime":
					videoPlayer.externalReferenceTime = reader.ReadProperty<double>();
					break;
				case "skipOnDrop":
					videoPlayer.skipOnDrop = reader.ReadProperty<bool>();
					break;
				case "controlledAudioTrackCount":
					videoPlayer.controlledAudioTrackCount = reader.ReadProperty<ushort>();
					break;
				case "audioOutputMode":
					videoPlayer.audioOutputMode = reader.ReadProperty<VideoAudioOutputMode>();
					break;
				case "sendFrameReadyEvents":
					videoPlayer.sendFrameReadyEvents = reader.ReadProperty<bool>();
					break;
				case "enabled":
					videoPlayer.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					videoPlayer.tag = reader.ReadProperty<string>();
					break;
				case "name":
					videoPlayer.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					videoPlayer.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
