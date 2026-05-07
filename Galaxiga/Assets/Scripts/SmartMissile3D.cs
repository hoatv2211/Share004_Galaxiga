using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SmartMissile3D : SmartMissile<Rigidbody, Vector3>
{
	private void Awake()
	{
		this.m_rigidbody = base.GetComponent<Rigidbody>();
	}

	protected override Transform findNewTarget()
	{
		foreach (Collider collider in Physics.OverlapSphere(base.transform.position, this.m_searchRange))
		{
			if (collider.gameObject.CompareTag(this.m_targetTag) && this.isWithinRange(collider.transform.position))
			{
				this.m_targetDistance = Vector3.Distance(collider.transform.position, base.transform.position);
				return collider.transform;
			}
		}
		return null;
	}

	protected override bool isWithinRange(Vector3 Coordinates)
	{
		return Vector3.Distance(Coordinates, base.transform.position) < this.m_targetDistance && Vector3.Angle(base.transform.forward, Coordinates - base.transform.position) < (float)(this.m_searchAngle / 2);
	}

	protected override void goToTarget()
	{
		this.m_direction = (this.m_target.position + this.m_targetOffset - base.transform.position).normalized * this.m_distanceInfluence.Evaluate(1f - (this.m_target.position + this.m_targetOffset - base.transform.position).magnitude / this.m_searchRange);
		this.m_rigidbody.linearVelocity = Vector3.ClampMagnitude(this.m_rigidbody.linearVelocity + this.m_direction * this.m_guidanceIntensity, this.m_rigidbody.linearVelocity.magnitude);
		if (this.m_rigidbody.linearVelocity != Vector3.zero)
		{
			base.transform.LookAt(this.m_rigidbody.linearVelocity);
		}
	}
}
