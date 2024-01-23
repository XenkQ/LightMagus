using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Custom Input Asset")]
    [SerializeField] private CustomInputs _customInputs;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction interactAction;
    
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsInteracting { get; private set; }
    
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
        interactAction = _customInputs.Player.Interaction;
    }

    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;
        
        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;
        
        interactAction.performed += context => IsInteracting = true;
        interactAction.canceled += context => IsInteracting = false;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        interactAction.Disable();
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        interactAction.Enable();
    }
}
