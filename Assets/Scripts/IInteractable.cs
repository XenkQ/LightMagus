using UnityEngine;

public interface IInteractable
{
    public virtual void Interact() =>
        Debug.Log($"Interacted with {this}");
}