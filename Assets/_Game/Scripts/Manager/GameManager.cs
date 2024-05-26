using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Object References :")]
    [SerializeField] PlayerBehaviour _player;

    [Header("Ads Settings :")]
    [SerializeField] int _interstitialAdInterval = 3;
    public static int _gameplayCount;

    bool _isGameOver;
    bool _isRevive;

    Vector2 _lastGoalPosition;
    ScoreController _scoreController;
    MenuController _menuController;

    // event
    public static event Action<int> OnScoreUpdated;

    private void Awake()
    {
        _scoreController = GetComponent<ScoreController>();
    }

    private void OnEnable()
    {
        RimBehaviour.OnGoal += HandleOnGoal;
        RimBehaviour.OnGameEnd += GameEnd;
        PlayerMovement.OnFirstJump += StartGameplay;
    }

    private void Start()
    {
        _menuController = MenuController.GetInstance();
    }

    public void StartGameplay()
    {
        _menuController.SwitchMenu(MenuType.Gameplay);
    }

    public void RevivePlayer()
    {
        _isGameOver = false;
        _menuController.SwitchMenu(MenuType.Gameplay);
        _player.Revive(_lastGoalPosition);
    }

    public void GameEnd()
    {
        if (_isGameOver) return;
        _isGameOver = true;

        ShowInterstitial();

        _player.PlayerDeath = true;

        bool isRewardLoaded = Admob.GetInstance().IsRewardAdLoaded();

        if (_scoreController.Score > 2 && !_isRevive && isRewardLoaded)
        {
            _menuController.SwitchMenu(MenuType.Revive);
            _isRevive = true;
        }
        else
        {
            _menuController.SwitchMenu(MenuType.GameOver);
        }

        // play jump audio
        SoundController.GetInstance().PlayAudio(AudioType.GAMEOVER);
    }

    private void HandleOnGoal(bool swish)
    {
        _scoreController.AddScore(swish);
        _lastGoalPosition = _player.transform.position;

        OnScoreUpdated?.Invoke(_scoreController.Score);
    }

    private void ShowInterstitial()
    {
        _gameplayCount++;

        // show interstitial every 3 times (default)
        if (Mathf.Repeat(_gameplayCount, _interstitialAdInterval) == 0)
        {
            Admob.GetInstance().ShowInterstitialAd();
        }
    }

    private void OnDisable()
    {
        RimBehaviour.OnGoal -= HandleOnGoal;
        RimBehaviour.OnGameEnd -= GameEnd;
        PlayerMovement.OnFirstJump -= StartGameplay;
    }
}
