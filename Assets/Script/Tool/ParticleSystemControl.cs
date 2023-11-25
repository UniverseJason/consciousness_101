using UnityEngine;

namespace Script.Tool
{
    public class ParticleSystemControl : MonoBehaviour
    {
        public ParticleSystem myParticleSystem;
        

        void OnBecameVisible()
        {
            if (myParticleSystem != null && !myParticleSystem.isPlaying)
            {
                myParticleSystem.Play();
            }
        }

        void OnBecameInvisible()
        {
            if (myParticleSystem != null && myParticleSystem.isPlaying)
            {
                myParticleSystem.Pause();
            }
        }
    }
}
