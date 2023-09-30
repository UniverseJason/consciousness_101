using Script.ActionSystem;
using UnityEngine;

namespace Script.Command
{
    public class MoveCommand : ICommand
    {
        private Movement _movementSystem;
        private Vector3 _direction;

        public MoveCommand(Movement movementSystem, Vector3 direction)
        {
            _movementSystem = movementSystem;
            _direction = direction;
        }

        public void Execute()
        {
            _movementSystem.MoveRb(_direction);
        }
    }
}
