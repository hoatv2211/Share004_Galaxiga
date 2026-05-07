using System;
using UnityEngine;

namespace BayatGames.SaveGamePro.Examples
{
	public class SaveTransform : MonoBehaviour
	{
		private void Awake()
		{
			if (this.target == null)
			{
				this.target = base.transform;
			}
		}

		public void DestroyTarget()
		{
			Transform parent = this.target.parent;
			while (parent.parent != null)
			{
				parent = parent.parent;
			}
			UnityEngine.Object.Destroy(parent.gameObject);
		}

		public void Save()
		{
			SaveGame.Save<Transform>("transform", this.target);
		}

		public void Load()
		{
			if (this.target == null)
			{
				this.target = SaveGame.Load<Transform>("transform");
			}
			else
			{
				SaveGame.LoadInto<Transform>("transform", this.target);
			}
		}

		public Transform target;
	}
}
