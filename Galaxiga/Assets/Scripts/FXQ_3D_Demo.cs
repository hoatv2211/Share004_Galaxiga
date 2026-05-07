using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FXQ_3D_Demo : MonoBehaviour
{
	private void Awake()
	{
		if (base.enabled)
		{
			GUIAnimSystemFREE.Instance.m_GUISpeed = 4f;
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}
	}

	private void Start()
	{
		if (this.m_ParticleTypeList.Length > 0)
		{
			this.m_ParticleType = 0;
			this.m_ParticleTypeOld = -1;
			this.m_ParticleIndex = 0;
			this.m_ParticleIndexOld = -1;
		}
		base.StartCoroutine(this.ShowUIs());
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.UpArrow))
		{
			this.NextParticleType();
		}
		else if (UnityEngine.Input.GetKeyUp(KeyCode.DownArrow))
		{
			this.PreviousParticleType();
		}
		else if (UnityEngine.Input.GetKeyUp(KeyCode.LeftArrow))
		{
			this.PreviousParticle();
		}
		else if (UnityEngine.Input.GetKeyUp(KeyCode.RightArrow))
		{
			this.NextParticle();
		}
		else if (UnityEngine.Input.GetKeyUp(KeyCode.Return) || UnityEngine.Input.GetKeyUp(KeyCode.KeypadEnter))
		{
			this.ShowParticle();
		}
	}

	private IEnumerator ShowUIs()
	{
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(this.m_3DDemo_UI, false);
		yield return new WaitForSeconds(0.25f);
		GUIAnimSystemFREE.Instance.MoveIn(this.m_Options_Window.transform, true);
		yield return new WaitForSeconds(0.25f);
		GUIAnimSystemFREE.Instance.MoveIn(this.m_ParticleDetails_Button_ParticleName.transform, true);
		yield return new WaitForSeconds(0.5f);
		GUIAnimSystemFREE.Instance.MoveIn(this.m_ParticleSelection_Window.transform, true);
		GUIAnimSystemFREE.Instance.MoveIn(this.m_HowTo.transform, true);
		yield return new WaitForSeconds(0.25f);
		GUIAnimSystemFREE.Instance.MoveIn(this.m_SelectDemo_Button.transform, true);
		GUIAnimSystemFREE.Instance.MoveIn(this.m_Help_Button.transform, true);
		GUIAnimSystemFREE.Instance.SetGraphicRaycasterEnable(this.m_3DDemo_UI, true);
		this.ShowParticle();
		this.SetParticleType(0);
		this.UpdateToggleDayNight();
		yield break;
	}

	private void ShowParticle()
	{
		if (this.m_ParticleType >= this.m_ParticleTypeList.Length)
		{
			this.m_ParticleType = 0;
		}
		else if (this.m_ParticleType < 0)
		{
			this.m_ParticleType = this.m_ParticleTypeList.Length - 1;
		}
		if (this.m_ParticleType != this.m_ParticleTypeOld)
		{
			if (this.m_ParticleTypeOld >= 0)
			{
				int num = 0;
				IEnumerator enumerator = this.m_ParticleTypeList[this.m_ParticleTypeOld].m_Particles.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Transform transform = (Transform)obj;
						ParticleSystem component = transform.gameObject.GetComponent<ParticleSystem>();
						if (component != null)
						{
							component.Stop();
							component.gameObject.SetActive(false);
						}
						num++;
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			if (this.m_ParticleType >= 0)
			{
				int num = 0;
				IEnumerator enumerator2 = this.m_ParticleTypeList[this.m_ParticleType].m_Particles.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						Transform transform2 = (Transform)obj2;
						ParticleSystem component2 = transform2.gameObject.GetComponent<ParticleSystem>();
						if (component2 != null)
						{
							component2.Stop();
							component2.gameObject.SetActive(false);
						}
						num++;
					}
				}
				finally
				{
					IDisposable disposable2;
					if ((disposable2 = (enumerator2 as IDisposable)) != null)
					{
						disposable2.Dispose();
					}
				}
			}
			if (this.m_ParticleTypeOld >= 0)
			{
				this.m_ParticleTypeList[this.m_ParticleTypeOld].m_Particles.gameObject.SetActive(false);
			}
			if (this.m_ParticleType >= 0)
			{
				this.m_ParticleTypeList[this.m_ParticleType].m_Particles.gameObject.SetActive(true);
			}
			this.m_ParticleTypeName = this.m_ParticleTypeList[this.m_ParticleType].m_Particles.name;
			this.m_ParticleTypeChildCount = this.m_ParticleTypeList[this.m_ParticleType].m_Particles.childCount;
		}
		if (this.m_ParticleIndex >= this.m_ParticleTypeChildCount)
		{
			this.m_ParticleIndex = 0;
		}
		else if (this.m_ParticleIndex < 0)
		{
			this.m_ParticleIndex = this.m_ParticleTypeChildCount - 1;
		}
		if (this.m_ParticleIndex != this.m_ParticleIndexOld || this.m_ParticleType != this.m_ParticleTypeOld)
		{
			if (this.m_Particle != null)
			{
				this.m_Particle.Stop();
				this.m_Particle.gameObject.SetActive(false);
			}
			int num = 0;
			IEnumerator enumerator3 = this.m_ParticleTypeList[this.m_ParticleType].m_Particles.GetEnumerator();
			try
			{
				while (enumerator3.MoveNext())
				{
					object obj3 = enumerator3.Current;
					Transform transform3 = (Transform)obj3;
					if (num == this.m_ParticleIndex)
					{
						this.m_ParticleOld = this.m_Particle;
						this.m_Particle = transform3.gameObject.GetComponent<ParticleSystem>();
						if (this.m_Particle != null)
						{
							this.m_Particle.gameObject.SetActive(true);
							this.m_Particle.Play();
							this.m_ParticleName = this.m_Particle.name;
							this.m_ParticleDetails_Text_Name.text = this.m_ParticleName;
							this.m_ParticleDetails_Text_Order.text = string.Concat(new object[]
							{
								this.m_ParticleTypeName,
								" (",
								this.m_ParticleIndex + 1,
								" / ",
								this.m_ParticleTypeChildCount,
								")"
							});
						}
						break;
					}
					num++;
				}
			}
			finally
			{
				IDisposable disposable3;
				if ((disposable3 = (enumerator3 as IDisposable)) != null)
				{
					disposable3.Dispose();
				}
			}
		}
	}

	private void SetParticleType(int ParticleType)
	{
		this.m_ParticleTypeOld = this.m_ParticleType;
		this.m_ParticleType = ParticleType;
		this.m_ParticleIndexOld = this.m_ParticleIndex;
		this.m_ParticleIndex = 0;
		this.UpdateButtonParticleType();
		this.ShowParticle();
	}

	private void NextParticleType()
	{
		this.m_ParticleTypeOld = this.m_ParticleType;
		this.m_ParticleType++;
		this.m_ParticleIndexOld = this.m_ParticleIndex;
		this.m_ParticleIndex = 0;
		if (this.m_ParticleType >= this.m_ParticleTypeList.Length)
		{
			this.m_ParticleType = 0;
		}
		this.UpdateButtonParticleType();
		this.ShowParticle();
	}

	private void PreviousParticleType()
	{
		this.m_ParticleTypeOld = this.m_ParticleType;
		this.m_ParticleType--;
		this.m_ParticleIndexOld = this.m_ParticleIndex;
		this.m_ParticleIndex = 0;
		if (this.m_ParticleType < 0)
		{
			this.m_ParticleType = this.m_ParticleTypeList.Length - 1;
		}
		this.UpdateButtonParticleType();
		this.ShowParticle();
	}

	private void NextParticle()
	{
		this.m_ParticleIndexOld = this.m_ParticleIndex;
		this.m_ParticleIndex++;
		this.ShowParticle();
	}

	private void PreviousParticle()
	{
		this.m_ParticleIndexOld = this.m_ParticleIndex;
		this.m_ParticleIndex--;
		this.ShowParticle();
	}

	private void UpdateButtonParticleType()
	{
		for (int i = 0; i < this.m_ParticleTypeList.Length; i++)
		{
			if (i == this.m_ParticleType)
			{
				if (this.m_ParticleTypeList[i].m_Buttons.interactable)
				{
					this.m_ParticleTypeList[i].m_Buttons.interactable = false;
					GUIAnimFREE component = this.m_ParticleTypeList[i].m_Buttons.gameObject.GetComponent<GUIAnimFREE>();
					if (component != null)
					{
						component.m_ScaleOut.Enable = true;
						component.m_ScaleOut.Time = 1.5f;
						component.m_ScaleOut.ScaleEnd = new Vector3(1.25f, 1.25f, 1.25f);
						component.MoveOut();
					}
				}
			}
			else if (!this.m_ParticleTypeList[i].m_Buttons.interactable)
			{
				this.m_ParticleTypeList[i].m_Buttons.interactable = true;
				GUIAnimFREE component2 = this.m_ParticleTypeList[i].m_Buttons.gameObject.GetComponent<GUIAnimFREE>();
				if (component2 != null)
				{
					component2.m_ScaleIn.Enable = true;
					component2.m_ScaleIn.Time = 1.5f;
					component2.m_ScaleIn.ScaleBegin = new Vector3(1.25f, 1.25f, 1.25f);
					component2.MoveIn();
				}
			}
		}
	}

	private void UpdateToggleDayNight()
	{
		if (this.m_Options_Toggle_Day.isOn)
		{
			RenderSettings.skybox = this.m_Day.m_Skybox;
			RenderSettings.ambientLight = this.m_Day.m_AmbientLight;
			RenderSettings.fogColor = this.m_Day.m_FogColor;
			RenderSettings.fog = true;
		}
		else if (this.m_Options_Toggle_Night.isOn)
		{
			RenderSettings.skybox = this.m_Night.m_Skybox;
			RenderSettings.ambientLight = this.m_Night.m_AmbientLight;
			RenderSettings.fogColor = this.m_Night.m_FogColor;
			RenderSettings.fog = true;
		}
	}

	public void Button_SelectDemo()
	{
		GUIAnimSystemFREE.Instance.MoveOut(this.m_SelectDemo_Button.transform, true);
		GUIAnimSystemFREE.Instance.MoveIn(this.m_SelectDemo_Window.transform, true);
		FXQ_SoundController.Instance.Play_SoundBack();
	}

	public void Button_SelectDemo_Minimize()
	{
		GUIAnimSystemFREE.Instance.MoveIn(this.m_SelectDemo_Button.transform, true);
		GUIAnimSystemFREE.Instance.MoveOut(this.m_SelectDemo_Window.transform, true);
		FXQ_SoundController.Instance.Play_SoundBack();
	}

	public void Button_SelectDemo_2D()
	{
		GUIAnimSystemFREE.Instance.LoadLevel("2D Demo", 1f);
		FXQ_SoundController.Instance.Play_SoundPress();
	}

	public void Button_SelectDemo_3D()
	{
	}

	public void Toggle_ShowDay()
	{
		if (this.m_Options_Toggle_Day.isOn)
		{
			RenderSettings.skybox = this.m_Day.m_Skybox;
			RenderSettings.ambientLight = this.m_Day.m_AmbientLight;
			RenderSettings.fogColor = this.m_Day.m_FogColor;
			RenderSettings.fog = true;
		}
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Toggle_ShowNight()
	{
		if (this.m_Options_Toggle_Night.isOn)
		{
			RenderSettings.skybox = this.m_Night.m_Skybox;
			RenderSettings.ambientLight = this.m_Night.m_AmbientLight;
			RenderSettings.fogColor = this.m_Night.m_FogColor;
			RenderSettings.fog = true;
		}
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Button_Help()
	{
		GUIAnimSystemFREE.Instance.MoveOut(this.m_Help_Button.transform, true);
		GUIAnimSystemFREE.Instance.MoveIn(this.m_Help_Window.transform, true);
		FXQ_SoundController.Instance.Play_SoundBack();
	}

	public void Button_Help_Minimize()
	{
		GUIAnimSystemFREE.Instance.MoveIn(this.m_Help_Button.transform, true);
		GUIAnimSystemFREE.Instance.MoveOut(this.m_Help_Window.transform, true);
		FXQ_SoundController.Instance.Play_SoundBack();
	}

	public void Button_Help_Support()
	{
		Application.OpenURL("mailto:geteamdev@gmail.com");
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Button_Help_Tutorials()
	{
		Application.ExternalEval("window.open('https://www.youtube.com/watch?v=TWpKPCGYEyI','FX Quest 0.4.0')");
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Button_Help_Products()
	{
		Application.ExternalEval("window.open('http://ge-team.com/pages/unity-3d/','GOLD EXPERIENCE TEAM')");
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Button_PlayParticle()
	{
		this.ShowParticle();
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Button_PreviousParticle()
	{
		this.PreviousParticle();
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Button_NextParticle()
	{
		this.NextParticle();
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Button_ParticleType_Abilities()
	{
		this.SetParticleType(0);
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Button_ParticleType_Explosion()
	{
		this.SetParticleType(1);
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Button_ParticleType_Fight()
	{
		this.SetParticleType(2);
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Button_ParticleType_Magic()
	{
		this.SetParticleType(3);
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Button_ParticleType_Misc()
	{
		this.SetParticleType(4);
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public void Button_ParticleType_Prop()
	{
		this.SetParticleType(5);
		FXQ_SoundController.Instance.Play_SoundClick();
	}

	public FXQ_ParticleType[] m_ParticleTypeList;

	private int m_ParticleType;

	private int m_ParticleTypeOld = -1;

	private int m_ParticleTypeChildCount;

	private int m_ParticleIndex;

	private int m_ParticleIndexOld = -1;

	private ParticleSystem m_Particle;

	private ParticleSystem m_ParticleOld;

	private string m_ParticleTypeName = string.Empty;

	private string m_ParticleName = string.Empty;

	public Canvas m_3DDemo_UI;

	public Button m_SelectDemo_Button;

	public GameObject m_SelectDemo_Window;

	public GameObject m_Options_Window;

	public Toggle m_Options_Toggle_Day;

	public Toggle m_Options_Toggle_Night;

	public Button m_Help_Button;

	public GameObject m_Help_Window;

	public GameObject m_ParticleSelection_Window;

	public Text m_ParticleDetails_Text_Order;

	public Button m_ParticleDetails_Button_ParticleName;

	public Text m_ParticleDetails_Text_Name;

	public GameObject m_HowTo;

	public FXQ_3D_Demo.LightAndSky m_Day;

	public FXQ_3D_Demo.LightAndSky m_Night;

	[Serializable]
	public class LightAndSky
	{
		public string m_Name;

		public Light m_Light;

		public Material m_Skybox;

		public Color m_FogColor;

		public Color m_AmbientLight;
	}
}
