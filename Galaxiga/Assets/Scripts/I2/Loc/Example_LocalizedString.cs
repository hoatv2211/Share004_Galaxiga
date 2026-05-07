using System;
using UnityEngine;

namespace I2.Loc
{
	public class Example_LocalizedString : MonoBehaviour
	{
		public void Start()
		{
			UnityEngine.Debug.Log(this._MyLocalizedString);
			UnityEngine.Debug.Log(LocalizationManager.GetTranslation(this._NormalString, true, 0, true, false, null, null));
			UnityEngine.Debug.Log(LocalizationManager.GetTranslation(this._StringWithTermPopup, true, 0, true, false, null, null));
			LocalizedString s = "Term2";
			string message = s;
			UnityEngine.Debug.Log(message);
			LocalizedString myLocalizedString = this._MyLocalizedString;
			UnityEngine.Debug.Log(myLocalizedString);
			LocalizedString localizedString = "Term3";
			UnityEngine.Debug.Log(localizedString);
			LocalizedString localizedString2 = "Term3";
			localizedString2.mRTL_IgnoreArabicFix = true;
			UnityEngine.Debug.Log(localizedString2);
			LocalizedString localizedString3 = "Term3";
			localizedString3.mRTL_ConvertNumbers = true;
			localizedString3.mRTL_MaxLineLength = 20;
			UnityEngine.Debug.Log(localizedString3);
			LocalizedString localizedString4 = localizedString3;
			UnityEngine.Debug.Log(localizedString4);
		}

		public LocalizedString _MyLocalizedString;

		public string _NormalString;

		[TermsPopup("")]
		public string _StringWithTermPopup;
	}
}
