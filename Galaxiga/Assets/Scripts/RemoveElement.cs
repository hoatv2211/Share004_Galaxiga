using System;
using SnapScrollView;
using UnityEngine;
using UnityEngine.UI;

public class RemoveElement : MonoBehaviour
{
	public void Awake()
	{
		Button component = base.GetComponent<Button>();
		component.onClick.AddListener(delegate()
		{
			Canvas componentInParent = base.GetComponentInParent<Canvas>();
			ScrollSnap componentInChildren = componentInParent.GetComponentInChildren<ScrollSnap>();
			componentInChildren.PopLayoutElement();
		});
	}

	[SerializeField]
	private LayoutElement layoutElementPrefab;
}
