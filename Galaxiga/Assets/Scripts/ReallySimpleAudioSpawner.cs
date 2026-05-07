using System;
using System.Collections;
using PathologicalGames;
using UnityEngine;

public class ReallySimpleAudioSpawner : MonoBehaviour
{
	private void Start()
	{
		this.pool = base.GetComponent<SpawnPool>();
		base.StartCoroutine(this.Spawner());
		if (this.musicPrefab != null)
		{
			base.StartCoroutine(this.MusicSpawner());
		}
	}

	private IEnumerator MusicSpawner()
	{
		AudioSource music = this.pool.Spawn(this.musicPrefab);
		yield return new WaitForSeconds(2f);
		this.pool.Despawn(music.transform);
		yield return new WaitForSeconds(1f);
		music = this.pool.Spawn(this.musicPrefab);
		yield return new WaitForSeconds(2f);
		music.Stop();
		yield return new WaitForSeconds(1f);
		music = this.pool.Spawn(this.musicPrefab);
		yield return new WaitForSeconds(2f);
		music.Stop();
		yield break;
	}

	private IEnumerator Spawner()
	{
		for (;;)
		{
			AudioSource current = this.pool.Spawn(this.prefab, base.transform.position, base.transform.rotation);
			current.pitch = UnityEngine.Random.Range(0.7f, 1.4f);
			yield return new WaitForSeconds(this.spawnInterval);
		}
		yield break;
	}

	public AudioSource prefab;

	public AudioSource musicPrefab;

	public float spawnInterval = 2f;

	private SpawnPool pool;
}
