using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorUIOption : MonoBehaviour {
	[SerializeField]
	Button _button;

	[SerializeField]
	TMP_Text _text;

	public void Load(string action, int status) {
		_text.text = action;
		_button.interactable = status == 1;
	}
}