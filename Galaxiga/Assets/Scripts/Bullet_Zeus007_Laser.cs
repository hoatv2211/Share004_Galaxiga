using System;
using System.Collections;
using UnityEngine;

public class Bullet_Zeus007_Laser : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.delay());
	}

	private IEnumerator delay()
	{
		for (int i = 0; i < this.listBox.Length; i++)
		{
			if (!this.listBox[i].activeInHierarchy)
			{
				this.listBox[i].SetActive(true);
			}
		}
		yield return new WaitForSeconds(0.1f);
		base.StartCoroutine(this.delay());
		yield break;
	}

	[SerializeField]
	private GameObject[] listBox;

	private bool isCheck;
}
