using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class ViewDescritionStat : MonoBehaviour
{
	private void Start()
	{
		switch (this.statType)
		{
		case ViewDescritionStat.StatType.Power:
			this.textName.text = ScriptLocalization.power;
			this.textDescription.text = ScriptLocalization.increase_power;
			break;
		case ViewDescritionStat.StatType.SubPower:
			this.textName.text = ScriptLocalization.sub_power;
			this.textDescription.text = ScriptLocalization.increase_sub_power;
			break;
		case ViewDescritionStat.StatType.SpecPower:
			this.textName.text = ScriptLocalization.special_power;
			this.textDescription.text = ScriptLocalization.increase_damage_of_skill;
			break;
		case ViewDescritionStat.StatType.FireRate:
			this.textName.text = ScriptLocalization.fire_rate;
			this.textDescription.text = ScriptLocalization.decrease_fire_rate;
			break;
		case ViewDescritionStat.StatType.DurationSkill:
			this.textName.text = ScriptLocalization.duration_skill;
			this.textDescription.text = ScriptLocalization.duration_skill;
			break;
		}
	}

	public ViewDescritionStat.StatType statType;

	public Text textName;

	public Text textDescription;

	public enum StatType
	{
		Power,
		SubPower,
		SpecPower,
		FireRate,
		DurationSkill
	}
}
