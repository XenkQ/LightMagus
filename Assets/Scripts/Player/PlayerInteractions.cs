using System;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private void Start()
    {
        Pointer.OnPointerShortInteraction += ShortInteractionHandler;
        Pointer.OnPointerLongInteraction += LongInteractionHandler;
    }
    
    private void ShortInteractionHandler(GameObject interactingWith)
    {
        Debug.Log("Short Interaction");
    }
    
    private void LongInteractionHandler(GameObject interactingWith)
    {
        Debug.Log("Long Interaction");
    }
}
