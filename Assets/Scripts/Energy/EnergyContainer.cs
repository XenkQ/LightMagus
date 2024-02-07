using System;
using UnityEngine;

namespace Energy
{
    [Serializable]
    public class EnergyContainer
    {
        [Header("Energy")]
        [SerializeField] private float _currentEnergy;
        [SerializeField] private float _maxEnergy;
        [SerializeField] private EnergyLevelFloatMultipliers _energhTresholds;
        private EnergyLevels _currentEnergyLevel;

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

        public EnergyContainer(float currentEnergy = 0, float maxEnergy = 0)
        {
            CurrentEnergy = currentEnergy;
            MaxEnergy = maxEnergy;

            RefreshCurrentEnergyLevel();
        }

        public void IncreaseEnergy(float amount)
        {
            if (_currentEnergy + amount <= _maxEnergy) _currentEnergy += amount;
            else _currentEnergy = _maxEnergy;

            RefreshCurrentEnergyLevel();
        }

        public void DecreaseEnergy(float amount)
        {
            if (_currentEnergy - amount >= 0) _currentEnergy -= amount;
            else ResetEnergy();

            RefreshCurrentEnergyLevel();
        }

        public void RefreshCurrentEnergyLevel()
        {
            if (_currentEnergy == 0) _currentEnergyLevel = EnergyLevels.EmptyEnergy;
            else if (_currentEnergy <= _maxEnergy * _energhTresholds.LowEnergyMultiplier) _currentEnergyLevel = EnergyLevels.LowEnergy;
            else if (_currentEnergy <= _maxEnergy * _energhTresholds.MediumEnergyMultiplier) _currentEnergyLevel = EnergyLevels.MediumEnergy;
            else if (_currentEnergy <= _maxEnergy * _energhTresholds.HighEnergyMultiplier) _currentEnergyLevel = EnergyLevels.HighEnergy;
            else _currentEnergyLevel = EnergyLevels.MaxEnergy;
        }

        public EnergyLevels GetCurrentEnergyLevel() => _currentEnergyLevel;

        public void ResetEnergy()
        {
            _currentEnergy = 0;
            RefreshCurrentEnergyLevel();
        }

        public bool IsHavingEnergy() => _currentEnergyLevel != EnergyLevels.EmptyEnergy;
        public bool IsHavingEnergy(float amount) => _currentEnergy >= amount;
        public bool IsFullyCharged() => _currentEnergyLevel == EnergyLevels.MaxEnergy;
    }
}