using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundLoop : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] SpriteRenderer[] _renderers;

    float groundWidth;
    int lastGroundIndex;

    private void Start()
    {
        groundWidth = _renderers[0].bounds.size.x;
        lastGroundIndex = _renderers.Length - 1;

        for (int i = 0; i < _renderers.Length; i++)
        {
            Vector2 newPos = _renderers[i].transform.position;
            newPos.x = groundWidth * i;
            _renderers[i].transform.position = newPos;
        }
    }

    private void Update()
    {
        if (_player.position.x >= _renderers[lastGroundIndex].transform.position.x)
        {
            Vector2 newPos = _renderers[lastGroundIndex].transform.position;
            newPos.x += groundWidth;

            lastGroundIndex = (lastGroundIndex + 1) % _renderers.Length;
            _renderers[lastGroundIndex].transform.position = newPos;
        }
    }
}
