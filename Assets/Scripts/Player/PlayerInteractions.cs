using UnityEngine;

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
        if (!MyUtils.Layer.IsLayerInLayerMask(interactingWith.layer, interactionMask)) return;
        
        Debug.Log("Short Interaction: " + interactingWith.name);
    }
    
    private void LongInteractionHandler(GameObject interactingWith)
    {
        if (!MyUtils.Layer.IsLayerInLayerMask(interactingWith.layer, interactionMask)) return;

        interactingWith.GetComponent<IInteractable>().LongInteraction();
    }
}
