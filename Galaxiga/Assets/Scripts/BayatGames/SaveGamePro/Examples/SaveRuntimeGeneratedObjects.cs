using System;
using BayatGames.SaveGamePro.Extensions;
using UnityEngine;

namespace BayatGames.SaveGamePro.Examples
{
	public class SaveRuntimeGeneratedObjects : MonoBehaviour
	{
		public string count
		{
			get
			{
				return this._count.ToString();
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this._count = 0;
				}
				else
				{
					this._count = int.Parse(value);
				}
			}
		}

		public void Spawn()
		{
			this.DestroyAll();
			for (int i = 0; i < this._count; i++)
			{
				Vector3 position = default(Vector3);
				position.x = UnityEngine.Random.Range(-20f, 20f);
				position.y = UnityEngine.Random.Range(-20f, 20f);
				position.z = UnityEngine.Random.Range(-20f, 20f);
				UnityEngine.Object.Instantiate<GameObject>(this.prefab, position, Quaternion.identity, this.container.transform);
			}
		}

		public void DestroyAll()
		{
			this.container.DestroyChilds();
		}

		public void Save()
		{
			SaveGame.Save<GameObject>("instantiatedGameObjects", this.container);
		}

		public void Load()
		{
			if (this.container == null)
			{
				this.container = SaveGame.Load<GameObject>("instantiatedGameObjects");
			}
			else
			{
				SaveGame.LoadInto<GameObject>("instantiatedGameObjects", this.container);
			}
		}

		public GameObject prefab;

		public GameObject container;

		[SerializeField]
		private int _count;
	}
}
