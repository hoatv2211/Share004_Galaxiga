using System;

namespace Spine.Unity
{
	public class SpinePathConstraint : SpineAttributeBase
	{
		public SpinePathConstraint(string startsWith = "", string dataField = "", bool includeNone = true, bool fallbackToTextField = false)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.includeNone = includeNone;
			this.fallbackToTextField = fallbackToTextField;
		}
	}
}
