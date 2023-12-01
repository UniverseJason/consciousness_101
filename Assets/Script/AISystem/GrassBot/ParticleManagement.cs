using Script.Role;
using UnityEngine;

namespace Script.AISystem.GrassBot
{
    public class ParticleManagement : MonoBehaviour
    {
        public ParticleSystem IdleParticle;
        public ParticleSystem MoveParticle;
        public ParticleSystem Attack1Particle;
        public ParticleSystem Attack2Particle;

        public void SwitchToParticle(ParticleSystem p)
        {
            if (p.isPlaying) return;
            StopAllParticle();
            p.Play();
        }

        public void StopAllParticle()
        {
            StopParticle(IdleParticle);
            StopParticle(MoveParticle);
            StopParticle(Attack1Particle);
            StopParticle(Attack2Particle);
        }

        private void StopParticle(ParticleSystem p)
        {
            if (p.isPlaying) p.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        public void PlayParticle(ParticleSystem p)
        {
            if (p.isPlaying) return;
            p.Play();
        }
    }
}