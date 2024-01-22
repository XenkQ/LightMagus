using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour, IInteractable
{
    public void OnInteraction()
    {
        Debug.Log("Interacted with cristal");
    }
}
