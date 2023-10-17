using UnityEngine;

namespace Script.Role
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float _health = 100f;
        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }

        [SerializeField] private float _energy = 100f;
        public float Energy
        {
            get { return _energy; }
            set { _energy = value; }
        }

        private GameObject currentCharacter;

        private void Awake()
        {
            currentCharacter = gameObject;
        }

        // Die
        public void Die()
        {
            // destroy current Character
            Destroy(currentCharacter);
        }

        // Take damage
        public void TakeDamage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        // Consume energy
        public void ConsumeEnergy(float energy)
        {
            _energy -= energy;
            if (_energy <= 0)
            {
                // Disable skills
            }
        }
    }
}
