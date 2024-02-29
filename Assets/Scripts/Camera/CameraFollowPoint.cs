using Inputs;
using UnityEngine;

public class CameraFollowPoint : MonoBehaviour
{
    [SerializeField] private Vector2 _maxDistanceFromTarget;
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        FollowPointerToMaxDistanceFromTarget(_maxDistanceFromTarget, _target);
    }

    private void FollowPointerToMaxDistanceFromTarget(Vector2 maxDistance, Transform target)
    {
        Vector3 pointerPos = Pointer.OnScreenWorldPosition;
        float maxHorizontal = target.position.x + maxDistance.x;
        float minHorizontal = target.position.x - maxDistance.x;
        float maxVertical = target.position.y + maxDistance.y;
        float minVertical = target.position.y - maxDistance.y;

        transform.position = new Vector3(
            Mathf.Clamp(pointerPos.x, minHorizontal, maxHorizontal),
            0,
            Mathf.Clamp(pointerPos.z, minVertical, maxVertical)
        );
    }
}
