
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class PlayerInteractionTrigger : MonoBehaviour {

	private Interacteble _focused = null;
	private List<Interacteble> _inArea = new();
	private CoroutineWrapper _tooltipsUpdateRoutine = null;

	private bool _initialized = false;
	private TooltipsManager _tooltipsManager;

	private void Awake() {

		Managers.SubscribeOnManagerCreated<TooltipsManager>((t) => {
			Managers.SubscribeOnManagerCreated<InputManager>((m) => {
				(m as InputManager).InputSheme.FindAction("Interact").performed += InteractActionPressed;
				_tooltipsManager = t as TooltipsManager;
				_initialized = true;
			});
		});
		
	}

	private void OnDestroy() {
		_tooltipsUpdateRoutine?.Stop();
		Managers.SubscribeOnManagerCreated<InputManager>((m) => {
			(m as InputManager).InputSheme.FindAction("Interact").performed -= InteractActionPressed;
		});
	}

	private void OnTriggerEnter(Collider other) {

		if (!_initialized) return;

		if (_inArea.ContaitsWhere(x => x.gameObject == other.gameObject)) {
			return;
		}

		if (other.gameObject.TryGetComponent<Interacteble>(out var interacteble)) {

			_inArea.Add(interacteble);
			_tooltipsUpdateRoutine ??= CoroutineBehavior.StartCoroutine(TooltipsUpdateCoroutine(), () => _tooltipsUpdateRoutine = null);

		}

	}

	private void OnTriggerExit(Collider other) {

		if (!_initialized) return;

		if (_inArea.ContaitsWhere(x => x.gameObject == other.gameObject, out var obj)) {

			_inArea.Remove(obj);
			if (_inArea.Count <= 0) {
				_tooltipsUpdateRoutine?.Stop();
				_tooltipsUpdateRoutine = null;
			}

			if (_focused == obj) {
				_focused = null;
				HideTooltip(obj);
			}

		}

	}

	private IEnumerator TooltipsUpdateCoroutine() {
		
		while (_inArea.Count > 0) {

			yield return new WaitForSeconds(0.25f);

			for (int i = _inArea.Count - 1; i >= 0; i--) {
				if (_inArea[i] == null || !_inArea[i].gameObject.activeInHierarchy) {
					if (_focused != null && _focused == _inArea[i]) {
						_focused = null;
						HideTooltip(_inArea[i]);
					}
					_inArea.RemoveAt(i);
				}
			}

			_inArea.Sort((x, y) => Vector3.Distance(x.transform.position, transform.position).CompareTo(Vector3.Distance(y.transform.position, transform.position)));

			if (_inArea.Count <= 0) {
				break;
			}

			if (_focused != _inArea[0]) {

				if (_focused != null) {
					HideTooltip(_focused);
				}

				_focused = _inArea[0];
				ShowTooltip(_focused);

			}

		}

	}

	private void InteractActionPressed(InputAction.CallbackContext obj) {
		if (_focused != null) {
			Managers.GetManager<TooltipsManager>().ActivateTooltip(_focused);
			_focused.Activate();
		}
	}

	private void ShowTooltip(Interacteble target) {
		_tooltipsManager?.ShowTooltip(target);
	}

	private void HideTooltip(Interacteble target) {
		_tooltipsManager?.HideTooltip(target);
	}

}
