using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using SkyGameKit;
using SWS;
using UnityEngine;

public class ShapeEnemy : EnemyAttack
{
	private void Awake()
	{
		this._splineMove = base.GetComponent<splineMove>();
	}

	[EnemyAction(displayName = "Action Xếp Hình - Xếp Xong Hình Cuối Cùng/Di Chuyển Ngẫu Nhiên 2 điểm - Hết Path Di Chuyển Theo Path Loop")]
	public void ActionShapeMovePath(float speed, PathManager pathLoop, Vector3 pointPathLoop, float speedPathLoop, int numberLoop)
	{
		Vector3[] path = new Vector3[]
		{
			base.transform.position,
			new Vector3(UnityEngine.Random.Range(-4.2f, 4.2f), (base.transform.position.y + pointPathLoop.y) / 2f, base.transform.position.z),
			pointPathLoop
		};
		if (!this.stattackPath)
		{
			if (this.afterSecond2 != null)
			{
				this.afterSecond2.ForEach(delegate(EnemyEventUnit<float> x)
				{
					this.doActionAttack = this.DoActionEveryTime(x.param, new Action(x.Invoke), false, false);
				});
			}
			this.stattackPath = true;
			Vector3 posLocal = default(Vector3);
			posLocal = base.transform.localPosition;
			base.transform.DOPath(path, speedPathLoop, PathType.Linear, PathMode.Full3D, 10, null).SetSpeedBased(true).OnComplete(delegate
			{
				this.SetPathLoopShape(pathLoop, speedPathLoop, posLocal, numberLoop);
			});
		}
	}

	[EnemyAction(displayName = "Action Xếp Hình - Xếp Xong Hình Cuối Cùng/Di Chuyển RanDom theo nhiều điểm phía dưới màn hình")]
	public void ActionShapeMovePath_FollowPlayer(float speed, int numberPoint, bool randomNunberPoint, int minPoint, float minx, float maxx, float miny, float maxy)
	{
		int num;
		if (randomNunberPoint)
		{
			num = UnityEngine.Random.Range(minPoint, numberPoint);
		}
		else
		{
			num = numberPoint;
		}
		Vector3[] array = new Vector3[num];
		array[0] = base.transform.position;
		for (int i = 1; i < num; i++)
		{
			array[i] = new Vector3(UnityEngine.Random.Range(minx, maxx), UnityEngine.Random.Range(miny, maxy), base.transform.position.z);
		}
		if (!this.stattackPath)
		{
			if (this.afterSecond2 != null)
			{
				this.afterSecond2.ForEach(delegate(EnemyEventUnit<float> x)
				{
					this.doActionAttack = this.DoActionEveryTime(x.param, new Action(x.Invoke), false, false);
				});
			}
			Vector3 posLocal = this.SetDefaultPath();
			base.transform.DOPath(array, speed, PathType.CatmullRom, PathMode.Full3D, 10, null).SetSpeedBased(true).OnComplete(delegate
			{
				if (this.transform.position.y < SgkCamera.bottomLeft.y)
				{
					this.transform.position = new Vector3(0f, 12f, this.transform.position.z);
					if (this.doActionAttack != null)
					{
						this.doActionAttack.Dispose();
					}
				}
				this.transform.DOLocalMove(posLocal, 2f, false).OnComplete(delegate
				{
					if (this.doActionAttack != null)
					{
						this.doActionAttack.Dispose();
					}
					this.stattackPath = false;
				});
			});
		}
	}

	[EnemyAction(displayName = "Action Xếp Hình - Xếp Xong Hình Cuối Cùng/Di Chuyển Theo Path- Hết Path Di Chuyển Theo Player")]
	public void ActionShapeMovePathFlowPlayer(PathManager pathEnemy, float speed, float speedFollow, float timeFollow)
	{
		if (!this.stattackPath)
		{
			if (this.afterSecond2 != null)
			{
				this.afterSecond2.ForEach(delegate(EnemyEventUnit<float> x)
				{
					this.doActionAttack = this.DoActionEveryTime(x.param, new Action(x.Invoke), false, false);
				});
			}
			Vector3 posLocal = this.SetDefaultPath();
			if (pathEnemy != null)
			{
				this.SetFieldPath(speed, pathEnemy);
				this._splineMove.events[pathEnemy.GetWaypointCount() - 1].AddListener(delegate()
				{
					this.gotoPlayer = true;
					this.speedMovetoPlayer = speedFollow;
					this.StartCoroutine(this.ReturnShape(timeFollow, posLocal));
				});
			}
			else
			{
				this.gotoPlayer = true;
				this.speedMovetoPlayer = speedFollow;
				base.StartCoroutine(this.ReturnShape(timeFollow, posLocal));
			}
		}
	}

