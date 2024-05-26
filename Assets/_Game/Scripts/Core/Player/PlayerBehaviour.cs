using System;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    bool _isPlayerDeath;
    PlayerMovement _playerMovement;

    public bool PlayerDeath { set => _isPlayerDeath = value; }

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }


    public void HandleOnJump()
    {
        if (_isPlayerDeath) return;

        _playerMovement.Jump();
    }

    public void Revive(Vector2 revivePosition)
    {
        _isPlayerDeath = false;
        transform.position = revivePosition;
        _playerMovement.Revive();
    }
}
