using UnityEngine;
using UnityEngine.Events;

public class RimTrigger : MonoBehaviour
{
    public UnityEvent OnGoal;
    public UnityEvent OnGameOver;

    bool _isOver;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isOver) return;

        if (collision.CompareTag("Player"))
        {
            Vector2 ballDirection = transform.position - collision.transform.position;

            // if player enter from below
            if (ballDirection.y > 0) 
            {
                _isOver = true;
                OnGameOver?.Invoke();
            }
        }

        if (collision.CompareTag("Wall"))
        {
            _isOver = true;
            OnGameOver?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isOver) return;

        if (collision.CompareTag("Player"))
        {
            Vector2 ballDirection = transform.position - collision.transform.position;

            // if player exit from below
            if (ballDirection.y > 0)
            {
                // disable trigger
                GetComponent<Collider2D>().enabled = false;

                OnGoal?.Invoke();
            }
        }
    }
}
