using System;
using System.Reflection;
using UnityEngine;

public class GeneralEasingTypes : MonoBehaviour
{
	private void Start()
	{
		this.demoEaseTypes();
	}

	private void demoEaseTypes()
	{
		for (int i = 0; i < this.easeTypes.Length; i++)
		{
			string text = this.easeTypes[i];
			Transform obj1 = GameObject.Find(text).transform.Find("Line");
			float obj1val = 0f;
			LTDescr ltdescr = LeanTween.value(obj1.gameObject, 0f, 1f, 5f).setOnUpdate(delegate(float val)
			{
				Vector3 localPosition = obj1.localPosition;
				localPosition.x = obj1val * this.lineDrawScale;
				localPosition.y = val * this.lineDrawScale;
				obj1.localPosition = localPosition;
				obj1val += Time.deltaTime / 5f;
				if (obj1val > 1f)
				{
					obj1val = 0f;
				}
			});
			if (text.IndexOf("AnimationCurve") >= 0)
			{
				ltdescr.setEase(this.animationCurve);
			}
			else
			{
				MethodInfo method = ltdescr.GetType().GetMethod("set" + text);
				method.Invoke(ltdescr, null);
			}
			if (text.IndexOf("EasePunch") >= 0)
			{
				ltdescr.setScale(1f);
			}
			else if (text.IndexOf("EaseOutBounce") >= 0)
			{
				ltdescr.setOvershoot(2f);
			}
		}
		LeanTween.delayedCall(base.gameObject, 10f, new Action(this.resetLines));
		LeanTween.delayedCall(base.gameObject, 10.1f, new Action(this.demoEaseTypes));
	}

	private void resetLines()
	{
		for (int i = 0; i < this.easeTypes.Length; i++)
		{
			Transform transform = GameObject.Find(this.easeTypes[i]).transform.Find("Line");
			transform.localPosition = new Vector3(0f, 0f, 0f);
		}
	}

	public float lineDrawScale = 10f;

	public AnimationCurve animationCurve;

	private string[] easeTypes = new string[]
	{
		"EaseLinear",
		"EaseAnimationCurve",
		"EaseSpring",
		"EaseInQuad",
		"EaseOutQuad",
		"EaseInOutQuad",
		"EaseInCubic",
		"EaseOutCubic",
		"EaseInOutCubic",
		"EaseInQuart",
		"EaseOutQuart",
		"EaseInOutQuart",
		"EaseInQuint",
		"EaseOutQuint",
		"EaseInOutQuint",
		"EaseInSine",
		"EaseOutSine",
		"EaseInOutSine",
		"EaseInExpo",
		"EaseOutExpo",
		"EaseInOutExpo",
		"EaseInCirc",
		"EaseOutCirc",
		"EaseInOutCirc",
		"EaseInBounce",
		"EaseOutBounce",
		"EaseInOutBounce",
		"EaseInBack",
		"EaseOutBack",
		"EaseInOutBack",
		"EaseInElastic",
		"EaseOutElastic",
		"EaseInOutElastic",
		"EasePunch",
		"EaseShake"
	};
}
