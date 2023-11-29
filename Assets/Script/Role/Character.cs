using Script.ActionSystem;
using Script.Animation;
using Script.Command;
using UnityEngine;

namespace Script.Role
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float _health = 100f;
        public float Health => _health;

        private GameObject currentCharacter;

        // Animation
        private Movement move;
        private AnimationStateChanger animationStateChanger;
        private string currentAnimationStateName;
        [SerializeField] private string takeDamageAnimationStateName;

        // UI Update
        public delegate void HealthChangedDelegate(float newHealth);
        public event HealthChangedDelegate OnHealthChanged;

        private void Awake()
        {
            move = GetComponent<Movement>();
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

            if (takeDamageAnimationStateName != null)
            {
                animationStateChanger.ChangeAnimationState(takeDamageAnimationStateName);
            }

            OnHealthChanged?.Invoke(_health);

            if (_health <= 0)
            {
                Die();
            }
        }

        public void ResetMovement()
        {
            Vector3 moveDirection = Vector3.zero;
            new MoveCommand(move, moveDirection, false).Execute();
        }
    }
}
