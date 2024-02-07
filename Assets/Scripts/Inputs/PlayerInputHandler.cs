using Player;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [Header("Custom Input Asset")]
        [SerializeField] private CustomInputs _customInputs;

        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction OnPointerClick;
        private InputAction pointerLongInteraction;

        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public bool IsPointerLongInteraction { get; private set; }

        public static PlayerInputHandler Instance { get; private set; }
        private event PlayerInteractions.InteractionDelegate OnPointerLongInteraction;

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

        private void Update()
        {
            if (IsPointerLongInteraction)
                OnPointerLongInteraction?.Invoke(Pointer.Instance.GetHoveredGameObject());
        }

        private void SetActionsBindings()
        {
            moveAction = _customInputs.Player.Movement;
            lookAction = _customInputs.Player.Look;
            OnPointerClick = _customInputs.Player.OnPointerClick;
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
        }

        private void OnDisable()
        {
            moveAction.Disable();
            lookAction.Disable();
            OnPointerClick.Disable();
            pointerLongInteraction.Disable();
        }

        private void OnEnable()
        {
            moveAction.Enable();
            lookAction.Enable();
            OnPointerClick.Enable();
            pointerLongInteraction.Enable();
        }

        public void AddFunctionToOnPointerClick(Action func)
        {
            OnPointerClick.performed += (context) => func();
        }

        public void AddFunctionToOnPointerClick(PlayerInteractions.InteractionDelegate func)
        {
            OnPointerClick.performed += (context) => func(Pointer.Instance.GetHoveredGameObject());
        }

        public void AddFunctionToOnPointerLongInteraction(Action func)
        {
            OnPointerLongInteraction += (context) => func();
        }

        public void AddFunctionToOnPointerLongInteraction(PlayerInteractions.InteractionDelegate func)
        {
            OnPointerLongInteraction += func;
        }
    }
}