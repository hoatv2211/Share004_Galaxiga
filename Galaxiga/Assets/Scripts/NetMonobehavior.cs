using System;
using OSNet;
using UnityEngine;

public class NetMonobehavior : MonoBehaviour
{
	private void Awake()
	{
		NetManager.Instance.Start("https://35.225.207.203:10443/galaxiga/");
	}

	public const string uri = "https://35.225.207.203:10443/galaxiga/";
}
