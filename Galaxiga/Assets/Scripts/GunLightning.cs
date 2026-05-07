using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hellmade.Sound;
using SkyGameKit;
using UnityEngine;

public class GunLightning : MonoBehaviour
{
	public List<BaseEnemy> listAliveEnemy
	{
		get
		{
			return SgkSingleton<LevelManager>.Instance.AliveEnemy;
		}
	}

	private void Awake()
	{
		this.minTimeLightningActive = this.timeLightningActive;
		this.maxTimeLightningDeactive = this.timeLightningDeactive;
		UnityEngine.Debug.Log(this.maxTimeLightningDeactive + " " + this.minTimeLightningActive);
	}

	private void Start()
	{
		if (GunLightning.bottomLeft.x == 0f)
		{
			GunLightning.topRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));
			GunLightning.bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane));
		}
		this.numberLightningActive = this.arrLightningTracker.Length;
	}

	public void SetPower(int _power, float _fireRate)
	{
		this.power = _power;
		this.fireRate = _fireRate;
		for (int i = 0; i < this.arrLightningTracker.Length; i++)
		{
			this.arrLightningTracker[i].SetPower(this.power, this.fireRate);
		}
	}

	public void StartFire()
	{
		if (this.coroutineStopFire != null)
		{
			base.StopCoroutine(this.coroutineStopFire);
		}
		this.coroutineStopFire = base.StartCoroutine("FireNow");
	}

	public void StopFire()
	{
		if (this.coroutineStopFire != null)
		{
			base.StopCoroutine(this.coroutineStopFire);
		}
		else
		{
			base.StopCoroutine("FireNow");
		}
		this.DeactiveAllLightning();
	}

	private IEnumerator FireNow()
	{
		for (;;)
		{
			if (!this.isSkillPlane)
			{
				this.CheckCurrentPowerPlane();
			}
			this.ActiveLightning();
			yield return new WaitForSeconds(this.timeLightningActive);
			this.DeactiveAllLightning();
			yield return new WaitForSeconds(this.timeLightningDeactive);
		}
		yield break;
	}

	private void ActiveLightning()
	{
		int num = 0;
		this.arrCurrentTargetEnemy = (from x in this.listAliveEnemy
		where !Fu.Outside(x.transform.position, GunLightning.bottomLeft, GunLightning.topRight)
		orderby Vector3.Distance(x.transform.position, this.transformPlayer.position)
		select x).Take(this.numberLightningActive).ToList<BaseEnemy>();
		int count = this.arrCurrentTargetEnemy.Count;
		if (count > 0)
		{
			int audioID = EazySoundManager.PlaySound(AudioCache.Sound.ShockLoop4);
			this.soundLightning = EazySoundManager.GetSoundAudio(audioID);
			if (count < this.numberLightningActive)
			{
				for (int i = 0; i < this.numberLightningActive; i++)
				{
					if (i < count)
					{
						this.SetLightning(i, this.arrCurrentTargetEnemy[i]);
					}
					else
					{
						this.SetLightning(i, this.arrCurrentTargetEnemy[UnityEngine.Random.Range(0, count)]);
					}
				}
			}
			else
			{
				foreach (BaseEnemy baseEnemy in this.arrCurrentTargetEnemy)
				{
					this.SetLightning(num, baseEnemy);
					num++;
				}
			}
		}
	}

	private void SetLightning(int index, BaseEnemy baseEnemy)
	{
		this.arrLightningTracker[index].baseEnemy = baseEnemy;
		this.arrEndTransforms[index].parent = baseEnemy.transform;
		this.arrEndTransforms[index].localPosition = Vector3.zero;
		this.arrObjLightning[index].SetActive(true);
	}

	private void DeactiveAllLightning()
	{
		if (this.soundLightning != null)
		{
			this.soundLightning.Stop();
		}
		for (int i = 0; i < this.numberLightningActive; i++)
		{
			this.arrEndTransforms[i].parent = null;
			this.arrObjLightning[i].SetActive(false);
			this.arrLightningTracker[i].baseEnemy = null;
		}
	}

	private void DeactiveLightning(int indexLightning)
	{
		this.arrEndTransforms[indexLightning].parent = null;
		this.arrObjLightning[indexLightning].SetActive(false);
		this.arrLightningTracker[indexLightning].baseEnemy = null;
	}

	public void GetRandomTarget(int indexLightning, BaseEnemy baseEnemy)
	{
		if (this.arrCurrentTargetEnemy.Contains(baseEnemy))
		{
			this.arrCurrentTargetEnemy.Remove(baseEnemy);
		}
		int count = this.arrCurrentTargetEnemy.Count;
		if (count > 0)
		{
			this.SetLightning(indexLightning, this.arrCurrentTargetEnemy[UnityEngine.Random.Range(0, count)]);
		}
		else if (this.listAliveEnemy.Count > 0)
		{
			this.SetLightning(indexLightning, this.listAliveEnemy[UnityEngine.Random.Range(0, this.listAliveEnemy.Count)]);
		}
	}

	private void CheckCurrentPowerPlane()
	{
		switch (GameContext.power)
		{
		case 5:
			this.timeLightningDeactive = this.maxTimeLightningDeactive * 0.65f;
			break;
		case 6:
			this.timeLightningDeactive = this.maxTimeLightningDeactive * 0.6f;
			break;
		case 7:
			this.timeLightningDeactive = this.maxTimeLightningDeactive * 0.55f;
			break;
		case 8:
			this.timeLightningDeactive = this.maxTimeLightningDeactive * 0.5f;
			break;
		case 9:
			this.timeLightningDeactive = this.maxTimeLightningDeactive * 0.5f;
			this.timeLightningActive = this.minTimeLightningActive + this.minTimeLightningActive * 0.25f;
			break;
		case 10:
			this.timeLightningDeactive = this.maxTimeLightningDeactive * 0.6f;
			this.timeLightningActive = this.minTimeLightningActive + this.minTimeLightningActive * 0.5f;
			break;
		}
	}

	private List<Collider2D> ConvertArrayToList(Collider2D[] arr)
	{
		return arr.ToList<Collider2D>();
	}

	public Transform[] arrEndTransforms;

	public GameObject[] arrObjLightning;

	public LightningPlayerTakeDamage[] arrLightningTracker;

	public Transform transformPlayer;

	public int power = 100;

	public float fireRate = 0.2f;

	public float timeLightningActive = 0.5f;

	public float timeLightningDeactive = 2f;

	public bool isSkillPlane;

	public LayerMask layerEnemy;

	private static Vector2 bottomLeft;

	private static Vector2 topRight;

	private int numberLightningActive = 1;

	private float minTimeLightningActive;

	private float maxTimeLightningDeactive;

	private Audio soundLightning;

	private Coroutine coroutineStopFire;

	private List<BaseEnemy> arrCurrentTargetEnemy;
}
