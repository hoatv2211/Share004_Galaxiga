using System;
using UnityEngine;

public class UIActiveSpecialSkill : MonoBehaviour
{
	public static UIActiveSpecialSkill current
	{
		get
		{
			if (UIActiveSpecialSkill._current == null)
			{
				UIActiveSpecialSkill._current = UnityEngine.Object.FindObjectOfType<UIActiveSpecialSkill>();
			}
			return UIActiveSpecialSkill._current;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ShowBlueScreen(int id)
	{
		this.listFxScreen[id].SetActive(true);
	}

	public void HideBlueScreen(int id)
	{
		for (int i = 0; i < this.listFxScreen.Length; i++)
		{
			if (this.listFxScreen[i].activeInHierarchy)
			{
				this.listFxScreen[i].SetActive(false);
			}
		}
	}

	public void ShowBlueScreen()
	{
		if (!this.fxSkill.activeInHierarchy)
		{
			this.fxSkill.SetActive(true);
		}
	}

	public void HideBlueScreen()
	{
		if (this.fxSkill.activeInHierarchy)
		{
			this.fxSkill.SetActive(false);
		}
	}

	public static UIActiveSpecialSkill _current;

	[SerializeField]
	private GameObject[] listFxScreen;

	public GameObject fxSkill;
}
