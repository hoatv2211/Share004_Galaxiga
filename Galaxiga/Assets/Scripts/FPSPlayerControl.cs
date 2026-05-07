using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPSPlayerControl : MonoBehaviour
{
	private void Awake()
	{
		this.anim = base.GetComponentInChildren<Animator>();
		this.audioSource = base.GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (ETCInput.GetButton("Fire") && !this.inFire && this.armoCount > 0 && !this.inReload)
		{
			this.inFire = true;
			this.anim.SetBool("Shoot", true);
			base.InvokeRepeating("GunFire", 0.12f, 0.12f);
			this.GunFire();
		}
		if (ETCInput.GetButtonDown("Fire") && this.armoCount == 0 && !this.inReload)
		{
			this.audioSource.PlayOneShot(this.needReload, 1f);
		}
		if (ETCInput.GetButtonUp("Fire"))
		{
			this.anim.SetBool("Shoot", false);
			this.muzzleEffect.SetActive(false);
			this.inFire = false;
			base.CancelInvoke();
		}
		if (ETCInput.GetButtonDown("Reload"))
		{
			this.inReload = true;
			this.audioSource.PlayOneShot(this.reload, 1f);
			this.anim.SetBool("Reload", true);
			base.StartCoroutine(this.Reload());
		}
		if (ETCInput.GetButtonDown("Back"))
		{
			base.transform.Rotate(Vector3.up * 180f);
		}
		this.armoText.text = this.armoCount.ToString();
	}

	public void MoveStart()
	{
		this.anim.SetBool("Move", true);
	}

	public void MoveStop()
	{
		this.anim.SetBool("Move", false);
	}

	public void GunFire()
	{
		if (this.armoCount > 0)
		{
			this.muzzleEffect.transform.Rotate(Vector3.forward * UnityEngine.Random.Range(0f, 360f));
			this.muzzleEffect.transform.localScale = new Vector3(UnityEngine.Random.Range(0.1f, 0.2f), UnityEngine.Random.Range(0.1f, 0.2f), 1f);
			this.muzzleEffect.SetActive(true);
			base.StartCoroutine(this.Flash());
			this.audioSource.PlayOneShot(this.gunSound, 1f);
			this.shellParticle.Emit(1);
			Vector3 vector = new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f);
			vector += new Vector3((float)UnityEngine.Random.Range(-10, 10), (float)UnityEngine.Random.Range(-10, 10), 0f);
			Ray ray = Camera.main.ScreenPointToRay(vector);
			RaycastHit[] array = Physics.RaycastAll(ray);
			if (array.Length > 0)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.impactEffect, array[0].point - array[0].normal * -0.2f, Quaternion.identity);
			}
		}
		else
		{
			this.anim.SetBool("Shoot", false);
			this.muzzleEffect.SetActive(false);
			this.inFire = false;
		}
		this.armoCount--;
		if (this.armoCount < 0)
		{
			this.armoCount = 0;
		}
	}

	public void TouchPadSwipe(bool value)
	{
		ETCInput.SetControlSwipeIn("FreeLookTouchPad", value);
	}

	private IEnumerator Flash()
	{
		yield return new WaitForSeconds(0.08f);
		this.muzzleEffect.SetActive(false);
		yield break;
	}

	private IEnumerator Reload()
	{
		yield return new WaitForSeconds(0.5f);
		this.armoCount = 30;
		this.inReload = false;
		this.anim.SetBool("Reload", false);
		yield break;
	}

	public AudioClip gunSound;

	public AudioClip reload;

	public AudioClip needReload;

	public ParticleSystem shellParticle;

	public GameObject muzzleEffect;

	public GameObject impactEffect;

	public Text armoText;

	private bool inFire;

	private bool inReload;

	private Animator anim;

	private int armoCount = 30;

	private AudioSource audioSource;
}
