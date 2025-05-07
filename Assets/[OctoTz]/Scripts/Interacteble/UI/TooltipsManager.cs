
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class TooltipsManager : SceneManager  {

	[SerializeField] private InteractebleTooltip _tooltipPrefab;
	[SerializeField] private RectTransform _spawnParent;

	private Camera _camera;
	private MonoPull<InteractebleTooltip> _pull;
	private List<InteractebleTooltip> _activeElements = new List<InteractebleTooltip>();

	private InputAction _input;

	private bool _visable = true;
	public bool Visable { get => _visable; set {
			_visable = value;
			_activeElements.ForEach(x => x.gameObject.SetActive(value));
		}
	}

	public override IEnumerator OnInitialize() {
		_camera = CameraLink.GetCamera("main");
		_pull = new MonoPull<InteractebleTooltip>(_tooltipPrefab, _spawnParent, false, 2);
		return base.OnInitialize();
	}

	public override IEnumerator Final() {
		_input = Managers.GetManager<InputManager>().InputSheme.FindAction("Interact");
		return base.Final();
	}

	public void ShowTooltip(Interacteble target) {
		if (!_activeElements.ContaitsWhere(x => x.Current == target)) {
			_activeElements.Add(_pull.GetItem().Setup(_input, target));
			_activeElements[^1].Rect.position = GetTooltipAnchor(_activeElements[^1]);
			_activeElements[^1].gameObject.SetActive(_visable);
		}
	}

	public void HideTooltip(Interacteble target) {
		if (_activeElements.ContaitsWhere(x => x.Current == target, out var active)) {
			_activeElements.Remove(active);
			active.Hide((x) => _pull.ReturnItem(x));
		}
	}

	public void ActivateTooltip(Interacteble target) {
		if (_activeElements.ContaitsWhere(x => x.Current == target, out var active)) {
			active.Hit();
		}
	}

	public void ShowTooltipHint(Interacteble target, string hint) {
		if (_activeElements.ContaitsWhere(x => x.Current == target, out var active)) {
			active.ShowHint(hint);
		}
	}

	private void Update() {

		if (!_visable || _activeElements.Count <= 0) return;

		foreach (var tooltip in _activeElements) {
			tooltip.Rect.position = GetTooltipAnchor(tooltip);
		}
		
	}

	private Vector2 GetTooltipAnchor(InteractebleTooltip target) {
		return _camera.WorldToScreenPoint(target.Current.GetViewAnchor());
	}

}
