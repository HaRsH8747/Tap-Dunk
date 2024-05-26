using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Space]
    [SerializeField] Transform _target;

    float _offsetX;
    bool _stopFollowing = false;

    private void Start()
    {
        _offsetX = transform.position.x - _target.transform.position.x;
    }

    private void LateUpdate()
    {
        if (_stopFollowing) return;

        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = new Vector3(_target.position.x + _offsetX, transform.position.y, transform.position.z);
        transform.position = targetPosition;
    }

    private void StopFollowingTarget()
    {
        _stopFollowing = true;
    }
}
