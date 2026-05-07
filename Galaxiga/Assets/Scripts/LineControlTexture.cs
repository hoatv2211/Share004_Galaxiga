using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineControlTexture : MonoBehaviour
{
	protected virtual void Start()
	{
		this.line = base.GetComponent<LineRenderer>();
	}

	protected virtual void Update()
	{
		if (this.line.positionCount > 1)
		{
			this.offset -= Time.deltaTime * this.speed;
			this.line.material.mainTextureOffset = new Vector2(this.offset, 0f);
			this.scale = Vector3.Distance(this.line.GetPosition(0), this.line.GetPosition(1)) / this.textureRatio;
			this.line.material.mainTextureScale = new Vector2(this.scale, 1f);
		}
	}

	private LineRenderer line;

	private float scale;

	private float offset;

	public float textureRatio = 4f;

	public float speed = 1f;
}
