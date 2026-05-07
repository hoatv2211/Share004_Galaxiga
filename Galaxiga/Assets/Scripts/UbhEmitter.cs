using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class UbhEmitter : UbhMonoBehaviour
{
	private IEnumerator Start()
	{
		if (this.m_waves.Length == 0)
		{
			yield break;
		}
		this.m_manager = UnityEngine.Object.FindObjectOfType<UbhGameManager>();
		for (;;)
		{
			while (!this.m_manager.IsPlaying())
			{
				yield return null;
			}
			GameObject wave = UnityEngine.Object.Instantiate<GameObject>(this.m_waves[this.m_currentWave], base.transform);
			Transform waveTrans = wave.transform;
			waveTrans.position = base.transform.position;
			while (0 < waveTrans.childCount)
			{
				yield return null;
			}
			UnityEngine.Object.Destroy(wave);
			this.m_currentWave = (int)Mathf.Repeat((float)this.m_currentWave + 1f, (float)this.m_waves.Length);
		}
		yield break;
	}

	[SerializeField]
	[FormerlySerializedAs("_Waves")]
	private GameObject[] m_waves;

	private int m_currentWave;

	private UbhGameManager m_manager;
}
