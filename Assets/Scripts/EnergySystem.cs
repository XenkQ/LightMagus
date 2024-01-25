using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    [SerializeField] private Slider _energySlider;
    [SerializeField] private float _maxEnergyAmount = 100f;
    private float _energyAmount;
    
    private void Start()
    {
        _energySlider.maxValue = _maxEnergyAmount;
    }

    public void IncreaseEnergy(float ammount)
    {
        if (_energyAmount + ammount <= _maxEnergyAmount)
            _energyAmount += _maxEnergyAmount;
        else
            _energyAmount = _maxEnergyAmount;
        
        UpdateEnergySlider();
    }
    
    public void DecreaseEnergy(float ammount)
    {
        if (_energyAmount - ammount >= 0)
            _energyAmount -= ammount;
        else ResetEnergy();
        
        UpdateEnergySlider();
    }

    private void ResetEnergy() => _energyAmount = 0;

    private void UpdateEnergySlider() => _energySlider.value = _energyAmount;
}
