public class EnergyContainer
{
    private float _currentEnergy;
    private float _maxEnergy;
    private float _minEnergy;
    public float CurrentEnergy { get => _currentEnergy; init => _currentEnergy = value; }
    public float MaxEnergy { get => _maxEnergy; init => _maxEnergy = value; }
    public float MinEnergy { get => _minEnergy; init => _minEnergy = value; }

    public EnergyContainer(float currentEnergy = 0, float minEnergy = 0, float maxEnergy = 0)
    {
        CurrentEnergy = currentEnergy;
        MaxEnergy = minEnergy;
        MinEnergy = maxEnergy;
    }

    public void IncreaseEnergy(float ammount)
    {
        if (_currentEnergy + ammount <= _maxEnergy)
            _currentEnergy += _maxEnergy;
        else
            _currentEnergy = _maxEnergy;
    }

    public void DecreaseEnergy(float ammount)
    {
        if (_currentEnergy - ammount >= 0)
            _currentEnergy -= ammount;
        else ResetEnergy();
    }

    public void ResetEnergy() => _currentEnergy = _minEnergy;
    public bool IsHavingEnergy() => _currentEnergy > 0;
}