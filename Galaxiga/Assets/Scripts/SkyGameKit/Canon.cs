using System;
using UnityEngine;

namespace SkyGameKit
{
	public class Canon : MonoBehaviour
	{
		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
			{
				this.Fire();
			}
		}

		private void Fire()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_projectile, base.transform.position, base.transform.rotation);
			if (gameObject.GetComponent<Rigidbody2D>())
			{
				gameObject.GetComponent<Rigidbody2D>().AddForce(base.transform.forward * this.m_launchIntensity, ForceMode2D.Impulse);
			}
			else if (gameObject.GetComponent<Rigidbody>())
			{
				gameObject.GetComponent<Rigidbody>().AddForce(base.transform.forward * this.m_launchIntensity, ForceMode.Impulse);
			}
		}

		[SerializeField]
		[Range(0f, 10f)]
		private float m_launchIntensity;

		[SerializeField]
		private GameObject m_projectile;
	}
}
