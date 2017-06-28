using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIDialogBox : MonoBehaviour {

	public static UIDialogBox instance;

	#region PUBLIC VARIABLES

	[System.Serializable]
	public struct UIDialogInformation
	{
		public string nameOfDialogBox;
		public bool hasCancelButton;
		[HeaderAttribute("Messege Information")]
		public string header;
		public string messegeBody;
		[HeaderAttribute("Yes Button : Attribute")]
		public string yesButtonText;
		public Button.ButtonClickedEvent onPressedYes;
		[HeaderAttribute("No Button : Attribute")]
		public string noButtonText;
		public Button.ButtonClickedEvent onPressedNo;

	}
	//-----------------------------------------------

	[Header("DialogBox Theme (Optional)")]
	public Sprite 	dialogBoxBackground;
	public Sprite 	buttonBackground;
	public Font 	dialogBoxFont;

	[Header("----------------------")]
	public UIDialogInformation[] dialogBox;

	//-----------------------------------------------

	[Header("DO NOT CHANGE!!!")]
	public GameObject glassEffectBackground;
	[Header("----------------------")]
	[Header("Dialog Box - 2 Button : Component")]
	public GameObject dialogBox2BReference;
	public Text header2BTextReference;
	public Text messege2BBodyTextReference;
	public Button yesButtonReference;
	public Button noButtonReference;

	[Header("----------------------")]
	[Header("Dialog Box - 3 Button : Component")]
	public GameObject dialogBox3BReference;
	public Text header3BTextReference;
	public Text messege3BBodyTextReference;
	public Button option1ButtonReference;
	public Button option2ButtonReference;
	public Button cancelButtonReference;

	#endregion

	#region PRIVATE VARIABLES

	float _widthRatio;
	float _heightRatio;

	#endregion

	void Awake(){

		if (instance == null)
			instance = this;

	}

	void Start(){

		_ChangeTheme ();

		_widthRatio = Camera.main.pixelHeight / Camera.main.pixelWidth;
		_heightRatio = Camera.main.pixelWidth / Camera.main.pixelWidth;

		dialogBox2BReference.SetActive (false);
		dialogBox3BReference.SetActive (false);
	}



	#region PUBLIC METHOD CALL


	public void CallDialogBox(string nameOfDialogBox){

		for (int i = 0; i < dialogBox.Length; i++) {

			if (dialogBox [i].nameOfDialogBox == nameOfDialogBox) {

				_onCreateDialogBox ();

				if (dialogBox [i].hasCancelButton) {
				
					if (Camera.main.aspect >= 1.0f) {
						Rect _size = dialogBox2BReference.GetComponent<RectTransform> ().rect;
						dialogBox2BReference.GetComponent<RectTransform> ().sizeDelta = new Vector2 (_size.width * _widthRatio, _size.height * _heightRatio);
					}

					UnityAction _ResetAction = new UnityAction (_Reset3BDialogBox);

					header3BTextReference.text = dialogBox [i].header;
					messege3BBodyTextReference.text = dialogBox [i].messegeBody;

					option1ButtonReference.GetComponentInChildren<Text> ().text = dialogBox [i].yesButtonText;
					option1ButtonReference.onClick = dialogBox [i].onPressedYes;
					option1ButtonReference.onClick.AddListener (_ResetAction);

					option2ButtonReference.GetComponentInChildren<Text> ().text = dialogBox [i].noButtonText;
					option2ButtonReference.onClick = dialogBox [i].onPressedNo;
					option2ButtonReference.onClick.AddListener (_ResetAction);

					cancelButtonReference.onClick.AddListener (_ResetAction);

					glassEffectBackground.SetActive (true);

					dialogBox3BReference.SetActive (true);

				} else {


					if (Camera.main.aspect >= 1.0f) {
						Rect _size = dialogBox3BReference.GetComponent<RectTransform> ().rect;
						dialogBox3BReference.GetComponent<RectTransform> ().sizeDelta = new Vector2 (_size.width * _widthRatio, _size.height * _heightRatio);
					}
					UnityAction _ResetAction = new UnityAction (_Reset2BDialogBox);

					header2BTextReference.text = dialogBox [i].header;
					messege2BBodyTextReference.text = dialogBox [i].messegeBody;

					yesButtonReference.GetComponentInChildren<Text> ().text = dialogBox [i].yesButtonText;
					yesButtonReference.onClick = dialogBox [i].onPressedYes;
					yesButtonReference.onClick.AddListener (_ResetAction);

					noButtonReference.GetComponentInChildren<Text> ().text = dialogBox [i].noButtonText;
					noButtonReference.onClick = dialogBox [i].onPressedNo;
					noButtonReference.onClick.AddListener (_ResetAction);

					glassEffectBackground.SetActive (true);

					dialogBox2BReference.SetActive (true);
				}

				break;
			}
		}
	}

	public void CreateCustomDialogBox2B(string _title, string _description,string _option1,UnityAction _option1Action,string _option2,UnityAction _option2Action){

		if (Camera.main.aspect >= 1.0f) {
			Rect _size = dialogBox2BReference.GetComponent<RectTransform> ().rect;
			dialogBox2BReference.GetComponent<RectTransform> ().sizeDelta = new Vector2 (_size.width * _widthRatio, _size.height * _heightRatio);
		}

		UnityAction _ResetAction = new UnityAction (_Reset2BDialogBox);

		header2BTextReference.text = _title;
		messege2BBodyTextReference.text = _description;

		yesButtonReference.GetComponentInChildren<Text> ().text = _option1;
		yesButtonReference.onClick.AddListener (_ResetAction);
		if(_option1Action != null)
			yesButtonReference.onClick.AddListener (_option1Action);
		
		noButtonReference.GetComponentInChildren<Text> ().text = _option2;
		noButtonReference.onClick.AddListener (_ResetAction);
		if(_option2Action != null)
			noButtonReference.onClick.AddListener (_option2Action);

		glassEffectBackground.SetActive (true);

		dialogBox2BReference.SetActive (true);
	}

	public void CreateCustomDialogBox3B(string _title, string _description,string _option1,UnityAction _option1Action,string _option2,UnityAction _option2Action){

		if (Camera.main.aspect >= 1.0f) {
			Rect _size = dialogBox3BReference.GetComponent<RectTransform> ().rect;
			dialogBox3BReference.GetComponent<RectTransform> ().sizeDelta = new Vector2 (_size.width * _widthRatio, _size.height * _heightRatio);
		}

		UnityAction _ResetAction = new UnityAction (_Reset3BDialogBox);

		header3BTextReference.text = _title;
		messege3BBodyTextReference.text = _description;

		option1ButtonReference.GetComponentInChildren<Text> ().text = _option1;
		option1ButtonReference.onClick.AddListener (_ResetAction);
		if(_option1Action != null)
			option1ButtonReference.onClick.AddListener (_option1Action);


		option2ButtonReference.GetComponentInChildren<Text> ().text = _option2;
		option2ButtonReference.onClick.AddListener (_ResetAction);
		if(_option2Action != null)
			option2ButtonReference.onClick.AddListener (_option1Action);
		
		cancelButtonReference.onClick.AddListener (_ResetAction);

		glassEffectBackground.SetActive (true);

		dialogBox3BReference.SetActive (true);
	}

	#endregion

	#region PRIVATE VARIABLES

	private void _ChangeTheme(){

		if (dialogBoxBackground != null) {

			dialogBox2BReference.GetComponent<Image> ().sprite = dialogBoxBackground;
			dialogBox3BReference.GetComponent<Image> ().sprite = dialogBoxBackground;
		}

		if (buttonBackground != null) {

			yesButtonReference.GetComponent<Image> ().sprite = buttonBackground;
			noButtonReference.GetComponent<Image> ().sprite = buttonBackground;
			option1ButtonReference.GetComponent<Image> ().sprite = buttonBackground;
			option2ButtonReference.GetComponent<Image> ().sprite = buttonBackground;
			cancelButtonReference.GetComponent<Image> ().sprite = buttonBackground;
		}

		if (dialogBoxFont != null) {

			header2BTextReference.GetComponent<Text> ().font = dialogBoxFont;
			messege2BBodyTextReference.GetComponent<Text> ().font = dialogBoxFont;
			yesButtonReference.GetComponentInChildren<Text> ().font = dialogBoxFont;
			noButtonReference.GetComponentInChildren<Text> ().font = dialogBoxFont;

			header3BTextReference.GetComponent<Text> ().font = dialogBoxFont;
			messege3BBodyTextReference.GetComponent<Text> ().font = dialogBoxFont;
			option1ButtonReference.GetComponentInChildren<Text> ().font = dialogBoxFont;
			option2ButtonReference.GetComponentInChildren<Text> ().font = dialogBoxFont;
			cancelButtonReference.GetComponentInChildren<Text> ().font = dialogBoxFont;
		}
	}

	private void _Reset2BDialogBox(){

		header2BTextReference.text = "";
		messege2BBodyTextReference.text = "";

		yesButtonReference.GetComponentInChildren<Text> ().text = "";
		yesButtonReference.onClick.RemoveAllListeners ();

		noButtonReference.GetComponentInChildren<Text> ().text = "";
		noButtonReference.onClick.RemoveAllListeners ();

		dialogBox2BReference.SetActive (false);

		glassEffectBackground.SetActive (false);
	}

	private void _Reset3BDialogBox(){

		header3BTextReference.text = "";
		messege3BBodyTextReference.text = "";

		option1ButtonReference.GetComponentInChildren<Text> ().text = "";
		option1ButtonReference.onClick.RemoveAllListeners ();

		option2ButtonReference.GetComponentInChildren<Text> ().text = "";
		option2ButtonReference.onClick.RemoveAllListeners ();

		cancelButtonReference.onClick.RemoveAllListeners ();

		dialogBox3BReference.SetActive (false);

		glassEffectBackground.SetActive (false);
	}

	private void _onCreateDialogBox(){

		if (dialogBox2BReference.activeInHierarchy)
			dialogBox2BReference.SetActive (false);

		if (dialogBox3BReference.activeInHierarchy)
			dialogBox3BReference.SetActive (false);
	}

	#endregion
}
