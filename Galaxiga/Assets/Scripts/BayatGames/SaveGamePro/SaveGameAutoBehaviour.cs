using System;

namespace BayatGames.SaveGamePro
{
	public abstract class SaveGameAutoBehaviour : SaveGameBehaviour
	{
		protected virtual void Awake()
		{
			this.Load();
		}

		protected virtual void OnDisable()
		{
			this.Save();
		}

		protected virtual void OnDestroy()
		{
			this.Save();
		}

		protected virtual void OnApplicationPause()
		{
			this.Save();
		}

		protected virtual void OnApplicationQuit()
		{
			this.Save();
		}
	}
}
