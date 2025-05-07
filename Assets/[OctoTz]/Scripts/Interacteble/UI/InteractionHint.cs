
using TMPro;
using UnityEngine;
using System.Collections;

public class InteractionHint : MonoBehaviour {

    [SerializeField] private TMP_Text _label;
    [SerializeField] private Animator _animator;

    private float _delay;
    private CoroutineWrapper _delayRoutine;

	private void OnDisable() {
        _delayRoutine?.Stop();
    }

	public void ShowHint(string hintText) {

        gameObject.SetActive(true);

        _label.text = hintText;
        _animator.SetBool("Showing", true);

        if (_delayRoutine == null) {
            _delay = 1.3f;
            _delayRoutine = CoroutineBehavior.StartCoroutine(DelayCoroutine());
        } else {
            _delay = 1f;
        }

    }

	public void HideHint(bool smooth = true) {
        _delay = 0f;
        if (!smooth) {
            gameObject.SetActive(false);
        }
    }

	private IEnumerator DelayCoroutine() {

        while (_delay > 0f) {
            _delay -= Time.deltaTime;
            yield return null;
        }

        _animator.SetBool("Showing", false);
        _delayRoutine = null;

    }

}

