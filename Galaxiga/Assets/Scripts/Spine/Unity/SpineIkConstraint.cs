using System;

namespace Spine.Unity
{
	public class SpineIkConstraint : SpineAttributeBase
	{
		public SpineIkConstraint(string startsWith = "", string dataField = "", bool includeNone = true, bool fallbackToTextField = false)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
			this.fallbackToTextField = fallbackToTextField;
		}
	}
}
