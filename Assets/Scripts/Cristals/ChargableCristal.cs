using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cristal))]
public class ChargableCristal : MonoBehaviour, IInteractable, IEnergyHoldable
{
    public EnergyContainer EnergyContainer { get; }
    private EnergyContainer _energyContainer;
    
    public void ShortInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void LongInteraction()
    {
        return;
    }
}
