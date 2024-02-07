using System;
using Inputs;
using Interactions;
using MyUtils;
using UnityEngine;

namespace Player
{
    public class PlayerInteractions : MonoBehaviour
    {
        [SerializeField] private LayerMask interactionMask;
        public delegate void InteractionDelegate(GameObject interactingWith);

        private void Start()
        {
            PlayerInputHandler.Instance.AddFunctionToOnPointerClick(ShortInteractionHandler);
            PlayerInputHandler.Instance.AddFunctionToOnPointerLongInteraction(LongInteractionHandler);
        }

        private void ShortInteractionHandler(GameObject interactingWith)
        {
            if (!interactionMask.IsContainingLayer(interactingWith.layer)) return;

            Debug.Log("Short Interaction: " + interactingWith.name);
        }

        private void LongInteractionHandler(GameObject interactingWith)
        {
            if (!interactionMask.IsContainingLayer(interactingWith.layer)) return;

            interactingWith.GetComponent<ILongInteractable>().OnLongInteraction();
        }
    }
}