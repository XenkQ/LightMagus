using System;
using MyUtils;
using UnityEngine;

public class Cristal : MonoBehaviour, IShortInteractable, ILongInteractable, IEnergyHoldable
{
    [Header("Energy")]
    [SerializeField] private EnergyContainer _energyContainer;
    public EnergyContainer EnergyContainer => _energyContainer;
    [SerializeField] private EnergyLevelFloatMultipliers _metallicMultipliersRelatedToEnergyLevel;
    
    [SerializeField] private bool _willDoSthAfterCharge;
    private Material[] _materials;
    
    private void Awake()
    {
        _materials = Materials.GetMaterialsFromChildrens(gameObject);
    }

    private void Start()
    {
        _energyContainer.RefreshCurrentEnergyLevel();
    }

    private void RefreshColorRelatedToEnergyLevel()
    {
        EnergyLevels energyLevel = _energyContainer.GetCurrentEnergyLevel();
        float multiplierValue = _metallicMultipliersRelatedToEnergyLevel.GetValueRelatedToEnergyLevel(energyLevel);
        Materials.ChangeMaterialsMetallicMapValue(_materials, multiplierValue);
    }

    public void OnLongInteraction()
    {
        EnergySystem.ChannelEnnergyToPlayer(this, transform.position);
        RefreshColorRelatedToEnergyLevel();
    }

    public void OnShortInteraction()
    {
        throw new NotImplementedException();
    }
}
