using System;
using System.Collections.Generic;
using UnityEngine;

namespace BayatGames.SaveGamePro
{
	[AddComponentMenu("Save Game Pro/Auto Save")]
	public class SaveGameAuto : SaveGameAutoBehaviour
	{
		public virtual List<UnityEngine.Object> Objects
		{
			get
			{
				return this.m_Objects;
			}
		}

		public override void Save()
		{
			SaveGame.Save<List<UnityEngine.Object>>(this.SaveSettings.Identifier, this.m_Objects);
		}

		public override void Load()
		{
			SaveGame.LoadInto<List<UnityEngine.Object>>(this.SaveSettings.Identifier, this.m_Objects);
		}

		[SerializeField]
		protected List<UnityEngine.Object> m_Objects;
	}
}
