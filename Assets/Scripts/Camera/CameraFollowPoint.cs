using Inputs;
using UnityEngine;

public class CameraFollowPoint : MonoBehaviour
{
    [SerializeField] private float _maxDistanceFromTarget = 20f;
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        FollowPointerToMaxDistanceFromTarget(_maxDistanceFromTarget, _target);
    }

    private void FollowPointerToMaxDistanceFromTarget(float maxDistance, Transform target)
    {
        Vector3 pointerPos = Pointer.OnScreenWorldPosition;
        Vector3 followerMaxPos = target.position + maxDistance * Vector3.one;

        transform.position = new Vector3(
            Mathf.Clamp(pointerPos.x, -followerMaxPos.x, followerMaxPos.x),
            0,
            Mathf.Clamp(pointerPos.z, -followerMaxPos.z, followerMaxPos.z)
        );
    }
}
