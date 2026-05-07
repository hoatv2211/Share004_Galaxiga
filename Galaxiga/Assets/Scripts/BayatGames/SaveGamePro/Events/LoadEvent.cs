using System;
using UnityEngine.Events;

namespace BayatGames.SaveGamePro.Events
{
	[Serializable]
	public class LoadEvent : UnityEvent<string, object, SaveGameSettings>
	{
	}
}
