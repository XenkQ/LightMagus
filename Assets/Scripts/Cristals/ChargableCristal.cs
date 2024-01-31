using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cristal))]
public class ChargableCristal : MonoBehaviour, IShortInteractable, IEnergyHoldable
{
    public EnergyContainer EnergyContainer { get; }
    private EnergyContainer _energyContainer;
    
    public void OnShortInteraction()
    {
        throw new System.NotImplementedException();
    }
}
