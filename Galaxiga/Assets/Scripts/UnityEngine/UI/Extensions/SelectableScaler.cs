using System;
using System.Collections;
using UnityEngine.EventSystems;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu("UI/Extensions/Selectable Scalar")]
	[RequireComponent(typeof(Button))]
	public class SelectableScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		public Selectable Target
		{
			get
			{
				if (this.selectable == null)
				{
					this.selectable = base.GetComponent<Selectable>();
				}
				return this.selectable;
			}
		}

		private void Awake()
		{
			if (this.target == null)
			{
				this.target = base.transform;
			}
			this.initScale = this.target.localScale;
		}

		private void OnEnable()
		{
			this.target.localScale = this.initScale;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.Target != null && !this.Target.interactable)
			{
				return;
			}
			base.StopCoroutine("ScaleOUT");
			base.StartCoroutine("ScaleIN");
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.Target != null && !this.Target.interactable)
			{
				return;
			}
			base.StopCoroutine("ScaleIN");
			base.StartCoroutine("ScaleOUT");
		}

		private IEnumerator ScaleIN()
		{
			if (this.animCurve.keys.Length > 0)
			{
				this.target.localScale = this.initScale;
				float t = 0f;
				float maxT = this.animCurve.keys[this.animCurve.length - 1].time;
				while (t < maxT)
				{
					t += this.speed * Time.unscaledDeltaTime;
					this.target.localScale = Vector3.one * this.animCurve.Evaluate(t);
					yield return null;
				}
			}
			yield break;
		}

		private IEnumerator ScaleOUT()
		{
			if (this.animCurve.keys.Length > 0)
			{
				float t = 0f;
				float maxT = this.animCurve.keys[this.animCurve.length - 1].time;
				while (t < maxT)
				{
					t += this.speed * Time.unscaledDeltaTime;
					this.target.localScale = Vector3.one * this.animCurve.Evaluate(maxT - t);
					yield return null;
				}
				base.transform.localScale = this.initScale;
			}
			yield break;
		}

		public AnimationCurve animCurve;

		[Tooltip("Animation speed multiplier")]
		public float speed = 1f;

		private Vector3 initScale;

		public Transform target;

		private Selectable selectable;
	}
}
