using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameoverMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _bestScoreText;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _homeButton;

    private void OnEnable()
    {
        _restartButton.interactable = true;
        _homeButton.interactable = true;

        SetScoreDisplay();
    }

    private void Start()
    {
        OnButtonPressed(_restartButton, RestartButton);
        OnButtonPressed(_homeButton, HomeButton);
    }

    private void SetScoreDisplay()
    {
        ScoreController sc = FindObjectOfType<ScoreController>();
        int lastScore = sc.Score;
        int bestScore = sc.GetBestScore();

        _scoreText.text = $"Last : {lastScore}";
        _bestScoreText.text = $"Best : {bestScore}";
    }

    private void HomeButton()
    {
        _restartButton.interactable = false;

        StartCoroutine(LevelLoader.ReloadLevelAsync(() =>
        {
            MenuController.GetInstance().SwitchMenu(MenuType.Main);
        }));
    }

    private void RestartButton()
    {
        _homeButton.interactable = false;

        StartCoroutine(LevelLoader.ReloadLevelAsync(() =>
        {
            MenuController.GetInstance().SwitchMenu(MenuType.Gameplay);
        }));
    }
}
