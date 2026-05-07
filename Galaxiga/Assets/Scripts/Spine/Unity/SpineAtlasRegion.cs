using System;
using UnityEngine;

namespace Spine.Unity
{
	public class SpineAtlasRegion : PropertyAttribute
	{
		public SpineAtlasRegion(string atlasAssetField = "")
		{
			this.atlasAssetField = atlasAssetField;
		}

		public string atlasAssetField;
	}
}
