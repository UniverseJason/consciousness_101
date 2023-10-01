using Script.ActionSystem;
using UnityEngine;

namespace Script.Command
{
    public class MoveCommand : ICommand
    {
        private Movement _movementSystem;
        private Vector3 _direction;
        private bool _isRunning;
        private bool _enableAnimation;

        public MoveCommand(Movement movementSystem, Vector3 direction, bool isRunning, bool enableAnimation = true)
        {
            _movementSystem = movementSystem;
            _direction = direction;
            _isRunning = isRunning;
            _enableAnimation = enableAnimation;
        }

        public void Execute()
        {
            _movementSystem.MoveRb(_direction, _enableAnimation, _isRunning);
        }
    }
}
