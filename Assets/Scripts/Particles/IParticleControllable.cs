using UnityEngine;

namespace Particles
{
    public interface IParticleControllable
    {
        bool CanPlayParticleSystem();
        bool CanStopParticleSystem();
    }
}