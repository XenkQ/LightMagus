using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRotation : MonoBehaviour
{
    private CustomInputs _customInputs;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        RotatePlayerToMouseDirection();
    }

    private void RotatePlayerToMouseDirection()
    {
        Vector3 mouseDir = Pointer.OnScreenWorldPosition - transform.position;
        float angle = Mathf.Atan2(mouseDir.z, mouseDir.x) * Mathf.Rad2Deg;
        _rb.MoveRotation(Quaternion.AngleAxis(angle, Vector3.down));
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawSphere(new Vector3(
                Pointer.OnScreenWorldPosition.x,
                0,
                Pointer.OnScreenWorldPosition.z),
                1);
        }
    }
}