	public void SetPathLoopShape(PathManager pathloop, float speedpathloop, Vector3 posLocal, int numberLoop)
	{
		if (pathloop.GetComponent<BezierPathManager>() != null)
		{
			pathloop.GetComponent<BezierPathManager>().CalculatePath();
		}
		pathloop.transform.position = base.transform.position;
		this.tweener = base.transform.DOLocalPath(pathloop.GetPathPoints(false), speedpathloop, PathType.CatmullRom, PathMode.Ignore, 10, null).SetEase(this._splineMove.easeType).SetLoops(numberLoop).OnComplete(delegate
		{
			this.transform.DOLocalMove(posLocal, 2f, false).OnComplete(delegate
			{
				if (this.doActionAttack != null)
				{
					this.doActionAttack.Dispose();
				}
				this.stattackPath = false;
			});
		});
	}

	private Vector3 SetDefaultPath()
	{
		this.stattackPath = true;
		Vector3 vector = default(Vector3);
		return base.transform.localPosition;
	}

	private IEnumerator ReturnShape(float timeFollow, Vector3 posLocal)
	{
		yield return new WaitForSeconds(timeFollow);
		this.gotoPlayer = false;
		base.transform.DOLocalMove(posLocal, 2f, false).OnComplete(delegate
		{
			if (this.doActionAttack != null)
			{
				this.doActionAttack.Dispose();
			}
			this.stattackPath = false;
		});
		yield return null;
		yield break;
	}

	public void SetFieldPath(float speedStart, PathManager pathMove)
	{
		if (pathMove.GetComponent<BezierPathManager>() != null)
		{
			pathMove.GetComponent<BezierPathManager>().CalculatePath();
		}
		pathMove.transform.position = base.transform.position;
		this._splineMove.speed = speedStart;
		this._splineMove.SetPath(pathMove);
	}

	[EnemyAction(displayName = "Attack/Nhảy Vào Player")]
	public void Throw(float timeAttack)
	{
		if (!this.stThrow)
		{
			base.StartCoroutine(this.SimulateProjectile(timeAttack));
		}
	}

	private IEnumerator SimulateProjectile(float timeAttack)
	{
		this.stThrow = true;
		MonoBehaviour.print("Throw");
		yield return new WaitForSeconds(timeAttack);
		this.parentObj = base.transform.parent;
		this.localPosition = this.Projectile.transform.localPosition;
		base.gameObject.transform.parent = null;
		this.Target = new Vector3(PlaneIngameManager.current.CurrentTransformPlayer.position.x, PlaneIngameManager.current.CurrentTransformPlayer.position.y + 0.5f, 0f);
		this.Projectile.position = this.Projectile.transform.position;
		float dx = this.Target.x - this.Projectile.position.x;
		float dy = this.Projectile.position.y - this.Target.y;
		float Vy = Mathf.Sqrt(this.maxY * 2f * this.gravity);
		float T = Vy / this.gravity;
		float T2 = Mathf.Sqrt(2f * (this.maxY + dy) / this.gravity);
		float flightDuration = T + T2;
		float Vx = dx / flightDuration;
		float elapse_time = 0f;
		while (elapse_time < flightDuration)
		{
			this.Projectile.Translate(Vx * Time.deltaTime, (Vy - this.gravity * elapse_time) * Time.deltaTime, 0f);
			elapse_time += Time.deltaTime;
			yield return null;
		}
		if (elapse_time >= flightDuration)
		{
			yield return new WaitForSeconds(this.timeReturn);
			base.StartCoroutine(this.ReturnObj());
		}
		yield break;
	}

	private IEnumerator ReturnObj()
	{
		base.gameObject.transform.SetParent(this.parentObj);
		this.Projectile.transform.DOLocalMove(this.localPosition, 1f, false).OnComplete(delegate
		{
			this.stThrow = false;
		});
		yield return null;
		yield break;
	}

	private splineMove _splineMove;

	private bool gotoPlayer;

	private float speedMovetoPlayer = 2f;

	private bool noHit;

	[Header("ATTACK_FOlLOWPLAYER")]
	private Vector3 Target;

	public Transform Projectile;

	public Vector3 localPosition;

	public float gravity = 9.8f;

	public float maxY = 1f;

	public float timeReturn = 1f;

	public float timeRate = 2f;

	private float timeFire;

	public Transform parentObj;

	[Header("CHECK_ENEMY")]
	public bool checkEnemyMove = true;

	[ShowIf("checkEnemyMove", true)]
	public LayerMask layerWall;

	[ShowIf("checkEnemyMove", true)]
	public RaycastHit2D hit;

	private bool stThrow;

	private bool stStartThrow;

	[EnemyEventCustom(paramName = "Second", displayName = "Di Chuyen Galaga/AfterSecond")]
	public EnemyEvent<float> afterSecond2 = new EnemyEvent<float>();

	private IDisposable doActionAttack;
}
