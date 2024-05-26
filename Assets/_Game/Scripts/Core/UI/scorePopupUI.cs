using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scorePopupUI : MonoBehaviour
{
    [SerializeField] float _tweenDuration = .4f;
    [SerializeField] CanvasGroup _cg;
    [SerializeField] TMP_Text _popupText;

    private RectTransform _rect;
    private Vector3 _defaultPos;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _defaultPos = _rect.anchoredPosition;
    }

    private void OnEnable()
    {
        LeanTween.move(_rect, _defaultPos + Vector3.up * 50, _tweenDuration);
        LeanTween.alphaCanvas(_cg, 0, _tweenDuration).setOnComplete(HandleOnTweeningComplete);
    }

    public void SetEnable(int score = 0)
    {
        if (_popupText)
        {
            _popupText.text = $"+{score}";
        }
        gameObject.SetActive(true);
    }

    private void HandleOnTweeningComplete()
    {
        gameObject.SetActive(false);
        _rect.anchoredPosition = _defaultPos;
        _cg.alpha = 1f;
    }
}
