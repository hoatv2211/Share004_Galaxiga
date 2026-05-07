using System;

namespace UnityEngine.UI.Extensions
{
	[Serializable]
	public struct Vector3_Array2D
	{
		public Vector3 this[int _idx]
		{
			get
			{
				return this.array[_idx];
			}
			set
			{
				this.array[_idx] = value;
			}
		}

		[SerializeField]
		public Vector3[] array;
	}
}
