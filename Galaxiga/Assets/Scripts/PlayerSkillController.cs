using System;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void StartSpecialSkill()
	{
	}

	public virtual void StopSpecialSkill()
	{
	}

	public virtual void SetPowerSkil(int power, float _fireRate)
	{
		this.powerSkill = power;
		this.fireRate = _fireRate;
	}

	public int powerSkill;

	public float fireRate;
}
