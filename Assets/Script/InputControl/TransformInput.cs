using JetBrains.Annotations;
using Script.Command;
using Script.ActionSystem;
using UnityEngine;

namespace Script.InputControl
{
    public class TransformInput : MonoBehaviour
    {
        [SerializeField] public bool EnablePlayerControl;
        [SerializeField] private Movement _move;
        [SerializeField] [CanBeNull] private Jump _jump;

        private void Awake()
        {
            _move = GetComponent<Movement>();
            _jump = GetComponent<Jump>();
        }

        private void Update()
        {
            if (EnablePlayerControl)
            {
                HandleMovementInput();
                HandleJumpInput();
            }
        }

        public bool IsRunning()
        {
            return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
                   (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
        }

        private void HandleMovementInput()
        {
            Vector3 moveDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.A))
                moveDirection.x -= 1;
            if (Input.GetKey(KeyCode.D))
                moveDirection.x += 1;

            // Check for running
            new MoveCommand(_move, moveDirection, IsRunning()).Execute();

            // Normalize move direction
            if (moveDirection.magnitude > 1)
                moveDirection.Normalize();
        }

        private void HandleJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                new JumpCommand(_jump).Execute();
            }
        }
    }
}
