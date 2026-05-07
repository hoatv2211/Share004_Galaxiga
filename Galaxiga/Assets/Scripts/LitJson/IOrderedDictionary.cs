using System;
using System.Collections;

namespace LitJson
{
	public interface IOrderedDictionary : IDictionary, ICollection, IEnumerable
	{
		IDictionaryEnumerator GetEnumerator();

		void Insert(int index, object key, object value);

		void RemoveAt(int index);

		object this[int index]
		{
			get;
			set;
		}
	}
}
