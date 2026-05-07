using System;
using SWS;
using UnityEngine;

[Serializable]
public class TurnGalagaInfo
{
	public bool isLeft;

	public float second;

	public int number;

	public int indexMin;

	public int indexMax;

	public PathManager pathAttack;

	public float speedMovePath;

	public float speedReturn;

	public bool stMoveToPath;

	public float speedMoveToPath;

	public Vector3 posPath;

	public bool telepos;

	public Vector3 telePosition;

	public bool followPlayer;

	public float speedMoveToPlayer;

	public float percentAction;
}
