using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ThrowSimulation : MonoBehaviour
{
	private void Start()
	{
		this.localPosition = this.Projectile.transform.localPosition;
		base.StartCoroutine(this.SimulateProjectile());
	}

	private IEnumerator SimulateProjectile()
	{
		yield return new WaitForSeconds(this.timeAttack);
		this.Target = PlaneIngameManager.current.CurrentTransformPlayer;
		base.gameObject.transform.parent = null;
		this.Projectile.position = this.Projectile.transform.position;
		float dx = this.Target.position.x - this.Projectile.position.x;
		float dy = this.Projectile.position.y - this.Target.position.y;
		float Vy = Mathf.Sqrt(this.maxY * 2f * this.gravity);
		float T = Vy / this.gravity;
		float T2 = Mathf.Sqrt(2f * (this.maxY + dy) / this.gravity);
		float flightDuration = T + T2;
		float Vx = dx / flightDuration;
		float elapse_time = 0f;
		while (elapse_time < flightDuration)
		{
			this.Projectile.Translate(Vx * Time.deltaTime, (Vy - this.gravity * elapse_time) * Time.deltaTime, 0f);
			elapse_time += Time.deltaTime;
			yield return null;
		}
		if (elapse_time >= flightDuration)
		{
			yield return new WaitForSeconds(this.timeReturn);
			base.StartCoroutine(this.ReturnObj());
		}
		yield break;
	}

	private IEnumerator ReturnObj()
	{
		base.gameObject.transform.SetParent(this.parentObj);
		this.Projectile.transform.DOLocalMove(this.localPosition, 1f, false).OnComplete(delegate
		{
			base.StartCoroutine(this.SimulateProjectile());
		});
		yield return null;
		yield break;
	}

	public Transform Target;

	public Transform Projectile;

	public Vector3 localPosition;

	public float gravity = 9.8f;

	public float maxY = 1f;

	public float timeAttack = 1f;

	public float timeReturn = 1f;

	public Transform parentObj;
}
