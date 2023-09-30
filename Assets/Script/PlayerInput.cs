using Script.Command;
using Script.ActionSystem;
using UnityEngine;

namespace Script
{
    public class PlayerInput : MonoBehaviour
    {
        private Movement _move;

        public void Awake()
        {
            _move = GetComponent<Movement>();
        }

        // handle user input
        public void Update()
        {
            // handle player go left or right, press shift key will speed up
            Vector3 v3 = Vector3.zero;

            if (Input.GetKey(KeyCode.A))
                v3.x -= 1;
            if (Input.GetKey(KeyCode.D))
                v3.x += 1;
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                _move.moveSpeed = 4f;
            else
                _move.moveSpeed = 2f;

            // normalize move speed
            if (v3.magnitude > 1)
                v3.Normalize();

            new MoveCommand(_move, v3).Execute();

            // handle player jump and crouch
            // TODO: need add crouch function
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                _move.jumpForce = 10f;
                new JumpCommand(_move).Execute();
            }
        }
    }
}
