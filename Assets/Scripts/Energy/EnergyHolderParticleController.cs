using UnityEngine;

[RequireComponent(typeof(IEnergyHoldable))]
public class EnergyHolderParticleController : ParticlesController
{
    [SerializeField] private EnergyLevelFloatMultipliers _burstOverTimeMultipliers;
    
    private IEnergyHoldable _energyHolder;
    private EnergyLevels _lastEnergyLevel;

    private void Awake()
    {
        _energyHolder = GetComponent<IEnergyHoldable>();
    }

    protected override void Start()
    {
        base.Start();
        _lastEnergyLevel = _energyHolder.EnergyContainer.GetCurrentEnergyLevel();
    }

    public void Update()
    {
        if (CanPlayParticleSystem()) _particleSystem.Play();
        else if(CanStopParticleSystem()) _particleSystem.Stop();

        RefreshParticleSystemValuesOnEnergyLevelChange();
    }

    protected override bool CanPlayParticleSystem()
        => (_particleSystem.isPaused || !_particleSystem.isPlaying) 
           && _energyHolder.EnergyContainer.IsHavingEnergy();
    
    protected override bool CanStopParticleSystem()
        => (_particleSystem.isPaused || _particleSystem.isPlaying)
           && !_energyHolder.EnergyContainer.IsHavingEnergy();
    
    private void RefreshParticleSystemValuesOnEnergyLevelChange()
    {
        EnergyLevels currentEnergyLevel = _energyHolder.EnergyContainer.GetCurrentEnergyLevel();
        if (_lastEnergyLevel != currentEnergyLevel)
        {
            float energyLevelMultiplier = _burstOverTimeMultipliers.GetValueRelatedToEnergyLevel(currentEnergyLevel);
            ChangeParticlesRateOverTime(_startBurstRateOverTime.constant * energyLevelMultiplier);
            _lastEnergyLevel = currentEnergyLevel;
        }
    }
}
