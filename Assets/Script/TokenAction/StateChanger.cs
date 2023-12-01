using System.Collections.Generic;
using Script.ActionSystem;
using UnityEngine;

namespace Script.TokenAction
{
    public class StateChanger : MonoBehaviour
    {
        private State state;
        private FollowObject followObject;
        private bool enableFollow;

        public GameObject characterListUIController;
        private SwitchObject switchObject;

        // Check if enemy is in range
        [Header("Enemy Detection")]
        public float detectRange = 5f;
        public LayerMask enemyLayer;
        public float detectionCooldown = 0.3f; // Cooldown in seconds for optimizing
        private Collider2D[] hitColliders = new Collider2D[3];
        private float detectionTimer = 0f;

        [Header("Token Healing")]
        public float skillCooldown;
        public float skillTimer;
        private float timerMax;

        private void Start()
        {
            timerMax = skillCooldown + 1;
            state = GetComponent<State>();
            followObject = GetComponent<FollowObject>();
            switchObject = characterListUIController.GetComponent<SwitchObject>();
        }

        private void Update()
        {
            int currentActiveObjectIndex = switchObject.ActiveObjectIndex;
            List<GameObject> switchableObjects = switchObject.SwitchableObjects;

            // Token Heal
            if (skillTimer <= timerMax) skillTimer += Time.deltaTime;
            if (skillTimer >= skillCooldown && Input.GetKeyDown(KeyCode.E) && switchableObjects[currentActiveObjectIndex].name == "Winter")
            {
                state.currentState = State.AIState.Heal;
                skillTimer = 0f;
                return;
            }

            // Follow Object when Token is not active
            if (switchableObjects[currentActiveObjectIndex].name != "Token") state.currentState = State.AIState.Follow;
            else
            {
                state.currentState = State.AIState.Idle;

                // Check Key Input
                if (Input.GetKeyDown(KeyCode.Q)) enableFollow = !enableFollow;

                // Press Q to enable/disable follow
                if (enableFollow) followObject.FollowTarget();
            }

            // Check if enemy is in range
            detectionTimer += Time.deltaTime;
            if (detectionTimer >= detectionCooldown)
            {
                state.currentState = IsEnemyInRange() ? State.AIState.WarningUp : State.AIState.WarningDown;
                detectionTimer = 0f; // Reset timer after checks
            }
        }

        private bool IsEnemyInRange()
        {
            int numColliders = Physics2D.OverlapCircleNonAlloc(transform.position, detectRange, hitColliders, enemyLayer);
            return numColliders > 0;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectRange);
        }
    }
}