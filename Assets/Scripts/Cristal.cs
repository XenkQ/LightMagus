using UnityEngine;

public class Cristal : MonoBehaviour, IInteractable
{
    public void OnInteraction()
    {
        Debug.Log("Interacted with cristal");
    }
}
