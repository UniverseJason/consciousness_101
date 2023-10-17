using Script.ActionSystem;

namespace Script.Command
{
    public class FollowCommand : ICommand
    {
        private FollowObject _followObject;

        public FollowCommand(FollowObject followObject)
        {
            _followObject = followObject;
        }

        public void Execute()
        {
            _followObject.FollowTarget();
        }
    }
}