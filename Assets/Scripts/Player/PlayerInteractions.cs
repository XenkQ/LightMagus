using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private LayerMask interactionMask;
    public delegate void InteractionDelegate(GameObject interactingWith);
    
    private void Start()
    {
        Pointer.OnPointerShortInteraction += ShortInteractionHandler;
        Pointer.OnPointerLongInteraction += LongInteractionHandler;
    }
    
    private void ShortInteractionHandler(GameObject interactingWith)
    {
        if (!MyUtils.Layer.IsLayerInLayerMask(interactingWith.layer, interactionMask)) return;
        
        Debug.Log("Short Interaction: " + interactingWith.name);
    }
    
    private void LongInteractionHandler(GameObject interactingWith)
    {
        if (!MyUtils.Layer.IsLayerInLayerMask(interactingWith.layer, interactionMask)) return;
        
        Debug.Log("Long Interaction: " + interactingWith.name);
    }
}
