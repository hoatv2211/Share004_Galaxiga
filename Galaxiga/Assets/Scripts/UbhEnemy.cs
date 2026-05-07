using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(UbhSpaceship))]
public class UbhEnemy : UbhMonoBehaviour
{
	private void Start()
	{
		this.m_spaceship = base.GetComponent<UbhSpaceship>();
		this.Move(base.transform.up * -1f);
	}

	private void FixedUpdate()
	{
		if (this.m_useStop && base.transform.position.y < this.m_stopPoint)
		{
			base.rigidbody2D.linearVelocity = UbhUtil.VECTOR2_ZERO;
			this.m_useStop = false;
		}
	}

	public void Move(Vector2 direction)
	{
		base.rigidbody2D.linearVelocity = direction * this.m_spaceship.m_speed;
	}

	private void OnTriggerEnter2D(Collider2D c)
	{
		if (c.name.Contains("PlayerBullet"))
		{
			UbhPlayerBullet component = c.transform.parent.GetComponent<UbhPlayerBullet>();
			if (component != null && component.isActive)
			{
				UbhSingletonMonoBehavior<UbhObjectPool>.instance.ReleaseBullet(component, false);
				this.m_hp -= component.m_power;
				if (this.m_hp <= 0)
				{
					UnityEngine.Object.FindObjectOfType<UbhScore>().AddPoint(this.m_point);
					this.m_spaceship.Explosion();
					UnityEngine.Object.Destroy(base.gameObject);
				}
				else
				{
					this.m_spaceship.GetAnimator().SetTrigger("Damage");
				}
			}
		}
	}

	public const string NAME_PLAYER = "Player";

	public const string NAME_PLAYER_BULLET = "PlayerBullet";

	private const string ANIM_DAMAGE_TRIGGER = "Damage";

	[SerializeField]
	[FormerlySerializedAs("_Hp")]
	private int m_hp = 1;

	[SerializeField]
	[FormerlySerializedAs("_Point")]
	private int m_point = 100;

	[SerializeField]
	[FormerlySerializedAs("_UseStop")]
	private bool m_useStop;

	[SerializeField]
	[FormerlySerializedAs("_StopPoint")]
	private float m_stopPoint = 2f;

	private UbhSpaceship m_spaceship;
}
