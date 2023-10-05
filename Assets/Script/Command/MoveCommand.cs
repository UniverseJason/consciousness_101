using Script.ActionSystem;
using UnityEngine;

namespace Script.Command
{
    public class MoveCommand : ICommand
    {
        private Movement _movement;
        private Vector3 _direction;
        private bool _isRunning;
        private bool _enableAnimation;

        public MoveCommand(Movement movement, Vector3 direction, bool isRunning, bool enableAnimation = true)
        {
            _movement = movement;
            _direction = direction;
            _isRunning = isRunning;
            _enableAnimation = enableAnimation;
        }

        public void Execute()
        {
            _movement.MoveRb(_direction, _enableAnimation, _isRunning);
        }
    }
}
