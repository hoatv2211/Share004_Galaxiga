using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_AnimationEvent : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(AnimationEvent);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			AnimationEvent animationEvent = (AnimationEvent)value;
			writer.WriteProperty<string>("stringParameter", animationEvent.stringParameter);
			writer.WriteProperty<float>("floatParameter", animationEvent.floatParameter);
			writer.WriteProperty<int>("intParameter", animationEvent.intParameter);
			writer.WriteProperty<string>("objectReferenceParameterType", animationEvent.objectReferenceParameter.GetType().AssemblyQualifiedName);
			writer.WriteProperty<UnityEngine.Object>("objectReferenceParameter", animationEvent.objectReferenceParameter);
			writer.WriteProperty<string>("functionName", animationEvent.functionName);
			writer.WriteProperty<float>("time", animationEvent.time);
			writer.WriteProperty<SendMessageOptions>("messageOptions", animationEvent.messageOptions);
		}

		public override object Read(ISaveGameReader reader)
		{
			AnimationEvent animationEvent = new AnimationEvent();
			this.ReadInto(animationEvent, reader);
			return animationEvent;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			AnimationEvent animationEvent = (AnimationEvent)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "stringParameter":
					animationEvent.stringParameter = reader.ReadProperty<string>();
					break;
				case "floatParameter":
					animationEvent.floatParameter = reader.ReadProperty<float>();
					break;
				case "intParameter":
					animationEvent.intParameter = reader.ReadProperty<int>();
					break;
				case "objectReferenceParameter":
				{
					Type type = Type.GetType(reader.ReadProperty<string>());
					animationEvent.objectReferenceParameter = (UnityEngine.Object)reader.ReadProperty(type);
					break;
				}
				case "functionName":
					animationEvent.functionName = reader.ReadProperty<string>();
					break;
				case "time":
					animationEvent.time = reader.ReadProperty<float>();
					break;
				case "messageOptions":
					animationEvent.messageOptions = reader.ReadProperty<SendMessageOptions>();
					break;
				}
			}
		}
	}
}
