using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
	private void Start()
	{
		if (Time.timeScale < 1f)
		{
			Time.timeScale = 1f;
		}
		this.LoadLevel(SceneContext.sceneName);
	}

	public void LoadLevel(int sceneIndex)
	{
		this.progressBar.gameObject.SetActive(true);
		base.StartCoroutine(this.LoadAsynchronously(sceneIndex));
	}

	public void LoadLevel(string sceneName)
	{
		this.progressBar.gameObject.SetActive(true);
		base.StartCoroutine(this.LoadAsynchronously(sceneName));
	}

	private IEnumerator LoadAsynchronously(int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			this.progressBar.value = progress;
			yield return null;
		}
		yield break;
	}

	private IEnumerator LoadAsynchronously(string sceneName)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / 0.9f);
			this.progressBar.value = progress;
			yield return null;
		}
		yield break;
	}

	public Slider progressBar;
}
