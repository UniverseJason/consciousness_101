using JetBrains.Annotations;
using Script.Command;
using Script.ActionSystem;
using UnityEngine;

namespace Script.InputControl
{
    public class TransformInput : MonoBehaviour
    {
        public bool EnablePlayerControl;

        [Header("Movement")]
        public bool EnableMovement;
        public bool EnableVerticalMovement = false;
        public Movement _move;
        public Vector3 _moveDirection;

        [Header("Jump")]
        public bool EnableJump;
        [CanBeNull] public Jump _jump;

        private void Awake()
        {
            _move = GetComponent<Movement>();
            _jump = GetComponent<Jump>();
        }

        private void Update()
        {
            if (EnablePlayerControl)
            {
                if (EnableMovement) HandleMovementInput();
                if (EnableJump) HandleJumpInput();
            }
        }

        public bool IsRunning()
        {
            return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
                   (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
        }

        private void HandleMovementInput()
        {
            _moveDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.A))
                _moveDirection.x -= 1;
            if (Input.GetKey(KeyCode.D))
                _moveDirection.x += 1;

            if (EnableVerticalMovement)
            {
                if (Input.GetKey(KeyCode.W))
                    _moveDirection.y += 1;
                if (Input.GetKey(KeyCode.S))
                    _moveDirection.y -= 1;
            }

            // Check for running
            new MoveCommand(_move, _moveDirection, IsRunning()).Execute();

            // Normalize move direction
            if (_moveDirection.magnitude > 1)
                _moveDirection.Normalize();
        }

        private void HandleJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                new JumpCommand(_jump).Execute();
            }
        }
    }
}
