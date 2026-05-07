using System;
using System.Collections;
using System.Collections.Generic;
using JsonFx.Json;
using UnityEngine;

public class SocialManager : MonoBehaviour
{
	public static SocialManager Instance
	{
		get
		{
			return SocialManager._instance;
		}
	}

	private void InitSocialLinks()
	{
		string[] array;
		(array = this.webUrls)[0] = array[0] + this.facebookFanpageID;
		(array = this.webUrls)[1] = array[1] + this.facebookGroupID;
		(array = this.webUrls)[2] = array[2] + this.instagramUserName;
		(array = this.webUrls)[3] = array[3] + this.twitterUserName;
		(array = this.appUrls)[0] = array[0] + this.facebookFanpageID;
		(array = this.appUrls)[1] = array[1] + this.facebookGroupID;
		(array = this.appUrls)[2] = array[2] + this.instagramUserName;
		(array = this.appUrls)[3] = array[3] + this.twitterID;
	}

	public virtual void Awake()
	{
		this.InitSocialLinks();
		if (SocialManager._instance == null)
		{
			SocialManager._instance = this;
			UnityEngine.Object.DontDestroyOnLoad(SocialManager._instance);
			
			return;
		}
		UnityEngine.Object.DestroyImmediate(this);
	}

	private void FBInitCallback()
	{
		
	}

	private void FBOnHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	public void FBLogIn()
	{
		List<string> permissions = new List<string>
		{
			"public_profile",
			"email",
			"user_friends"
		};
		
	}

	public bool FBIsInit()
	{
		return false;
	}

	public bool FBIsLoggedIn()
	{
		return false;
	}

	public virtual void FBLogginSuccess()
	{
	}

	

	public void FBLogOut()
	{
		
	}

	public void FBShare()
	{
		
	}

	

	public void FBOnClickBtnLike()
	{
		this.coCheckForOpenBrowser = this.CoCheckForOpenBrowser(SocialManager.SocialType.LikeFB);
		base.StartCoroutine(this.coCheckForOpenBrowser);
	}

	public void FBOnClickBtnJoinGroup()
	{
		this.coCheckForOpenBrowser = this.CoCheckForOpenBrowser(SocialManager.SocialType.JoinFBGroup);
		base.StartCoroutine(this.coCheckForOpenBrowser);
	}

	public void InstagramOnClickBtnFollow()
	{
		this.coCheckForOpenBrowser = this.CoCheckForOpenBrowser(SocialManager.SocialType.FollowInstagram);
		base.StartCoroutine(this.coCheckForOpenBrowser);
	}

	public void TwitterOnClickBtnFollow()
	{
		this.coCheckForOpenBrowser = this.CoCheckForOpenBrowser(SocialManager.SocialType.FollowTwitter);
		base.StartCoroutine(this.coCheckForOpenBrowser);
	}

	public void CheckLoginStatus()
	{
		
	}

