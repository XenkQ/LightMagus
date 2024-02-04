using UnityEngine;

public interface ILightConnectable
{
    bool CanConnect { get; set; }
    ILightConnectable IsConnectedTo { get; set; }
    public Vector3 CurrentPosition { get; }
}
