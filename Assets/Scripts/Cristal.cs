using System;
using UnityEngine;

public class Cristal : MonoBehaviour, IInteractable, IEnergyHoldable
{
    [SerializeField] private EnergyContainer _energyContainer;
    public EnergyContainer EnergyContainer => _energyContainer;

    public void Interact() =>
        EnergySystem.ChannelEnnergyToPlayer(this, transform.position);
}
