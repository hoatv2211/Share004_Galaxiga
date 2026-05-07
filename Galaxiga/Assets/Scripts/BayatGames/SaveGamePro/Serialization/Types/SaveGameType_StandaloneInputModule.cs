using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BayatGames.SaveGamePro.Serialization.Types
{
	public class SaveGameType_StandaloneInputModule : SaveGameType
	{
		public override Type AssociatedType
		{
			get
			{
				return typeof(StandaloneInputModule);
			}
		}

		public override void Write(object value, ISaveGameWriter writer)
		{
			StandaloneInputModule standaloneInputModule = (StandaloneInputModule)value;
			writer.WriteProperty<bool>("forceModuleActive", standaloneInputModule.forceModuleActive);
			writer.WriteProperty<float>("inputActionsPerSecond", standaloneInputModule.inputActionsPerSecond);
			writer.WriteProperty<float>("repeatDelay", standaloneInputModule.repeatDelay);
			writer.WriteProperty<string>("horizontalAxis", standaloneInputModule.horizontalAxis);
			writer.WriteProperty<string>("verticalAxis", standaloneInputModule.verticalAxis);
			writer.WriteProperty<string>("submitButton", standaloneInputModule.submitButton);
			writer.WriteProperty<string>("cancelButton", standaloneInputModule.cancelButton);
			writer.WriteProperty<bool>("useGUILayout", standaloneInputModule.useGUILayout);
			writer.WriteProperty<bool>("enabled", standaloneInputModule.enabled);
			writer.WriteProperty<string>("tag", standaloneInputModule.tag);
			writer.WriteProperty<string>("name", standaloneInputModule.name);
			writer.WriteProperty<HideFlags>("hideFlags", standaloneInputModule.hideFlags);
		}

		public override object Read(ISaveGameReader reader)
		{
			StandaloneInputModule standaloneInputModule = SaveGameType.CreateComponent<StandaloneInputModule>();
			this.ReadInto(standaloneInputModule, reader);
			return standaloneInputModule;
		}

		public override void ReadInto(object value, ISaveGameReader reader)
		{
			StandaloneInputModule standaloneInputModule = (StandaloneInputModule)value;
			foreach (string text in reader.Properties)
			{
				switch (text)
				{
				case "forceModuleActive":
					standaloneInputModule.forceModuleActive = reader.ReadProperty<bool>();
					break;
				case "inputActionsPerSecond":
					standaloneInputModule.inputActionsPerSecond = reader.ReadProperty<float>();
					break;
				case "repeatDelay":
					standaloneInputModule.repeatDelay = reader.ReadProperty<float>();
					break;
				case "horizontalAxis":
					standaloneInputModule.horizontalAxis = reader.ReadProperty<string>();
					break;
				case "verticalAxis":
					standaloneInputModule.verticalAxis = reader.ReadProperty<string>();
					break;
				case "submitButton":
					standaloneInputModule.submitButton = reader.ReadProperty<string>();
					break;
				case "cancelButton":
					standaloneInputModule.cancelButton = reader.ReadProperty<string>();
					break;
				case "useGUILayout":
					standaloneInputModule.useGUILayout = reader.ReadProperty<bool>();
					break;
				case "enabled":
					standaloneInputModule.enabled = reader.ReadProperty<bool>();
					break;
				case "tag":
					standaloneInputModule.tag = reader.ReadProperty<string>();
					break;
				case "name":
					standaloneInputModule.name = reader.ReadProperty<string>();
					break;
				case "hideFlags":
					standaloneInputModule.hideFlags = reader.ReadProperty<HideFlags>();
					break;
				}
			}
		}
	}
}
