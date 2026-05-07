using System;
using DG.Tweening;
using Hellmade.Sound;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
	private void Awake()
	{
		SettingManager.Curret = this;
		this.GetDataSetting();
	}

	private void Start()
	{
		this.sliderMusic.onValueChanged.AddListener(delegate(float A_1)
		{
			this.OnChangeValueSliderMusic();
		});
		this.sliderSound.onValueChanged.AddListener(delegate(float A_1)
		{
			this.OnChangeValueSliderSound();
		});
		this.sliderSensitivity.onValueChanged.AddListener(delegate(float A_1)
		{
			this.OnChangeValueSliderSensitivity();
		});
		this.btnTouchMove.onClick.AddListener(new UnityAction(this.SetTouchMove));
		this.btnRelativeMove.onClick.AddListener(new UnityAction(this.SetRelativeMove));
		this.SetImageBtnLanguage();
	}

	private void GetDataSetting()
	{
		this.volumeMusic = GameContext.volumeMusic;
		this.volumeSound = GameContext.volumeSound;
		this.sliderMusic.value = this.volumeMusic;
		this.sliderSound.value = this.volumeSound;
		this.sliderSensitivity.value = (GameContext.sensitivity - 1f) / 0.25f;
		this.isVibration = GameContext.isVibration;
		if (this.isVibration)
		{
			this.checkmarkVibration.SetActive(true);
		}
		else
		{
			this.checkmarkVibration.SetActive(false);
		}
		if (GameContext.typeMove == 0)
		{
			this.checkmarkTouchMove.SetActive(true);
		}
		else
		{
			this.checkmarkRelativeMove.SetActive(true);
		}
		this.textIDPlayer.text = "ID: " + AccountManager.Instance.Code;
		this.textVersion.text = "v" + Application.version;
	}

	private void OnChangeValueSliderMusic()
	{
		float value = this.sliderMusic.value;
		EazySoundManager.GlobalMusicVolume = value;
		GameContext.volumeMusic = value;
	}

	private void OnChangeValueSliderSound()
	{
		float value = this.sliderSound.value;
		EazySoundManager.GlobalSoundsVolume = value;
		EazySoundManager.GlobalUISoundsVolume = value;
		GameContext.volumeSound = value;
	}

	private void OnChangeValueSliderSensitivity()
	{
		int num = (int)this.sliderSensitivity.value;
		GameContext.sensitivity = 1f + (float)num * 0.25f;
		UnityEngine.Debug.Log("sensitivity " + GameContext.sensitivity);
	}

	public void SetViration()
	{
		if (this.isVibration)
		{
			this.checkmarkVibration.SetActive(false);
			GameContext.isVibration = false;
			this.isVibration = false;
		}
		else
		{
			this.checkmarkVibration.SetActive(true);
			GameContext.isVibration = true;
			this.isVibration = true;
#if UNITY_ANDROID || UNITY_IOS
			Handheld.Vibrate();
#endif
		}
	}

	private void SetTouchMove()
	{
		if (GameContext.typeMove != 0)
		{
			GameContext.typeMove = 0;
			this.checkmarkTouchMove.SetActive(true);
			this.checkmarkRelativeMove.SetActive(false);
		}
	}

	private void SetRelativeMove()
	{
		if (GameContext.typeMove != 1)
		{
			GameContext.typeMove = 1;
			this.checkmarkTouchMove.SetActive(false);
			this.checkmarkRelativeMove.SetActive(true);
		}
	}

	[EnumAction(typeof(GameContext.Language))]
	public void ChangeLanguage(int idLanguage)
	{
		GameContext.SetLanguage((GameContext.Language)idLanguage);
		if (this.currentIdLanguage != idLanguage)
		{
			this.imagesBtnLanguage[this.currentIdLanguage].sprite = this.sprBtn;
			this.imagesBtnLanguage[idLanguage].sprite = this.sprBtnSelected;
			this.currentIdLanguage = idLanguage;
			this.textCountry.text = this.arrTextCountry[this.currentIdLanguage].text;
		}
	}

	public void SaveDataSetting()
	{
		CacheGame.IsVibration = this.isVibration;
		CacheGame.VolumeMusic = GameContext.volumeMusic;
		CacheGame.VolumeSound = GameContext.volumeSound;
		CacheGame.Sensitivy = GameContext.sensitivity;
		CacheGame.TypeMovePlayer = GameContext.typeMove;
	}

	private void SetImageBtnLanguage()
	{
		if (GameContext.CurrentIdLanguage > 5)
		{
			this.SnapTo(this.imagesBtnLanguage[GameContext.CurrentIdLanguage].GetComponent<RectTransform>());
		}
		else
		{
			this.contentPanel.anchoredPosition = new Vector2(0f, 0f);
		}
		this.currentIdLanguage = GameContext.CurrentIdLanguage;
		this.textCountry.text = this.arrTextCountry[this.currentIdLanguage].text;
		this.imagesBtnLanguage[GameContext.CurrentIdLanguage].sprite = this.sprBtnSelected;
	}

	public void ShowPopupLanguage()
	{
		EscapeManager.Current.AddAction(new Action(this.HidePopupLanguage));
		this.popupLanguage.SetActive(true);
		DOTween.Restart("POPUP_LANGUAGE", true, -1f);
		DOTween.Play("POPUP_LANGUAGE");
	}

	public void HidePopupLanguage()
	{
		DOTween.PlayBackwards("POPUP_LANGUAGE");
		EscapeManager.Current.RemoveAction(new Action(this.HidePopupLanguage));
		base.StartCoroutine(GameContext.Delay(0.1f, delegate
		{
			this.popupLanguage.SetActive(false);
		}));
	}

	public void SnapTo(RectTransform target)
	{
		this.contentPanel.anchoredPosition = this.scrollBtnLanguage.transform.InverseTransformPoint(this.contentPanel.position) - this.scrollBtnLanguage.transform.InverseTransformPoint(target.position);
		this.contentPanel.anchoredPosition = new Vector2(0f, this.contentPanel.anchoredPosition.y - 500f);
	}

	public void NewGame()
	{
		AccountManager.Instance.DeleteAccount();
	}

	public static SettingManager Curret;

	public Slider sliderMusic;

	public Slider sliderSound;

	public Slider sliderSensitivity;

	public GameObject checkmarkVibration;

	public GameObject checkmarkTouchMove;

	public GameObject checkmarkRelativeMove;

	public Button btnTouchMove;

	public Button btnRelativeMove;

	public GameObject popupLanguage;

	public Sprite sprBtn;

	public Sprite sprBtnSelected;

	public ScrollRect scrollBtnLanguage;

	public RectTransform contentPanel;

	public Text textCountry;

	public Text textIDPlayer;

	public Text textVersion;

	public Image[] imagesBtnLanguage;

	public Text[] arrTextCountry;

	private bool isVibration;

	private float volumeMusic;

	private float volumeSound;

	private int currentIdLanguage = -1;
}
