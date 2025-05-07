
using TMPro;
using System;
using Naninovel.UI;
using UnityEngine;

public class InputFieldUI : CustomUI {

	[SerializeField] private TMP_InputField _inputField;
	[SerializeField] private GameObject _wrongInputHint;

	private Action<string> _inputCallback;
	private IStringValidator _inputValidator;

	public InputFieldUI Setup(string placeholderLabel, Action<string> inputResultCallback, IStringValidator inputValidator) {

		_inputField.text = "";
		(_inputField.placeholder as TextMeshProUGUI).text = placeholderLabel;

		_inputCallback = inputResultCallback;
		_inputValidator = inputValidator;

		return this;

	}

	public void ConfirmInput() {

		if (_inputValidator == null || _inputValidator.Validate(_inputField.text)) {

			_inputCallback.Invoke(_inputField.text);

		} else {

			_wrongInputHint.SetActive(true);

		}

	}

}

