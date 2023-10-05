using Script.ActionSystem;

namespace Script.Command
{
    public class JumpCommand : ICommand
    {
        private Jump _jump;

        public JumpCommand(Jump jump)
        {
            _jump = jump;
        }

        public void Execute()
        {
            _jump.JumpRB();
        }
    }
}
