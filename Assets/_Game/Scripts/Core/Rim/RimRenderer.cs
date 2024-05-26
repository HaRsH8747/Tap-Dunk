using UnityEngine;

public class RimRenderer : MonoBehaviour
{
    Transform _renderer;

    private void Awake()
    {
        _renderer = transform;
    }

    public void GoalAnimation()
    {
        foreach (Transform child in _renderer)
        {
            SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();
            Color toColor = renderer.color;
            toColor.a = 0;
            LeanTween.color(child.gameObject, toColor, .4f);
        }

        LeanTween.scale(_renderer.gameObject, Vector3.one * 1.3f, .4f);
    }
}
