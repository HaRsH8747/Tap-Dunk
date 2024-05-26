using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongScale : MonoBehaviour
{
    private void Start()
    {
        LeanTween.scale(gameObject, Vector3.one * 1.05f, .5f).setEase(LeanTweenType.easeOutQuad).setLoopPingPong();
    }
}
