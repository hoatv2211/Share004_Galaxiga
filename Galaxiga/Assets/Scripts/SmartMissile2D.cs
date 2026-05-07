using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SmartMissile2D : SmartMissile<Rigidbody2D, Vector2>
{
	private void Awake()
	{
		this.m_rigidbody = base.GetComponent<Rigidbody2D>();
	}

	protected override Transform findNewTarget()
	{
		foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(base.transform.position, this.m_searchRange))
		{
			if (collider2D.gameObject.CompareTag(this.m_targetTag) && this.isWithinRange(collider2D.transform.position))
			{
				this.m_targetDistance = Vector2.Distance(collider2D.transform.position, base.transform.position);
				return collider2D.transform;
			}
		}
		return null;
	}

	protected override bool isWithinRange(Vector3 Coordinates)
	{
		return Vector2.Distance(Coordinates, base.transform.position) < this.m_targetDistance && Vector2.Angle(base.transform.forward, Coordinates - base.transform.position) < (float)(this.m_searchAngle / 2);
	}

	protected override void goToTarget()
	{
        m_direction = (m_target.position + (Vector3)m_targetOffset - base.transform.position).normalized * m_distanceInfluence.Evaluate(1f - (m_target.position + (Vector3)m_targetOffset - base.transform.position).magnitude / m_searchRange);
        m_rigidbody.linearVelocity = Vector2.ClampMagnitude(m_rigidbody.linearVelocity + m_direction * m_guidanceIntensity, m_rigidbody.linearVelocity.magnitude);
        if (m_rigidbody.linearVelocity != Vector2.zero)
        {
            base.transform.LookAt(new Vector3(m_rigidbody.linearVelocity.x, m_rigidbody.linearVelocity.y, base.transform.position.z));
        }
    }
}
