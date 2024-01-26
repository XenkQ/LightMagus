using UnityEngine;

public interface IInteractable
{
    void Interact() => Debug.Log($"Interacted with {this}");
}