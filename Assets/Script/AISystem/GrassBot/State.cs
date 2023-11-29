using Script.Animation;
using UnityEngine;

namespace Script.AISystem.GrassBot
{
    public class State : MonoBehaviour
    {
        public ParticleManagement ParticleManagement;

        // Animation
        public AnimationStateChanger AnimationStateChanger;
        [SerializeField] private string idleStateName = "Boss Idle";
        [SerializeField] private string moveStateName = "Boss Move";
        [SerializeField] private string warningStateName = "Boss Warning";
        [SerializeField] private string readyBloomStateName = "Boss Attack Ready toBloom";
        [SerializeField] private string bloomStateName = "Boss Attack Bloom";

        private enum AIState
        {
            Idle,
            Attack1,
            Attack2,
            Movement
        }

        [SerializeField] private AIState currentState = AIState.Idle;

        private void Start()
        {
            AnimationStateChanger = GetComponent<AnimationStateChanger>();
            ParticleManagement = GetComponent<ParticleManagement>();
            ParticleManagement.StopAllParticle();
            ParticleManagement.IdleParticle.Play();
        }

        private void Update()
        {
            switch (currentState)
            {
                case AIState.Idle:
                    Idle();
                    break;
                case AIState.Attack1:
                    Attack1();
                    break;
                case AIState.Attack2:
                    Attack2();
                    break;
                case AIState.Movement:
                    Movement();
                    break;
                default:
                    Idle();
                    break;
            }
        }

        void Idle()
        {
            if (currentState == AIState.Idle) return;
            ParticleManagement.ParticleSwitch(ParticleManagement.IdleParticle);
            AnimationStateChanger.ChangeAnimationState(idleStateName);
        }

        void Attack1()
        {
            // Attack 1 behavior
        }

        void Attack2()
        {
            // Attack 2 behavior
        }

        void Movement()
        {
            // Movement behavior
        }

        
    }
}