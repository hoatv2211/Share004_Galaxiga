using System;
using Hellmade.Sound;
using PathologicalGames;
using Sirenix.OdinInspector;
using SkyGameKit;
using UnityEngine;

public class ItemGold : MonoBehaviour
{
	protected virtual void OnTriggerStay2D(Collider2D c)
	{
		if (!this.goToPlayer && c.CompareTag("Get_Item"))
		{
			this.goToPlayer = true;
		}
	}

	protected virtual void OnTriggerEnter2D(Collider2D c)
	{
		if (c.name.Equals("BulletCheck"))
		{
			this.OnItemHitPlayer();
			this.Despawn();
		}
		else if (!this.goToPlayer && c.CompareTag("Get_Item"))
		{
			this.goToPlayer = true;
		}
	}

	public virtual void Despawn()
	{
		if (PoolManager.Pools["ItemPool"].IsSpawned(base.transform))
		{
			PoolManager.Pools["ItemPool"].Despawn(base.transform);
		}
	}

	private void OnItemHitPlayer()
	{
		EazySoundManager.PlaySound(AudioCache.Sound.get_coin);
		SgkSingleton<LevelInfo>.Instance.totalcoinadd++;
		if (!PlaneIngameManager.current.currentPlayerController.immortal)
		{
			SgkSingleton<LevelInfo>.Instance.percentCoin = (float)SgkSingleton<LevelInfo>.Instance.totalcoinadd * 1f / (float)SgkSingleton<LevelInfo>.Instance.totalcoinDefault;
			GameContext.percentCoinLevel = SgkSingleton<LevelInfo>.Instance.percentCoin;
		}
	}

	protected virtual void OnSpawned()
	{
		if (ItemGold.topRight.x < 0.01f)
		{
			ItemGold.bottomLeft = SgkCamera.bottomLeft;
			ItemGold.bottomLeft.x = ItemGold.bottomLeft.x + this.coinWidth;
			ItemGold.topRight = SgkCamera.topRight;
			ItemGold.topRight.x = ItemGold.topRight.x - this.coinWidth;
			ItemGold.screenWidth = ItemGold.topRight.x - ItemGold.bottomLeft.x;
		}
		this.goToPlayer = false;
		float d = UnityEngine.Random.Range(this.force.x, this.force.y);
		float degrees = UnityEngine.Random.Range(this.angle.x, this.angle.y);
		this.speed = Fu.RotateVector2(Vector2.up * d, degrees) * this.fallSpeed;
		this.realPosX = base.transform.position.x - ItemGold.bottomLeft.x;
		this.position.y = base.transform.position.y;
		this.bounceCount = this.bounce;
		this.moveToPlayerSpeedTmp = this.moveToPlayerSpeed;
	}

	protected virtual void Update()
	{
		if (this.goToPlayer)
		{
			this.moveToPlayerSpeedTmp += 30f * Time.deltaTime;
			base.transform.position = Vector3.MoveTowards(base.transform.position, PlaneIngameManager.current.CurrentTransformPlayer.position, this.moveToPlayerSpeedTmp * Time.deltaTime);
			return;
		}
		this.realPosX += this.speed.x * Time.deltaTime;
		this.position.x = Mathf.PingPong(this.realPosX, ItemGold.screenWidth) + ItemGold.bottomLeft.x;
		this.speed.y = this.speed.y - this.gravity * Time.deltaTime;
		this.position.y = this.position.y + this.speed.y * Time.deltaTime;
		base.transform.position = this.position;
		if (base.transform.position.y < ItemGold.bottomLeft.y + this.coinWidth && this.speed.y < 0f && this.bounceCount > 0)
		{
			this.speed.y = -this.bounceForce * this.speed.y;
			this.bounceCount--;
		}
		if (base.transform.position.y < ItemGold.bottomLeft.y - this.coinWidth)
		{
			this.Despawn();
		}
	}

	public float coinWidth = 0.3f;

	public float fallSpeed = 1.5f;

	public float moveToPlayerSpeed = 10f;

	public int bounce = 2;

	public float bounceForce = 0.4f;

	public float gravity = 6f;

	[MinMaxSlider(0f, 10f, false)]
	public Vector2 force;

	[MinMaxSlider(-45f, 45f, false)]
	public Vector2 angle;

	private static Vector2 bottomLeft;

	private static Vector2 topRight;

	private static float screenWidth;

	private Vector2 speed;

	private Vector2 position;

	private float realPosX;

	public bool goToPlayer;

	private int bounceCount;

	private float moveToPlayerSpeedTmp;
}
