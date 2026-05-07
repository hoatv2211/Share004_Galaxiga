using System;
using System.Collections;
using Coffee.UIExtensions;
using UnityEngine;

public class ShinyEffectForUGUIController : MonoBehaviour
{
	private void Awake()
	{
		this.shinyEffect = base.GetComponent<ShinyEffectForUGUI>();
	}

	private void OnEnable()
	{
		this.coroutineShiny = base.StartCoroutine(this.Shiny());
	}

	private IEnumerator Shiny()
	{
		float temp = 0f;
		this.shinyEffect.location = temp;
		while (temp < 1f)
		{
			yield return new WaitForSeconds(Time.deltaTime / (float)this.speed);
			temp += Time.deltaTime;
			this.shinyEffect.location = temp;
		}
		yield return new WaitForSeconds(this.delayTime);
		this.coroutineShiny = base.StartCoroutine(this.Shiny());
		yield break;
	}

	private void OnDisable()
	{
		base.StopCoroutine(this.coroutineShiny);
	}

	[SerializeField]
	private int speed = 2;

	[SerializeField]
	private float delayTime = 1.5f;

	private ShinyEffectForUGUI shinyEffect;

	private Coroutine coroutineShiny;
}
