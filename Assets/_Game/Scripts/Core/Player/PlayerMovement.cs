using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _forwardForce;

    bool _isFirstJump = true;
    Rigidbody2D _rb;

    public static event Action OnFirstJump;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        DeceleratePhysicsRotation();
    }

    public void Revive()
    {
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0;
        _rb.isKinematic = true;
    }

    public void Jump()
    {
        if (_isFirstJump)
        {
            _isFirstJump = false;

            OnFirstJump?.Invoke();
        }

        if (_rb.isKinematic) _rb.isKinematic = false;

        // perform jump
        _rb.velocity = new Vector2(_forwardForce, _jumpForce);

        // play jump audio
        SoundController.GetInstance().PlayAudio(AudioType.JUMP);
    }

    private void DeceleratePhysicsRotation()
    {
        if (_rb.angularVelocity == 0) return;
        if (_rb.angularVelocity < -5) _rb.AddTorque(.1f);
        else if (_rb.angularVelocity > 5) _rb.AddTorque(-.1f);
        else _rb.angularVelocity = 0;
    }
}
