using System;
using System.Collections;
using UnityEngine;

public class UbhShotCtrl_NewWaveSin : MonoBehaviour
{
	private void OnEnable()
	{
		base.StartCoroutine(this.WaitStart());
	}

	private void Update()
	{
		if (this.isFly)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, new Vector3(this._startPosition.x + Mathf.Sin(this.speed * Time.time) * this.fluctuationSinWave, base.transform.position.y, base.transform.position.z), 0.2f);
		}
	}

	private IEnumerator WaitStart()
	{
		this.isFly = false;
		this.speed = UnityEngine.Random.Range(-this.speedSinWave, this.speedSinWave);
		this._startPosition = base.transform.position;
		yield return new WaitForSeconds(0.01f);
		this.isFly = true;
		yield break;
	}

	private bool isFly;

	private Vector3 _startPosition;

	private float speed;

	public float speedSinWave = 2f;

	public float fluctuationSinWave = 2f;
}
