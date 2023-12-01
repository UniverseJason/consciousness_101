using System.Collections.Generic;
using UnityEngine;
using Script.ActionSystem;
using Script.Animation;
using Script.Command;
using Script.Role;

namespace Script.TokenAction
{
    public class State : MonoBehaviour
    {
        public AIState currentState = AIState.Idle;
        public enum AIState
        {
            Idle,
            Heal,
            Follow,
            WarningUp,
            WarningDown
        }

        private Movement move;
        private FollowObject followObject;
        private AnimationStateChanger animationStateChanger;

        // Healing
        public List<GameObject> teamMembers;
        public float healAmount = 10f;
        private Character character;

        private void Start()
        {
            move = GetComponent<Movement>();
            followObject = GetComponent<FollowObject>();
            animationStateChanger = GetComponent<AnimationStateChanger>();
            character = GetComponent<Character>();
        }

        private void Update()
        {
            switch (currentState)
            {
                case AIState.Idle:
                    Idle();
                    break;
                case AIState.Heal:
                    Heal();
                    break;
                case AIState.Follow:
                    Follow();
                    break;
                case AIState.WarningUp:
                    WarningUp();
                    break;
                case AIState.WarningDown:
                    WarningDown();
                    break;
                default:
                    Idle();
                    break;
            }
        }

        private void Idle()
        {
            Vector3 direction = Vector3.zero;
            new MoveCommand(move, direction, false).Execute();
        }

        private void Heal()
        {
            character.Heal(healAmount);
            foreach (var teamMember in teamMembers)
            {
                teamMember.GetComponent<Character>().Heal(healAmount);
                teamMember.transform.Find("Heal Particle").GetComponent<ParticleSystem>().Play();
            }
        }

        private void Follow()
        {
            followObject.FollowTarget();
        }

        private void WarningUp()
        {
            animationStateChanger.ChangeAnimationState("Token Warning");
        }

        private void WarningDown()
        {
            animationStateChanger.ChangeAnimationState("Token Idle");
        }
    }
}