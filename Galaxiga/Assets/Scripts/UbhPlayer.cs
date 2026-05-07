using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(UbhSpaceship))]
public class UbhPlayer : UbhMonoBehaviour
{
	private void Start()
	{
		this.m_spaceship = base.GetComponent<UbhSpaceship>();
		this.m_manager = UnityEngine.Object.FindObjectOfType<UbhGameManager>();
		this.m_backgroundTransform = UnityEngine.Object.FindObjectOfType<UbhBackground>().transform;
	}

	private void Update()
	{
		if (UbhUtil.IsMobilePlatform())
		{
			this.TouchMove();
		}
		else
		{
			this.KeyMove();
		}
	}

	private void KeyMove()
	{
		this.m_tempVector2.x = UnityEngine.Input.GetAxisRaw("Horizontal");
		this.m_tempVector2.y = UnityEngine.Input.GetAxisRaw("Vertical");
		this.Move(this.m_tempVector2.normalized);
	}

	private void TouchMove()
	{
		float num = 0f;
		float num2 = 0f;
		if (Input.GetMouseButtonDown(0))
		{
			this.m_isTouch = true;
			Vector3 vector = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			num = vector.x;
			num2 = vector.y;
		}
		else if (Input.GetMouseButton(0))
		{
			Vector3 vector2 = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			num = vector2.x;
			num2 = vector2.y;
			if (this.m_isTouch)
			{
				this.m_tempVector2.x = (num - this.m_lastXpos) * 10f;
				this.m_tempVector2.y = (num2 - this.m_lastYpos) * 10f;
				this.Move(this.m_tempVector2.normalized);
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			this.m_isTouch = false;
		}
		this.m_lastXpos = num;
		this.m_lastYpos = num2;
	}

	private void Move(Vector2 direction)
	{
		Vector2 vector;
		Vector2 vector2;
		if (this.m_manager != null && this.m_manager.m_scaleToFit)
		{
			vector = Camera.main.ViewportToWorldPoint(this.VIEW_PORT_LEFT_BOTTOM);
			vector2 = Camera.main.ViewportToWorldPoint(this.VIEW_PORT_RIGHT_TOP);
		}
		else
		{
			Vector2 a = this.m_backgroundTransform.localScale;
			vector = a * -0.5f;
			vector2 = a * 0.5f;
		}
		Vector2 vector3 = base.transform.position;
		if (this.m_useAxis == UbhUtil.AXIS.X_AND_Z)
		{
			vector3.y = base.transform.position.z;
		}
		vector3 += direction * this.m_spaceship.m_speed * UbhSingletonMonoBehavior<UbhTimer>.instance.deltaTime;
		vector3.x = Mathf.Clamp(vector3.x, vector.x, vector2.x);
		vector3.y = Mathf.Clamp(vector3.y, vector.y, vector2.y);
		if (this.m_useAxis == UbhUtil.AXIS.X_AND_Z)
		{
			base.transform.SetPosition(vector3.x, base.transform.position.y, vector3.y);
		}
		else
		{
			base.transform.position = vector3;
		}
	}

	private void Damage()
	{
		if (this.m_manager != null)
		{
			this.m_manager.GameOver();
		}
		this.m_spaceship.Explosion();
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D c)
	{
		this.HitCheck(c.transform);
	}

	private void OnTriggerEnter(Collider c)
	{
		this.HitCheck(c.transform);
	}

	private void HitCheck(Transform colTrans)
	{
		string name = colTrans.name;
		if (name.Contains("EnemyBullet"))
		{
			UbhBullet componentInParent = colTrans.GetComponentInParent<UbhBullet>();
			if (componentInParent.isActive)
			{
				UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(componentInParent, false);
				this.Damage();
			}
		}
		else if (name.Contains("Enemy"))
		{
			this.Damage();
		}
	}

	public const string NAME_ENEMY_BULLET = "EnemyBullet";

	public const string NAME_ENEMY = "Enemy";

	private const string AXIS_HORIZONTAL = "Horizontal";

	private const string AXIS_VERTICAL = "Vertical";

	private readonly Vector2 VIEW_PORT_LEFT_BOTTOM = UbhUtil.VECTOR2_ZERO;

	private readonly Vector2 VIEW_PORT_RIGHT_TOP = UbhUtil.VECTOR2_ONE;

	[SerializeField]
	[FormerlySerializedAs("_UseAxis")]
	private UbhUtil.AXIS m_useAxis;

	private UbhSpaceship m_spaceship;

	private UbhGameManager m_manager;

	private Transform m_backgroundTransform;

	private bool m_isTouch;

	private float m_lastXpos;

	private float m_lastYpos;

	private Vector2 m_tempVector2 = UbhUtil.VECTOR2_ZERO;
}
