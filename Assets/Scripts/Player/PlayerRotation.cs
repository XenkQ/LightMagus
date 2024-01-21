using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerRotation : MonoBehaviour
{
    private CustomInputs _customInputs;
    private Rigidbody _rb;
    private Camera mainCamera;
    private Vector3 currentPlayerLookPos;
    [SerializeField] private Transform cameraPosObject;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _customInputs = new CustomInputs();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        RotatePlayerToMouseDirection();
    }
    
    private void OnEnable()
    {
        _customInputs.Enable();
    }

    private void OnDisable()
    {
        _customInputs.Disable();
    }

    private void RotatePlayerToMouseDirection()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(
            _customInputs.Player.Look.ReadValue<Vector2>()
            );
        
        currentPlayerLookPos = mousePos;
        cameraPosObject.transform.position = new Vector3(mousePos.x, 0, mousePos.z);

        transform.LookAt(new Vector3(mousePos.x, 0, mousePos.z));
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawSphere(new Vector3(
                currentPlayerLookPos.x,
                0,
                currentPlayerLookPos.z),
                1);
        }
    }
}
