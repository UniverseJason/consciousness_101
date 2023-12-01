using Script.ActionSystem;
using Script.Animation;
using Script.Command;
using Script.InputControl;
using Script.Music;
using UnityEngine;

namespace Script.Role
{
    public class Character : MonoBehaviour
    {
        public float maxHealth = 100f;
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

        private AudioManager audioManager;

        private TransformInput transInput;

        private void Awake()
        {
            move = GetComponent<Movement>();
            animationStateChanger = GetComponent<AnimationStateChanger>();
            transInput = GetComponent<TransformInput>();
            audioManager = GetComponent<AudioManager>();
            currentCharacter = gameObject;
        }

        // Die
        public void Die()
        {
            audioManager.SmoothChangeTo("BasicV");
            transform.position = new Vector3(5000, 5000, 0);
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

        public void Heal(float heal)
        {
            _health += heal;
            if (_health > maxHealth) _health = maxHealth;
            OnHealthChanged?.Invoke(_health);
        }

        public void ResetMovement()
        {
            if (!transInput.enabled) transInput.enabled = true;
            Vector3 moveDirection = Vector3.zero;
            new MoveCommand(move, moveDirection, false).Execute();
        }
    }
}
