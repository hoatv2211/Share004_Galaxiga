using System;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

namespace Moments
{
	public class ReflectionUtils<T> where T : class, new()
	{
		public ReflectionUtils(T instance)
		{
			this._Instance = instance;
		}

		public string GetFieldName<U>(Expression<Func<T, U>> fieldAccess)
		{
			MemberExpression memberExpression = fieldAccess.Body as MemberExpression;
			if (memberExpression != null)
			{
				return memberExpression.Member.Name;
			}
			throw new InvalidOperationException("Member expression expected");
		}

		public FieldInfo GetField(string fieldName)
		{
			return typeof(T).GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
		}

		public A GetAttribute<A>(FieldInfo field) where A : Attribute
		{
			return (A)((object)Attribute.GetCustomAttribute(field, typeof(A)));
		}

		public void ConstrainMin<U>(Expression<Func<T, U>> fieldAccess, float value)
		{
			FieldInfo field = this.GetField(this.GetFieldName<U>(fieldAccess));
			field.SetValue(this._Instance, Mathf.Max(value, this.GetAttribute<MinAttribute>(field).min));
		}

		public void ConstrainMin<U>(Expression<Func<T, U>> fieldAccess, int value)
		{
			FieldInfo field = this.GetField(this.GetFieldName<U>(fieldAccess));
			field.SetValue(this._Instance, (int)Mathf.Max((float)value, this.GetAttribute<MinAttribute>(field).min));
		}

		public void ConstrainRange<U>(Expression<Func<T, U>> fieldAccess, float value)
		{
			FieldInfo field = this.GetField(this.GetFieldName<U>(fieldAccess));
			RangeAttribute attribute = this.GetAttribute<RangeAttribute>(field);
			field.SetValue(this._Instance, Mathf.Clamp(value, attribute.min, attribute.max));
		}

		public void ConstrainRange<U>(Expression<Func<T, U>> fieldAccess, int value)
		{
			FieldInfo field = this.GetField(this.GetFieldName<U>(fieldAccess));
			RangeAttribute attribute = this.GetAttribute<RangeAttribute>(field);
			field.SetValue(this._Instance, (int)Mathf.Clamp((float)value, attribute.min, attribute.max));
		}

		private readonly T _Instance;
	}
}
