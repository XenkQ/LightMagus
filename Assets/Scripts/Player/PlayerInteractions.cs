using System;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private void Update()
    {
        if (PlayerInputHandler.Instance.IsInteracting)
        {
            CristalInteraction();
        }
    }

    private void CristalInteraction()
    {
        //Add ray from mouse pos checking if objects' type is cristal
        Console.WriteLine("Interaction");
    }
}
