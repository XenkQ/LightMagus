using System;
using UnityEngine;

[Serializable]
public class EnergyContainer
{
    [SerializeField] private float _currentEnergy;
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _minEnergy;

    public float CurrentEnergy
    {
        get => _currentEnergy;
        init => _currentEnergy = value;
    }

    public float MaxEnergy
    {
        get => _maxEnergy;
        init => _maxEnergy = value;
    }

    public float MinEnergy
    {
        get => _minEnergy;
        init => _minEnergy = value;
    }

    public EnergyContainer(float currentEnergy = 0, float minEnergy = 0, float maxEnergy = 0)
    {
        CurrentEnergy = currentEnergy;
        MaxEnergy = minEnergy;
        MinEnergy = maxEnergy;
    }

    public bool IncreaseEnergy(float amount)
    {
        if (_currentEnergy + amount <= _maxEnergy)
        {
            _currentEnergy += amount;
            return true;
        }
        
        _currentEnergy = _maxEnergy;
        return false;
    }

    public bool DecreaseEnergy(float amount)
    {
        if (_currentEnergy - amount >= 0)
        {
            _currentEnergy -= amount;
            return true;
        }
        
        ResetEnergy();
        return false;
    }

    public void ResetEnergy() => _currentEnergy = _minEnergy;

    public bool IsHavingEnergy() => _currentEnergy > 0;
}