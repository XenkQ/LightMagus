using System;
using System.Collections;
using UnityEngine;

public class Cristal : MonoBehaviour, IInteractable, IEnergyHoldable
{
    [SerializeField] private float _currentEnergy = 0;
    [SerializeField] private float _maxEnergy = 40f;
    public EnergyContainer EnergyContainer { get; private set; }

    private void Start()
    {
        EnergyContainer = new EnergyContainer(_currentEnergy, 0, _maxEnergy);
    }

    public void Interact()
    {
        EnergySystem.ChannelEnnergyToPlayer(this, transform.position);
        this.EnergyContainer.DecreaseEnergy(EnergySystem.EnergyAmountPerChannel);
    }
}
