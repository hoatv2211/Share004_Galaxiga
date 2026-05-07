using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class SmartMissile<RgbdType, VecType> : SmartMissile
{
	public string TargetTag
	{
		get
		{
			return this.m_targetTag;
		}
		set
		{
			this.m_targetTag = value;
		}
	}

	private void Start()
	{
		this.m_targetDistance = this.m_searchRange;
		if (this.m_lifeTime > 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject, this.m_lifeTime);
		}
	}

	private void FixedUpdate()
	{
		if (this.m_target != null)
		{
			if (this.m_canLooseTarget && !this.isWithinRange(this.m_target.transform.position))
			{
				this.m_target = null;
				this.m_targetDistance = this.m_searchRange;
				this.m_onTargetLost.Invoke();
			}
			else
			{
				this.goToTarget();
			}
		}
		else if (this.m_target = this.findNewTarget())
		{
			this.m_onNewTargetFound.Invoke();
		}
	}

	protected abstract Transform findNewTarget();

	protected abstract bool isWithinRange(Vector3 coordinates);

	protected abstract void goToTarget();

	[Header("Missile")]
	[SerializeField]
	[Tooltip("In seconds, 0 for infinite lifetime.")]
	private float m_lifeTime = 5f;

	[SerializeField]
	private UnityEvent m_onNewTargetFound;

	[SerializeField]
	private UnityEvent m_onTargetLost;

	[Header("Detection")]
	[SerializeField]
	[Tooltip("Range within the missile will search a new target.")]
	protected float m_searchRange = 10f;

	[SerializeField]
	[Range(0f, 360f)]
	protected int m_searchAngle = 90;

	[SerializeField]
	[Tooltip("If enabled, target is lost when out of the range.")]
	private bool m_canLooseTarget = true;

	[Header("Guidance")]
	[SerializeField]
	[Tooltip("Intensity the missile will be guided with.")]
	protected float m_guidanceIntensity = 5f;

	[SerializeField]
	[Tooltip("Increase the intensity depending on the ditance.")]
	protected AnimationCurve m_distanceInfluence = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	[Header("Target")]
	[SerializeField]
	[Tooltip("Use this if the center of the object is not what you target.")]
	protected VecType m_targetOffset;

	[SerializeField]
	[HideInInspector]
	protected string m_targetTag = "Untagged";

	[Header("Debug")]
	[SerializeField]
	[Tooltip("Color of the search zone.")]
	[HideInInspector]
	protected Color m_zoneColor = new Color(255f, 0f, 155f, 0.1f);

	[SerializeField]
	[Tooltip("Color of the line to the target.")]
	[HideInInspector]
	protected Color m_lineColor = new Color(255f, 0f, 155f, 1f);

	[SerializeField]
	[Tooltip("Draw the search zone.")]
	[HideInInspector]
	protected bool m_drawSearchZone;

	protected RgbdType m_rigidbody;

	protected Transform m_target;

	protected float m_targetDistance;

	protected VecType m_direction;
}
