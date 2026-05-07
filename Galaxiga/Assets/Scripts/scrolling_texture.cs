using System;
using UnityEngine;

[Serializable]
public class scrolling_texture : MonoBehaviour
{
	public scrolling_texture()
	{
		this.scrollSpeed = 0.9f;
		this.scrollSpeed2 = 0.9f;
	}

	public virtual void FixedUpdate()
	{
		float num = Time.time * this.scrollSpeed;
		float x = Time.time * this.scrollSpeed2;
		this.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x, -num);
	}

	public virtual void Main()
	{
	}

	public float scrollSpeed;

	public float scrollSpeed2;
}
