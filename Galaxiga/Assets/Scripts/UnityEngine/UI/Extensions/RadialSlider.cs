using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Extensions/Radial Slider")]
	[RequireComponent(typeof(Image))]
	public class RadialSlider : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		public float Angle
		{
			get
			{
				return this.RadialImage.fillAmount * 360f;
			}
			set
			{
				if (this.LerpToTarget)
				{
					this.StartLerp(value / 360f);
				}
				else
				{
					this.UpdateRadialImage(value / 360f);
				}
			}
		}

		public float Value
		{
			get
			{
				return this.RadialImage.fillAmount;
			}
			set
			{
				if (this.LerpToTarget)
				{
					this.StartLerp(value);
				}
				else
				{
					this.UpdateRadialImage(value);
				}
			}
		}

		public Color EndColor
		{
			get
			{
				return this.m_endColor;
			}
			set
			{
				this.m_endColor = value;
			}
		}

		public Color StartColor
		{
			get
			{
				return this.m_startColor;
			}
			set
			{
				this.m_startColor = value;
			}
		}

		public bool LerpToTarget
		{
			get
			{
				return this.m_lerpToTarget;
			}
			set
			{
				this.m_lerpToTarget = value;
			}
		}

		public AnimationCurve LerpCurve
		{
			get
			{
				return this.m_lerpCurve;
			}
			set
			{
				this.m_lerpCurve = value;
				this.m_lerpTime = this.LerpCurve[this.LerpCurve.length - 1].time;
			}
		}

		public bool LerpInProgress
		{
			get
			{
				return this.lerpInProgress;
			}
		}

		public Image RadialImage
		{
			get
			{
				if (this.m_image == null)
				{
					this.m_image = base.GetComponent<Image>();
					this.m_image.type = Image.Type.Filled;
					this.m_image.fillMethod = Image.FillMethod.Radial360;
					this.m_image.fillOrigin = 3;
					this.m_image.fillAmount = 0f;
				}
				return this.m_image;
			}
		}

		public RadialSlider.RadialSliderValueChangedEvent onValueChanged
		{
			get
			{
				return this._onValueChanged;
			}
			set
			{
				this._onValueChanged = value;
			}
		}

		public RadialSlider.RadialSliderTextValueChangedEvent onTextValueChanged
		{
			get
			{
				return this._onTextValueChanged;
			}
			set
			{
				this._onTextValueChanged = value;
			}
		}

		private void Awake()
		{
			if (this.LerpCurve != null && this.LerpCurve.length > 0)
			{
				this.m_lerpTime = this.LerpCurve[this.LerpCurve.length - 1].time;
			}
			else
			{
				this.m_lerpTime = 1f;
			}
		}

		private void Update()
		{
			if (this.isPointerDown)
			{
				this.m_targetAngle = this.GetAngleFromMousePoint();
				if (!this.lerpInProgress)
				{
					if (!this.LerpToTarget)
					{
						this.UpdateRadialImage(this.m_targetAngle);
						this.NotifyValueChanged();
					}
					else
					{
						if (this.isPointerReleased)
						{
							this.StartLerp(this.m_targetAngle);
						}
						this.isPointerReleased = false;
					}
				}
			}
			if (this.lerpInProgress && this.Value != this.m_lerpTargetAngle)
			{
				this.m_currentLerpTime += Time.deltaTime;
				float num = this.m_currentLerpTime / this.m_lerpTime;
				if (this.LerpCurve != null && this.LerpCurve.length > 0)
				{
					this.UpdateRadialImage(Mathf.Lerp(this.m_startAngle, this.m_lerpTargetAngle, this.LerpCurve.Evaluate(num)));
				}
				else
				{
					this.UpdateRadialImage(Mathf.Lerp(this.m_startAngle, this.m_lerpTargetAngle, num));
				}
			}
			if (this.m_currentLerpTime >= this.m_lerpTime || this.Value == this.m_lerpTargetAngle)
			{
				this.lerpInProgress = false;
				this.UpdateRadialImage(this.m_lerpTargetAngle);
				this.NotifyValueChanged();
			}
		}

		private void StartLerp(float targetAngle)
		{
			if (!this.lerpInProgress)
			{
				this.m_startAngle = this.Value;
				this.m_lerpTargetAngle = targetAngle;
				this.m_currentLerpTime = 0f;
				this.lerpInProgress = true;
			}
		}

		private float GetAngleFromMousePoint()
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, UnityEngine.Input.mousePosition, this.m_eventCamera, out this.m_localPos);
			return (Mathf.Atan2(-this.m_localPos.y, this.m_localPos.x) * 180f / 3.14159274f + 180f) / 360f;
		}

		private void UpdateRadialImage(float targetAngle)
		{
			this.RadialImage.fillAmount = targetAngle;
			this.RadialImage.color = Color.Lerp(this.m_startColor, this.m_endColor, targetAngle);
		}

		private void NotifyValueChanged()
		{
			this._onValueChanged.Invoke((int)(this.m_targetAngle * 360f));
			this._onTextValueChanged.Invoke(((int)(this.m_targetAngle * 360f)).ToString());
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			this.m_eventCamera = eventData.enterEventCamera;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			this.m_eventCamera = eventData.enterEventCamera;
			this.isPointerDown = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			this.isPointerDown = false;
			this.isPointerReleased = true;
		}

		private bool isPointerDown;

		private bool isPointerReleased;

		private bool lerpInProgress;

		private Vector2 m_localPos;

		private float m_targetAngle;

		private float m_lerpTargetAngle;

		private float m_startAngle;

		private float m_currentLerpTime;

		private float m_lerpTime;

		private Camera m_eventCamera;

		private Image m_image;

		[SerializeField]
		[Tooltip("Radial Gradient Start Color")]
		private Color m_startColor = Color.green;

		[SerializeField]
		[Tooltip("Radial Gradient End Color")]
		private Color m_endColor = Color.red;

		[Tooltip("Move slider absolute or use Lerping?\nDragging only supported with absolute")]
		[SerializeField]
		private bool m_lerpToTarget;

		[Tooltip("Curve to apply to the Lerp\nMust be set to enable Lerp")]
		[SerializeField]
		private AnimationCurve m_lerpCurve;

		[Tooltip("Event fired when value of control changes, outputs an INT angle value")]
		[SerializeField]
		private RadialSlider.RadialSliderValueChangedEvent _onValueChanged = new RadialSlider.RadialSliderValueChangedEvent();

		[Tooltip("Event fired when value of control changes, outputs a TEXT angle value")]
		[SerializeField]
		private RadialSlider.RadialSliderTextValueChangedEvent _onTextValueChanged = new RadialSlider.RadialSliderTextValueChangedEvent();

		[Serializable]
		public class RadialSliderValueChangedEvent : UnityEvent<int>
		{
		}

		[Serializable]
		public class RadialSliderTextValueChangedEvent : UnityEvent<string>
		{
		}
	}
}
