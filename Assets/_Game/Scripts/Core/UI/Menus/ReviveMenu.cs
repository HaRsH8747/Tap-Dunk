using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviveMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] Button _continueButton;
    [SerializeField] Button _skipButton;

    [Space]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _timerText;
    [SerializeField] Image _timerFill;

    private Timer _timer;

    protected override void Awake()
    {
        base.Awake();

        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        _continueButton.interactable = true;

        StartTimer();
        SetScoreDisplay();

        LeanTween.scale(_continueButton.gameObject, Vector2.one * 1.1f, .3f).setEase(LeanTweenType.easeOutQuad).setLoopPingPong();
    }

    private void Start()
    {
        OnButtonPressed(_continueButton, ContinueButtonPressed);
        OnButtonPressed(_skipButton, SkipButtonPressed);
    }

    private void SetScoreDisplay()
    {
        ScoreController sc = FindObjectOfType<ScoreController>();
        int lastScore = sc.Score;

        _scoreText.text = lastScore.ToString();
    }

    private void SkipButtonPressed()
    {
        ResetWatchAdButton();

        _timer.StopTimer();
        MenuController.GetInstance().SwitchMenu(MenuType.GameOver);
    }

    private void ContinueButtonPressed()
    {
        _continueButton.interactable = false;
        ResetWatchAdButton();

        Admob.GetInstance().ShowRewardAd();
    }

    private void ResetWatchAdButton()
    {
        LeanTween.cancel(_continueButton.gameObject);
        _continueButton.transform.localScale = Vector3.one;
    }

    private void StartTimer()
    {
        StartCoroutine(_timer.CalculateTimer(i => _timerText.text = i, j => _timerFill.fillAmount = j, ()=>
        {
            MenuController.GetInstance().SwitchMenu(MenuType.GameOver);
        }));
    }
}
