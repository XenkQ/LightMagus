using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour, IEnergyHoldable
{
    [Header("UI")]
    [SerializeField] private Slider _energySlider;
    [SerializeField] private float _energyTransitionEffectSpeed = 4f;
    
    [Header("Energy Storage")]
    [SerializeField] private EnergyContainer _energyContainer;
    public EnergyContainer EnergyContainer => _energyContainer;

    [Header("Energy Channeling")]
    [SerializeField] private float _delayBetweenChanneling = 1;
    
    public static float EnergyAmountPerChannel = 15f;
    private static bool _isChannelingEnergy;

    private static EnergySystem Instance;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        _energySlider.maxValue = _energyContainer.MaxEnergy;
        _energyContainer.RefreshCurrentEnergyLevel();
    }

    private void Update()
    {
        if (CanUpdateEnergySlider()) UpdateEnergySlider();
    }

    private bool CanUpdateEnergySlider()
    {
        return _energySlider.value != Instance.EnergyContainer.CurrentEnergy;
    }

    public static void ChannelEnnergyToPlayer(IEnergyHoldable energyHolder, Vector3 channelEffectPos)
    {
        if(_isChannelingEnergy) return;

        Instance.StartCoroutine(ChannelingProcess(energyHolder, channelEffectPos));
    }
    
    private static IEnumerator ChannelingProcess(IEnergyHoldable energyHolder, Vector3 channelEffectPos)
    {
        _isChannelingEnergy = true;
        
        if (energyHolder.EnergyContainer.IsHavingEnergy())
        {
            if (energyHolder.EnergyContainer.DecreaseEnergy(EnergyAmountPerChannel))
            {
                Instance.EnergyContainer.IncreaseEnergy(EnergyAmountPerChannel);
            }
            else
            {
                float remainingEnergy = energyHolder.EnergyContainer.MaxEnergy % EnergyAmountPerChannel;
                Instance.EnergyContainer.IncreaseEnergy(remainingEnergy);
            }
            
            Instance.UpdateEnergySlider();
            yield return new WaitForSeconds(Instance._delayBetweenChanneling);
        }
        
        _isChannelingEnergy = false;
    }

    private void UpdateEnergySlider()
    {
        _energySlider.value = Mathf.Lerp(
            _energySlider.value,
            Instance.EnergyContainer.CurrentEnergy,
            _energyTransitionEffectSpeed * Time.deltaTime
        );
    }
}