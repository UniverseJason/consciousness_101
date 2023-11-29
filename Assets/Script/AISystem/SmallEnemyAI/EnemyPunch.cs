using Script.InputControl;
using Script.Role;
using UnityEngine;

namespace Script.AISystem.SmallEnemyAI
{
    public class EnemyPunch : MonoBehaviour
    {
        [Header("Detection")]
        [SerializeField] private float punchDamage = 10f;
        public CapsuleCollider2D punchCollider;
        private TransformInput transInput;

        private State state;

        private void Awake()
        {
            punchCollider = GetComponent<CapsuleCollider2D>();
            transInput = GetComponent<TransformInput>();
            state = GetComponent<State>();
            punchCollider.enabled = false;
        }

        public void SetPunchStart()
        {
            punchCollider.enabled = true;
        }

        public void SetPunchEnd()
        {
            punchCollider.enabled = false;
            transInput.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && punchCollider.enabled)
            {
                var enemy = collision.GetComponent<Character>();
                if (enemy != null)
                {
                    enemy.TakeDamage(punchDamage);
                }
            }
        }
    }
}