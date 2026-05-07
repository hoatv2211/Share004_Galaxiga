using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
	private void Awake()
	{
		HealthBarManager.current = this;
	}

	private void Start()
	{
	}

	public void SetHP(int numberHP)
	{
		this.totalHP = numberHP;
	}

	public void SetBar(int currentHP)
	{
		float num = (float)currentHP / (float)this.totalHP;
		UnityEngine.Debug.Log(num);
		this.imgBarHP.fillAmount = num;
	}

	public static HealthBarManager current;

	public Image imgBarHP;

	private int totalHP;
}
