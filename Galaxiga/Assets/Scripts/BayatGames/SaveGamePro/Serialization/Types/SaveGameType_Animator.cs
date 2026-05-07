using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_Animator : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(Animator);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			Animator animator = (Animator)value;
			writer.WriteProperty<Vector3>("rootPosition", animator.rootPosition);
			writer.WriteProperty<Quaternion>("rootRotation", animator.rootRotation);
			writer.WriteProperty<bool>("applyRootMotion", animator.applyRootMotion);
			writer.WriteProperty<bool>("linearVelocityBlending", animator.linearVelocityBlending);
			writer.WriteProperty<AnimatorUpdateMode>("updateMode", animator.updateMode);
			writer.WriteProperty<Vector3>("bodyPosition", animator.bodyPosition);
			writer.WriteProperty<Quaternion>("bodyRotation", animator.bodyRotation);
			writer.WriteProperty<bool>("stabilizeFeet", animator.stabilizeFeet);
			writer.WriteProperty<float>("feetPivotActive", animator.feetPivotActive);
			writer.WriteProperty<float>("speed", animator.speed);
			writer.WriteProperty<AnimatorCullingMode>("cullingMode", animator.cullingMode);
			writer.WriteProperty<float>("playbackTime", animator.playbackTime);
			writer.WriteProperty<float>("recorderStartTime", animator.recorderStartTime);
			writer.WriteProperty<float>("recorderStopTime", animator.recorderStopTime);
			writer.WriteProperty<RuntimeAnimatorController>("runtimeAnimatorController", animator.runtimeAnimatorController);
			writer.WriteProperty<Avatar>("avatar", animator.avatar);
			writer.WriteProperty<bool>("layersAffectMassCenter", animator.layersAffectMassCenter);
			writer.WriteProperty<bool>("logWarnings", animator.logWarnings);
			writer.WriteProperty<bool>("fireEvents", animator.fireEvents);
			writer.WriteProperty<bool>("enabled", animator.enabled);
			writer.WriteProperty<string>("tag", animator.tag);
			writer.WriteProperty<string>("name", animator.name);
			writer.WriteProperty<HideFlags>("hideFlags", animator.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			Animator animator = SaveGameType.CreateComponent<Animator>();
			this.ReadInto(animator, reader);
			return animator;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			Animator animator = (Animator)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "rootPosition":
					animator.rootPosition = reader.ReadProperty<Vector3>();
					break;
				case "rootRotation":
					animator.rootRotation = reader.ReadProperty<Quaternion>();
					break;
				case "applyRootMotion":
					animator.applyRootMotion = reader.ReadProperty<bool>();
					break;
				case "linearVelocityBlending":
					animator.linearVelocityBlending = reader.ReadProperty<bool>();
					break;
				case "updateMode":
					animator.updateMode = reader.ReadProperty<AnimatorUpdateMode>();
					break;
				case "bodyPosition":
					animator.bodyPosition = reader.ReadProperty<Vector3>();
					break;
				case "bodyRotation":
					animator.bodyRotation = reader.ReadProperty<Quaternion>();
					break;
				case "stabilizeFeet":
					animator.stabilizeFeet = reader.ReadProperty<bool>();
					break;
				case "feetPivotActive":
					animator.feetPivotActive = reader.ReadProperty<float>();
					break;
				case "speed":
					animator.speed = reader.ReadProperty<float>();
					break;
				case "cullingMode":
					animator.cullingMode = reader.ReadProperty<AnimatorCullingMode>();
					break;
				case "playbackTime":
					animator.playbackTime = reader.ReadProperty<float>();
					break;
				case "recorderStartTime":
					animator.recorderStartTime = reader.ReadProperty<float>();
					break;
				case "recorderStopTime":
					animator.recorderStopTime = reader.ReadProperty<float>();
					break;
				case "runtimeAnimatorController":
					if (animator.runtimeAnimatorController == null)
					{
						animator.runtimeAnimatorController = reader.ReadProperty<RuntimeAnimatorController>();
					}
					else
					{
						reader.ReadIntoProperty<RuntimeAnimatorController>(animator.runtimeAnimatorController);
					}
					break;
				case "avatar":
					if (animator.avatar == null)
					{
						animator.avatar = reader.ReadProperty<Avatar>();
					}
					else
					{
						reader.ReadIntoProperty<Avatar>(animator.avatar);
					}
					break;
				case "layersAffectMassCenter":
					animator.layersAffectMassCenter = reader.ReadProperty<bool>();
					break;
				case "logWarnings":
					animator.logWarnings = reader.ReadProperty<bool>();
					break;
				case "fireEvents":
					animator.fireEvents = reader.ReadProperty<bool>();
					break;
				case "enabled":
					animator.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					animator.tag = reader.ReadProperty<string>();
					break;
				case "name":
					animator.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					animator.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
