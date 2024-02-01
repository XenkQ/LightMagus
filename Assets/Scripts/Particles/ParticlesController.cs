using UnityEngine;

public abstract class ParticlesController : MonoBehaviour
{
    [SerializeField] protected ParticleSystem _particleSystem;
    protected ParticleSystem.MinMaxCurve _startBurstRateOverTime;

    protected virtual void Start()
    {
        _startBurstRateOverTime = _particleSystem.emission.rateOverTime;
    }

    protected abstract bool CanPlayParticleSystem();
    protected abstract bool CanStopParticleSystem();

    protected virtual void ChangeParticlesRateOverTime(float rate)
    {
        var emmision = _particleSystem.emission;
        emmision.rateOverTime = rate;
    }
}