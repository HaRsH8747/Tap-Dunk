using UnityEngine;
using UnityEngine.Events;

public class RimCollision : MonoBehaviour
{
    [SerializeField] Collider2D[] _colliderList;

    [Space]
    public UnityEvent OnPlayerColliding;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            OnPlayerColliding?.Invoke();
        }
    }

    public void DisableCollision()
    {
        foreach (var col in _colliderList)
        {
            col.enabled = false;
        }
    }
}
