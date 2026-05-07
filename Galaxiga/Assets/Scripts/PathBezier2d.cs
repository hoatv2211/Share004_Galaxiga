using System;
using UnityEngine;

public class PathBezier2d : MonoBehaviour
{
	private void Start()
	{
		Vector3[] array = new Vector3[]
		{
			this.cubes[0].position,
			this.cubes[1].position,
			this.cubes[2].position,
			this.cubes[3].position
		};
		this.visualizePath = new LTBezierPath(array);
		LeanTween.move(this.dude1, array, 10f).setOrientToPath2d(true);
		LeanTween.moveLocal(this.dude2, array, 10f).setOrientToPath2d(true);
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

	private LTBezierPath visualizePath;
}
