using System;
using SkyGameKit;
using SWS;
using UnityEngine;

public class BulletFollowPath : MonoBehaviour
{
	private void Start()
	{
	}

	public void Attack(int idPath, int idBullet, float speed, Transform posPlayer = null, bool isFindFullMap = false)
	{
		UbhBullet bullet = UbhSingletonMonoBehavior<UbhObjectPool>.instance.GetBullet(this.prefabsBullet[idBullet], base.transform.position, false);
		this.sMove = bullet.GetComponent<splineMove>();
		this.bezierPathManager = this.pathEnemy[idPath].GetComponent<BezierPathManager>();
		if (this.bezierPathManager != null)
		{
			this.bezierPathManager.CalculatePath();
		}
		this.sMove.speed = speed;
		this.pathEnemy[idPath].waypoints[this.pathEnemy[idPath].waypoints.Length - 1].transform.position = new Vector3(this.pathEnemy[idPath].waypoints[this.pathEnemy[idPath].waypoints.Length - 1].transform.position.x, SgkCamera.bottomLeft.y + 0.4f, 0f);
		if (posPlayer)
		{
			this.FindPlayer(idPath, posPlayer, isFindFullMap);
		}
		this.sMove.SetPath(this.pathEnemy[idPath]);
	}

	private void FindPlayer(int idPath, Transform posPlayer, bool isFindFullMap)
	{
		if (isFindFullMap)
		{
			if (this.pathEnemy[idPath].waypoints[0].transform.position.y > posPlayer.position.y)
			{
				this.pathEnemy[idPath].waypoints[this.pathEnemy[idPath].waypoints.Length - 1].transform.position = new Vector3(posPlayer.position.x, this.pathEnemy[idPath].waypoints[this.pathEnemy[idPath].waypoints.Length - 1].transform.position.y, 0f);
			}
			else
			{
				this.pathEnemy[idPath].waypoints[this.pathEnemy[idPath].waypoints.Length - 1].transform.position = new Vector3(posPlayer.position.x, -this.pathEnemy[idPath].waypoints[this.pathEnemy[idPath].waypoints.Length - 1].transform.position.y, 0f);
			}
		}
		else
		{
			this.pathEnemy[idPath].waypoints[this.pathEnemy[idPath].waypoints.Length - 1].transform.position = new Vector3(posPlayer.position.x, this.pathEnemy[idPath].waypoints[this.pathEnemy[idPath].waypoints.Length - 1].transform.position.y, 0f);
		}
	}

	private splineMove sMove;

	private BezierPathManager bezierPathManager;

	[SerializeField]
	private PathManager[] pathEnemy;

	[SerializeField]
	private GameObject[] prefabsBullet;
}
