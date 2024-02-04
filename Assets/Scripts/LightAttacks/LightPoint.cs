using System;
using UnityEngine;

public class LightPoint : MonoBehaviour, ILightConnectable
{
    public bool CanConnect { get; set; }
    public ILightConnectable IsConnectedTo { get; set; }
    public Vector3 CurrentPosition { get; private set; }

    private void OnEnable()
        => CurrentPosition = transform.position;
}
