using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        transform.eulerAngles = Vector3.forward * -10;
        LeanTween.scale(gameObject, Vector3.one * 1.05f, .3f).setEase(LeanTweenType.easeOutQuad).setLoopPingPong();
        LeanTween.rotate(gameObject,Vector3.forward * 7f, 3f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();
    }

    private void OnDisable()
    {
        LeanTween.cancel(gameObject);
    }
}
