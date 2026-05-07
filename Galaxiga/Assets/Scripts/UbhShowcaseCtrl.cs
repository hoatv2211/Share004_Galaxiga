using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UbhShowcaseCtrl : MonoBehaviour
{
	private void Start()
	{
		if (this.m_goShotCtrlList != null)
		{
			for (int i = 0; i < this.m_goShotCtrlList.Length; i++)
			{
				this.m_goShotCtrlList[i].SetActive(false);
			}
		}
		this.m_nowIndex = -1;
		this.ChangeShot(true);
	}

	public void ChangeShot(bool toNext)
	{
		if (this.m_goShotCtrlList == null)
		{
			return;
		}
		base.StopAllCoroutines();
		if (0 <= this.m_nowIndex && this.m_nowIndex < this.m_goShotCtrlList.Length)
		{
			this.m_goShotCtrlList[this.m_nowIndex].SetActive(false);
		}
		if (toNext)
		{
			this.m_nowIndex = (int)Mathf.Repeat((float)this.m_nowIndex + 1f, (float)this.m_goShotCtrlList.Length);
		}
		else
		{
			this.m_nowIndex--;
			if (this.m_nowIndex < 0)
			{
				this.m_nowIndex = this.m_goShotCtrlList.Length - 1;
			}
		}
		if (0 <= this.m_nowIndex && this.m_nowIndex < this.m_goShotCtrlList.Length)
		{
			this.m_goShotCtrlList[this.m_nowIndex].SetActive(true);
			this.m_nowGoName = this.m_goShotCtrlList[this.m_nowIndex].name;
			this.m_shotNameText.text = "No." + (this.m_nowIndex + 1).ToString() + " : " + this.m_nowGoName;
			base.StartCoroutine(this.StartShot());
		}
	}

	private IEnumerator StartShot()
	{
		float cntTimer = 0f;
		while (cntTimer < 1f)
		{
			cntTimer += UbhSingletonMonoBehavior<UbhTimer>.instance.deltaTime;
			yield return null;
		}
		yield return null;
		UbhShotCtrl shotCtrl = this.m_goShotCtrlList[this.m_nowIndex].GetComponent<UbhShotCtrl>();
		if (shotCtrl != null)
		{
			shotCtrl.StartShotRoutine();
		}
		yield break;
	}

	[SerializeField]
	[FormerlySerializedAs("_GoShotCtrlList")]
	private GameObject[] m_goShotCtrlList;

	[SerializeField]
	private Text m_shotNameText;

	private int m_nowIndex;

	private string m_nowGoName;
}
