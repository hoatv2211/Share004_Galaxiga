using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ViewVIP : MonoBehaviour
{
	private void Start()
	{
		this.btnAdd.onClick.AddListener(new UnityAction(this.ShowPanelShop));
		this.vipPoint = this.vipManager.arrVIPs[this.vip - 1].vipPoint;
		this.sliderProgressVIP.maxValue = (float)this.vipPoint;
		this.UpdateVIP();
	}

	public void UpdateVIP()
	{
		this.sliderProgressVIP.value = (float)GameContext.currentVIPPoint;
		if (GameContext.currentVIPPoint >= this.vipPoint)
		{
			this.textCurrentVIPPoint.text = this.vipPoint + "/" + this.vipPoint;
			this.youNeedMoreVIP.text = string.Format(ScriptLocalization.you_need_more_vip_point, 0, this.vip);
		}
		else
		{
			this.textCurrentVIPPoint.text = GameContext.currentVIPPoint + "/" + this.vipPoint;
			this.youNeedMoreVIP.text = string.Format(ScriptLocalization.you_need_more_vip_point, this.vipPoint - GameContext.currentVIPPoint, this.vip);
		}
	}

	public void ShowPanelShop()
	{
		this.vipManager.HidePanelVIP();
		ShopManager.Instance.ShowPopupShopGem(3);
	}

	public int vip;

	public VIPManager vipManager;

	public Slider sliderProgressVIP;

	public Text textCurrentVIPPoint;

	public Text youNeedMoreVIP;

	public Button btnAdd;

	private int vipPoint;
}
