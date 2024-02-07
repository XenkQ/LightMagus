using Energy;
using Interactions;
using UnityEngine;

namespace Cristals
{
    public class SupplyCristal : Cristal, ILongInteractable
    {
        public void OnLongInteraction()
        {
            EnergySystem.ChannelEnnergyToPlayer(this, transform.position);
            RefreshColorRelatedToEnergyLevel();
        }
    }
}