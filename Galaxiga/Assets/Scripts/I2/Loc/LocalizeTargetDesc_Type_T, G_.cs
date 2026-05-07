using System;
using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTargetDesc_Type<T, G> : LocalizeTargetDesc<G> where T : UnityEngine.Object where G : LocalizeTarget<T>
	{
		public override bool CanLocalize(Localize cmp)
		{
			return cmp.GetComponent<T>() != null;
		}

		public override ILocalizeTarget CreateTarget(Localize cmp)
		{
			T component = cmp.GetComponent<T>();
			if (component == null)
			{
				return null;
			}
			G g = ScriptableObject.CreateInstance<G>();
			g.mTarget = component;
			return g;
		}
	}
}
