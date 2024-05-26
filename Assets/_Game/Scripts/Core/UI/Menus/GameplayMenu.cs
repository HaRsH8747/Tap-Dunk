using TMPro;
using UnityEngine;

public class GameplayMenu : Menu
{
    [Header("UI References :")]
    [SerializeField] private TMP_Text _scoreText;

    private void OnEnable()
    {
        GameManager.OnScoreUpdated += UpdateScore;

        int score = FindObjectOfType<ScoreController>().Score;
        UpdateScore(score);
    }

    private void UpdateScore(int currentScore)
    {
        _scoreText.text = currentScore.ToString();
    }

    private void OnDisable()
    {
        GameManager.OnScoreUpdated -= UpdateScore;
    }
}