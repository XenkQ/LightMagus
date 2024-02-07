using Energy;
using MyUtils;
using UnityEngine;

namespace Cristals
{
    public class Cristal : MonoBehaviour, IEnergyHoldable
    {
        [Header("Energy")]
        [SerializeField] protected EnergyContainer _energyContainer;

        public EnergyContainer EnergyContainer => _energyContainer;
        [SerializeField] protected EnergyLevelFloatMultipliers _metallicMultipliersRelatedToEnergyLevel;

        private Material[] _materials;

        protected virtual void Awake()
        {
            _materials = Materials.GetMaterialsFromChildrens(gameObject);
        }

        protected virtual void Start()
        {
            _energyContainer.RefreshCurrentEnergyLevel();
        }

        protected void RefreshColorRelatedToEnergyLevel()
        {
            EnergyLevels energyLevel = _energyContainer.GetCurrentEnergyLevel();
            float multiplierValue = _metallicMultipliersRelatedToEnergyLevel.GetValueRelatedToEnergyLevel(energyLevel);
            Materials.ChangeMaterialsMetallicMapValue(_materials, multiplierValue);
        }
    }
}