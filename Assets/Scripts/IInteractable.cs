using UnityEngine;

public interface IInteractable
{
    public virtual void OnInteraction() => Debug.Log("Interacted");
}