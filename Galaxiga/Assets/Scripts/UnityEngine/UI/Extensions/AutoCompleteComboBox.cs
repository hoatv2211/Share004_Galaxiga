using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace UnityEngine.UI.Extensions
{
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Extensions/AutoComplete ComboBox")]
	public class AutoCompleteComboBox : MonoBehaviour
	{
		public DropDownListItem SelectedItem { get; private set; }

		public string Text { get; private set; }

		public float ScrollBarWidth
		{
			get
			{
				return this._scrollBarWidth;
			}
			set
			{
				this._scrollBarWidth = value;
				this.RedrawPanel();
			}
		}

		public int ItemsToDisplay
		{
			get
			{
				return this._itemsToDisplay;
			}
			set
			{
				this._itemsToDisplay = value;
				this.RedrawPanel();
			}
		}

		public bool InputColorMatching
		{
			get
			{
				return this._ChangeInputTextColorBasedOnMatchingItems;
			}
			set
			{
				this._ChangeInputTextColorBasedOnMatchingItems = value;
				if (this._ChangeInputTextColorBasedOnMatchingItems)
				{
					this.SetInputTextColor();
				}
			}
		}

		public void Awake()
		{
			this.Initialize();
		}

		public void Start()
		{
			if (this.SelectFirstItemOnStart && this.AvailableOptions.Count > 0)
			{
				this.ToggleDropdownPanel(false);
				this.OnItemClicked(this.AvailableOptions[0]);
			}
		}

		private bool Initialize()
		{
			bool result = true;
			try
			{
				this._rectTransform = base.GetComponent<RectTransform>();
				this._inputRT = this._rectTransform.Find("InputField").GetComponent<RectTransform>();
				this._mainInput = this._inputRT.GetComponent<InputField>();
				this._overlayRT = this._rectTransform.Find("Overlay").GetComponent<RectTransform>();
				this._overlayRT.gameObject.SetActive(false);
				this._scrollPanelRT = this._overlayRT.Find("ScrollPanel").GetComponent<RectTransform>();
				this._scrollBarRT = this._scrollPanelRT.Find("Scrollbar").GetComponent<RectTransform>();
				this._slidingAreaRT = this._scrollBarRT.Find("SlidingArea").GetComponent<RectTransform>();
				this._itemsPanelRT = this._scrollPanelRT.Find("Items").GetComponent<RectTransform>();
				this._canvas = base.GetComponentInParent<Canvas>();
				this._canvasRT = this._canvas.GetComponent<RectTransform>();
				this._scrollRect = this._scrollPanelRT.GetComponent<ScrollRect>();
				this._scrollRect.scrollSensitivity = this._rectTransform.sizeDelta.y / 2f;
				this._scrollRect.movementType = ScrollRect.MovementType.Clamped;
				this._scrollRect.content = this._itemsPanelRT;
				this.itemTemplate = this._rectTransform.Find("ItemTemplate").gameObject;
				this.itemTemplate.SetActive(false);
			}
			catch (NullReferenceException exception)
			{
				UnityEngine.Debug.LogException(exception);
				UnityEngine.Debug.LogError("Something is setup incorrectly with the dropdownlist component causing a Null Refernece Exception");
				result = false;
			}
			this.panelObjects = new Dictionary<string, GameObject>();
			this._prunedPanelItems = new List<string>();
			this._panelItems = new List<string>();
			this.RebuildPanel();
			return result;
		}

		private void RebuildPanel()
		{
			this._panelItems.Clear();
			this._prunedPanelItems.Clear();
			this.panelObjects.Clear();
			foreach (string text in this.AvailableOptions)
			{
				this._panelItems.Add(text.ToLower());
			}
			List<GameObject> list = new List<GameObject>(this.panelObjects.Values);
			int num = 0;
			while (list.Count < this.AvailableOptions.Count)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.itemTemplate);
				gameObject.name = "Item " + num;
				gameObject.transform.SetParent(this._itemsPanelRT, false);
				list.Add(gameObject);
				num++;
			}
			for (int i = 0; i < list.Count; i++)
			{
				list[i].SetActive(i <= this.AvailableOptions.Count);
				if (i < this.AvailableOptions.Count)
				{
					list[i].name = string.Concat(new object[]
					{
						"Item ",
						i,
						" ",
						this._panelItems[i]
					});
					list[i].transform.Find("Text").GetComponent<Text>().text = this._panelItems[i];
					Button component = list[i].GetComponent<Button>();
					component.onClick.RemoveAllListeners();
					string textOfItem = this._panelItems[i];
					component.onClick.AddListener(delegate()
					{
						this.OnItemClicked(textOfItem);
					});
					this.panelObjects[this._panelItems[i]] = list[i];
				}
			}
			this.SetInputTextColor();
		}

		private void OnItemClicked(string item)
		{
			this.Text = item;
			this._mainInput.text = this.Text;
			this.ToggleDropdownPanel(true);
		}

		private void RedrawPanel()
		{
			float num = (this._panelItems.Count <= this.ItemsToDisplay) ? 0f : this._scrollBarWidth;
			this._scrollBarRT.gameObject.SetActive(this._panelItems.Count > this.ItemsToDisplay);
			if (!this._hasDrawnOnce || this._rectTransform.sizeDelta != this._inputRT.sizeDelta)
			{
				this._hasDrawnOnce = true;
				this._inputRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._rectTransform.sizeDelta.x);
				this._inputRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this._rectTransform.sizeDelta.y);
				this._scrollPanelRT.SetParent(base.transform, true);
				this._scrollPanelRT.anchoredPosition = new Vector2(0f, -this._rectTransform.sizeDelta.y);
				this._overlayRT.SetParent(this._canvas.transform, false);
				this._overlayRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._canvasRT.sizeDelta.x);
				this._overlayRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this._canvasRT.sizeDelta.y);
				this._overlayRT.SetParent(base.transform, true);
				this._scrollPanelRT.SetParent(this._overlayRT, true);
			}
			if (this._panelItems.Count < 1)
			{
				return;
			}
			float num2 = this._rectTransform.sizeDelta.y * (float)Mathf.Min(this._itemsToDisplay, this._panelItems.Count);
			this._scrollPanelRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num2);
			this._scrollPanelRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._rectTransform.sizeDelta.x);
			this._itemsPanelRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this._scrollPanelRT.sizeDelta.x - num - 5f);
			this._itemsPanelRT.anchoredPosition = new Vector2(5f, 0f);
			this._scrollBarRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, num);
			this._scrollBarRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num2);
			this._slidingAreaRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0f);
			this._slidingAreaRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num2 - this._scrollBarRT.sizeDelta.x);
		}

		public void OnValueChanged(string currText)
		{
			this.Text = currText;
			this.PruneItems(currText);
			this.RedrawPanel();
			if (this._panelItems.Count == 0)
			{
				this._isPanelActive = true;
				this.ToggleDropdownPanel(false);
			}
			else if (!this._isPanelActive)
			{
				this.ToggleDropdownPanel(false);
			}
			bool flag = this._panelItems.Contains(this.Text) != this._selectionIsValid;
			this._selectionIsValid = this._panelItems.Contains(this.Text);
			this.OnSelectionChanged.Invoke(this.Text, this._selectionIsValid);
			this.OnSelectionTextChanged.Invoke(this.Text);
			if (flag)
			{
				this.OnSelectionValidityChanged.Invoke(this._selectionIsValid);
			}
			this.SetInputTextColor();
		}

		private void SetInputTextColor()
		{
			if (this.InputColorMatching)
			{
				if (this._selectionIsValid)
				{
					this._mainInput.textComponent.color = this.ValidSelectionTextColor;
				}
				else if (this._panelItems.Count > 0)
				{
					this._mainInput.textComponent.color = this.MatchingItemsRemainingTextColor;
				}
				else
				{
					this._mainInput.textComponent.color = this.NoItemsRemainingTextColor;
				}
			}
		}

		public void ToggleDropdownPanel(bool directClick)
		{
			this._isPanelActive = !this._isPanelActive;
			this._overlayRT.gameObject.SetActive(this._isPanelActive);
			if (this._isPanelActive)
			{
				base.transform.SetAsLastSibling();
			}
			else if (directClick)
			{
			}
		}

		private void PruneItems(string currText)
		{
			if (this.autocompleteSearchType == AutoCompleteSearchType.Linq)
			{
				this.PruneItemsLinq(currText);
			}
			else
			{
				this.PruneItemsArray(currText);
			}
		}

		private void PruneItemsLinq(string currText)
		{
			currText = currText.ToLower();
			string[] array = (from x in this._panelItems
			where !x.Contains(currText)
			select x).ToArray<string>();
			foreach (string text in array)
			{
				this.panelObjects[text].SetActive(false);
				this._panelItems.Remove(text);
				this._prunedPanelItems.Add(text);
			}
			string[] array3 = (from x in this._prunedPanelItems
			where x.Contains(currText)
			select x).ToArray<string>();
			foreach (string text2 in array3)
			{
				this.panelObjects[text2].SetActive(true);
				this._panelItems.Add(text2);
				this._prunedPanelItems.Remove(text2);
			}
		}

		private void PruneItemsArray(string currText)
		{
			string value = currText.ToLower();
			for (int i = this._panelItems.Count - 1; i >= 0; i--)
			{
				string text = this._panelItems[i];
				if (!text.Contains(value))
				{
					this.panelObjects[this._panelItems[i]].SetActive(false);
					this._panelItems.RemoveAt(i);
					this._prunedPanelItems.Add(text);
				}
			}
			for (int j = this._prunedPanelItems.Count - 1; j >= 0; j--)
			{
				string text2 = this._prunedPanelItems[j];
				if (text2.Contains(value))
				{
					this.panelObjects[this._prunedPanelItems[j]].SetActive(true);
					this._prunedPanelItems.RemoveAt(j);
					this._panelItems.Add(text2);
				}
			}
		}

		public Color disabledTextColor;

		public List<string> AvailableOptions;

		private bool _isPanelActive;

		private bool _hasDrawnOnce;

		private InputField _mainInput;

		private RectTransform _inputRT;

		private RectTransform _rectTransform;

		private RectTransform _overlayRT;

		private RectTransform _scrollPanelRT;

		private RectTransform _scrollBarRT;

		private RectTransform _slidingAreaRT;

		private RectTransform _itemsPanelRT;

		private Canvas _canvas;

		private RectTransform _canvasRT;

		private ScrollRect _scrollRect;

		private List<string> _panelItems;

		private List<string> _prunedPanelItems;

		private Dictionary<string, GameObject> panelObjects;

		private GameObject itemTemplate;

		[SerializeField]
		private float _scrollBarWidth = 20f;

		[SerializeField]
		private int _itemsToDisplay;

		public bool SelectFirstItemOnStart;

		[SerializeField]
		[Tooltip("Change input text color based on matching items")]
		private bool _ChangeInputTextColorBasedOnMatchingItems;

		public Color ValidSelectionTextColor = Color.green;

		public Color MatchingItemsRemainingTextColor = Color.black;

		public Color NoItemsRemainingTextColor = Color.red;

		public AutoCompleteSearchType autocompleteSearchType = AutoCompleteSearchType.Linq;

		private bool _selectionIsValid;

		public AutoCompleteComboBox.SelectionTextChangedEvent OnSelectionTextChanged;

		public AutoCompleteComboBox.SelectionValidityChangedEvent OnSelectionValidityChanged;

		public AutoCompleteComboBox.SelectionChangedEvent OnSelectionChanged;

		[Serializable]
		public class SelectionChangedEvent : UnityEvent<string, bool>
		{
		}

		[Serializable]
		public class SelectionTextChangedEvent : UnityEvent<string>
		{
		}

		[Serializable]
		public class SelectionValidityChangedEvent : UnityEvent<bool>
		{
		}
	}
}
