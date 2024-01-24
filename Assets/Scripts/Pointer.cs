using System;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private float worldHeightOffset = 0;
    public static Vector3 OnScreenWorldPosition { get; private set; }
    private Camera _mainCamera;

    public delegate void PointerInteractionDelegate(GameObject interactingWith);
    public static event PointerInteractionDelegate OnPointerShortInteraction;
    public static event PointerInteractionDelegate OnPointerLongInteraction;

    private void Awake()
    {
        _mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        UpdateMousePosition();
    }
    
    private void Update()
    {
        if(PlayerInputHandler.Instance.IsPointerShortInteraction)
            OnPointerShortInteraction?.Invoke(this.gameObject);
        
        if(PlayerInputHandler.Instance.IsPointerLongInteraction)
            OnPointerLongInteraction?.Invoke(this.gameObject);
    }

    private void UpdateMousePosition()
    {
        OnScreenWorldPosition = _mainCamera.ScreenToWorldPoint(PlayerInputHandler.Instance.LookInput);
        OnScreenWorldPosition = new Vector3(
            OnScreenWorldPosition.x,
            worldHeightOffset,
            OnScreenWorldPosition.z
        );
    }
}
