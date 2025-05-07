
using System;
using UnityEngine;
using UnityEngine.Events;

public interface IInteracteble {
    public void Activate();
}

public class Interacteble : MonoBehaviour, IInteracteble {

	[field: SerializeField] public string Label { get; protected set; }
	[Header("Not required")]
	[SerializeField] private Transform _tooltipViewAnchor;

	public event Action OnInteract;
	[SerializeField] protected UnityEvent _onInteract;
	
	public virtual void Activate() {
		OnInteract?.Invoke();
		_onInteract?.Invoke();
		return;
	}

	public virtual Vector3 GetViewAnchor() {
		return _tooltipViewAnchor == null ? transform.position : _tooltipViewAnchor.position;
	}

}
