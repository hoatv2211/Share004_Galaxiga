using System;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
	private void Awake()
	{
		this.particle = base.GetComponent<ParticleSystem>();
	}

	private void OnEnable()
	{
	}

	private void OnBecameVisible()
	{
		if (this.particle != null)
		{
			this.particle.Play();
		}
		base.Invoke("DestroyObj", this.setTime);
	}

	public void DestroyObj()
	{
		if (!this.particlePool)
		{
			this.DisableObj();
		}
		else if (base.gameObject.activeInHierarchy)
		{
			GameUtil.ObjectPoolDespawn(this.objectPoolName, base.gameObject);
		}
	}

	public void DisableObj()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(false);
		}
	}

	private ParticleSystem particle;

	public bool particlePool;

	public float setTime = 1f;

	public string objectPoolName = string.Empty;
}
