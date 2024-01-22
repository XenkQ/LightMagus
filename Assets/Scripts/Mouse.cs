using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public static Vector3 OnScreenWorldPosition { get; private set; }
    private CustomInputs _customInputs;
    private Camera _mainCamera;

    private void Awake()
    {
        _customInputs = new CustomInputs();
        _mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        UpdateMousePosition();
    }

    private void UpdateMousePosition() => 
        OnScreenWorldPosition = _mainCamera.ScreenToWorldPoint(
            _customInputs.Player.Look.ReadValue<Vector2>()
        );
    
    private void OnEnable()
    {
        _customInputs.Enable();
    }

    private void OnDisable()
    {
        _customInputs.Disable();
    }
}
