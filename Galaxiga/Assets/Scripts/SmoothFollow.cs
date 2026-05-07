using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
	private void Awake()
	{
		this.transform = base.gameObject.transform;
	}

	private void OnEnable()
	{
		this.timeChange = UnityEngine.Random.Range(this.smoothDampTime - 0.5f, this.smoothDampTime + 0.5f);
		this.smoothDampTimeX = UnityEngine.Random.Range(this.smoothDampTime - this.xRandom, this.smoothDampTime + this.xRandom);
		base.StartCoroutine(this.StartMove());
	}

	private IEnumerator StartMove()
	{
		yield return new WaitForSeconds(2f);
		this.stStart = true;
		this.isSmoothFollow = true;
		this.positionTarget = PlaneIngameManager.current.CurrentTransformPlayer.position;
		yield return null;
		yield break;
	}

	private void LateUpdate()
	{
		if (this.stStart && this.isSmoothFollow)
		{
			this.SetTimeCheck();
			if (this.isCheck)
			{
				this.isCheck = false;
				if (!this.ramdomSmooth)
				{
					if (PlaneIngameManager.current.CurrentTransformPlayer != null)
					{
						this.positionTarget = PlaneIngameManager.current.CurrentTransformPlayer.position;
					}
				}
				else
				{
					this.positionTarget = new Vector3(UnityEngine.Random.Range(this.minmaxX.x, this.minmaxX.y), UnityEngine.Random.Range(this.minmaxY.x, this.minmaxY.y), 0f);
				}
			}
			if (!this.useFixedUpdate)
			{
				this.updatePosition();
			}
		}
	}

	private void updatePosition()
	{
		this.smoothDampTimeX = UnityEngine.Random.Range(this.smoothDampTime - this.xRandom, this.smoothDampTime + this.xRandom);
		this.transform.position = Vector3.SmoothDamp(this.transform.position, this.positionTarget - this.cameraOffset, ref this._smoothDampVelocity, this.smoothDampTimeX);
	}

	public void SetTimeCheck()
	{
		if (Time.time > this.timeFire)
		{
			this.isCheck = true;
			this.timeFire = Time.time + this.timeChange;
		}
	}

	public bool isSmoothFollow;

	public float smoothDampTime = 0.2f;

	[HideInInspector]
	public new Transform transform;

	public Vector3 cameraOffset;

	public bool useFixedUpdate;

	public float xRandom = 2f;

	private float smoothDampTimeX;

	private Vector3 _smoothDampVelocity;

	[Header("CHECK MOVE RANDOM")]
	public float timeChange = 2f;

	public bool isCheck;

	private float timeFire;

	private Vector3 positionTarget = new Vector3(0f, 0f, 0f);

	public bool ramdomSmooth;

	[ShowIf("ramdomSmooth", true)]
	public Vector2 minmaxX = new Vector2(-5f, 5f);

	[ShowIf("ramdomSmooth", true)]
	public Vector2 minmaxY = new Vector2(-9f, 9f);

	private bool stStart;
}
