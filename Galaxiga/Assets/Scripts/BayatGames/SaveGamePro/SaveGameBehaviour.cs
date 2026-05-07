using System;
using BayatGames.SaveGamePro.Events;
using UnityEngine;

namespace BayatGames.SaveGamePro
{
	public abstract class SaveGameBehaviour : MonoBehaviour
	{
		public virtual SaveGameSettings SaveSettings
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_SaveSettings.Identifier))
				{
					this.m_SaveSettings.Identifier = base.name + "/" + base.GetInstanceID().ToString();
				}
				return this.m_SaveSettings;
			}
			set
			{
				this.m_SaveSettings = value;
			}
		}

		public virtual SaveEvent OnSaved
		{
			get
			{
				return this.m_OnSaved;
			}
		}

		public virtual LoadEvent OnLoaded
		{
			get
			{
				return this.m_OnLoaded;
			}
		}

		public virtual void Save()
		{
			SaveGame.Save<SaveGameBehaviour>(this.SaveSettings.Identifier, this, this.SaveSettings);
			this.m_OnSaved.Invoke(this.SaveSettings.Identifier, this, this.SaveSettings);
		}

		public virtual void Load()
		{
			SaveGame.LoadInto<SaveGameBehaviour>(this.SaveSettings.Identifier, this, this.SaveSettings);
			this.m_OnLoaded.Invoke(this.SaveSettings.Identifier, this, this.SaveSettings);
		}

		[SerializeField]
		protected SaveGameSettings m_SaveSettings;

		[SerializeField]
		protected SaveEvent m_OnSaved;

		[SerializeField]
		protected LoadEvent m_OnLoaded;
	}
}
