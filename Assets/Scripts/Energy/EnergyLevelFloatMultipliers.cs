using System;
using UnityEngine;

[Serializable]
public class EnergyLevelFloatMultipliers
{
    [SerializeField] [Range(0, 1f)] private float _emptyEnergyMultiplier;
    [SerializeField] [Range(0, 1f)] private float _lowEnergyMultiplier;
    [SerializeField] [Range(0, 1f)] private float _mediumEnergyMultiplier;
    [SerializeField] [Range(0, 1f)] private float _highEnergyMultiplier;
    [SerializeField] [Range(0, 1f)] private float _maxEnergyMultiplier;
    
    public float EmptyEnergyMultiplier { get => _emptyEnergyMultiplier; init => _emptyEnergyMultiplier = value; }
    public float LowEnergyMultiplier { get => _lowEnergyMultiplier; init => _lowEnergyMultiplier = value; }
    public float MediumEnergyMultiplier { get => _mediumEnergyMultiplier; init => _mediumEnergyMultiplier = value; }
    public float HighEnergyMultiplier { get => _highEnergyMultiplier; init => _highEnergyMultiplier = value; }
    public float MaxEnergyMultiplier { get => _maxEnergyMultiplier; init => _maxEnergyMultiplier = value; }

    public EnergyLevelFloatMultipliers(float empty, float low, float medium, float high, float max)
    {
        EmptyEnergyMultiplier = empty;
        LowEnergyMultiplier = low;
        MediumEnergyMultiplier = medium;
        HighEnergyMultiplier = high;
        MaxEnergyMultiplier = max;
    }

    public float GetValueRelatedToEnergyLevel(EnergyLevels energyLevel) => energyLevel switch
    {
        EnergyLevels.EmptyEnergy => EmptyEnergyMultiplier,
        EnergyLevels.LowEnergy => LowEnergyMultiplier,
        EnergyLevels.MediumEnergy => MediumEnergyMultiplier,
        EnergyLevels.HighEnergy => HighEnergyMultiplier,
        EnergyLevels.MaxEnergy => MaxEnergyMultiplier
    };
}