using System;
using UnityEngine;

public class PathSpline2d : MonoBehaviour
{
	private void Start()
	{
		Vector3[] array = new Vector3[]
		{
			this.cubes[0].position,
			this.cubes[1].position,
			this.cubes[2].position,
			this.cubes[3].position,
			this.cubes[4].position
		};
		this.visualizePath = new LTSpline(array);
		LeanTween.moveSpline(this.dude1, array, 10f).setOrientToPath2d(true).setSpeed(2f);
		LeanTween.moveSplineLocal(this.dude2, array, 10f).setOrientToPath2d(true).setSpeed(2f);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		if (this.visualizePath != null)
		{
			this.visualizePath.gizmoDraw(-1f);
		}
	}

	public Transform[] cubes;

	public GameObject dude1;

	public GameObject dude2;

	private LTSpline visualizePath;
}
