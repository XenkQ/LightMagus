using UnityEngine;

namespace LightAttacks
{
    public interface ILightConnectable
    {
        bool CanConnect { get; set; }
        ILightConnectable IsConnectedTo { get; set; }
        Vector3 CurrentPosition { get; }
    }
}