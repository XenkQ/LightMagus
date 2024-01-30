using System;
using UnityEngine;

public class Cristal : MonoBehaviour, IInteractable, IEnergyHoldable
{
    [SerializeField] private EnergyContainer _energyContainer;
    public EnergyContainer EnergyContainer => _energyContainer;

    public void LongInteraction() =>
        EnergySystem.ChannelEnnergyToPlayer(this, transform.position);

    public void ShortInteraction()
    {
        throw new NotImplementedException();
    }
}
