using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RimLines : MonoBehaviour
{
    [SerializeField] Transform _target;
    [Space]
    [SerializeField] float _from = 1f;
    [SerializeField] float _to = -1f;
    [SerializeField] float _duration = 3f;

    private void Start()
    {
        _target.transform.localPosition = Vector2.up * _from;
        LeanTween.moveLocalY(_target.gameObject, _to, _duration).setLoopPingPong();
    }
}
