using Script.Role;
using UnityEngine;

namespace Script.AISystem.GrassBot
{
    public class StateChanger : MonoBehaviour
    {
        private State state;

        [Header("Player Detection")]
        [SerializeField] private float detectionRadius = 3f;
        [SerializeField] private float newDetectionRadius;

        [SerializeField] private float attackRadius = 1f;
        [SerializeField] private float newAttackRadius;

        [SerializeField] private LayerMask visibleLayerMask;
        private Collider2D[] hitColliders = new Collider2D[5];

        [SerializeField] private float detectionCooldown = 0.1f; // Cooldown in seconds for optimizing
        private float detectionTimer = 0f;

        private Transform lockedTarget = null; // Variable to store the first detected player
        private Character bossCharacter;

        private void Start()
        {
            state = GetComponent<State>();
            bossCharacter = GetComponent<Character>();
            newAttackRadius = attackRadius * 2;
            newDetectionRadius = detectionRadius * 2;
        }

        private void Update()
        {
            detectionTimer += Time.deltaTime;

            if (detectionTimer >= detectionCooldown)
            {
                if (bossCharacter.Health <= bossCharacter.maxHealth / 2) StartBossPhase2();
                else PerformDetectionChecks();
                detectionTimer = 0f; // Reset timer after checks
            }
        }

        // Phase 1: Attack 1
        private void PerformDetectionChecks()
        {
            var (isPlayerDetected, playerTransform) = CanSeePlayer();

            if (lockedTarget == null && isPlayerDetected)
            {
                lockedTarget = playerTransform; // Lock on the first detected player
            }

            if (lockedTarget != null)
            {
                if (IsTargetInAttackRange(lockedTarget))
                {
                    if (IsAbovePlayer(lockedTarget))
                    {
                        // When the target is above and in attack range
                        state.CurrentTarget = lockedTarget;
                        state.currentState = State.AIState.Attack1;
                    }
                    else
                    {
                        // When the target is in attack range but not above
                        state.CurrentTarget = lockedTarget;
                        state.currentState = State.AIState.Movement;

                        // Enlarge the detection radius and attack radius
                        detectionRadius = newDetectionRadius;
                        attackRadius = newAttackRadius;
                    }
                }
                else if (isPlayerDetected)
                {
                    // When the target is detected but not in attack range
                    state.currentState = State.AIState.WarningUp;
                }
                else
                {
                    // When the target was detected but now is out of range
                    state.currentState = State.AIState.WarningDown;
                    lockedTarget = null; // Reset target since player left the area
                }
            }
            else
            {
                // When no player has been detected at all
                state.currentState = State.AIState.Idle;
            }
        }

        // Phase 2: Attack 2
        private void StartBossPhase2()
        {
            state.currentState = State.AIState.Attack2;
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

        private bool IsTargetInAttackRange(Transform target)
        {
            return Vector2.Distance(transform.position, target.position) <= attackRadius;
        }

        private bool IsAbovePlayer(Transform player)
        {
            float xThreshold = 1f;
            float bossX = transform.position.x;
            float playerX = player.position.x;

            return Mathf.Abs(bossX - playerX) <= xThreshold && transform.position.y > player.position.y;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}