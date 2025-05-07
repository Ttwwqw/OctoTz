
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BindsListener : SceneManager, IDisposable {

	private GameFrames _gameFrames;
	private InputManager _inputManager;

	public override IEnumerator Final() {

		_gameFrames = Managers.GetManager<GameFrames>();
		_inputManager = Managers.GetManager<InputManager>();
		Subscribe();

		return base.OnInitialize();

	}

	private void Subscribe() {

		_inputManager.UiMap.FindAction("Journal").performed += OpenJournal;
		_inputManager.UiMap.FindAction("Map").performed += OpenMap;
		_inputManager.UiMap.FindAction("Cancel").performed += UiCloseEvent;

	}

	private void Unsubscribe() {
		if (_inputManager != null) {

			_inputManager.UiMap.FindAction("Journal").performed -= OpenJournal;
			_inputManager.UiMap.FindAction("Map").performed -= OpenMap;
			_inputManager.UiMap.FindAction("Cancel").performed -= UiCloseEvent;

		}
	}

	

	public void Dispose() {
		Unsubscribe();
	}

	private void UiCloseEvent(InputAction.CallbackContext obj) {
		if (_gameFrames.HasOpenedFrames()) {

			_gameFrames.CloseAll();

		} else {

			// open pause \ settings frame

		}
	}

	private void OpenJournal(InputAction.CallbackContext obj) {
		if (_gameFrames.IsOpen<JournalFrame>()) {

			Managers.GetManager<GameFrames>().Close<JournalFrame>();

		} else {

			Managers.GetManager<GameFrames>().Open<JournalFrame>();

		}
	}

	private void OpenMap(InputAction.CallbackContext obj) {




	}


}
