using System;

namespace UnityEngine.UI.Extensions.Examples
{
	[RequireComponent(typeof(Image))]
	public class CooldownEffect_Image : MonoBehaviour
	{
		private void Start()
		{
			if (this.cooldown == null)
			{
				UnityEngine.Debug.LogError("Missing Cooldown Button assignment");
			}
			this.target = base.GetComponent<Image>();
		}

		private void Update()
		{
			this.target.fillAmount = Mathf.Lerp(0f, 1f, this.cooldown.CooldownTimeRemaining / this.cooldown.CooldownTimeout);
			if (this.displayText)
			{
				this.displayText.text = string.Format("{0}%", this.cooldown.CooldownPercentComplete);
			}
		}

		private void OnDisable()
		{
			if (this.displayText)
			{
				this.displayText.text = this.originalText;
			}
		}

		private void OnEnable()
		{
			if (this.displayText)
			{
				this.originalText = this.displayText.text;
			}
		}

		public CooldownButton cooldown;

		public Text displayText;

		private Image target;

		private string originalText;
	}
}
