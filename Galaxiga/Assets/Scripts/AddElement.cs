using System;
using SnapScrollView;
using UnityEngine;
using UnityEngine.UI;

public class AddElement : MonoBehaviour
{
	public void Awake()
	{
		Button component = base.GetComponent<Button>();
		component.onClick.AddListener(delegate()
		{
			Canvas componentInParent = base.GetComponentInParent<Canvas>();
			ScrollSnap componentInChildren = componentInParent.GetComponentInChildren<ScrollSnap>();
			componentInChildren.PushLayoutElement(UnityEngine.Object.Instantiate<LayoutElement>(this.layoutElementPrefab));
		});
	}

	[SerializeField]
	private LayoutElement layoutElementPrefab;
}