	public virtual void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			this.isGamePaused = true;
		}
		else
		{
			if (this.rewardSocialType == SocialManager.SocialType.None)
			{
				return;
			}
			this.OnRewardSocial();
		}
	}

	private void OnRewardSocial()
	{
		switch (this.rewardSocialType)
		{
		case SocialManager.SocialType.LikeFB:
			if (this.OnLikeFBReward != null)
			{
				this.OnLikeFBReward();
			}
			break;
		case SocialManager.SocialType.JoinFBGroup:
			if (this.OnJoinGroupFBReward != null)
			{
				this.OnJoinGroupFBReward();
			}
			break;
		case SocialManager.SocialType.FollowInstagram:
			if (this.OnFollowInstagramReward != null)
			{
				this.OnFollowInstagramReward();
			}
			break;
		case SocialManager.SocialType.FollowTwitter:
			if (this.OnFollowTwitterReward != null)
			{
				this.OnFollowTwitterReward();
			}
			break;
		}
		this.rewardSocialType = SocialManager.SocialType.None;
	}

	private void OnDisable()
	{
		if (this.coCheckForOpenBrowser != null)
		{
			base.StopCoroutine(this.coCheckForOpenBrowser);
		}
	}

	public void FBRequestFriendsData()
	{
		
	}

	public void FetchAvatar(string userID, int width, int height, Action<Sprite> SetAvatar)
	{
	
	}

	private IEnumerator CoCheckForOpenBrowser(SocialManager.SocialType type)
	{
		this.isGamePaused = false;
		this.rewardSocialType = type;
		UnityEngine.Debug.Log(this.appUrls[(int)type]);
		Application.OpenURL(this.appUrls[(int)type]);
		yield return new WaitForSeconds(this.waitingTime);
		if (!this.isGamePaused)
		{
			UnityEngine.Debug.Log(this.webUrls[(int)type]);
			Application.OpenURL(this.webUrls[(int)type]);
		}
		yield break;
	}

	public void FBLogCompleteLevel(string level)
	{
		
	}

	public void FBLogBuyItem(string package_sku, float priceAmount, string priceCurrency)
	{
		
	}

	public void FBLogCompleteViewVideo()
	{
		
	}

	public string FBGetUserID()
	{
		return PlayerPrefs.GetString("M_FacebookUserID", "Unknown");
	}

	public void FBSetUserID(string userID)
	{
		PlayerPrefs.SetString("M_FacebookUserID", userID);
	}

	public string FBGetUserToken()
	{
		return PlayerPrefs.GetString("M_FacebookUserToken", string.Empty);
	}

	public void FBSetUserToken(string userToken)
	{
		PlayerPrefs.SetString("M_FacebookUserToken", userToken);
	}

	public int FBGetNumberOfFriends()
	{
		return PlayerPrefs.GetInt("M_FacebookFriendsNumber", 0);
	}

	public void FBSetNumberOfFriends(int number)
	{
		PlayerPrefs.SetInt("M_FacebookFriendsNumber", number);
	}

	public bool FBIsAlreadyLike()
	{
		return PlayerPrefs.GetInt("M_AlreadyLikeFacebook", 0) == 1;
	}

	public void FBSetAlreadyLike()
	{
		PlayerPrefs.SetInt("M_AlreadyLikeFacebook", 1);
	}

	public bool FBIsAlreadyJoinGroup()
	{
		return PlayerPrefs.GetInt("M_AlreadyJoinGroupFacebook", 0) == 1;
	}

	public void FBSetAlreadyJoinGroup()
	{
		PlayerPrefs.SetInt("M_AlreadyJoinGroupFacebook", 1);
	}

	public bool FBIsAlreadyClaimFriendsReward(int numberOfFriends)
	{
		return PlayerPrefs.GetInt("M_AlreadyHaveFriendsReward" + numberOfFriends, 0) == 1;
	}

	public void FBSetAlreadyClaimFriendsReward(int numberOfFriends)
	{
		PlayerPrefs.SetInt("M_AlreadyHaveFriendsReward" + numberOfFriends, 1);
	}

	public void FBSetAlreadyShare()
	{
		PlayerPrefs.SetInt("M_AlreadyShareFacebook", 1);
	}

	public bool InstagramIsAlreadyFollow()
	{
		return PlayerPrefs.GetInt("M_AlreadyFollowInstagram", 0) == 1;
	}

	public void InstagramSetAlreadyFollow()
	{
		PlayerPrefs.SetInt("M_AlreadyFollowInstagram", 1);
	}

	public bool TwitterIsAlreadyFollow()
	{
		return PlayerPrefs.GetInt("M_AlreadyFollowTwitter", 0) == 1;
	}

	public void TwitterSetAlreadyFollow()
	{
		PlayerPrefs.SetInt("M_AlreadyFollowTwitter", 1);
	}

	private static SocialManager _instance;

	[SerializeField]
	private string android_URL;

	public string facebookFanpageID;

	public string facebookGroupID;

	public string instagramUserName;

	public string twitterUserName;

	public string twitterID;

	[Tooltip("Waiting time for open a browser instead of a social app")]
	[SerializeField]
	private float waitingTime = 1.2f;

	private SocialManager.SocialType rewardSocialType = SocialManager.SocialType.None;

	public string[] webUrls = new string[]
	{
		"https://www.facebook.com/",
		"https://www.facebook.com/groups/",
		"https://www.instagram.com/",
		"https://twitter.com/"
	};

	public string[] appUrls = new string[]
	{
		"fb://page/",
		"fb://group/",
		"instagram://user?username=",
		"twitter://user?user_id="
	};

	public Action OnFacebookLoginSuccessed;

	public Action OnFacebookLoginFail;

	public Action OnLikeFBReward;

	public Action OnJoinGroupFBReward;

	public Action OnFollowInstagramReward;

	public Action OnFollowTwitterReward;

	public Action OnFacebookShareSuccessed;

	private bool isGamePaused;

	private IEnumerator coCheckForOpenBrowser;

	public enum SocialType
	{
		None = -1,
		LikeFB,
		JoinFBGroup,
		FollowInstagram,
		FollowTwitter
	}
}
