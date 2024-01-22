using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private CustomInputs _customInputs;

    private void Awake()
    {
        _customInputs = new CustomInputs();
        _customInputs.Player.CristalInteraction.performed += (ctx) =>
        {
            CristalInteraction();
        };
    }

    private void OnEnable()
    {
        _customInputs.Enable();
    }

    private void OnDisable()
    {
        _customInputs.Disable();
    }

    private void CristalInteraction()
    {
        //Add ray from mouse pos checking if objects' type is cristal
    }
}
