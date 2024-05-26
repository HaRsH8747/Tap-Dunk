using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
    [SerializeField] PlayerFX _playerFx;
    [SerializeField] scorePopupUI _scorePopup;
    [SerializeField] scorePopupUI _perfectPopup;

    int _swishMultiple = 1;

    public int Score { get; private set; }

    public void AddScore(bool isSwish)
    {
        if (isSwish)
        {
            _swishMultiple += 1;
            _perfectPopup.SetEnable();

            // play jump audio
            SoundController.GetInstance().PlayAudio(AudioType.SWISHSCORE);
        }
        else
        {
            _swishMultiple = 1;

            // play jump audio
            SoundController.GetInstance().PlayAudio(AudioType.NORMALSCORE);
        }

        Score += 1 * _swishMultiple;
        _scorePopup.SetEnable(_swishMultiple);

        ShowHidePlayerVFX();
    }

    void ShowHidePlayerVFX()
    {
        if (_swishMultiple == 3)
        {
            _playerFx.SetActive(0, true);
        }
        else if (_swishMultiple == 4)
        {
            _playerFx.SetActive(1, true);
        }
        else if (_swishMultiple == 1)
        {
            _playerFx.SetActive(0, false);
            _playerFx.SetActive(1, false);
        }
    }

    public int GetBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (Score > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", Score);
            return Score;
        }
        else
        {
            return bestScore;
        }
    }
}