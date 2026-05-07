using System;
using Com.LuisPedroFonseca.ProCamera2D;
using FalconSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager : MonoBehaviour
{
	private void Awake()
	{
		CameraManager.curret = this;
		CameraManager.topRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));
		CameraManager.bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane));
	}

	private void OnEnable()
	{
		ProCamera2DTransitionsFX proCamera2DTransitionsFX = this.transitionsFX;
		proCamera2DTransitionsFX.OnTransitionEnterEnded = (Action)Delegate.Combine(proCamera2DTransitionsFX.OnTransitionEnterEnded, new Action(this.TransitionEnterEnded));
		ProCamera2DTransitionsFX proCamera2DTransitionsFX2 = this.transitionsFX;
		proCamera2DTransitionsFX2.OnTransitionExitEnded = (Action)Delegate.Combine(proCamera2DTransitionsFX2.OnTransitionExitEnded, new Action(this.TransitionExitEnded));
	}

	private void OnDisable()
	{
		ProCamera2DTransitionsFX proCamera2DTransitionsFX = this.transitionsFX;
		proCamera2DTransitionsFX.OnTransitionEnterEnded = (Action)Delegate.Remove(proCamera2DTransitionsFX.OnTransitionEnterEnded, new Action(this.TransitionEnterEnded));
		ProCamera2DTransitionsFX proCamera2DTransitionsFX2 = this.transitionsFX;
		proCamera2DTransitionsFX2.OnTransitionExitEnded = (Action)Delegate.Remove(proCamera2DTransitionsFX2.OnTransitionExitEnded, new Action(this.TransitionExitEnded));
	}

	private void TransitionExitEnded()
	{
		SceneManager.LoadScene("Loading");
	}

	private void TransitionEnterEnded()
	{
		this.planeIngameManager.ActivePlaneEquiped();
		UIGameManager.current.ShowUI();
	}

	private void Start()
	{
		this.transitionsFX.TransitionEnter();
	}

	public void ExitGameScreen(string _sceneName)
	{
		if (!this.isExitGame)
		{
			this.isExitGame = true;
			if (Time.timeScale < 1f)
			{
				Time.timeScale = 1f;
			}
			SceneContext.sceneName = _sceneName;
			LoadingScenes.Current.LoadLevel(_sceneName);
			UIGameManager.current.HideUI();
			GameScreenManager.current.ResumeMusic();
			GameContext.audioMainMenu.Play();
			if (GameContext.isPlayerDie)
			{
				GameContext.isPlayerDie = false;
			}
			FSDK.CrossPromotion.CanShowFixedPromo(true);
			GameContext.totalSecondEndGame = (int)UIGameManager.current.timer;
		}
	}

	public void StartShake(CameraManager.ShakeType shakeType)
	{
		if (shakeType != CameraManager.ShakeType.EatBullet)
		{
			if (shakeType == CameraManager.ShakeType.PlayerDie)
			{
				ShakePreset preset = this.proCamera2DShake.ShakePresets[1];
				this.proCamera2DShake.Shake(preset);
			}
		}
		else
		{
			ShakePreset preset2 = this.proCamera2DShake.ShakePresets[0];
			this.proCamera2DShake.Shake(preset2);
		}
	}

	public static CameraManager curret;

	public PlaneIngameManager planeIngameManager;

	public ProCamera2DShake proCamera2DShake;

	public ProCamera2DTransitionsFX transitionsFX;

	public static Vector2 topRight;

	public static Vector2 bottomLeft;

	private bool isExitGame;

	[Serializable]
	public enum ShakeType
	{
		EatBullet,
		PlayerDie,
		BossDie
	}
}
