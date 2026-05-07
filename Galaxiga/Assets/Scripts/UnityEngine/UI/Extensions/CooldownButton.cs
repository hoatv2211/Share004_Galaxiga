using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Extensions/Cooldown Button")]
	public class CooldownButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
	{
		public float CooldownTimeout
		{
			get
			{
				return this.cooldownTimeout;
			}
			set
			{
				this.cooldownTimeout = value;
			}
		}

		public float CooldownSpeed
		{
			get
			{
				return this.cooldownSpeed;
			}
			set
			{
				this.cooldownSpeed = value;
			}
		}

		public bool CooldownInEffect
		{
			get
			{
				return this.cooldownInEffect;
			}
		}

		public bool CooldownActive
		{
			get
			{
				return this.cooldownActive;
			}
			set
			{
				this.cooldownActive = value;
			}
		}

		public float CooldownTimeElapsed
		{
			get
			{
				return this.cooldownTimeElapsed;
			}
			set
			{
				this.cooldownTimeElapsed = value;
			}
		}

		public float CooldownTimeRemaining
		{
			get
			{
				return this.cooldownTimeRemaining;
			}
		}

		public int CooldownPercentRemaining
		{
			get
			{
				return this.cooldownPercentRemaining;
			}
		}

		public int CooldownPercentComplete
		{
			get
			{
				return this.cooldownPercentComplete;
			}
		}

		private void Update()
		{
			if (this.CooldownActive)
			{
				this.cooldownTimeRemaining -= Time.deltaTime * this.cooldownSpeed;
				this.cooldownTimeElapsed = this.CooldownTimeout - this.CooldownTimeRemaining;
				if (this.cooldownTimeRemaining < 0f)
				{
					this.StopCooldown();
				}
				else
				{
					this.cooldownPercentRemaining = (int)(100f * this.cooldownTimeRemaining * this.CooldownTimeout / 100f);
					this.cooldownPercentComplete = (int)((this.CooldownTimeout - this.cooldownTimeRemaining) / this.CooldownTimeout * 100f);
				}
			}
		}

		public void PauseCooldown()
		{
			if (this.CooldownInEffect)
			{
				this.CooldownActive = false;
			}
		}

		public void RestartCooldown()
		{
			if (this.CooldownInEffect)
			{
				this.CooldownActive = true;
			}
		}

		public void StopCooldown()
		{
			this.cooldownTimeElapsed = this.CooldownTimeout;
			this.cooldownTimeRemaining = 0f;
			this.cooldownPercentRemaining = 0;
			this.cooldownPercentComplete = 100;
			this.cooldownActive = (this.cooldownInEffect = false);
			if (this.OnCoolDownFinish != null)
			{
				this.OnCoolDownFinish.Invoke(this.buttonSource.button);
			}
		}

		public void CancelCooldown()
		{
			this.cooldownActive = (this.cooldownInEffect = false);
		}

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			this.buttonSource = eventData;
			if (this.CooldownInEffect && this.OnButtonClickDuringCooldown != null)
			{
				this.OnButtonClickDuringCooldown.Invoke(eventData.button);
			}
			if (!this.CooldownInEffect)
			{
				if (this.OnCooldownStart != null)
				{
					this.OnCooldownStart.Invoke(eventData.button);
				}
				this.cooldownTimeRemaining = this.cooldownTimeout;
				this.cooldownActive = (this.cooldownInEffect = true);
			}
		}

		[SerializeField]
		private float cooldownTimeout;

		[SerializeField]
		private float cooldownSpeed = 1f;

		[SerializeField]
		[ReadOnly]
		private bool cooldownActive;

		[SerializeField]
		[ReadOnly]
		private bool cooldownInEffect;

		[SerializeField]
		[ReadOnly]
		private float cooldownTimeElapsed;

		[SerializeField]
		[ReadOnly]
		private float cooldownTimeRemaining;

		[SerializeField]
		[ReadOnly]
		private int cooldownPercentRemaining;

		[SerializeField]
		[ReadOnly]
		private int cooldownPercentComplete;

		private PointerEventData buttonSource;

		[Tooltip("Event that fires when a button is initially pressed down")]
		public CooldownButton.CooldownButtonEvent OnCooldownStart;

		[Tooltip("Event that fires when a button is released")]
		public CooldownButton.CooldownButtonEvent OnButtonClickDuringCooldown;

		[Tooltip("Event that continually fires while a button is held down")]
		public CooldownButton.CooldownButtonEvent OnCoolDownFinish;

		[Serializable]
		public class CooldownButtonEvent : UnityEvent<PointerEventData.InputButton>
		{
		}
	}
}
