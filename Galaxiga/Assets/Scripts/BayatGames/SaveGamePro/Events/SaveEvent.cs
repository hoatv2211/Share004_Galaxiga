using System;
using UnityEngine.Events;

namespace BayatGames.SaveGamePro.Events
{
	[Serializable]
	public class SaveEvent : UnityEvent<string, object, SaveGameSettings>
	{
	}
}
