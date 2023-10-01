using Script.Command;
using Script.ActionSystem;
using UnityEngine;

namespace Script
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Movement _move;

        private void Awake()
        {
            if (_move == null)
            {
                _move = GetComponent<Movement>();
            }
        }

        private void Update()
        {
            HandleMovementInput();
            HandleJumpInput();
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

        private bool IsRunning()
        {
            return (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
                   (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
        }

        private void HandleJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                new JumpCommand(_move).Execute();
            }
        }
    }
}
