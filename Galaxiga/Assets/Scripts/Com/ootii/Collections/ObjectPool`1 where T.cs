using System;

namespace com.ootii.Collections
{
	public sealed class ObjectPool<T> where T : new()
	{
		public ObjectPool(int rSize)
		{
			this.Resize(rSize, false);
		}

		public ObjectPool(int rSize, int rGrowSize)
		{
			this.mGrowSize = rGrowSize;
			this.Resize(rSize, false);
		}

		public int Length
		{
			get
			{
				return this.mPool.Length;
			}
		}

		public int Available
		{
			get
			{
				return this.mPool.Length - this.mNextIndex;
			}
		}

		public int Allocated
		{
			get
			{
				return this.mNextIndex;
			}
		}

		public T Allocate()
		{
			T result = default(T);
			if (this.mNextIndex >= this.mPool.Length)
			{
				if (this.mGrowSize <= 0)
				{
					return result;
				}
				this.Resize(this.mPool.Length + this.mGrowSize, true);
			}
			if (this.mNextIndex >= 0 && this.mNextIndex < this.mPool.Length)
			{
				result = this.mPool[this.mNextIndex];
				this.mNextIndex++;
			}
			return result;
		}

		public void Release(T rInstance)
		{
			if (this.mNextIndex > 0)
			{
				this.mNextIndex--;
				this.mPool[this.mNextIndex] = rInstance;
			}
		}

		public void Reset()
		{
			int rSize = this.mGrowSize;
			if (this.mPool != null)
			{
				rSize = this.mPool.Length;
			}
			this.Resize(rSize, false);
			this.mNextIndex = 0;
		}

		public void Resize(int rSize, bool rCopyExisting)
		{
			lock (this)
			{
				int num = 0;
				T[] array = new T[rSize];
				if (this.mPool != null && rCopyExisting)
				{
					num = this.mPool.Length;
					Array.Copy(this.mPool, array, Math.Min(num, rSize));
				}
				for (int i = num; i < rSize; i++)
				{
					array[i] = Activator.CreateInstance<T>();
				}
				this.mPool = array;
			}
		}

		private int mGrowSize = 20;

		private T[] mPool;

		private int mNextIndex;
	}
}
