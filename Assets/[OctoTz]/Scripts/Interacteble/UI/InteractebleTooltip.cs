
using TMPro;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractebleTooltip : MonoBehaviour {

	[field: SerializeField] public RectTransform Rect { get; private set; }
	[SerializeField] private TMP_Text _label;
	[SerializeField] private InteractionHint _hintLabel;
	[SerializeField] private AnimatorWithCallback _animator;

	public Interacteble Current { get; private set; }

	public InteractebleTooltip Setup(InputAction inputAction, Interacteble target) {
		Current = target;
		_hintLabel.HideHint(false);
		_label.text = string.Format("[{0}] {1}", inputAction.GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontIncludeInteractions), target.Label);
		return this;
	}

	public InteractebleTooltip Clear() {
		Current = null;
		_label.text = "";
		_hintLabel.HideHint(false);
		return this;
	}

	public InteractebleTooltip Hide(Action<InteractebleTooltip> onHided) {
		_animator.SetTrigger("Hide", () => onHided?.Invoke(this));
		_hintLabel.HideHint(true);
		return this;
	}

	public InteractebleTooltip Hit() {
		_animator.SetTrigger("Hit");
		return this;
	}

	public InteractebleTooltip ShowHint(string hintText) {
		_hintLabel.ShowHint(hintText);
		return this;
	}

}
