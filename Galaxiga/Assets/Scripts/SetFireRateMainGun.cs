using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class SetFireRateMainGun : MonoBehaviour
{
	private void Awake()
	{
		if (this.isNWayShot)
		{
			this.ubhNwayShot = base.GetComponent<UbhNwayShot>();
		}
		if (this.isLinearShot)
		{
			this.ubhLinearShot = base.GetComponent<UbhLinearShot>();
		}
		if (this.isRandomShot)
		{
			this.ubhRandomShot = base.GetComponent<UbhRandomShot>();
		}
	}

	private void OnEnable()
	{
		this.SetFireRate();
	}

	private void SetFireRate()
	{
		switch (this.planeID)
		{
		case GameContext.Plane.BataFD01:
			if (this.isNWayShot)
			{
				this.ubhNwayShot.m_nextLineDelay = ShipBataFD01Sheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isLinearShot)
			{
				this.ubhLinearShot.m_betweenDelay = ShipBataFD01Sheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isRandomShot)
			{
				this.ubhRandomShot.m_randomDelayMax = ShipBataFD01Sheet.Get(this.indexShot).mainFireRate;
				this.ubhRandomShot.m_randomDelayMin = ShipBataFD01Sheet.Get(this.indexShot).mainFireRate;
			}
			break;
		case GameContext.Plane.FuryOfAres:
			if (this.isNWayShot)
			{
				this.ubhNwayShot.m_nextLineDelay = ShipFuryOfAresSheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isLinearShot)
			{
				this.ubhLinearShot.m_betweenDelay = ShipFuryOfAresSheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isRandomShot)
			{
				this.ubhRandomShot.m_randomDelayMax = ShipFuryOfAresSheet.Get(this.indexShot).mainFireRate;
				this.ubhRandomShot.m_randomDelayMin = ShipFuryOfAresSheet.Get(this.indexShot).mainFireRate;
			}
			break;
		case GameContext.Plane.SkyWraith:
			if (this.isNWayShot)
			{
				this.ubhNwayShot.m_nextLineDelay = ShipSkywraithSheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isLinearShot)
			{
				this.ubhLinearShot.m_betweenDelay = ShipSkywraithSheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isRandomShot)
			{
				this.ubhRandomShot.m_randomDelayMax = ShipSkywraithSheet.Get(this.indexShot).mainFireRate;
				this.ubhRandomShot.m_randomDelayMin = ShipSkywraithSheet.Get(this.indexShot).mainFireRate;
			}
			break;
		case GameContext.Plane.TwilightX:
			if (this.isNWayShot)
			{
				this.ubhNwayShot.m_nextLineDelay = ShipTwilightXSheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isLinearShot)
			{
				this.ubhLinearShot.m_betweenDelay = ShipTwilightXSheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isRandomShot)
			{
				this.ubhRandomShot.m_randomDelayMax = ShipTwilightXSheet.Get(this.indexShot).mainFireRate;
				this.ubhRandomShot.m_randomDelayMin = ShipTwilightXSheet.Get(this.indexShot).mainFireRate;
			}
			break;
		case GameContext.Plane.Greataxe:
			if (this.isNWayShot)
			{
				this.ubhNwayShot.m_nextLineDelay = ShipGreataxeSheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isLinearShot)
			{
				this.ubhLinearShot.m_betweenDelay = ShipGreataxeSheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isRandomShot)
			{
				this.ubhRandomShot.m_randomDelayMax = ShipGreataxeSheet.Get(this.indexShot).mainFireRate;
				this.ubhRandomShot.m_randomDelayMin = ShipGreataxeSheet.Get(this.indexShot).mainFireRate;
			}
			break;
		case GameContext.Plane.SSLightning:
			if (this.isNWayShot)
			{
				this.ubhNwayShot.m_nextLineDelay = ShipSSLightningSheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isLinearShot)
			{
				this.ubhLinearShot.m_betweenDelay = ShipSSLightningSheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isRandomShot)
			{
				this.ubhRandomShot.m_randomDelayMax = ShipSSLightningSheet.Get(this.indexShot).mainFireRate;
				this.ubhRandomShot.m_randomDelayMin = ShipSSLightningSheet.Get(this.indexShot).mainFireRate;
			}
			break;
		case GameContext.Plane.Warlock:
			if (this.isNWayShot)
			{
				this.ubhNwayShot.m_nextLineDelay = ShipWarlockSheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isLinearShot)
			{
				this.ubhLinearShot.m_betweenDelay = ShipWarlockSheet.Get(this.indexShot).mainFireRate;
			}
			if (this.isRandomShot)
			{
				this.ubhRandomShot.m_randomDelayMax = ShipWarlockSheet.Get(this.indexShot).mainFireRate;
				this.ubhRandomShot.m_randomDelayMin = ShipWarlockSheet.Get(this.indexShot).mainFireRate;
			}
			break;
		}
	}

	public GameContext.Plane planeID;

	public int indexShot;

	[HideIf("isLinearShot", true)]
	[HideIf("isRandomShot", true)]
	public bool isNWayShot;

	[HideIf("isNWayShot", true)]
	[HideIf("isRandomShot", true)]
	public bool isLinearShot;

	[HideIf("isNWayShot", true)]
	[HideIf("isLinearShot", true)]
	public bool isRandomShot;

	private UbhNwayShot ubhNwayShot;

	private UbhLinearShot ubhLinearShot;

	private UbhRandomShot ubhRandomShot;
}
