using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTutorial", menuName = "New DataTutorial", order = 51)]
public class TutorialData : ScriptableObject
{
	[SerializeField]
	public string[] mess;
}
