using Script.ActionSystem;
using UnityEngine;

namespace Script.Command
{
    public class MoveCommand : ICommand
    {
        private Movement _movement;
        private Vector3 _direction;
        private bool _isRunning;

        public MoveCommand(Movement movement, Vector3 direction, bool isRunning)
        {
            _movement = movement;
            _direction = direction;
            _isRunning = isRunning;
        }

        public void Execute()
        {
            _movement.MoveRb(_direction, _isRunning);
        }
    }
}
