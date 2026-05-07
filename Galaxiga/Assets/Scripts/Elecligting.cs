using System;
using UnityEngine;

public class Elecligting : MonoBehaviour
{
	private void Start()
	{
		this.lRend = base.GetComponent<LineRenderer>();
		this.points = new Vector3[this.pointsCount];
		this.lRend.positionCount = this.pointsCount;
	}

	private void Update()
	{
		this.CalculatePoints();
	}

	private void CalculatePoints()
	{
		this.timer += Time.deltaTime;
		if (this.timer > this.timerTimeOut)
		{
			this.timer = 0f;
			this.points[this.pointIndexA] = this.transformPointA.position;
			this.points[this.pointIndexE] = this.transformPointB.position;
			this.points[this.pointIndexC] = this.GetCenter(this.points[this.pointIndexA], this.points[this.pointIndexE]);
			this.points[this.pointIndexB] = this.GetCenter(this.points[this.pointIndexA], this.points[this.pointIndexC]);
			this.points[this.pointIndexD] = this.GetCenter(this.points[this.pointIndexC], this.points[this.pointIndexE]);
			float num = Vector3.Distance(this.transformPointA.position, this.transformPointB.position) / (float)this.points.Length;
			this.mainTextureScale.x = num;
			this.mainTextureOffset.x = UnityEngine.Random.Range(-this.randomness, this.randomness);
			this.lRend.material.SetTextureScale(this.mainTexture, this.mainTextureScale);
			this.lRend.material.SetTextureOffset(this.mainTexture, this.mainTextureOffset);
			this.randomness = num / (float)(this.pointsCount * this.half);
			this.SetRandomness();
			this.lRend.SetPositions(this.points);
		}
	}

	private void SetRandomness()
	{
		for (int i = 0; i < this.points.Length; i++)
		{
			if (i != this.pointIndexA && i != this.pointIndexE)
			{
				Vector3[] array = this.points;
				int num = i;
				array[num].x = array[num].x + UnityEngine.Random.Range(-this.randomness, this.randomness);
				Vector3[] array2 = this.points;
				int num2 = i;
				array2[num2].y = array2[num2].y + UnityEngine.Random.Range(-this.randomness, this.randomness);
				Vector3[] array3 = this.points;
				int num3 = i;
				array3[num3].z = array3[num3].z + UnityEngine.Random.Range(-this.randomness, this.randomness);
			}
		}
	}

	private Vector3 GetCenter(Vector3 a, Vector3 b)
	{
		return (a + b) / (float)this.half;
	}

	private LineRenderer lRend;

	public Transform transformPointA;

	public Transform transformPointB;

	private readonly int pointsCount = 5;

	private readonly int half = 2;

	private float randomness;

	private Vector3[] points;

	private readonly int pointIndexA;

	private readonly int pointIndexB = 1;

	private readonly int pointIndexC = 2;

	private readonly int pointIndexD = 3;

	private readonly int pointIndexE = 4;

	private readonly string mainTexture = "_MainTex";

	private Vector2 mainTextureScale = Vector2.one;

	private Vector2 mainTextureOffset = Vector2.one;

	private float timer;

	private float timerTimeOut = 0.05f;
}
