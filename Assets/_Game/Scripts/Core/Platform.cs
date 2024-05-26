using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour
{
    public UnityEvent OnGameOver;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnGameOver?.Invoke();
        }
    }
}
