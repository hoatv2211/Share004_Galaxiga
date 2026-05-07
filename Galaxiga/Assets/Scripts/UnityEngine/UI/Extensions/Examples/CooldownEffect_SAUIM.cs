using System;

namespace UnityEngine.UI.Extensions.Examples
{
	[RequireComponent(typeof(SoftMaskScript))]
	public class CooldownEffect_SAUIM : MonoBehaviour
	{
		private void Start()
		{
			if (this.cooldown == null)
			{
				UnityEngine.Debug.LogError("Missing Cooldown Button assignment");
			}
			this.sauim = base.GetComponent<SoftMaskScript>();
		}

		private void Update()
		{
			this.sauim.CutOff = Mathf.Lerp(0f, 1f, this.cooldown.CooldownTimeElapsed / this.cooldown.CooldownTimeout);
		}

		public CooldownButton cooldown;

		private SoftMaskScript sauim;
	}
}
