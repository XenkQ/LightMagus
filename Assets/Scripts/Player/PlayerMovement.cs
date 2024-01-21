using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private CustomInputs _customInputs;
    private Rigidbody _rb;
    private Vector2 moveDir;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _customInputs = new CustomInputs();
        _customInputs.Player.Movement.performed += SetMoveDirection;
        _customInputs.Player.Movement.canceled += StopMovement;
    }

    private void FixedUpdate()
    { 
        Move();
    }
    
    private void OnEnable()
    {
        _customInputs.Enable();
    }

    private void OnDisable()
    {
        _customInputs.Disable();
    }

    private void SetMoveDirection(InputAction.CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<Vector2>();
    }

    private void StopMovement(InputAction.CallbackContext ctx) => moveDir = Vector2.zero;

    private void Move()
    {
        _rb.velocity = new Vector3(moveDir.x * speed, 0, moveDir.y * speed);
    }
}
