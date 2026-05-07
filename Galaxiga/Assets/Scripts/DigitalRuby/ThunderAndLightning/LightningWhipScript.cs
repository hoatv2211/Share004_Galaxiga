using System;
using System.Collections;
using UnityEngine;

namespace DigitalRuby.ThunderAndLightning
{
	[RequireComponent(typeof(AudioSource))]
	public class LightningWhipScript : MonoBehaviour
	{
		private IEnumerator WhipForward()
		{
			if (this.canWhip)
			{
				this.canWhip = false;
				for (int i = 0; i < this.whipStart.transform.childCount; i++)
				{
					GameObject gameObject = this.whipStart.transform.GetChild(i).gameObject;
					Rigidbody2D component = gameObject.GetComponent<Rigidbody2D>();
					if (component != null)
					{
						component.linearDamping = 0f;
					}
				}
				this.audioSource.PlayOneShot(this.WhipCrack);
				this.whipSpring.GetComponent<SpringJoint2D>().enabled = true;
				this.whipSpring.GetComponent<Rigidbody2D>().position = this.whipHandle.GetComponent<Rigidbody2D>().position + new Vector2(-15f, 5f);
				yield return new WaitForSeconds(0.2f);
				this.whipSpring.GetComponent<Rigidbody2D>().position = this.whipHandle.GetComponent<Rigidbody2D>().position + new Vector2(15f, 2.5f);
				yield return new WaitForSeconds(0.15f);
				this.audioSource.PlayOneShot(this.WhipCrackThunder, 0.5f);
				yield return new WaitForSeconds(0.15f);
				this.whipEndStrike.GetComponent<ParticleSystem>().Play();
				this.whipSpring.GetComponent<SpringJoint2D>().enabled = false;
				yield return new WaitForSeconds(0.65f);
				for (int j = 0; j < this.whipStart.transform.childCount; j++)
				{
					GameObject gameObject2 = this.whipStart.transform.GetChild(j).gameObject;
					Rigidbody2D component2 = gameObject2.GetComponent<Rigidbody2D>();
					if (component2 != null)
					{
						component2.linearVelocity = Vector2.zero;
						component2.linearDamping = 0.5f;
					}
				}
				this.canWhip = true;
			}
			yield break;
		}

		private void Start()
		{
			this.whipStart = GameObject.Find("WhipStart");
			this.whipEndStrike = GameObject.Find("WhipEndStrike");
			this.whipHandle = GameObject.Find("WhipHandle");
			this.whipSpring = GameObject.Find("WhipSpring");
			this.audioSource = base.GetComponent<AudioSource>();
		}

		private void Update()
		{
			if (!this.dragging && Input.GetMouseButtonDown(0))
			{
				Vector2 point = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
				Collider2D collider2D = Physics2D.OverlapPoint(point);
				if (collider2D != null && collider2D.gameObject == this.whipHandle)
				{
					this.dragging = true;
					this.prevDrag = point;
				}
			}
			else if (this.dragging && Input.GetMouseButton(0))
			{
				Vector2 a = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
				Vector2 b = a - this.prevDrag;
				Rigidbody2D component = this.whipHandle.GetComponent<Rigidbody2D>();
				component.MovePosition(component.position + b);
				this.prevDrag = a;
			}
			else
			{
				this.dragging = false;
			}
			if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
			{
				base.StartCoroutine(this.WhipForward());
			}
		}

		public AudioClip WhipCrack;

		public AudioClip WhipCrackThunder;

		private AudioSource audioSource;

		private GameObject whipStart;

		private GameObject whipEndStrike;

		private GameObject whipHandle;

		private GameObject whipSpring;

		private Vector2 prevDrag;

		private bool dragging;

		private bool canWhip = true;
	}
}
