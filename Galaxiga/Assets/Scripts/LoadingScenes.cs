using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScenes : MonoBehaviour
{
	public static LoadingScenes Current
	{
		get
		{
			if (LoadingScenes._ins == null)
			{
				LoadingScenes._ins = UnityEngine.Object.FindObjectOfType<LoadingScenes>();
			}
			return LoadingScenes._ins;
		}
	}

	public void LoadLevel(string sceneName)
	{
		this.objLoading.SetActive(true);
		base.StartCoroutine(this.LoadYourAsyncScene(sceneName));
	}

	private IEnumerator LoadYourAsyncScene(string sceneName)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
		this.objLoading.SetActive(false);
		yield break;
	}

	public static LoadingScenes _ins;

	public GameObject objLoading;
}
