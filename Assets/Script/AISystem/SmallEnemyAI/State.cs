using UnityEngine;
using Script.ActionSystem;
using Script.Animation;
using Script.Command;
using Script.InputControl;

namespace Script.AISystem.SmallEnemyAI
{
    public class State : MonoBehaviour
    {
        public AIState currentState = AIState.Idle;
        public enum AIState
        {
            Idle,
            Attack,
            Movement,
            TakeDamage,
        }

        [Header("Enemy Movement")]
        private Movement move;
        public Transform Target;
        public bool isRunning = false;

        [Header("Enemy Punch")]
        private TransformInput transInput;
        private AnimationStateChanger animationStateChanger;
        [SerializeField] private string punchAnimationStateName;

        [Header("Enemy Take Damage")]
        [SerializeField] private string takeDamageAnimationStateName;


        private void Start()
        {
            move = GetComponent<Movement>();
            transInput = GetComponent<TransformInput>();
            animationStateChanger = GetComponent<AnimationStateChanger>();
        }

        private void Update()
        {
            switch (currentState)
            {
                case AIState.Idle:
                    Idle();
                    break;
                case AIState.Attack:
                    Attack();
                    break;
                case AIState.Movement:
                    Movement();
                    break;
                case AIState.TakeDamage:
                    TakeDamage();
                    break;
                default:
                    Idle();
                    break;
            }
        }

        void Idle()
        {
            Vector3 _moveDirection = Vector3.zero;
            new MoveCommand(move, _moveDirection, false).Execute();
        }

        void Attack()
        {
            transInput.enabled = false;
            animationStateChanger.ChangeAnimationState(punchAnimationStateName);
        }

        void Movement()
        {
            Vector3 moveDirection = Vector3.zero;

            if (Target.position.x > transform.position.x) moveDirection.x += 1;

            else if (Target.position.x < transform.position.x) moveDirection.x -= 1;

            else moveDirection = Vector3.zero;

            new MoveCommand(move, moveDirection, isRunning).Execute();
        }

        void TakeDamage()
        {
            Vector3 moveDirection = Vector3.zero;
            new MoveCommand(move, moveDirection, isRunning).Execute();
            animationStateChanger.ChangeAnimationState(takeDamageAnimationStateName);
        }
    }
}