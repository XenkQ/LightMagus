using System;
using MyUtils;
using UnityEngine;

public class Cristal : MonoBehaviour, IShortInteractable, ILongInteractable, IEnergyHoldable
{
    [Header("Energy")]
    [SerializeField] private EnergyContainer _energyContainer;
    public EnergyContainer EnergyContainer => _energyContainer;
    [SerializeField] private EnergyLevelFloatMultipliers _energyLevelMultipliers;
    
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private bool _willDoSthAfterCharge;

    private Material[] _materials;
    
    private void Awake()
    {
        _materials = MyUtils.Materials.GetAllMaterialsFromChildrens(gameObject);
    }

    private void Start()
    {
        _energyContainer.RefreshCurrentEnergyLevel();
    }

    public void Update()
    {
        if (CanPlayParticleSystem()) _particleSystem.Play();
    }

    private bool CanPlayParticleSystem()
    {
        return (_particleSystem.isPaused || !_particleSystem.isPlaying) && _energyContainer.IsHavingEnergy();
    }

    private void RefreshColorRelatedToEnergyLevel()
    {
        EnergyLevels energyLevel = _energyContainer.GetCurrentEnergyLevel();
        float multiplierValue = _energyLevelMultipliers.GetValueRelatedToEnergyLevel(energyLevel);
        Materials.ChangeMaterialsMetalicMapValue(_materials, multiplierValue);
        //TODO: naprawić zmianę metalu materiału
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
