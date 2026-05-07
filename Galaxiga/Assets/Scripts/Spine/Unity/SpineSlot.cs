using System;

namespace Spine.Unity
{
	public class SpineSlot : SpineAttributeBase
	{
		public SpineSlot(string startsWith = "", string dataField = "", bool containsBoundingBoxes = false, bool includeNone = true, bool fallbackToTextField = false)
		{
			this.startsWith = startsWith;
			this.dataField = dataField;
			this.containsBoundingBoxes = containsBoundingBoxes;
			this.includeNone = includeNone;
			this.fallbackToTextField = fallbackToTextField;
		}

		public bool containsBoundingBoxes;
	}
}
