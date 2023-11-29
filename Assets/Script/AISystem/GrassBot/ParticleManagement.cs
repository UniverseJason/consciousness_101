using UnityEngine;

namespace Script.AISystem.GrassBot
{
    public class ParticleManagement : MonoBehaviour
    {
        public ParticleSystem IdleParticle;
        public ParticleSystem MoveParticle;
        public ParticleSystem Attack1Particle;
        public ParticleSystem Attack2Particle;

        public void ParticleSwitch(ParticleSystem p)
        {
            StopAllParticle();
            p.Play();
        }

        public void StopAllParticle()
        {
            IdleParticle.Stop();
            MoveParticle.Stop();
            Attack1Particle.Stop();
            Attack2Particle.Stop();
        }
    }
}