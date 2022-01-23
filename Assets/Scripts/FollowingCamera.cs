using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public static FollowingCamera Instance;

    [SerializeField] private float cameraSpeed = 0.1f;
    [SerializeField] private Transform target;

    private Vector3 _targetPos;
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        Instance = this;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        var targetPosition = target.position;
        var cameraPosition = transform.position;
        _targetPos = new Vector3(targetPosition.x, targetPosition.y, -10);

        cameraPosition = Vector3.SmoothDamp(cameraPosition, _targetPos, ref _velocity, cameraSpeed);
        transform.position = cameraPosition;
    }
}