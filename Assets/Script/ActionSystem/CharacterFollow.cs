using Script.Command;
using Script.InputControl;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.ActionSystem
{
    public class CharacterFollow : MonoBehaviour
    {
        [Header("Check Character Activation")]
        public GameObject characterListUIController;
        private SwitchObject switchObject;

        [Header("Follow Object")]
        private Movement move;
        [FormerlySerializedAs("currentMoveSpeed")] public float prevMoveSpeed;
        private float slowedMoveSpeed;
        public Transform Target;
        public bool isRunning = false;
        public float StopDistance = 1f;
        public float EnableRunDistance = 5f;
        private float runDelay = 1f; // Time to continue running after getting close
        private float runTimer = 0.0f; // Timer for running delay

        private void Start()
        {
            switchObject = characterListUIController.GetComponent<SwitchObject>();
            move = GetComponent<Movement>();
            prevMoveSpeed = move.MoveSpeed;
            slowedMoveSpeed = prevMoveSpeed - 0.3f;
        }

        private void Update()
        {
            if (CheckActive()) return;
            Follow();
        }

        private bool CheckActive()
        {
            int currentActiveObjectIndex = switchObject.ActiveObjectIndex;
            bool isActive = switchObject.SwitchableObjects[currentActiveObjectIndex].name == gameObject.name;
            if (isActive) move.MoveSpeed = prevMoveSpeed;
            return isActive;
        }

        private void Follow()
        {
            Vector3 moveDirection = Vector3.zero;
            float distanceToTarget = Vector3.Distance(transform.position, Target.position);
            bool isFollow = distanceToTarget > StopDistance;

            if (!isFollow)
            {
                move.MoveSpeed = prevMoveSpeed;
                moveDirection = Vector3.zero;
                new MoveCommand(move, moveDirection, false).Execute();
                runTimer = 0.0f; // Reset the timer
                return;
            }

            if (distanceToTarget > EnableRunDistance)
            {
                isRunning = true;
                runTimer = runDelay; // Reset the timer
            }
            else
            {
                if (runTimer > 0)
                {
                    runTimer -= Time.deltaTime; // Decrease the timer
                    isRunning = true; // Continue running
                }
                else
                {
                    isRunning = false; // Stop running
                }
            }

            move.MoveSpeed = isRunning ? prevMoveSpeed : slowedMoveSpeed;

            Vector3 directionToTarget = (Target.position - transform.position).normalized;
            moveDirection = new Vector3(directionToTarget.x, 0, 0);

            new MoveCommand(move, moveDirection, isRunning).Execute();
        }
    }
}