using System.Collections;
using UnityEngine;

public class Cristal : MonoBehaviour, IInteractable, IEnergyHoldable
{
    [SerializeField] private float _maxEnergy = 100f;
    [SerializeField] private float _minEnergy;
    private float _currentEnergy;
    
    private bool isChannelingEnergy;

    public void Interact()
    {
        if (isChannelingEnergy) return;
        
        //StartCoroutine(ChannelEnnergyToPlayer());
    }

    // private IEnumerator ChannelEnnergyToPlayer()
    // {
    //     isChannelingEnergy = true;
    //     
    //     if (_currentEnergy > 0)
    //     {
    //         
    //     }
    //     
    //     isChannelingEnergy = false;
    // }
    
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
}
