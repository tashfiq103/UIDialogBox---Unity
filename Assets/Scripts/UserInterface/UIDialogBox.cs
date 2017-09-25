using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIDialogBox : MonoBehaviour {

	public static UIDialogBox Instance;

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
	[Tooltip("If the current scene have both portrait & landscape mode for UI/Gameplay")]

	#if UNITY_ANDROID || UNITY_IOS
		public bool hasDifferentScreenOrientation;
	#endif

	[Header("----------------------")]
	public Sprite 	dialogBoxBackground;
	public Sprite 	buttonBackground;
	public Font 	dialogBoxFont;

	[Space]
	[Header("----------------------")]
	public UIDialogInformation[] dialogBox;
	[Header("----------------------")]
	[Space]

	//-----------------------------------------------

	
	[Header("Settings (Don't Change Anything)!!!")]
	[Header("----------------------")]
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

	private Vector2 mAspectRatio;

	#if UNITY_ANDROID || UNITY_IOS
	private DeviceOrientation mCurrentDeviceOrientation;
	#endif

	#endregion

	void Awake(){

		Instance = this;

	}

	void Start(){

		#if UNITY_ANDROID || UNITY_IOS

		switch (Input.deviceOrientation) {

		case DeviceOrientation.LandscapeLeft:
			mCurrentDeviceOrientation = DeviceOrientation.LandscapeLeft;
			break;
		case DeviceOrientation.LandscapeRight:
			mCurrentDeviceOrientation = DeviceOrientation.LandscapeRight;
			break;
		case DeviceOrientation.Portrait:
			mCurrentDeviceOrientation = DeviceOrientation.Portrait;
			break;
		case DeviceOrientation.PortraitUpsideDown:
			mCurrentDeviceOrientation = DeviceOrientation.PortraitUpsideDown;
			break;
		}

		#endif

		mAspectRatio = GetAspectRatio (Screen.width, Screen.height);

		mChangeTheme ();

		dialogBox2BReference.SetActive (false);
		dialogBox3BReference.SetActive (false);
	}

	void Update(){

		#if UNITY_ANDROID || UNITY_IOS

		if (hasDifferentScreenOrientation) {

			if (Input.deviceOrientation != mCurrentDeviceOrientation) {

				StartCoroutine(mAdaptNewScreenChange());
			}
		}

		#endif
	}

	#region PUBLIC METHOD

	public Vector2 GetAspectRatio(int x, int y){
		float f = (float)x / (float)y;
		int i = 0;
		while(true){
			i++;
			if(System.Math.Round(f * i, 2) == Mathf.RoundToInt(f * i))
				break;
		}
		return new Vector2((float)System.Math.Round(f * i, 2), i);
	}

	public void CreatePresetDialogBox(string nameOfDialogBox){

		for (int i = 0; i < dialogBox.Length; i++) {

			if (dialogBox [i].nameOfDialogBox == nameOfDialogBox) {

				mOnCreateDialogBox ();

				if (dialogBox [i].hasCancelButton) {

					mChange3BDialogBoxSize ();

					UnityAction _ResetAction = new UnityAction (mReset3BDialogBox);

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

					mChange2BDialogBoxSize ();

					UnityAction _ResetAction = new UnityAction (mReset2BDialogBox);

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
		
		mChange2BDialogBoxSize ();

		UnityAction _ResetAction = new UnityAction (mReset2BDialogBox);

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

	public void CreateCustomDialogBox3B(string _title, string _description,string _option1,UnityAction _option1Action,string _option2,UnityAction _option2Action,string _cancel){

		mChange3BDialogBoxSize ();

		UnityAction _ResetAction = new UnityAction (mReset3BDialogBox);

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
		
		if (_cancel == null || _cancel.Length == 0)
			_cancel = "Cancel";
		cancelButtonReference.GetComponentInChildren<Text> ().text = _cancel;
		cancelButtonReference.onClick.AddListener (_ResetAction);

		glassEffectBackground.SetActive (true);

		dialogBox3BReference.SetActive (true);
	}

	#endregion

	#region PRIVATE METHOD

	#if UNITY_ANDROID || UNITY_IOS

	private IEnumerator mAdaptNewScreenChange(){

		yield return new WaitForSeconds (0.5f);

		mCurrentDeviceOrientation = Input.deviceOrientation;

		mChange2BDialogBoxSize ();
		mChange3BDialogBoxSize ();
	}

	#endif

	private void mChangeTheme(){

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

	private void mOnCreateDialogBox(){

		if (dialogBox2BReference.activeInHierarchy)
			dialogBox2BReference.SetActive (false);

		if (dialogBox3BReference.activeInHierarchy)
			dialogBox3BReference.SetActive (false);
	}

	private void mReset2BDialogBox(){

		header2BTextReference.text = "";
		messege2BBodyTextReference.text = "";

		yesButtonReference.GetComponentInChildren<Text> ().text = "";
		yesButtonReference.onClick.RemoveAllListeners ();

		noButtonReference.GetComponentInChildren<Text> ().text = "";
		noButtonReference.onClick.RemoveAllListeners ();

		dialogBox2BReference.SetActive (false);

		glassEffectBackground.SetActive (false);
	}

	private void mReset3BDialogBox(){

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

	private void mChange2BDialogBoxSize(){
	
		mAspectRatio = GetAspectRatio (Screen.width, Screen.height);

		if (mAspectRatio.x > mAspectRatio.y) {

			Vector2 newSize = new Vector2 ( (Screen.width*1)/3, (Screen.height*1)/3);
			dialogBox2BReference.GetComponent<RectTransform> ().sizeDelta = newSize;
		}else {

			Vector2 newSize = new Vector2 ( 	Screen.width/2, (Screen.height*1)/5);
			dialogBox2BReference.GetComponent<RectTransform> ().sizeDelta = newSize;
		}
	}

	private void mChange3BDialogBoxSize(){
	
		mAspectRatio = GetAspectRatio (Screen.width, Screen.height);

		if (mAspectRatio.x > mAspectRatio.y) {

			Vector2 newSize = new Vector2 ( (Screen.width*1)/3, (Screen.height*1)/2.5f);
			dialogBox3BReference.GetComponent<RectTransform> ().sizeDelta = newSize;
		}else {

			Vector2 newSize = new Vector2 ( (Screen.width*3)/5, (Screen.height*1)/4);
			dialogBox3BReference.GetComponent<RectTransform> ().sizeDelta = newSize;
		}
	}

	#endregion

}
