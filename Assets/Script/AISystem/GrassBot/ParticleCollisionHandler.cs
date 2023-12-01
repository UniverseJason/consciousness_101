using Script.Role;
using UnityEngine;

namespace Script.AISystem.GrassBot
{
    public class ParticleCollisionHandler : MonoBehaviour
    {
        public ParticleSystem attackParticle;
        public float damage = 2f;
        public float damageCooldown = 0.5f; // Cooldown in seconds between damage

        private float damageTimer = 0f; // Timer to track cooldown

        void OnParticleCollision(GameObject other)
        {
            if (damageTimer <= 0f && other.CompareTag("Player"))
            {
                Character character = other.GetComponent<Character>();
                if (character != null)
                {
                    character.TakeDamage(damage);
                    damageTimer = damageCooldown; // Reset the damage timer
                }
            }
        }

        void Update()
        {
            if (damageTimer > 0f)
            {
                damageTimer -= Time.deltaTime; // Countdown the timer
            }
        }

        void Start()
        {
            if (attackParticle == null)
            {
                attackParticle = GetComponent<ParticleSystem>();
            }
        }
    }
}