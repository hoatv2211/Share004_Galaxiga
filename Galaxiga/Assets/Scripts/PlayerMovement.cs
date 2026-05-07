using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool IsInRelativeMode()
	{
		return this.moveMode == PlayerMovement.TypeMove.RelativeMove;
	}

	public bool IsInTouchMove()
	{
		return this.moveMode == PlayerMovement.TypeMove.TouchMove;
	}

	private void Awake()
	{
		this.playerController = base.GetComponent<PlayerController>();
	}

	private void Start()
	{
		this.SetController();
		this.playerTransformZ = base.transform.position.z;
		this.bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.nearClipPlane));
		this.bottomLeft.x = this.bottomLeft.x + this.marginRightAndLeft;
		this.bottomLeft.y = this.bottomLeft.y + this.marginBottom;
		this.topRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));
		this.topRight.x = this.topRight.x - this.marginRightAndLeft;
		this.topRight.y = this.topRight.y - this.marginTop;
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"Speed starship ",
			this.playerController.namePlane,
			": ",
			this.speed
		}));
	}

	private void SetController()
	{
		this.moveMode = (PlayerMovement.TypeMove)GameContext.typeMove;
		UnityEngine.Debug.Log(string.Concat(new object[]
		{
			"type move: ",
			this.moveMode,
			" || sensitivity: ",
			GameContext.sensitivity
		}));
		if (this.moveMode == PlayerMovement.TypeMove.RelativeMove)
		{
			this.speed = 200f;
		}
		else
		{
			this.speed = 40f;
		}
	}

	private void Update()
	{
		if (!this.isReady || GameContext.isMissionComplete)
		{
			return;
		}
		if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
		{
			if (UnityEngine.Input.touchCount > 0)
			{
				if (this.currentTouchId < 0)
				{
					this.currentTouchId = Input.touches[0].fingerId;
					this.currentTouchChanged = true;
				}
				this.currentTouch = PlayerMovement.GetTouch(this.currentTouchId);
				if (this.currentTouch.phase == TouchPhase.Ended || this.currentTouch.phase == TouchPhase.Canceled)
				{
					this.currentTouchId = -1;
				}
				this.mousePos = Camera.main.ScreenToWorldPoint(this.currentTouch.position);
			}
			else
			{
				this.mousePos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			}
			this.mousePos.z = this.playerTransformZ;
			this.step = this.speed * Time.deltaTime;
			if (this.moveMode == PlayerMovement.TypeMove.RelativeMove)
			{
				this.RelativeMove();
			}
			else
			{
				this.TouchMove();
			}
		}
	}

	public static Touch GetTouch(int fingerId)
	{
		foreach (Touch result in Input.touches)
		{
			if (result.fingerId == fingerId)
			{
				return result;
			}
		}
		return new Touch
		{
			fingerId = -1
		};
	}

	private void TouchMove()
	{
		this.mousePos += (Vector3)this.offsetTouch;
		Vector3 position;
		if (this.lerpMove)
		{
			position = Vector3.Lerp(base.transform.position, this.mousePos, (0.4f * this.step >= 1f) ? 1f : (0.5f * this.step));
		}
		else
		{
			position = Vector3.MoveTowards(base.transform.position, this.mousePos, this.step);
		}
		position.x = Mathf.Clamp(position.x, this.bottomLeft.x, this.topRight.x);
		position.y = Mathf.Clamp(position.y, this.bottomLeft.y, this.topRight.y);
		position.z = base.transform.position.z;
		base.transform.position = position;
	}

	private void RelativeMove()
	{
		if (UnityEngine.Input.touchCount == 0)
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.lastMousePos = this.mousePos;
				return;
			}
		}
		else if (this.currentTouchChanged)
		{
			this.lastMousePos = this.mousePos;
			this.currentTouchChanged = false;
			return;
		}
		this.offsetMouse = this.mousePos - this.lastMousePos;
		if (this.lerpMove)
		{
			this.trans = base.transform.position + this.offsetMouse * ((0.4f * this.step >= 1f) ? 1f : (0.5f * this.step)) * GameContext.sensitivity;
		}
		else
		{
			this.trans = Vector3.MoveTowards(base.transform.position, base.transform.position + this.offsetMouse * GameContext.sensitivity, this.step);
		}
		if (GameContext.sensitivity > 1.01f)
		{
			this.lastMousePos = this.mousePos;
		}
		else
		{
			this.lastMousePos += this.trans - base.transform.position;
		}
		this.trans.x = Mathf.Clamp(this.trans.x, this.bottomLeft.x, this.topRight.x);
		this.trans.y = Mathf.Clamp(this.trans.y, this.bottomLeft.y, this.topRight.y);
		this.trans.z = base.transform.position.z;
		base.transform.position = this.trans;
	}

	public void SetPosition(Vector3 lastPosMouse)
	{
		this.lastMousePos = lastPosMouse;
	}

	public void ResetPosition()
	{
		this.currentTouchChanged = true;
		this.offsetMouse = Vector3.zero;
		this.lastMousePos = Vector3.zero;
		this.mousePos = Vector3.zero;
		this.step = 0f;
	}

	public void StartDotweenMove()
	{
		float endValue = base.transform.position.y + 11f;
		base.gameObject.transform.DOMoveY(endValue, 0.5f, false).SetEase(Ease.InOutQuad).OnComplete(new TweenCallback(this.DotweenMoveComplete));
	}

	public void DotweenMoveComplete()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.StartCoroutine(this.DelayStartAllShot());
		}
	}

	private IEnumerator DelayStartAllShot()
	{
		yield return new WaitForSeconds(0.5f);
		if (!GameContext.isGameReady)
		{
			this.isReady = true;
			GameContext.isGameReady = true;
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"plane is ready ",
				this.isReady,
				" ",
				GameContext.isMissionComplete
			}));
			UIGameManager.current.ShowFXStartGame();
		}
		GameScreenManager.current.StartAllShot();
		BackupPlaneManager.current.isRevivedPlane = false;
		if (GameContext.maxLevelUnlocked < 2)
		{
			NewTutorial.current.UseSkillPlane_Step0();
		}
		yield break;
	}

	public void VictoryDotweenMove(TweenCallback VictoryMoveComplete)
	{
		this.isReady = false;
		float endValue = base.transform.position.y + 18f;
		base.gameObject.transform.DOMoveY(endValue, 0.5f, false).SetEase(Ease.InOutQuad).OnComplete(VictoryMoveComplete);
	}

	public float speed = 200f;

	public bool lerpMove = true;

	public PlayerMovement.TypeMove moveMode = PlayerMovement.TypeMove.RelativeMove;

	[HideIf("IsInRelativeMode", true)]
	public Vector2 offsetTouch = Vector2.down;

	[HideIf("IsInTouchMove", true)]
	public float relativeScale = 1f;

	private float rangeAttack = 15f;

	public float marginTop;

	public float marginBottom;

	public float marginRightAndLeft;

	[HideInInspector]
	public bool isReady;

	private bool overUI;

	private Vector3 bottomLeft;

	private Vector3 topRight;

	private float playerTransformZ;

	private float step;

	private Vector3 mousePos;

	[HideInInspector]
	public Vector3 lastMousePos = Vector3.zero;

	private int currentTouchId = -1;

	private Touch currentTouch;

	private bool currentTouchChanged;

	private PlayerController playerController;

	private Vector3 trans;

	private Vector3 offsetMouse;

	public enum TypeMove
	{
		TouchMove,
		RelativeMove
	}
}
