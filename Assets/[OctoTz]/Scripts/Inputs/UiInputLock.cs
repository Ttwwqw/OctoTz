
using UnityEngine;

[RequireComponent(typeof(GameFrames))]
public class UiInputLock : MonoBehaviour {

	//[SerializeField] private string[] actions;

	private GameFrames _framesManager;
	private InputManager _inputManager;

	private void Awake() {

		Managers.SubscribeOnManagerCreated<InputManager>((input) => {

			_inputManager = input as InputManager;
			_framesManager = GetComponent<GameFrames>();
			_framesManager.OnFrameStateChanged += OnFrameStateChanged;

		});
		
	}

	private void OnFrameStateChanged(GameFrames manager, IFrame frame) {

		if (manager.HasOpenedFrames()) {

			_inputManager.DisableActionMap(_inputManager.PlayerMap);

		} else {

			_inputManager.EnableActionMap(_inputManager.PlayerMap);

		}

	}

}
