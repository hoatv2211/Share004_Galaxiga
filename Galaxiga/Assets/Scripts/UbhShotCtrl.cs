using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[AddComponentMenu("UniBulletHell/Controller/Shot Controller")]
public sealed class UbhShotCtrl : UbhMonoBehaviour
{
	public bool shooting
	{
		get
		{
			return this.m_shooting;
		}
	}

	private IEnumerator Start()
	{
		if (this.m_startOnAwake)
		{
			if (0f < this.m_startOnAwakeDelay)
			{
				yield return UbhUtil.WaitForSeconds(this.m_startOnAwakeDelay);
			}
			this.StartShotRoutine();
		}
		yield break;
	}

	private void OnDisable()
	{
		this.m_shooting = false;
	}

	public void StartShotRoutine()
	{
		if (this.m_shotList == null || this.m_shotList.Count <= 0)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because ShotList is null or empty.");
			return;
		}
		bool flag = false;
		for (int i = 0; i < this.m_shotList.Count; i++)
		{
			if (this.m_shotList[i].m_shotObj != null)
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			UnityEngine.Debug.LogWarning("Cannot shot because all ShotObj of ShotList is not set.");
			return;
		}
		if (this.m_loop)
		{
			bool flag2 = false;
			for (int j = 0; j < this.m_shotList.Count; j++)
			{
				if (0f < this.m_shotList[j].m_afterDelay)
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				UnityEngine.Debug.LogWarning("Cannot shot because loop is true and all AfterDelay of ShotList is zero.");
				return;
			}
		}
		if (this.m_shooting)
		{
			UnityEngine.Debug.LogWarning("Already shooting.");
			return;
		}
		this.m_shooting = true;
		base.StartCoroutine(this.ShotCoroutine());
	}

	private IEnumerator ShotCoroutine()
	{
		this.m_workShotInfoList.AddRange(this.m_shotList);
		int nowIndex = 0;
		do
		{
			if (this.m_atRandom)
			{
				nowIndex = UnityEngine.Random.Range(0, this.m_workShotInfoList.Count);
			}
			if (this.m_workShotInfoList[nowIndex].m_shotObj != null)
			{
				this.m_workShotInfoList[nowIndex].m_shotObj.SetShotCtrl(this);
				this.m_workShotInfoList[nowIndex].m_shotObj.Shot();
			}
			if (0f < this.m_workShotInfoList[nowIndex].m_afterDelay)
			{
				yield return UbhUtil.WaitForSeconds(this.m_workShotInfoList[nowIndex].m_afterDelay);
			}
			if (this.m_atRandom)
			{
				this.m_workShotInfoList.RemoveAt(nowIndex);
				if (this.m_workShotInfoList.Count <= 0)
				{
					if (!this.m_loop)
					{
						break;
					}
					this.m_workShotInfoList.AddRange(this.m_shotList);
				}
			}
			else
			{
				if (!this.m_loop && this.m_workShotInfoList.Count - 1 <= nowIndex)
				{
					break;
				}
				nowIndex = (int)Mathf.Repeat((float)nowIndex + 1f, (float)this.m_workShotInfoList.Count);
			}
		}
		while (this.m_shooting);
		this.m_workShotInfoList.Clear();
		this.m_shooting = false;
		this.m_shotRoutineFinishedCallbackEvents.Invoke();
		yield break;
	}

	public void StopShotRoutine()
	{
		base.StopAllCoroutines();
		this.m_shooting = false;
	}

	[FormerlySerializedAs("_AxisMove")]
	public UbhUtil.AXIS m_axisMove;

	public bool m_inheritAngle;

	[FormerlySerializedAs("_StartOnAwake")]
	public bool m_startOnAwake = true;

	[FormerlySerializedAs("_StartOnAwakeDelay")]
	public float m_startOnAwakeDelay = 1f;

	[FormerlySerializedAs("_Loop")]
	public bool m_loop = true;

	[FormerlySerializedAs("_AtRandom")]
	public bool m_atRandom;

	[FormerlySerializedAs("_ShotList")]
	public List<UbhShotCtrl.ShotInfo> m_shotList = new List<UbhShotCtrl.ShotInfo>();

	[Space(10f)]
	public UnityEvent m_shotRoutineFinishedCallbackEvents = new UnityEvent();

	private bool m_shooting;

	private List<UbhShotCtrl.ShotInfo> m_workShotInfoList = new List<UbhShotCtrl.ShotInfo>(32);

	[Serializable]
	public class ShotInfo
	{
		[FormerlySerializedAs("_ShotObj")]
		public UbhBaseShot m_shotObj;

		[FormerlySerializedAs("_AfterDelay")]
		public float m_afterDelay;
	}
}
