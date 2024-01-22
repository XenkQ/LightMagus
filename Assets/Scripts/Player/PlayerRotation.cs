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
    [SerializeField] private Transform cameraPosObject;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _customInputs = new CustomInputs();
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
        Vector3 mousePos = Mouse.OnScreenWorldPosition;
        cameraPosObject.transform.position = new Vector3(mousePos.x, 0, mousePos.z);
        transform.LookAt(new Vector3(mousePos.x, 0, mousePos.z));
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.DrawSphere(new Vector3(
                Mouse.OnScreenWorldPosition.x,
                0,
                Mouse.OnScreenWorldPosition.z),
                1);
        }
    }
}
