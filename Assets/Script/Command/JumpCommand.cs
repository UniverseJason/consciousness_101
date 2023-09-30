using Script.ActionSystem;
using UnityEngine;

namespace Script.Command
{
    public class JumpCommand : ICommand
    {
        private Movement _movementSystem;

        public JumpCommand(Movement movementSystem)
        {
            _movementSystem = movementSystem;
        }

        public void Execute()
        {
            _movementSystem.JumpRB();
        }
    }
}
