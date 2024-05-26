using System;
using UnityEngine;

public class RimBehaviour : MonoBehaviour
{
    bool _isGoal = false;
    bool _isPlayerSwish = true;

    public static event Action<bool> OnGoal;
    public static event Action OnGameEnd;

    public void HandleOnGoal()
    {
        if (_isGoal) return; _isGoal = true;

        OnGoal?.Invoke(_isPlayerSwish);

        Destroy(gameObject, 2f);
    }

    public void DisableSwish()
    {
        if (!_isPlayerSwish) return; 
        
        _isPlayerSwish = false;
    }

    public void GameOver()
    {
        OnGameEnd?.Invoke();
    }
}
