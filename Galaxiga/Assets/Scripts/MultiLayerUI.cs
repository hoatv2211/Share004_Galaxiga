using System;
using HedgehogTeam.EasyTouch;
using UnityEngine;

public class MultiLayerUI : MonoBehaviour
{
	public void SetAutoSelect(bool value)
	{
		EasyTouch.SetEnableAutoSelect(value);
	}

	public void SetAutoUpdate(bool value)
	{
		EasyTouch.SetAutoUpdatePickedObject(value);
	}

	public void Layer1(bool value)
	{
		LayerMask mask = EasyTouch.Get3DPickableLayer();
		if (value)
		{
			mask |= 256;
		}
		else
		{
			mask = ~mask;
			mask = ~(mask | 256);
		}
		EasyTouch.Set3DPickableLayer(mask);
	}

	public void Layer2(bool value)
	{
		LayerMask mask = EasyTouch.Get3DPickableLayer();
		if (value)
		{
			mask |= 512;
		}
		else
		{
			mask = ~mask;
			mask = ~(mask | 512);
		}
		EasyTouch.Set3DPickableLayer(mask);
	}

	public void Layer3(bool value)
	{
		LayerMask mask = EasyTouch.Get3DPickableLayer();
		if (value)
		{
			mask |= 1024;
		}
		else
		{
			mask = ~mask;
			mask = ~(mask | 1024);
		}
		EasyTouch.Set3DPickableLayer(mask);
	}
}
