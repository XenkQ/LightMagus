using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour, IEnergyHoldable
{
    [SerializeField] private Slider _energySlider;
    [SerializeField] private float _maxEnergy = 100f;
    public EnergyContainer EnergyContainer { get; private set; }
    public static float EnergyAmountPerChannel = 15f;
    private static EnergySystem Instance;
    private static bool _isChannelingEnergy;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(Instance);
    }

    private void Start()
    {
        _energySlider.maxValue = _maxEnergy;
        this.EnergyContainer = new EnergyContainer(0, 0, _maxEnergy);
    }

    public static void ChannelEnnergyToPlayer(IEnergyHoldable energyHolder,
        Vector3 channelEffectPos)
    {
        if(_isChannelingEnergy) return;

        Instance.StartCoroutine(ChannelingProcess(energyHolder, channelEffectPos));
    }
    
    private static IEnumerator ChannelingProcess(IEnergyHoldable energyHolder,
        Vector3 channelEffectPos)
    {
        _isChannelingEnergy = true;
        
        if (Instance.EnergyContainer.IsHavingEnergy())
        {
            //TODO: Add chaneling animation and functionality!!!
            yield return null;
        }
        
        _isChannelingEnergy = false;
    }
}
