using System;
using UnityEngine;

namespace DigitalRuby.ThunderAndLightning
{
	public class SingleLineAttribute : PropertyAttribute
	{
		public SingleLineAttribute(string tooltip)
		{
			this.Tooltip = tooltip;
		}

		public string Tooltip { get; private set; }
	}
}
