using System;
using UnityEngine;
using UnityEngine.UI;

namespace BayatGames.SaveGamePro.Examples
{
	[ExecuteInEditMode]
	public class SaveSlots : MonoBehaviour
	{
		public int currentSlot
		{
			get
			{
				return this._currentSlot;
			}
			set
			{
				if (value >= 0 || value < this._slots.Length)
				{
					this._currentSlot = value;
				}
			}
		}

		public void Save()
		{
			SaveGame.Save<Transform>(this.GetSlotIdentifier("target"), this.target);
		}

		public void Load()
		{
			if (SaveGame.Exists(this.GetSlotIdentifier("target")))
			{
				SaveGame.LoadInto<Transform>(this.GetSlotIdentifier("target"), this.target);
			}
			else
			{
				this.target.position = Vector3.zero;
				this.target.rotation = Quaternion.identity;
				this.target.localScale = Vector3.one;
			}
		}

		public string GetSlotIdentifier(string identifier)
		{
			return string.Format("{0}/{1}", this._slots[this.currentSlot], identifier);
		}

		public Transform target;

		public Dropdown slotDropdown;

		[SerializeField]
		private string[] _slots = new string[]
		{
			"Slot 1",
			"Slot 2",
			"Slot 3"
		};

		[SerializeField]
		private int _currentSlot;
	}
}
