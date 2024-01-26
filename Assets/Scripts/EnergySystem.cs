using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour, IEnergyHoldable
{
    [SerializeField] private Slider _energySlider;
    [SerializeField] private float _maxEnergy = 100f;
    private float _currentEnergy;
    
    private void Start()
    {
        _energySlider.maxValue = _maxEnergy;
    }

    public void IncreaseEnergy(float ammount)
    {
        if (_currentEnergy + ammount <= _maxEnergy)
            _currentEnergy += _maxEnergy;
        else
            _currentEnergy = _maxEnergy;
        
        UpdateEnergySlider();
    }
    
    public void DecreaseEnergy(float ammount)
    {
        if (_currentEnergy - ammount >= 0)
            _currentEnergy -= ammount;
        else ResetEnergy();
        
        UpdateEnergySlider();
    }

    public void ResetEnergy() => _currentEnergy = 0;

    private void UpdateEnergySlider() => _energySlider.value = _currentEnergy;
}
