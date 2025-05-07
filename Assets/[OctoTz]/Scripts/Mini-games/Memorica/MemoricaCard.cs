
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MemoricaCard : MonoBehaviour, IPointerClickHandler {

    public string Id { get; private set; } = string.Empty;
    public bool IsOpen { get; private set; } = false;

    [SerializeField] private Image _image;
    [SerializeField] private Animator _animator;

    public event Action<MemoricaCard> Clicked;

    public MemoricaCard Setup(string id, Sprite sprite) {
        Id = id;
        _image.sprite = sprite;
        SetOpened(false);
        return this;
    }

    public void SetOpened(bool isOpen) {
        IsOpen = isOpen;
        _animator.SetBool("IsOpen", isOpen);
    }

    public void PlayComparisonAnimation(bool success) {
        _animator.SetTrigger(success ? "Success" : "Fail");
    }

    public void OnPointerClick(PointerEventData eventData) {
        Clicked?.Invoke(this);
    }

}
