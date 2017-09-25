using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogBoxTestInterface : MonoBehaviour {

	public Text  screenResolutionWindow;
	public Image screenOrientationConfigImageReference;

	[Header("--------------")]

	public InputField Option1_Text;
	public InputField Option2_Text;
	public InputField Cancel_Text;

	public InputField DBTitle_Text;
	public InputField DBDescription_Text;

	public InputField DBCustomName_Text;

	public void Create2B_DialogBox(){

		UIDialogBox.Instance.CreateCustomDialogBox2B (
			DBTitle_Text.text,
			DBDescription_Text.text,
			Option1_Text.text,
			null,
			Option2_Text.text,
			null);
	}

	public void Create3B_DialogBox(){

		UIDialogBox.Instance.CreateCustomDialogBox3B (
			DBTitle_Text.text,
			DBDescription_Text.text,
			Option1_Text.text,
			null,
			Option2_Text.text,
			null,
			Cancel_Text.text);
	}

	public void CreatePreset_DialogBox(){

		UIDialogBox.Instance.CreatePresetDialogBox (DBCustomName_Text.text); 
	}

	public void DifferentScreenOrientationConfig(){

		UIDialogBox.Instance.hasDifferentScreenOrientation = !UIDialogBox.Instance.hasDifferentScreenOrientation;

		if (UIDialogBox.Instance.hasDifferentScreenOrientation) {
		
			screenOrientationConfigImageReference.color = Color.green;
		} else {

			screenOrientationConfigImageReference.color = Color.red;
		}
	}

	void Update(){

		screenResolutionWindow.text = "Screen Resolution\n" + UIDialogBox.Instance.GetAspectRatio (Screen.width, Screen.height).ToString ();
	}
}
