using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Custom Input Asset")]
    [SerializeField] private CustomInputs _customInputs;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction pointerShortInteraction;
    private InputAction pointerLongInteraction;
    
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsPointerShortInteraction { get; private set; }
    public bool IsPointerLongInteraction { get; private set; }
    
    public static PlayerInputHandler Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _customInputs = new CustomInputs();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance);
        }

        SetActionsBindings();

        RegisterInputActions();
    }

    private void SetActionsBindings()
    {
        moveAction = _customInputs.Player.Movement;
        lookAction = _customInputs.Player.Look;
        pointerShortInteraction = _customInputs.Player.PointerShortInteraction;
        pointerLongInteraction = _customInputs.Player.PointerLongInteraction;
    }

    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;
        
        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;
        
        pointerLongInteraction.performed += context => IsPointerLongInteraction = true;
        pointerLongInteraction.canceled += context => IsPointerLongInteraction = false;
        
        pointerShortInteraction.performed += context => IsPointerShortInteraction = true;
        pointerShortInteraction.canceled += context => IsPointerShortInteraction = false;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        pointerShortInteraction.Disable();
        pointerLongInteraction.Disable();
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        pointerShortInteraction.Enable();
        pointerLongInteraction.Enable();
    }
}
