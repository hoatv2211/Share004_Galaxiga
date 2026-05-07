using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoreMountains.Tools;
using UnityEngine;

public class FireBaseRemote : PersistentSingleton<FireBaseRemote>
{
	protected override void Awake()
	{
		base.Awake();
		
	}

	private void InitializeFirebase()
	{
		
	}



	public bool isFirebaseInitialized;
}
