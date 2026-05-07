using System;
using System.Collections.Generic;
using UnityEngine;

namespace BayatGames.SaveGamePro.Examples
{
	public class SaveAbstractClass : MonoBehaviour
	{
		private void Start()
		{
			this.items = new List<ItemBase>();
			this.items.Add(this.weapon);
			this.Save();
			this.Load();
		}

		public void Save()
		{
			SaveGame.Save<List<ItemBase>>("abstract.dat", this.items);
		}

		public void Load()
		{
			SaveGame.LoadInto<List<ItemBase>>("abstract.dat", this.items);
			UnityEngine.Debug.Log(this.items[0]);
		}

		public ItemWeapon weapon;

		public List<ItemBase> items;
	}
}
