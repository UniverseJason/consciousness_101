using Script.Animation;
using Script.InputControl;
using UnityEngine;

namespace Script.Role
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float _health = 100f;
        [SerializeField] private float _energy = 100f;

        private GameObject currentCharacter;

        // Animation
        private TransformInput transInput;
        private AnimationStateChanger animationStateChanger;
        private string currentAnimationStateName;
        [SerializeField] private string takeDamageAnimationStateName = "Test Character Hurt";

        private void Awake()
        {
            transInput = GetComponent<TransformInput>();
            animationStateChanger = GetComponent<AnimationStateChanger>();
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
            animationStateChanger.ChangeAnimationState(takeDamageAnimationStateName);
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
