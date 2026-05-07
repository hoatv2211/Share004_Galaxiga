using System;
using System.Collections.Generic;
using SkyGameKit;

public class NewBoss : BossGeneral
{
	private void Start()
	{
		NewBoss.ints = this;
	}

	public void ShowRedScreen()
	{
		UIBossMode.inst.Show();
	}

	public void ShowTextScreen()
	{
		UIBossMode.inst.ShowText();
	}

	public void HideRedScreen()
	{
		UIBossMode.inst.Hide();
	}

	public void ShowHealthBar()
	{
		UIBossMode.inst.txtName.gameObject.SetActive(true);
		UIBossMode.inst.parentHealthBar.SetActive(true);
		if (UIBossMode.inst.numberHealthBar == 1)
		{
			UIBossMode.inst.listHealthBar[0].gameObject.SetActive(true);
			UIBossMode.inst.listHealthBar[1].gameObject.SetActive(false);
			UIBossMode.inst.listHealthBar[2].gameObject.SetActive(false);
		}
		else if (UIBossMode.inst.numberHealthBar == 2)
		{
			UIBossMode.inst.listHealthBar[0].gameObject.SetActive(true);
			UIBossMode.inst.listHealthBar[1].gameObject.SetActive(true);
			UIBossMode.inst.listHealthBar[2].gameObject.SetActive(false);
		}
		else if (UIBossMode.inst.numberHealthBar == 3)
		{
			UIBossMode.inst.listHealthBar[0].gameObject.SetActive(true);
			UIBossMode.inst.listHealthBar[1].gameObject.SetActive(true);
			UIBossMode.inst.listHealthBar[2].gameObject.SetActive(true);
		}
	}

	public void HideHealthBar()
	{
		UIBossMode.inst.txtName.gameObject.SetActive(false);
		UIBossMode.inst.parentHealthBar.SetActive(false);
	}

	public void SetNameBoss(string name)
	{
		UIBossMode.inst.txtName.text = name;
	}

	public override void Die(EnemyKilledBy type = EnemyKilledBy.Player)
	{
		base.Die(type);
		this.HideRedScreen();
		this.HideHealthBar();
		foreach (TurnManager turnManager in this.childTurn)
		{
			turnManager.ForceStopAndKillAllEnemies();
		}
		this.childTurn.Clear();
	}

	[EnemyAction(displayName = "NewBoss/StartFreeTurn")]
	public virtual void StartTurnByNewBoss(TurnManager turn)
	{
		turn.StartWithDelayAndDisplay();
		this.childTurn.Add(turn);
	}

	public override int CurrentHP
	{
		get
		{
			return base.CurrentHP;
		}
		set
		{
			base.CurrentHP = value;
			float num = 1f;
			if (UIBossMode.inst.numberHealthBar == 1)
			{
				this.newRatio = (float)this.CurrentHP * 1f / (float)this.startHP;
				UIBossMode.inst.listHealthBar[0].fillAmount = this.newRatio;
			}
			else if (UIBossMode.inst.numberHealthBar == 2)
			{
				if ((float)this.CurrentHP * 1f / (float)this.startHP < 1f)
				{
					num = (float)this.CurrentHP * 1f / (float)this.startHP;
					if (num >= 0.5f)
					{
						this.newStartHP = (float)this.startHP * 0.5f;
						this.newCurrentHP = this.newStartHP - (float)this.startHP + (float)this.CurrentHP;
						this.newRatio = this.newCurrentHP / this.newStartHP;
						UIBossMode.inst.listHealthBar[1].fillAmount = this.newRatio;
						UIBossMode.inst.listHealthBar[0].fillAmount = 1f;
					}
					else
					{
						this.newStartHP = (float)this.startHP * 0.5f;
						this.newCurrentHP = this.newStartHP - (float)this.startHP * 0.5f + (float)this.CurrentHP;
						this.newRatio = this.newCurrentHP / this.newStartHP;
						UIBossMode.inst.listHealthBar[0].fillAmount = this.newRatio;
					}
				}
			}
			else if (UIBossMode.inst.numberHealthBar == 3)
			{
				if ((float)this.CurrentHP * 1f / (float)this.startHP < 1f)
				{
					num = (float)this.CurrentHP * 1f / (float)this.startHP;
					if (num >= 0.7f)
					{
						this.newStartHP = (float)this.startHP * 0.3f;
						this.newCurrentHP = this.newStartHP - (float)this.startHP + (float)this.CurrentHP;
						this.newRatio = this.newCurrentHP / this.newStartHP;
						UIBossMode.inst.listHealthBar[2].fillAmount = this.newRatio;
						UIBossMode.inst.listHealthBar[1].fillAmount = 1f;
						UIBossMode.inst.listHealthBar[0].fillAmount = 1f;
					}
					else if (num >= 0.35f)
					{
						this.newStartHP = (float)this.startHP * 0.35f;
						this.newCurrentHP = this.newStartHP - (float)this.startHP * 0.7f + (float)this.CurrentHP;
						this.newRatio = this.newCurrentHP / this.newStartHP;
						UIBossMode.inst.listHealthBar[1].fillAmount = this.newRatio;
						UIBossMode.inst.listHealthBar[0].fillAmount = 1f;
					}
					else if (num >= 0f)
					{
						float num2 = (float)this.startHP * 0.35f;
						float num3 = num2 - (float)this.startHP * 0.35f + (float)this.CurrentHP;
						float fillAmount = num3 / num2;
						UIBossMode.inst.listHealthBar[0].fillAmount = fillAmount;
					}
				}
				if ((double)num < 1.0 && this.CurrentHP > 0)
				{
					base.HitBossEvent();
				}
				if (num < 0f)
				{
				}
			}
		}
	}

	public static NewBoss ints;

	private float newRatio;

	private float newStartHP;

	private float newCurrentHP;

	private List<TurnManager> childTurn = new List<TurnManager>();
}
