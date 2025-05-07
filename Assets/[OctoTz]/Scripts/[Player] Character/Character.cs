
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour {

	private Camera _camera;
	private InputAction _movementInput;
	private CharacterController _chController;

	private Vector2 _inputValue;
	private float _smoothing = 10f;

	private void Awake() {

		Managers.SubscribeOnManagerCreated<InputManager>((m) => {
			_camera = CameraLink.GetCamera("main");
			_chController = GetComponent<CharacterController>();
			_movementInput = (m as InputManager).PlayerMap.FindAction("Move");
		});

	}

	private void Update() {

		if (_movementInput == null) return;

		// !raw
		_inputValue = Vector2.Lerp(_inputValue, _movementInput.ReadValue<Vector2>(), _smoothing * Time.deltaTime);

		Vector3 input = Quaternion.Euler(0, _camera.transform.localEulerAngles.y,0) * new Vector3(_inputValue.x, 0, _inputValue.y);
		_chController.Move(5f * Time.deltaTime * input);

	}

}
