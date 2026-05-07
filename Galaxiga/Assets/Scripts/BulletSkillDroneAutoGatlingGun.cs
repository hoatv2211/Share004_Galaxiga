using System;
using DG.Tweening;
using Hellmade.Sound;
using UnityEngine;

public class BulletSkillDroneAutoGatlingGun : MonoBehaviour
{
	public void StartSkill()
	{
		float x = base.gameObject.transform.position.x;
		float y = -GameContext.orthorgrhicSize + 2f;
		Vector2 v = new Vector2(x, y);
		TweenParams tweenParams = new TweenParams().SetLoops(1, null).SetEase(Ease.InOutQuad, null, null).OnComplete(new TweenCallback(this.MoveComplete)).SetUpdate(true);
		base.transform.DOMove(v, 0.15f, false).SetAs(tweenParams);
	}

	private void MoveComplete()
	{
		this.SpawnHomingMissile();
	}

	public void SpawnHomingMissile()
	{
		GameUtil.ObjectPoolSpawn("ExplosionPool", "Fx_Enemy_Die", base.transform.position, Quaternion.identity);
		EazySoundManager.PlaySound(AudioCache.Sound.enemy_die2);
		float z = base.transform.localRotation.z;
		this.SpawnMissile(base.gameObject.transform, Quaternion.Euler(0f, 0f, z - 30f));
		this.SpawnMissile(base.gameObject.transform, Quaternion.Euler(0f, 0f, z - 15f));
		this.SpawnMissile(base.gameObject.transform, Quaternion.Euler(0f, 0f, z));
		this.SpawnMissile(base.gameObject.transform, Quaternion.Euler(0f, 0f, z + 15f));
		this.SpawnMissile(base.gameObject.transform, Quaternion.Euler(0f, 0f, z + 30f));
		GameUtil.ObjectPoolDespawn("BulletDrone", base.gameObject);
	}

	private void SpawnMissile(Transform pos, Quaternion quaternion)
	{
		if (GameContext.isMissionComplete || GameContext.isMissionFail)
		{
			return;
		}
		GameObject gameObject = GameUtil.ObjectPoolSpawnGameObject("BulletDrone", "Homing Missile Skill Auto Gatling Gun", pos.position, quaternion);
		HomingMissile component = gameObject.GetComponent<HomingMissile>();
		component.SetFollowTarget();
	}
}
