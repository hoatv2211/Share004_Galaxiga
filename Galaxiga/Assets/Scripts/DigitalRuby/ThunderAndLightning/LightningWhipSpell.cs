using System;
using System.Collections;
using UnityEngine;

namespace DigitalRuby.ThunderAndLightning
{
	public class LightningWhipSpell : LightningSpellScript
	{
		private IEnumerator WhipForward()
		{
			for (int i = 0; i < this.WhipStart.transform.childCount; i++)
			{
				GameObject gameObject = this.WhipStart.transform.GetChild(i).gameObject;
				Rigidbody component = gameObject.GetComponent<Rigidbody>();
				if (component != null)
				{
					component.linearDamping = 0f;
					component.linearVelocity = Vector3.zero;
					component.angularVelocity = Vector3.zero;
				}
			}
			this.WhipSpring.SetActive(true);
			Vector3 anchor = this.WhipStart.GetComponent<Rigidbody>().position;
			RaycastHit hit;
			Vector3 whipPositionForwards;
			Vector3 whipPositionBackwards;
			if (Physics.Raycast(anchor, this.Direction, out hit, this.MaxDistance, this.CollisionMask))
			{
				Vector3 normalized = (hit.point - anchor).normalized;
				whipPositionForwards = anchor + normalized * this.MaxDistance;
				whipPositionBackwards = anchor - normalized * 25f;
			}
			else
			{
				whipPositionForwards = anchor + this.Direction * this.MaxDistance;
				whipPositionBackwards = anchor - this.Direction * 25f;
			}
			this.WhipSpring.GetComponent<Rigidbody>().position = whipPositionBackwards;
			yield return new WaitForSeconds(0.25f);
			this.WhipSpring.GetComponent<Rigidbody>().position = whipPositionForwards;
			yield return new WaitForSeconds(0.1f);
			if (this.WhipCrackAudioSource != null)
			{
				this.WhipCrackAudioSource.Play();
			}
			yield return new WaitForSeconds(0.1f);
			if (this.CollisionParticleSystem != null)
			{
				this.CollisionParticleSystem.Play();
			}
			base.ApplyCollisionForce(this.SpellEnd.transform.position);
			this.WhipSpring.SetActive(false);
			if (this.CollisionCallback != null)
			{
				this.CollisionCallback(this.SpellEnd.transform.position);
			}
			yield return new WaitForSeconds(0.1f);
			for (int j = 0; j < this.WhipStart.transform.childCount; j++)
			{
				GameObject gameObject2 = this.WhipStart.transform.GetChild(j).gameObject;
				Rigidbody component2 = gameObject2.GetComponent<Rigidbody>();
				if (component2 != null)
				{
					component2.linearVelocity = Vector3.zero;
					component2.angularVelocity = Vector3.zero;
					component2.linearDamping = 0.5f;
				}
			}
			yield break;
		}

		protected override void Start()
		{
			base.Start();
			this.WhipSpring.SetActive(false);
			this.WhipHandle.SetActive(false);
		}

		protected override void Update()
		{
			base.Update();
			base.gameObject.transform.position = this.AttachTo.transform.position;
			base.gameObject.transform.rotation = this.RotateWith.transform.rotation;
		}

		protected override void OnCastSpell()
		{
			base.StartCoroutine(this.WhipForward());
		}

		protected override void OnStopSpell()
		{
		}

		protected override void OnActivated()
		{
			base.OnActivated();
			this.WhipHandle.SetActive(true);
		}

		protected override void OnDeactivated()
		{
			base.OnDeactivated();
			this.WhipHandle.SetActive(false);
		}

		[Header("Whip")]
		[Tooltip("Attach the whip to what object")]
		public GameObject AttachTo;

		[Tooltip("Rotate the whip with this object")]
		public GameObject RotateWith;

		[Tooltip("Whip handle")]
		public GameObject WhipHandle;

		[Tooltip("Whip start")]
		public GameObject WhipStart;

		[Tooltip("Whip spring")]
		public GameObject WhipSpring;

		[Tooltip("Whip crack audio source")]
		public AudioSource WhipCrackAudioSource;

		[HideInInspector]
		public Action<Vector3> CollisionCallback;
	}
}
