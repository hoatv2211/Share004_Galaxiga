using System;
using System.Collections;
using BayatGames.SaveGamePro.Reflection;

namespace BayatGames.SaveGamePro.Networking
{
	public abstract class SaveGameCloud
	{
		public SaveGameCloud() : this(SaveGame.DefaultSettings)
		{
		}

		public SaveGameCloud(SaveGameSettings settings)
		{
			this.m_Settings = settings;
		}

		public virtual SaveGameSettings Settings
		{
			get
			{
				return this.m_Settings;
			}
			set
			{
				this.m_Settings = value;
			}
		}

		public virtual IEnumerator Save<T>(string identifier, T value)
		{
			return this.Save(identifier, value, this.Settings);
		}

		public virtual IEnumerator Save(string identifier, object value)
		{
			return this.Save(identifier, value, this.Settings);
		}

		public virtual IEnumerator Save<T>(string identifier, T value, SaveGameSettings settings)
		{
			return this.Save(identifier, value, settings);
		}

		public abstract IEnumerator Save(string identifier, object value, SaveGameSettings settings);

		public virtual IEnumerator Download(string identifier)
		{
			return this.Download(identifier, this.Settings);
		}

		public abstract IEnumerator Download(string identifier, SaveGameSettings settings);

		public virtual T Load<T>()
		{
			return (T)((object)this.Load(typeof(T), default(T), this.Settings));
		}

		public virtual object Load(Type type)
		{
			return this.Load(type, type.GetDefault(), this.Settings);
		}

		public virtual T Load<T>(T defaultValue)
		{
			return (T)((object)this.Load(typeof(T), defaultValue, this.Settings));
		}

		public virtual object Load(Type type, object defaultValue)
		{
			return this.Load(type, defaultValue, this.Settings);
		}

		public virtual T Load<T>(SaveGameSettings settings)
		{
			return (T)((object)this.Load(typeof(T), default(T), settings));
		}

		public virtual object Load(Type type, SaveGameSettings settings)
		{
			return this.Load(type, type.GetDefault(), settings);
		}

		public virtual T Load<T>(T defaultValue, SaveGameSettings settings)
		{
			if (defaultValue == null)
			{
				defaultValue = default(T);
			}
			return (T)((object)this.Load(typeof(T), defaultValue, settings));
		}

		public abstract object Load(Type type, object defaultValue, SaveGameSettings settings);

		public virtual void LoadInto<T>(T value)
		{
			this.LoadInto(value, this.Settings);
		}

		public virtual void LoadInto(object value)
		{
			this.LoadInto(value, this.Settings);
		}

		public virtual void LoadInto<T>(T value, SaveGameSettings settings)
		{
			this.LoadInto(value, settings);
		}

		public abstract void LoadInto(object value, SaveGameSettings settings);

		public virtual IEnumerator Clear()
		{
			return this.Clear(this.Settings);
		}

		public abstract IEnumerator Clear(SaveGameSettings settings);

		public virtual IEnumerator Delete(string identifier)
		{
			return this.Delete(identifier, this.Settings);
		}

		public abstract IEnumerator Delete(string identifier, SaveGameSettings settings);

		protected SaveGameSettings m_Settings;
	}
}
