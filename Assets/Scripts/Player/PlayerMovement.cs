using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    { 
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 moveDir = PlayerInputHandler.Instance.MoveInput;
        _rb.velocity = new Vector3(moveDir.x * speed, 0, moveDir.y * speed);
    }
}
