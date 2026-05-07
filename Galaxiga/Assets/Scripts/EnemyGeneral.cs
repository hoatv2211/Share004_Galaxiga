using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using Hellmade.Sound;
using PathologicalGames;
using Sirenix.OdinInspector;
using SkyGameKit;
using SWS;
using UnityEngine;

public class EnemyGeneral : BoxItemEnemy
{
	private void OnBecameVisible()
	{
		this.startAttack = true;
	}

	public virtual void Awake()
	{
		this._splineMove = base.GetComponent<splineMove>();
		this._anim = this.avatar.GetComponent<Animator>();
		if (this.avatar != null)
		{
			this.avatarSpawBullet = this.avatar.GetComponent<AvatarSpawnBullet>();
		}
	}

	private void Start()
	{
		if (this.speedBullet <= 0f)
		{
			this.speedBullet = 3f;
		}
		this._animatorStart = this._anim.runtimeAnimatorController;
	}

	protected override void Update()
	{
		base.Update();
		if (this.gotoPlayer)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, PlaneIngameManager.current.CurrentTransformPlayer.position, this.speedMovetoPlayer * Time.deltaTime);
			return;
		}
	}

	private IEnumerator Hold(float timeHold, bool random, int numStyleAttack)
	{
		this._anim.SetTrigger("hold");
		yield return new WaitForSeconds(timeHold);
		this.Attack(random, 100, numStyleAttack, 1f);
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "Attack/Bắn Đạn")]
	public virtual void Attack(bool randomTime, int percentAttack, int numStyleAttack, float delaytime = 1f)
	{
		if (percentAttack >= UnityEngine.Random.Range(0, 100) && this.startAttack)
		{
			base.StartCoroutine(this.AttackIE(delaytime, randomTime, numStyleAttack));
		}
	}

	[EnemyAction(displayName = "Attack/Bắn Đạn Theo ID")]
	public void AttackID(bool randomTime, int percentAttack, int[] listIndex, int numStyleAttack, float timeDelay = 1f)
	{
		if (timeDelay == 0f)
		{
			timeDelay = 1f;
		}
		bool flag = false;
		for (int i = 0; i < listIndex.Length; i++)
		{
			if (this.Index == listIndex[i])
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.Attack(randomTime, percentAttack, numStyleAttack, timeDelay);
		}
	}

	private IEnumerator AttackIE(float timeDelay, bool random, int numStyleAttack)
	{
		this.avatarSpawBullet.ActiveEffect();
		if (timeDelay <= 0f)
		{
			timeDelay = 1f;
		}
		if (random)
		{
			timeDelay = (float)UnityEngine.Random.Range(1, 3);
		}
		yield return new WaitForSeconds(timeDelay);
		this._anim.SetTrigger("attack");
		if (this.childObjAttack != null)
		{
			this.childObjAttack.m_bulletSpeed = this.speedBullet;
		}
		if (this.childObjAttack2 != null)
		{
			this.childObjAttack2.m_bulletSpeed = this.speedBullet;
		}
		this.avatarSpawBullet.numberAttack = numStyleAttack;
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "Di Chuyển/Di Chuyển Theo Path")]
	public void SetPathStartSetPoint(PathManager pathEnemy, float speed, float speedEndPath, Vector3 pointStart, Ease ease)
	{
		if (ease == Ease.Unset)
		{
			ease = Ease.Linear;
		}
		if (pointStart != Vector3.zero)
		{
			base.transform.position = pointStart;
		}
		this.SetFieldPath(speed, pathEnemy, ease);
	}

	[EnemyAction(displayName = "Di Chuyển/Di Chuyển Theo Path- Hết Path Di Chuyển Theo Path Loop")]
	public void SetPathInPath(PathManager pathEnemy, float speed, PathManager pathloop, float speedPathLoop, Ease easePathEnemy, Ease easePathLoop)
	{
		if (easePathEnemy == Ease.Unset)
		{
			easePathEnemy = Ease.Linear;
		}
		if (easePathLoop == Ease.Unset)
		{
			easePathLoop = Ease.Linear;
		}
		this.SetFieldPath(speed, pathEnemy, easePathEnemy);
		this._splineMove.events[pathEnemy.GetWaypointCount() - 1].AddListener(delegate()
		{
			this._splineMove.events.Clear();
			this.SetPathLoop(pathloop, speedPathLoop, easePathLoop);
		});
	}

	public void SetPathInPathCurve(PathManager pathEnemy, float speed, PathManager pathloop, float speedPathLoop, Ease easePathEnemy, Ease easePathLoop, AnimationCurve curvePath, AnimationCurve curvePathLoop, int pointChangePath)
	{
		if (easePathEnemy == Ease.Unset)
		{
			this._splineMove.easeType = Ease.Unset;
			this._splineMove.animEaseType = curvePath;
		}
		this.SetFieldPath(speed, pathEnemy, easePathEnemy);
		if (pointChangePath > pathEnemy.GetWaypointCount() - 1)
		{
			pointChangePath = pathEnemy.GetWaypointCount() - 1;
		}
		this._splineMove.events[pointChangePath].AddListener(delegate()
		{
			this._splineMove.events.Clear();
			if (easePathLoop == Ease.Unset)
			{
				this._splineMove.easeType = Ease.Unset;
				this._splineMove.animEaseType = curvePathLoop;
			}
			this.SetPathLoop(pathloop, speedPathLoop, easePathLoop);
		});
	}

	[EnemyAction(displayName = "Di Chuyển/Di Chuyển theo Path Loop")]
	public void SetPathLoop(PathManager pathloop, float speedpathloop, Ease ease)
	{
		if (pathloop.GetComponent<BezierPathManager>() != null)
		{
			pathloop.GetComponent<BezierPathManager>().CalculatePath();
		}
		if (ease != Ease.Unset)
		{
			this._splineMove.easeType = ease;
		}
		pathloop.transform.position = base.transform.position;
		this._splineMove.loopType = splineMove.LoopType.loop;
		this._splineMove.speed = speedpathloop;
		this._splineMove.SetPath(pathloop);
	}

	public void SetPathLoopCurve(PathManager pathloop, float speedpathloop, AnimationCurve curve, Ease ease)
	{
		if (ease == Ease.Unset)
		{
			this._splineMove.easeType = Ease.Unset;
			this._splineMove.animEaseType = curve;
		}
		this.SetPathLoop(pathloop, speedpathloop, ease);
	}

	public void SetPathLoopCurveExtend(List<PathLoopInfo> ListPathLoop, int numberloop, bool stloop)
	{
		if (numberloop < ListPathLoop.Count)
		{
			UnityEngine.Debug.LogError("ListPathLoop" + ListPathLoop.Count);
			int numberloop2 = numberloop;
			Vector3[] path = Fu.MovePathToPoint(ListPathLoop[numberloop2].pathEnemy.GetPathPoints(false), base.transform.position);
			if (ListPathLoop[numberloop2].ease != Ease.Unset)
			{
				this.tweener = base.transform.DOPath(path, ListPathLoop[numberloop2].speed, ListPathLoop[numberloop2].pathType, PathMode.Ignore, 10, null).SetLoops(ListPathLoop[numberloop2].loop, LoopType.Restart).SetEase(ListPathLoop[numberloop2].ease).OnComplete(delegate
				{
					UnityEngine.Debug.LogError("Khong Vao Day");
					numberloop++;
					if (numberloop == ListPathLoop.Count && stloop)
					{
						numberloop = 0;
					}
					this.transform.DOKill(false);
					this.SetPathLoopCurveExtend(ListPathLoop, numberloop, stloop);
				});
			}
			else
			{
				this.tweener = base.transform.DOPath(path, ListPathLoop[numberloop2].speed, ListPathLoop[numberloop2].pathType, PathMode.Ignore, 10, null).SetLoops(ListPathLoop[numberloop2].loop, LoopType.Restart).SetEase(ListPathLoop[numberloop2].curve).OnComplete(delegate
				{
					UnityEngine.Debug.LogError("Khong Vao Day");
					numberloop++;
					this.transform.DOKill(false);
					if (numberloop == ListPathLoop.Count && stloop)
					{
						numberloop = 0;
					}
					this.SetPathLoopCurveExtend(ListPathLoop, numberloop, stloop);
				});
			}
		}
		UnityEngine.Debug.LogError("i=" + numberloop);
	}

	[EnemyAction(displayName = "Attack/Giữ >> Tấn Công")]
	public virtual void Attack_Hold(float timeHold, bool random, int numStyleAttack)
	{
		base.StartCoroutine(this.Hold(timeHold, random, numStyleAttack));
	}

	private void MovePositionEnd(float speedEndPath, int numberShape)
	{
		base.transform.DOLocalMove(this.listpositionEne.points[1], speedEndPath, false).OnComplete(delegate
		{
			this.stStartThrow = true;
		});
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
		if (base.transform.position.y > PlaneIngameManager.current.CurrentTransformPlayer.position.y)
		{
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
		}
		else
		{
			this.stThrow = false;
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

	public void SetFieldPath(float speedStart, PathManager pathMove, Ease ease)
	{
		if (pathMove.GetComponent<BezierPathManager>() != null)
		{
			pathMove.GetComponent<BezierPathManager>().CalculatePath();
		}
		if (ease != Ease.Unset)
		{
			this._splineMove.easeType = ease;
		}
		pathMove.transform.position = base.transform.position;
		this._splineMove.speed = speedStart;
		this._splineMove.SetPath(pathMove);
	}

	public override void Restart()
	{
		this._splineMove.events.Clear();
		this._splineMove.Stop();
		base.StopAllCoroutines();
		this.speedBullet = BaseEnemyDataSheet.Get(this.id).speedBullet * LevelHardEnemySheet.Get(SgkSingleton<LevelInfo>.Instance.numHard).percentSpeed * LevelEnemySheet.Get(SgkSingleton<LevelInfo>.Instance.numPlanet).percentSpeed;
		this.score = BaseEnemyDataSheet.Get(this.id).scoreEnemy;
		base.Restart();
		if (this.listAnimator != null)
		{
			this.numberAvatar = this.listAnimator.Count - 1;
		}
		this.startHP = this.CurrentHP;
		this.Delay(0.1f, delegate
		{
			if (this.EventLoop != null)
			{
				this.EventLoop.ForEach(delegate(EnemyEventUnit<float> x)
				{
					this.DoActionEveryTime(x.param, new Action(x.Invoke), false, false);
				});
			}
			if (this.Index == base.MotherTurn.TotalEnemy - 1)
			{
				List<BaseEnemy> list = new List<BaseEnemy>();
				list = base.MotherTurn.GetAliveEnemy();
				for (int i = 0; i < list.Count; i++)
				{
					(list[i] as EnemyGeneral).SetEventLoop();
				}
			}
		}, false);
	}

	public void SetEventLoop()
	{
		if (this.EveryXSEvent != null)
		{
			this.EveryXSEvent.ForEach(delegate(EnemyEventUnit<int> x)
			{
				this.DoActionEveryTime((float)x.param, new Action(x.Invoke), false, false);
			});
		}
	}

	[EnemyAction(displayName = "Wave/Di Chuyển Wave")]
	public void MoveWaveShape(float timeDelay, float unitMove, float speed)
	{
		base.StartCoroutine(this.MoveWaveShapeIE(timeDelay, unitMove, speed));
	}

	private IEnumerator MoveWaveShapeIE(float timeDelay, float unitMove, float speed)
	{
		yield return new WaitForSeconds(timeDelay);
		this.waveControl.GetComponent<MoveWay>().enabled = true;
		this.waveControl.GetComponent<MoveWay>().speed = speed;
		this.waveControl.GetComponent<MoveWay>().unitToMove = unitMove;
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "Wave/Tắt Di Chuyển Wave")]
	public void OffMoveWaveShape(float timeDelay)
	{
		base.transform.parent.parent.gameObject.GetComponent<MoveWay>().enabled = false;
	}

	[EnemyAction(displayName = "Action/Dập Dềnh")]
	public void TweenAvatar(float miny, float maxy, float minDuration, float maxDuration)
	{
		this.avatar.transform.DOLocalMove(new Vector3(0f, UnityEngine.Random.Range(miny, maxy), 0f), UnityEngine.Random.Range(minDuration, maxDuration), false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
	}

	[EnemyAction(displayName = "Action/Tắt Dập Dềnh")]
	public void TweenAvatar1()
	{
		this.avatar.transform.DOKill(this.avatar);
		this.avatar.transform.localPosition = new Vector3(0f, 0f, 0f);
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		if (type == EnemyKilledBy.Player)
		{
			EazySoundManager.PlaySound(AudioCache.Sound.enemy_die3);
		}
		if (base.MotherTurn.transform.parent.GetComponent<FreeWave>() == null)
		{
			float num = (float)UnityEngine.Random.Range(0, 100);
			if (num < this.waveControl.percentSpawnItemEvent && this.waveControl.spawItemNumberDrop)
			{
				this.waveControl.addNumberDrop++;
				if (this.waveControl.addNumberDrop <= this.waveControl.numberDrop)
				{
					PoolManager.Pools["ItemPool"].Spawn(this.waveControl.itemSpawnDrop, base.transform.position, Quaternion.identity);
				}
			}
		}
		this.avatar.transform.DOKill(false);
		base.Die(type);
		this.noHit = false;
		this._anim.runtimeAnimatorController = this._animatorStart;
		if (this.stattackPath)
		{
			this.ResetNumberMove();
		}
		if (this.avatarSpawBullet.effectHold != null)
		{
			this.avatarSpawBullet.effectHold.SetActive(false);
			if (this.avatarSpawBullet.effectAttack != null)
			{
				this.avatarSpawBullet.effectAttack.SetActive(false);
			}
		}
		if (SgkSingleton<LevelInfo>.Instance.mode == LevelInfo.modeLevel.EndLess)
		{
			SaveDataQuestEndless.SetProcessQuest(EndlessDailyQuestManager.Quest.defeat_enemies0, 1);
		}
	}

	private void ResetNumberMove()
	{
		this.waveControl.numberEnemyMove = this.waveControl.numberEnemyMove - 1;
		if (this.waveControl.numberEnemyMove < 0)
		{
			this.waveControl.numberEnemyMove = 0;
		}
	}

	public override void OnTransformShapeComplete(int shapeIndex, string shapeName)
	{
		this.avatar.transform.DOLocalMove(new Vector3(0f, UnityEngine.Random.Range(this.minY, this.maxYDD), 0f), UnityEngine.Random.Range(this.minDuration, this.maxDuration), false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
		base.OnTransformShapeComplete(shapeIndex, shapeName);
	}

	public override void OnHoldPositionEnd(int shapeIndex, string shapeName)
	{
		this.avatar.transform.DOKill(this.avatar);
		this.avatar.transform.DOLocalMove(new Vector3(0f, 0f, 0f), 0.2f, false);
		base.OnHoldPositionEnd(shapeIndex, shapeName);
	}

	public override int CurrentHP
	{
		get
		{
			return base.CurrentHP;
		}
		set
		{
			if (value < base.CurrentHP)
			{
				if (this.hitEvent != null)
				{
					this.hitEvent();
				}
			}
			if ((!this.noHit && value < base.CurrentHP) || value > base.CurrentHP)
			{
				base.CurrentHP = value;
			}
			if (this.numberAvatar > 0 && this.startHP > 0)
			{
				this.CheckAvatar();
			}
		}
	}

	[EnemyAction(displayName = "Action/Không Mất Máu")]
	public void NotHit(float time)
	{
		this.noHit = true;
		base.StartCoroutine(this.NotHitIE(time));
	}

	private IEnumerator NotHitIE(float time)
	{
		yield return new WaitForSeconds(time);
		this.noHit = false;
		yield return null;
		yield break;
	}

	private void CheckAvatar()
	{
		if (this.listAnimator != null && this.numberAvatar > 0 && (float)this.CurrentHP * 1f / (float)this.startHP < (float)this.numberAvatar * 1f / (float)this.listAnimator.Count)
		{
			this._anim.runtimeAnimatorController = this.listAnimator[this.numberAvatar - 1];
			this.numberAvatar--;
		}
	}

	[EnemyAction(displayName = "Enemy/Tăng Tốc Độ Đạn")]
	public void SetSpeed(float ratio)
	{
		this.speedBullet *= ratio;
	}

	[EnemyAction(displayName = "Attack/Bắn Nhiều Loại Đạn")]
	public void MultyAttack(int percentAttack, bool randomAttack)
	{
		if (this.add >= this.listUbh.Count)
		{
			this.add = 0;
		}
		if (randomAttack)
		{
			this.add = UnityEngine.Random.Range(0, this.listUbh.Count);
		}
		if (percentAttack >= UnityEngine.Random.Range(0, 100) && this.startAttack)
		{
			MonoBehaviour.print("multiAttack");
			base.StartCoroutine(this.MultyAttackIE(this.add));
		}
	}

	private IEnumerator MultyAttackIE(int number)
	{
		MonoBehaviour.print("multiAttack");
		yield return new WaitForSeconds(2f);
		this._anim.SetTrigger("attack");
		this.add++;
		this.avatarSpawBullet.numberAttack = this.add;
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "Attack/Đi Path")]
	public void SetPathAttack(PathManager pathAttack, float speedMovePath, float durationReturn, bool stToPath, Vector3 posPath, bool telepos, Vector3 telePosition, int[] numEnemyAction, float percent, bool followPlayer, float durationFollowPlayer)
	{
		bool flag = false;
		float num = (float)UnityEngine.Random.Range(0, 99);
		if (num < percent)
		{
			for (int i = 0; i < numEnemyAction.Length; i++)
			{
				if (this.Index == numEnemyAction[i])
				{
					flag = true;
					break;
				}
			}
			if (!this.stattackPath && flag)
			{
				this.stattackPath = true;
				Vector3 localPoint = new Vector3(0f, 0f, 0f);
				localPoint = base.transform.localPosition;
				if (!stToPath)
				{
					pathAttack.transform.position = base.transform.position;
					this._splineMove.SetPath(pathAttack);
				}
				else
				{
					pathAttack.transform.position = posPath;
					base.transform.DOLocalMove(pathAttack.transform.position, 2f, false).SetEase(Ease.Linear).OnComplete(delegate
					{
						this._splineMove.SetPath(pathAttack);
					});
				}
				this._splineMove.speed = speedMovePath;
				if (this.afterSecond2 != null)
				{
					this.afterSecond2.ForEach(delegate(EnemyEventUnit<float> x)
					{
						this.doActionAttack = this.DoActionEveryTime(x.param, new Action(x.Invoke), false, false);
					});
				}
				this._splineMove.events[pathAttack.GetWaypointCount() - 1].AddListener(delegate()
				{
					if (telepos)
					{
						this.transform.position = telePosition;
					}
					this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
					if (this.doActionAttack != null)
					{
						this.doActionAttack.Dispose();
					}
					if (followPlayer)
					{
						this.transform.DOMove(PlaneIngameManager.current.CurrentTransformPlayer.position, durationFollowPlayer, false).SetSpeedBased(true).OnComplete(delegate
						{
							this.transform.DOLocalMove(localPoint, durationReturn, false).OnComplete(delegate
							{
								this.stattackPath = false;
							});
						});
					}
					else
					{
						this.transform.DOLocalMove(localPoint, durationReturn, false).OnComplete(delegate
						{
							this.stattackPath = false;
						});
					}
				});
			}
		}
	}

	public void SetPathAttack2(TurnGalagaInfo galagaInfo)
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
			this.stattackPath = true;
			this._splineMove.speed = galagaInfo.speedMovePath;
			Vector3 localPoint = new Vector3(0f, 0f, 0f);
			localPoint = base.transform.localPosition;
			if (!galagaInfo.stMoveToPath)
			{
				galagaInfo.pathAttack.transform.position = base.transform.position;
				this._splineMove.SetPath(galagaInfo.pathAttack);
				this._splineMove.events[galagaInfo.pathAttack.GetWaypointCount() - 1].AddListener(delegate()
				{
					if (galagaInfo.telepos)
					{
						this.transform.position = galagaInfo.telePosition;
					}
					if (this.doActionAttack != null)
					{
						this.doActionAttack.Dispose();
					}
					this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
					if (galagaInfo.followPlayer)
					{
						this.transform.DOMove(PlaneIngameManager.current.CurrentTransformPlayer.position, galagaInfo.speedMoveToPlayer, false).SetSpeedBased(true).OnComplete(delegate
						{
							this.transform.DOLocalMove(localPoint, galagaInfo.speedReturn, false).OnComplete(delegate
							{
								this.stattackPath = false;
							});
						});
					}
					else
					{
						if (this._splineMove.pathMode == PathMode.TopDown2D)
						{
							this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
						}
						this.transform.DOLocalMove(localPoint, galagaInfo.speedReturn, false).OnComplete(delegate
						{
							this.stattackPath = false;
						});
					}
				});
			}
			else
			{
				galagaInfo.pathAttack.transform.position = galagaInfo.posPath;
				base.transform.DOMove(galagaInfo.pathAttack.transform.position, galagaInfo.speedMoveToPath, false).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(delegate
				{
					this._splineMove.SetPath(galagaInfo.pathAttack);
					this._splineMove.events[galagaInfo.pathAttack.GetWaypointCount() - 1].AddListener(delegate()
					{
						if (galagaInfo.telepos)
						{
							this.transform.position = galagaInfo.telePosition;
						}
						if (this.doActionAttack != null)
						{
							this.doActionAttack.Dispose();
						}
						this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
						if (galagaInfo.followPlayer)
						{
							this.transform.DOMove(PlaneIngameManager.current.CurrentTransformPlayer.position, galagaInfo.speedReturn, false).SetSpeedBased(true).OnComplete(delegate
							{
								this.transform.DOLocalMove(localPoint, galagaInfo.speedReturn, false).OnComplete(delegate
								{
									this.stattackPath = false;
								});
							});
						}
						else
						{
							this.transform.DOLocalMove(localPoint, galagaInfo.speedReturn, false).OnComplete(delegate
							{
								this.stattackPath = false;
							});
						}
					});
				});
			}
		}
	}

	public bool Check1EnemyGalaGa(float unitXStart, float unitYStart, float unitX, float unitY)
	{
		this.hit = Physics2D.Linecast(new Vector3(base.transform.position.x - unitXStart, base.transform.position.y - unitYStart, base.transform.position.z), new Vector3(base.transform.position.x - unitX, base.transform.position.y - unitY, base.transform.position.z), this.layerWall);
		return this.hit && this.hit.collider != null;
	}

	[EnemyAction(displayName = "Action Xếp Hình - Xếp Xong Hình Cuối Cùng/Di Chuyển Theo Path- Hết Path Di Chuyển Theo Path Loop")]
	public void ActionShapeMovePathToPathLoop(PathManager pathEnemy, float speed, PathManager pathLoop, float speedPathLoop, int numberLoop, Ease easePath1, Ease easepathLoop)
	{
		if (this.waveControl.numberEnemyMove < this.waveControl.maxEnemyMove)
		{
			if (easePath1 == Ease.Unset)
			{
				easePath1 = Ease.Linear;
			}
			if (easepathLoop == Ease.Unset)
			{
				easepathLoop = Ease.Linear;
			}
			if (!this.stattackPath)
			{
				this.stattackPath = true;
				this.waveControl.numberEnemyMove = this.waveControl.numberEnemyMove + 1;
				if (this.afterSecond2 != null)
				{
					this.afterSecond2.ForEach(delegate(EnemyEventUnit<float> x)
					{
						this.doActionAttack = this.DoActionEveryTime(x.param, new Action(x.Invoke), false, false);
					});
				}
				this.SetFieldPath(speed, pathEnemy, easePath1);
				Vector3 posLocal = this.SetDefaultPath();
				this._splineMove.events[pathEnemy.GetWaypointCount() - 1].AddListener(delegate()
				{
					this._splineMove.events.Clear();
					this.SetPathLoopShape(pathLoop, speedPathLoop, posLocal, numberLoop);
				});
			}
		}
	}

	private Vector3 SetDefaultPath()
	{
		this.stattackPath = true;
		Vector3 vector = default(Vector3);
		return base.transform.localPosition;
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
				this.ResetNumberMove();
			});
		});
	}

	[EnemyAction(displayName = "Action Xếp Hình - Xếp Xong Hình Cuối Cùng/Di Chuyển Ngẫu Nhiên 2 điểm - Hết Path Di Chuyển Theo Path Loop")]
	public void ActionShapeMovePath(float speed, PathManager pathLoop, Vector3 pointPathLoop, float speedPathLoop, int numberLoop)
	{
		if (this.waveControl.numberEnemyMove < this.waveControl.maxEnemyMove)
		{
			Vector3[] path = new Vector3[]
			{
				base.transform.position,
				new Vector3(UnityEngine.Random.Range(-4.2f, 4.2f), (base.transform.position.y + pointPathLoop.y) / 2f, base.transform.position.z),
				pointPathLoop
			};
			if (!this.stattackPath)
			{
				this.waveControl.numberEnemyMove = this.waveControl.numberEnemyMove + 1;
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
	}

	[EnemyAction(displayName = "Action Xếp Hình - Xếp Xong Hình Cuối Cùng/Di Chuyển RanDom theo nhiều điểm phía dưới màn hình")]
	public void ActionShapeMovePath_FollowPlayer(float speed, int numberPoint, bool randomNunberPoint, int minPoint, float minx, float maxx, float miny, float maxy)
	{
		if (this.waveControl.numberEnemyMove < this.waveControl.maxEnemyMove)
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
				this.waveControl.numberEnemyMove = this.waveControl.numberEnemyMove + 1;
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
						this.ResetNumberMove();
					});
				});
			}
		}
	}

	[EnemyAction(displayName = "Action Xếp Hình - Xếp Xong Hình Cuối Cùng/Di Chuyển Theo Path- Hết Path Di Chuyển Theo Player")]
	public void ActionShapeMovePathFlowPlayer(PathManager pathEnemy, float speed, float speedFollow, float timeFollow, Ease ease)
	{
		if (this.waveControl.numberEnemyMove < this.waveControl.maxEnemyMove && !this.stattackPath)
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
				this.stattackPath = true;
				this.waveControl.numberEnemyMove = this.waveControl.numberEnemyMove + 1;
				this.SetFieldPath(speed, pathEnemy, ease);
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
			this.ResetNumberMove();
		});
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "Attack/Bắn Hướng vào Player")]
	public void AttackToPlayer(float timeAttack, int numStyleAttack)
	{
		if (this.childObjAttack != null)
		{
			this.childObjAttack.m_bulletSpeed = this.speedBullet;
		}
		Quaternion rotation = this.avatar.transform.rotation;
		this.avatar.GetComponent<EnemyFollowPlayer>().enabled = true;
		this.avatarSpawBullet.numberAttack = numStyleAttack;
		this.avatarSpawBullet.ActiveEffect();
		this.avatarSpawBullet.stFlowPlayer = true;
		this.avatarSpawBullet.rotAvatar = rotation;
		base.StartCoroutine(this.AttackToPlayerIE(timeAttack, rotation));
	}

	private IEnumerator AttackToPlayerIE(float timeAttack, Quaternion rotAvatar)
	{
		yield return new WaitForSeconds(timeAttack);
		this._anim.SetTrigger("attack");
		yield return null;
		yield break;
	}

	[EnemyAction(displayName = "Attack/Bắn Hướng vào Player Theo ID")]
	public void AttackToPlayerID(int[] listIndex, int numStyleAttack, float timeDelay = 1f)
	{
		if (timeDelay == 0f)
		{
			timeDelay = 1f;
		}
		bool flag = false;
		for (int i = 0; i < listIndex.Length; i++)
		{
			if (this.Index == listIndex[i])
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.AttackToPlayer(timeDelay, numStyleAttack);
		}
	}

	[EnemyAction(displayName = "Enemy/Quay góc Avatar")]
	public void RotateAvatar()
	{
	}

	public float speedBullet = 10f;

	public bool stAttackThrow;

	[Header("ANIMATION")]
	public GameObject avatar;

	private Animator _anim;

	[Header("ATTACK")]
	public bool stHold = true;

	public float timeAttack;

	public UbhBaseShot childObjAttack;

	public UbhBaseShot childObjAttack2;

	public bool stattackPath;

	[Header("MULTY_ATTACK")]
	public bool multiAttack;

	[ShowIf("multiAttack", true)]
	public List<UbhShotCtrl> listUbh = new List<UbhShotCtrl>();

	[ShowIf("multiAttack", true)]
	public List<float> listTimeAttack = new List<float>();

	[ShowIf("multiAttack", true)]
	public int add;

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

	[EnemyEventCustom(paramName = "time", displayName = "BaseEnemy/Event Lặp sau Khi tất cả Enemy Sinh ra")]
	public EnemyEvent<int> EveryXSEvent = new EnemyEvent<int>();

	[EnemyEventCustom(paramName = "time", displayName = "BaseEnemy/Event Lặp Khi Enemy sinh ra")]
	public EnemyEvent<float> EventLoop = new EnemyEvent<float>();

	[EnemyEventCustom(paramName = "Second", displayName = "Di Chuyen Galaga/AfterSecond")]
	public EnemyEvent<float> afterSecond2 = new EnemyEvent<float>();

	[EnemyEventCustom(displayName = "BaseEnemy/Trúng Đạn")]
	public EnemyEvent hitEvent;

	private bool startAttack;

	public ShapeInfoTurn listpositionEne;

	public splineMove _splineMove;

	[Header("MOVE_TURN")]
	public bool stParent;

	public bool stSpawGold;

	private AnimationCurve curvePrivate;

	public bool stSpawItemUpgrade;

	[Header("DAP_DENH")]
	public bool setDapDenh;

	[ShowIf("setDapDenh", true)]
	public float minY = 0.4f;

	[ShowIf("setDapDenh", true)]
	public float maxYDD = 0.6f;

	[ShowIf("setDapDenh", true)]
	public float minDuration = 0.4f;

	[ShowIf("setDapDenh", true)]
	public float maxDuration = 0.6f;

	[Header("Trung_Dan")]
	public bool setTrungDan;

	[ShowIf("setTrungDan", true)]
	public float yTD;

	[ShowIf("setTrungDan", true)]
	public float durationTrungDan = 0.1f;

	[ShowIf("setTrungDan", true)]
	public float durationTrungDan2 = 0.2f;

	private int numberAvatar;

	public List<RuntimeAnimatorController> listAnimator = new List<RuntimeAnimatorController>();

	private RuntimeAnimatorController _animatorStart;

	private bool gotoPlayer;

	private float speedMovetoPlayer = 2f;

	private bool noHit;

	private AvatarSpawnBullet avatarSpawBullet;

	private IDisposable doActionAttack;
}
