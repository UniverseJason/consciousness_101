using UnityEngine;

namespace Script.AISystem.SmallEnemyAI
{
    public class StateChanger : MonoBehaviour
    {
        private State State;

        [Header("Player Detection")]
        [SerializeField] private float detectionRadius = 3f;
        [SerializeField] private float attackRadius = 1f;
        [SerializeField] private LayerMask visibleLayerMask;
        [SerializeField] private float detectionCooldown = 0.1f; // Cooldown in seconds for optimizing

        private Collider2D[] hitColliders = new Collider2D[10];
        private float detectionTimer = 0f;

        private void Start()
        {
            State = GetComponent<State>();
        }

        private void Update()
        {
            detectionTimer += Time.deltaTime;

            if (detectionTimer >= detectionCooldown)
            {
                PerformDetectionChecks();
                detectionTimer = 0f; // Reset timer after checks
            }
        }

        private void PerformDetectionChecks()
        {
            var (isPlayerDetected, playerTransform) = CanSeePlayer();

            if (IsPlayerInAttackRange())
            {
                State.currentState = State.AIState.Attack;
            }
            else if (isPlayerDetected)
            {
                State.Target = playerTransform;
                State.currentState = State.AIState.Movement;
                State.isRunning = true;
            }
            else State.currentState = State.AIState.Idle;
        }

        private (bool, Transform) CanSeePlayer()
        {
            int numColliders = Physics2D.OverlapCircleNonAlloc(transform.position, detectionRadius, hitColliders, visibleLayerMask);
            for (int i = 0; i < numColliders; i++)
            {
                if (hitColliders[i].CompareTag("Player"))
                {
                    return (true, hitColliders[i].transform); // Player is detected
                }
            }
            return (false, null); // Player is not detected
        }

        private bool IsPlayerInAttackRange()
        {
            int numColliders = Physics2D.OverlapCircleNonAlloc(transform.position, attackRadius, hitColliders, visibleLayerMask);
            for (int i = 0; i < numColliders; i++)
            {
                if (hitColliders[i].CompareTag("Player"))
                {
                    return true; // Player is detected
                }
            }
            return false; // Player is not detected
        }

        // Debug
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}